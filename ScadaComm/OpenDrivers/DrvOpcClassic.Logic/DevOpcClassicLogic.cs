// Copyright (c) Rapid Software LLC. All rights reserved.

using Scada.Comm.Config;
using Scada.Comm.Devices;
using Scada.Comm.Drivers.DrvOpcClassic.Config;
using Scada.Comm.Lang;
using Scada.Data.Const;
using Scada.Data.Models;
using Scada.Lang;

namespace Scada.Comm.Drivers.DrvOpcClassic.Logic
{
    /// <summary>
    /// Implements the device logic.
    /// <para>Реализует логику устройства.</para>
    /// </summary>
    internal class DevOpcClassicLogic : DeviceLogic
    {
        /// <summary>
        /// Contains data common to a communication line.
        /// </summary>
        private class OpcClassicLineData
        {
            public bool FatalError { get; set; } = false;
            public OpcClientHelper ClientHelper { get; init; }
            public override string ToString() => CommPhrases.SharedObject;
        }

        /// <summary>
        /// Represents metadata about a device tag.
        /// </summary>
        private class DeviceTagMeta
        {
            public Type ActualDataType { get; set; }
        }

        private readonly OpcDeviceConfig config; // the device configuration
        private readonly object opcLock;         // synchronizes communication with OPC server

        private bool configError;                            // indicates that that device configuration is not loaded
        private OpcClassicLineData lineData;                 // data common to the communication line
        private SubscriptionTag[] subscrTags;                // describes DA subscriptions for synchronous reading
        private Dictionary<int, CommandConfig> cmdByNum;     // the commands accessed by number
        private Dictionary<string, CommandConfig> cmdByCode; // the commands accessed by code


        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        public DevOpcClassicLogic(ICommContext commContext, ILineContext lineContext, DeviceConfig deviceConfig)
            : base(commContext, lineContext, deviceConfig)
        {
            config = new OpcDeviceConfig();
            opcLock = new object();

            configError = false;
            lineData = null;
            subscrTags = null;
            cmdByNum = null;
            cmdByCode = null;

            CanSendCommands = true;
            ConnectionRequired = false;
        }


        /// <summary>
        /// Initializes data common to the communication line.
        /// </summary>
        private void InitLineData()
        {
            if (LineContext.SharedData.TryGetValueOfType(nameof(OpcClassicLineData), out OpcClassicLineData data))
            {
                lineData = data;
            }
            else
            {
                OpcLineConfig lineConfig = new();
                bool lineConfigError = false;

                if (!lineConfig.Load(Storage, OpcLineConfig.GetFileName(LineContext.CommLineNum), out string errMsg))
                {
                    Log.WriteLine(errMsg);
                    Log.WriteLine(Locale.IsRussian ?
                        "Взаимодействие с OPC-сервером невозможно, т.к. конфигурация линии не загружена" :
                        "Interaction with OPC server is impossible because line configuration is not loaded");
                    lineConfigError = true;
                }

                lineData = new OpcClassicLineData()
                {
                    FatalError = lineConfigError,
                    ClientHelper = new OpcClientHelper(lineConfig.ConnectionOptions, Log)
                };

                LineContext.SharedData[nameof(OpcClassicLineData)] = lineData;
            }
        }
        
        /// <summary>
        /// Initializes command maps.
        /// </summary>
        private void InitCommandMaps()
        {
            cmdByNum = new Dictionary<int, CommandConfig>();
            cmdByCode = new Dictionary<string, CommandConfig>();

            // explicit commands
            foreach (CommandConfig commandConfig in config.Commands)
            {
                if (commandConfig.CmdNum > 0 && !cmdByNum.ContainsKey(commandConfig.CmdNum))
                    cmdByNum.Add(commandConfig.CmdNum, commandConfig);

                if (!string.IsNullOrEmpty(commandConfig.CmdCode) && !cmdByCode.ContainsKey(commandConfig.CmdCode))
                    cmdByCode.Add(commandConfig.CmdCode, commandConfig);
            }

            // commands from subscriptions
            foreach (SubscriptionConfig subscriptionConfig in config.Subscriptions)
            {
                foreach (ItemConfig itemConfig in subscriptionConfig.Items)
                {
                    if (itemConfig.Active &&
                        !string.IsNullOrEmpty(itemConfig.TagCode) &&
                        !cmdByCode.ContainsKey(itemConfig.TagCode))
                    {
                        cmdByCode.Add(itemConfig.TagCode, new CommandConfig
                        {
                            Path = itemConfig.Path,
                            Name = itemConfig.Name,
                            CmdCode = itemConfig.TagCode,
                            DataTypeName = itemConfig.DataTypeName
                        });
                    }
                }
            }
        }

        /// <summary>
        /// Finds a command configuration by command code or number.
        /// </summary>
        private bool FindCommandConfig(TeleCommand cmd, out CommandConfig commandConfig)
        {
            if (cmdByCode != null && !string.IsNullOrEmpty(cmd.CmdCode) &&
                cmdByCode.TryGetValue(cmd.CmdCode, out commandConfig))
            {
                return true;
            }

            if (cmdByNum != null && cmd.CmdNum > 0 &&
                cmdByNum.TryGetValue(cmd.CmdNum, out commandConfig))
            {
                return true;
            }

            commandConfig = null;
            return false;
        }

        /// <summary>
        /// Sets data of the single tag.
        /// </summary>
        private void SetTagData(DeviceTag deviceTag, object val, int stat)
        {
            try
            {
                if (deviceTag.Aux is DeviceTagMeta tagMeta && val != null)
                    tagMeta.ActualDataType = val.GetType();

                DeviceData.Set(deviceTag, val, stat);
            }
            catch (Exception ex)
            {
                Log.WriteLine(ex.BuildErrorMessage(Locale.IsRussian ?
                    "Ошибка при установке данных тега" :
                    "Error setting tag data"));
            }
        }

        /// <summary>
        /// Sets data of the array tag.
        /// </summary>
        private void SetArrayTagData(DeviceTag deviceTag, Array array, int stat)
        {
            try
            {
                int arrLen = Math.Min(deviceTag.DataLength, array.Length);
                double[] arr = new double[arrLen];
                TagFormat tagFormat = TagFormat.FloatNumber;

                for (int i = 0; i < arrLen; i++)
                {
                    arr[i] = ConvertArrayElem(array.GetValue(i), out TagFormat format);
                    if (i == 0)
                        tagFormat = format;
                }

                deviceTag.Format = tagFormat;
                DeviceData.SetDoubleArray(deviceTag.Index, arr, stat);
            }
            catch (Exception ex)
            {
                Log.WriteLine(ex.BuildErrorMessage(Locale.IsRussian ?
                    "Ошибка при установке данных тега-массива" :
                    "Error setting array tag data"));
            }
        }

        /// <summary>
        /// Gets the tag status corresponding to the item quality.
        /// </summary>
        private static int GetTagStatus(Opc.Da.ItemValueResult itemValue)
        {
            return itemValue.QualitySpecified && itemValue.Quality.QualityBits == Opc.Da.qualityBits.good ?
                CnlStatusID.Defined : CnlStatusID.Undefined;
        }

        /// <summary>
        /// Converts the array element value received from the OPC server to a double.
        /// </summary>
        private static double ConvertArrayElem(object val, out TagFormat tagFormat)
        {
            if (val is string)
            {
                // string arrays not supported
                tagFormat = TagFormat.FloatNumber;
                return 0.0;
            }
            else if (val is DateTime dtVal)
            {
                tagFormat = TagFormat.DateTime;
                return dtVal.ToOADate();
            }
            else
            {
                tagFormat = TagFormat.FloatNumber;
                return Convert.ToDouble(val);
            }
        }

        /// <summary>
        /// Copies client handles from the items to the values.
        /// </summary>
        private static void CopyClientHandles(Opc.Da.Item[] items, Opc.Da.ItemValueResult[] values)
        {
            for (int i = 0, len = Math.Min(items.Length, values.Length); i < len; i++)
            {
                values[i].ClientHandle = items[i].ClientHandle;
            }
        }
        
        /// <summary>
        /// Writes an item value to the OPC server.
        /// </summary>
        private bool WriteItemValue(Opc.Da.Server daServer, CommandConfig commandConfig, double cmdVal, byte[] cmdData)
        {
            try
            {
                // get data type
                string dataTypeName = commandConfig.DataTypeName;
                Type itemDataType = null;
                object itemVal;

                if (string.IsNullOrEmpty(dataTypeName))
                {
                    if (DeviceTags.TryGetTag(commandConfig.CmdCode, out DeviceTag deviceTag) &&
                        deviceTag.Aux is DeviceTagMeta tagMeta)
                    {
                        itemDataType = tagMeta.ActualDataType;
                    }
                }
                else
                {
                    itemDataType = Type.GetType(dataTypeName, false, true);
                }

                if (itemDataType == null)
                {
                    throw new ScadaException(Locale.IsRussian ?
                        "Не удалось получить тип данных {0}" :
                        "Unable to get data type {0}", dataTypeName);
                }

                if (itemDataType.IsArray)
                {
                    throw new ScadaException(Locale.IsRussian ?
                        "Тип данных {0} не поддерживается" :
                        "Data type {0} not supported", dataTypeName);
                }

                // define command value
                try
                {
                    if (itemDataType == typeof(string))
                        itemVal = TeleCommand.CmdDataToString(cmdData);
                    else if (itemDataType == typeof(DateTime))
                        itemVal = DateTime.FromOADate(cmdVal);
                    else
                        itemVal = Convert.ChangeType(cmdVal, itemDataType);
                }
                catch
                {
                    throw new ScadaException(Locale.IsRussian ?
                        "Не удалось привести значение к типу {0}" :
                        "Unable to convert value to the type {0}", itemDataType.FullName);
                }

                // write value
                Log.WriteLine("{0} {1} = {2}", CommPhrases.SendNotation, commandConfig.Name, itemVal);

                Opc.Da.ItemValue valueToWrite = new()
                {
                    ItemPath = commandConfig.Path,
                    ItemName = commandConfig.Name,
                    Value = itemVal
                };

                Opc.IdentifiedResult[] results = daServer.Write(new Opc.Da.ItemValue[] { valueToWrite });

                if (results[0].ResultID == Opc.ResultID.S_OK)
                {
                    Log.WriteLine(CommPhrases.ResponseOK);
                    return true;
                }
                else
                {
                    Log.WriteLine(CommPhrases.ResponseError);
                    return false;
                }
            }
            catch (Exception ex)
            {
                Log.WriteLine(CommPhrases.ErrorPrefix + ex.Message);
                return false;
            }
        }


        /// <summary>
        /// Performs actions when starting a communication line.
        /// </summary>
        public override void OnCommLineStart()
        {
            InitLineData();

            if (config.Load(Storage, OpcDeviceConfig.GetFileName(DeviceNum), out string errMsg))
            {
                InitCommandMaps();
                subscrTags = lineData.ClientHelper.AddDaSubscriptions(this, config)
                    .Where(tag => tag.SubscriptionConfig.ReadSync).ToArray();
                lineData.ClientHelper.AddAeSubscriptions(this, config);
            }
            else
            {
                Log.WriteLine(CommPhrases.DeviceMessage, Title, errMsg);
                configError = true;
            }
        }

        /// <summary>
        /// Performs actions when terminating a communication line.
        /// </summary>
        public override void OnCommLineTerminate()
        {
            lineData.ClientHelper.Disconnect();
        }

        /// <summary>
        /// Initializes the device tags.
        /// </summary>
        public override void InitDeviceTags()
        {
            if (configError)
                return;

            foreach (SubscriptionConfig subscriptionConfig in config.Subscriptions)
            {
                TagGroup tagGroup = new(subscriptionConfig.DisplayName);

                foreach (ItemConfig itemConfig in subscriptionConfig.Items)
                {
                    DeviceTag deviceTag = tagGroup.AddTag(itemConfig.TagCode, itemConfig.Name);
                    deviceTag.Aux = new DeviceTagMeta();
                    itemConfig.Tag = deviceTag;

                    if (itemConfig.IsString)
                    {
                        deviceTag.DataType = TagDataType.Unicode;
                        deviceTag.DataLen = DeviceTag.CalcDataLength(itemConfig.DataLength, TagDataType.Unicode);
                        deviceTag.Format = TagFormat.String;
                    }
                    else if (itemConfig.IsArray)
                    {
                        deviceTag.DataLen = itemConfig.DataLength;
                    }
                }

                DeviceTags.AddGroup(tagGroup);
            }
        }

        /// <summary>
        /// Performs a communication session.
        /// </summary>
        public override void Session()
        {
            bool sleepRequired = true;

            if (lineData.FatalError || configError)
            {
                DeviceStatus = DeviceStatus.Error;
            }
            else if (!lineData.ClientHelper.IsConnected &&
                !(lineData.ClientHelper.Connect() && lineData.ClientHelper.CreateSubscriptions()))
            {
                DeviceStatus = DeviceStatus.Error;
                DeviceData.Invalidate();
            }
            else if (subscrTags.Length > 0)
            {
                // read values synchronously
                foreach (SubscriptionTag subscriptionTag in subscrTags)
                {
                    if (subscriptionTag.SubscriptionItems is Opc.Da.Item[] items && items.Length > 0)
                    {
                        Opc.Da.ItemValueResult[] values = lineData.ClientHelper.DaSever.Read(items);
                        CopyClientHandles(items, values);
                        ProcessDataChanges(subscriptionTag, values);
                        SleepPollingDelay();
                        sleepRequired = false;
                    }
                }
            }

            if (sleepRequired)
                SleepPollingDelay();
        }

        /// <summary>
        /// Sends the telecontrol command.
        /// </summary>
        public override void SendCommand(TeleCommand cmd)
        {
            try
            {
                Monitor.Enter(opcLock);
                base.SendCommand(cmd);
                LastRequestOK = false;
                Opc.Da.Server daServer = lineData.ClientHelper.DaSever;

                if (daServer == null)
                {
                    Log.WriteLine(Locale.IsRussian ?
                        "Ошибка: соединение с OPC-сервером не установлено" :
                        "Error: connection with the OPC server is not established");
                }
                else if (!FindCommandConfig(cmd, out CommandConfig commandConfig))
                {
                    Log.WriteLine(CommPhrases.InvalidCommand);
                }
                else
                {
                    LastRequestOK = WriteItemValue(daServer, commandConfig, cmd.CmdVal, cmd.CmdData);
                }

                FinishCommand();
            }
            finally
            {
                Monitor.Exit(opcLock);
            }
        }

        /// <summary>
        /// Processes new data.
        /// </summary>
        public void ProcessDataChanges(SubscriptionTag subscriptionTag, Opc.Da.ItemValueResult[] values)
        {
            if (subscriptionTag == null || values == null)
                return;

            try
            {
                Monitor.Enter(opcLock);
                LastSessionTime = DateTime.UtcNow;
                LastRequestOK = true;

                Log.WriteLine();
                Log.WriteAction(Locale.IsRussian ?
                    "Устройство {0}. Обработка новых данных. Подписка: {1}" :
                    "Device {0}. Process new data. Subscription: {1}",
                    Title, subscriptionTag.SubscriptionConfig.DisplayName);

                foreach (Opc.Da.ItemValueResult itemValue in values)
                {
                    if (itemValue != null)
                    {
                        string qualityStr = itemValue.QualitySpecified ? itemValue.Quality.ToString() : "unspecified";
                        Log.WriteLine("{0} {1} = {2} ({3})", CommPhrases.ReceiveNotation,
                            itemValue.ItemName, itemValue.Value, qualityStr);

                        if (itemValue.ClientHandle is ItemTag itemTag &&
                            itemTag.DeviceTag is DeviceTag deviceTag)
                        {
                            if (itemTag.ItemConfig.IsArray && itemValue.Value is Array array)
                                SetArrayTagData(deviceTag, array, GetTagStatus(itemValue));
                            else
                                SetTagData(deviceTag, itemValue.Value, GetTagStatus(itemValue));
                        }
                        else
                        {
                            Log.WriteLine(Locale.IsRussian ?
                                "Ошибка: тег не найден" :
                                "Error: tag not found");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteError(ex, Locale.IsRussian ?
                    "Ошибка при обработке новых данных" :
                    "Error processing new data");
                LastRequestOK = false;
            }
            finally
            {
                FinishSession();
                Monitor.Exit(opcLock);
            }
        }

        /// <summary>
        /// Processes new event.
        /// </summary>
        public void ProcessEvent(EventSubscriptionTag subscriptionTag, Opc.Ae.EventNotification notification)
        {
            if (subscriptionTag == null || notification == null)
                return;

            try
            {
                Monitor.Enter(opcLock);
                LastSessionTime = DateTime.UtcNow;
                LastRequestOK = true;

                Log.WriteLine();
                Log.WriteAction(Locale.IsRussian ?
                    "Устройство {0}. Обработка нового события. Подписка: {1}" :
                    "Device {0}. Process new event. Subscription: {1}",
                    Title, subscriptionTag.SubscriptionConfig.DisplayName);

                Log.WriteLine("{0} {1}, {2}, {3}", CommPhrases.ReceiveNotation, 
                    notification.SourceID, notification.Time, notification.Message);

                if (DeviceTags.TryGetTag(notification.SourceID, out DeviceTag deviceTag))
                {
                    DeviceData.EnqueueEvent(new DeviceEvent(deviceTag)
                    {
                        Timestamp = notification.Time.ToUniversalTime(),
                        TextFormat = EventTextFormat.CustomText,
                        Text = notification.Message,
                        Descr = notification.Message
                    });
                }
                else
                {
                    Log.WriteLine(Locale.IsRussian ?
                        "Ошибка: тег \"{0}\" не найден" :
                        "Error: tag \"{0}\" not found", notification.SourceID);
                }
            }
            catch (Exception ex)
            {
                Log.WriteError(ex, Locale.IsRussian ?
                    "Ошибка при обработке нового события" :
                    "Error processing new event");
                LastRequestOK = false;
            }
            finally
            {
                FinishSession();
                Monitor.Exit(opcLock);
            }
        }
    }
}
