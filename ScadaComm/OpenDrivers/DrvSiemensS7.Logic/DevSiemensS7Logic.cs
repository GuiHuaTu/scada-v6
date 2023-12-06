// Copyright (c) Rapid Software LLC. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using S7.Net;
using Scada.Client;
using Scada.Comm.Config;
using Scada.Comm.Devices;
using Scada.Comm.Drivers.DrvSiemensS7.Config;
using Scada.Comm.Drivers.DrvSiemensS7.Protocol;
using Scada.Comm.Lang;
using Scada.Config;
using Scada.Data.Const;
using Scada.Data.Entities;
using Scada.Data.Models;
using Scada.Data.Tables;
using Scada.Lang;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;

namespace Scada.Comm.Drivers.DrvSiemensS7.Logic
{
    /// <summary>
    /// Implements the device logic.
    /// <para>Реализует логику устройства.</para>
    /// </summary>
    public class DevSiemensS7Logic : DeviceLogic
    {

        /// <summary>
        /// Contains data common to a communication line.
        /// </summary>
        private class SiemensS7LineData
        {
            public ScadaClient ScadaClient { get; set; }
            public bool FatalError { get; set; } = false;
            public SiemensS7Poll ClientHelper { get; set; }
            public override string ToString() => CommPhrases.SharedObject;
        }


        /// <summary>
        /// Represents a template dictionary.
        /// </summary>
        protected class TemplateDict : Dictionary<string, DeviceTemplate>
        {
            public override string ToString()
            {
                return Locale.IsRussian ?
                    $"Словарь из {Count} шаблонов" :
                    $"Dictionary of {Count} templates";
            }
        }

        private int[] publishCnlNums;                 // the numbers of the published channels
        private long cnlListID;                       // the cached channel list ID
        private readonly DeviceTemplate config;             // the device configuration
        private bool configError;                            // indicates that that device configuration is not loaded
        private SiemensS7LineData lineData;                      // data common to the communication line
        protected string cpuType;     // the cpu type of the communication line
        protected string plcIP;     // the ip of the communication line
        protected short plcRack;     // the rack of the communication line
        protected short plcSlot;     // the slot of the communication line
        protected DeviceModel deviceModel; // the device model 




        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        public DevSiemensS7Logic(ICommContext commContext, ILineContext lineContext, DeviceConfig deviceConfig)
            : base(commContext, lineContext, deviceConfig)
        {

            cpuType = LineContext.LineConfig.CustomOptions.GetValueAsString("CpuType", Enum.GetName(typeof(VarType), CpuType.S71200));

            plcIP = LineContext.LineConfig.CustomOptions.GetValueAsString("PlcIP");
            plcRack = (short)LineContext.LineConfig.CustomOptions.GetValueAsInt("PlcRack");
            plcSlot = (short)LineContext.LineConfig.CustomOptions.GetValueAsInt("PlcSlot");


            deviceModel = null;
            lineData = null;

            publishCnlNums = null;
            cnlListID = 0;

            Log.WriteLine($"S7连接配置:CpuType={cpuType},PlcIP={plcIP},PlcRack={plcRack},PlcSlot={plcSlot}");

            ConnectionRequired = false;
        }


        /// <summary>
        /// Gets the shared data key of the template dictionary.
        /// </summary>
        protected virtual string TemplateDictKey => "SiemensS7.Templates";


        /// <summary>
        /// Creates a new SiemensS7 command based on the element configuration.
        /// 基于元素配置创建一个新的SiemensS7命令。
        /// </summary>
        private SiemensS7Cmd CreateSiemensS7Cmd(ElemGroupConfig elemGroupConfig, ElemConfig elemConfig )
        {
            SiemensS7Cmd siemensS7Cmd = deviceModel.CreateSiemensS7Cmd(elemGroupConfig.DataBlock, elemConfig.Quantity > 1);
            siemensS7Cmd.Name = elemConfig.Name;
            siemensS7Cmd.Address = elemConfig.Address;
            siemensS7Cmd.ElemType = elemConfig.ElemType;
            siemensS7Cmd.ElemCnt = 1;
            siemensS7Cmd.CmdNum = 0;
            siemensS7Cmd.CmdCode = elemConfig.TagCode;
             
            return siemensS7Cmd;
        }

        /// <summary>
        /// Creates a new SiemensS7 command based on the command configuration.
        /// 基于命令配置创建新的SiemensS7命令
        /// </summary>
        private SiemensS7Cmd CreateSiemensS7Cmd( CmdConfig cmdConfig)
        {
            SiemensS7Cmd siemensS7Cmd = deviceModel.CreateSiemensS7Cmd(cmdConfig.DataBlock, cmdConfig.Multiple);
            siemensS7Cmd.Name = cmdConfig.Name;
            siemensS7Cmd.Address = cmdConfig.Address;
            siemensS7Cmd.ElemType = cmdConfig.ElemType;
            siemensS7Cmd.ElemCnt = cmdConfig.ElemCnt;
            siemensS7Cmd.CmdNum = cmdConfig.CmdNum;
            siemensS7Cmd.CmdCode = cmdConfig.CmdCode;
             
            return siemensS7Cmd;
        }

        /// <summary>
        /// Initializes an object for polling the device.
        /// 初始化用于轮询设备的对象。
        /// </summary>
        private void InitSiemensS7Poll()
        {
            if (lineData.FatalError || configError)
            {
                DeviceStatus = DeviceStatus.Error;
            }
            else if (lineData.ClientHelper.SiemensS7Session == null)
            {
                Log.WriteLine($"--------- SiemenssS7 Session Connection");
                lineData.ClientHelper.Connection();

                if (!lineData.ClientHelper.SiemensS7Session.IsConnected)
                {
                    DeviceStatus = DeviceStatus.Error;
                    DeviceData.Invalidate();
                }
                Log.WriteLine($"--------- SiemenssS7 Session Connection Successful");

            }
            else if (!lineData.ClientHelper.SiemensS7Session.IsConnected)
            {
                Log.WriteLine($"--------- SiemenssS7 Session Connection Fail");
                DeviceStatus = DeviceStatus.Error;
                DeviceData.Invalidate();
            }

        }

        /// <summary>
        /// Sets the data of the element group tags.
        /// </summary>
        private void SetTagData(ElemGroup elemGroup)
        {
             Log.WriteLine($"--------- SiemenssS7 SetTagData ");
            int tagStatus = LastRequestOK ? CnlStatusID.Defined : CnlStatusID.Undefined;

             Log.WriteLine($"--------- SiemenssS7 SetTagData Status={tagStatus}"); 

            //for (int elemIdx = 0, tagIdx = elemGroup.StartTagIdx + elemIdx, cnt = elemGroup.Elems.Count; 
            //    elemIdx < cnt; elemIdx++, tagIdx++)
            //{
            //    Log.WriteLine($"--------- {elemGroup.Elems[elemIdx].DeviceTag.Index}-{elemGroup.Elems[elemIdx].DeviceTag.Code} :   {elemGroup.ElemData[elemIdx]}");
            //    DeviceData.Set(elemGroup.Elems[elemIdx].DeviceTag, elemGroup.ElemData[elemIdx], tagStatus);
            //}
            for (int i=0;i< elemGroup.Elems.Count;i++)
            {
                DeviceTag deviceTag = elemGroup.Elems[i].DeviceTag;
                object val = elemGroup.ElemData[i];
                Log.WriteLine($"--------- SiemenssS7 SetTagData deviceTag.Index={deviceTag.Index} deviceTag.Name={deviceTag.Name}  deviceTag.Val={val}");

                DeviceData.Set(deviceTag, val, tagStatus);

                DeviceData.Invalidate(deviceTag.Index);
            }
             
        }
 

        /// <summary>
        /// Gets the device tag format depending on the SiemensS7 element type.
        /// </summary>
        private static TagFormat GetTagFormat(ElemConfig elemConfig)
        {
            if (elemConfig.ElemType == ElemType.Bool)
                return TagFormat.OffOn;
            else if (elemConfig.ElemType == ElemType.Float || elemConfig.ElemType == ElemType.Double)
                return TagFormat.FloatNumber;
            else if (elemConfig.IsBitMask)
                return TagFormat.HexNumber;
            else
                return TagFormat.IntNumber;
        }


        /// <summary>
        /// Gets a template dictionary from the shared data of the communication line, or creates a new one.
        /// </summary>
        protected virtual TemplateDict GetTemplateDict()
        {
            Log.WriteLine($"--------- SiemenssS7 GetTemplateDict ");

            TemplateDict templateDict = LineContext.SharedData.ContainsKey(TemplateDictKey) ?
                LineContext.SharedData[TemplateDictKey] as TemplateDict : null;

            if (templateDict == null)
            {
                templateDict = new TemplateDict();
                LineContext.SharedData.Add(TemplateDictKey, templateDict);
            }

            return templateDict;
        }

        /// <summary>
        /// Gets the device template from the shared dictionary.
        /// </summary>
        protected virtual DeviceTemplate GetDeviceTemplate()
        {

            DeviceTemplate deviceTemplate = null;
            string fileName = PollingOptions.CmdLine.Trim();
            Log.WriteLine($"--------- SiemenssS7 GetDeviceTemplate :fileName="+ fileName);

            if (string.IsNullOrEmpty(fileName))
            {
                Log.WriteLine(string.Format(Locale.IsRussian ?
                    "Ошибка: Не задан шаблон устройства для {0}" :
                    "Error: Device template is undefined for {0}", Title));
            }
            else
            {
                TemplateDict templateDict = GetTemplateDict();

                if (templateDict.TryGetValue(fileName, out DeviceTemplate existingTemplate))
                {
                    deviceTemplate = existingTemplate;
                }
                else
                {
                    Log.WriteLine(string.Format(Locale.IsRussian ?
                        "Загрузка шаблона устройства из файла {0}" :
                        "Load device template from file {0}", fileName));

                    DeviceTemplate newTemplate = CreateDeviceTemplate();
                    templateDict.Add(fileName, newTemplate);

                    if (newTemplate.Load(Storage, fileName, out string errMsg))
                        deviceTemplate = newTemplate;
                    else
                        Log.WriteLine(errMsg);
                }
            }

            return deviceTemplate;
        }

        /// <summary>
        /// Create a new device template.
        /// </summary>
        protected virtual DeviceTemplate CreateDeviceTemplate()
        {
            return new DeviceTemplate();
        }

        /// <summary>
        /// Create a new device model.
        /// </summary>
        protected virtual DeviceModel CreateDeviceModel()
        {
            return new DeviceModel();
        }


        /// <summary>
        /// Initializes data common to the communication line.
        /// 初始化通信线路的公用数据。
        /// </summary>
        private void InitLineData()
        {

            if (LineContext.SharedData.TryGetValueOfType(nameof(SiemensS7LineData), out SiemensS7LineData data))
            {
                lineData = data;
            }
            else
            {
                SiemensS7LineConfig lineConfig = new SiemensS7LineConfig();
                bool lineConfigError = false;
                lineConfig.ConnectionOptions.CpuType = cpuType;
                lineConfig.ConnectionOptions.PlcIP = plcIP;
                lineConfig.ConnectionOptions.PlcRack= plcRack;
                lineConfig.ConnectionOptions.PlcSlot= plcSlot; 

                Log.WriteLine($"S7连接配置:CpuType={lineConfig.ConnectionOptions.CpuType},PlcIP={lineConfig.ConnectionOptions.PlcIP},PlcRack={lineConfig.ConnectionOptions.PlcRack}" +
                    $",PlcSlot={lineConfig.ConnectionOptions.PlcSlot}" );
                lineData = new SiemensS7LineData()
                {
                    FatalError = lineConfigError,

                    ScadaClient = new ScadaClient(CommContext.AppConfig.ConnectionOptions),

                    ClientHelper = new SiemensS7Poll(lineConfig.ConnectionOptions, Log, Storage)
                    {
                        Timeout = PollingOptions.Timeout,
                        Log = Log
                    }
                };

                LineContext.SharedData[nameof(SiemensS7LineData)] = lineData;
            }
        }


        /// <summary>
        /// Performs actions when starting a communication line.
        /// 启动通信线路时执行操作。
        /// </summary>
        public override void OnCommLineStart()
        {
            cpuType = LineContext.LineConfig.CustomOptions.GetValueAsString("CpuType", Enum.GetName(typeof(VarType), CpuType.S71200));

            plcIP = LineContext.LineConfig.CustomOptions.GetValueAsString("PlcIP");
            plcRack = (short)LineContext.LineConfig.CustomOptions.GetValueAsInt("PlcRack");
            plcSlot = (short)LineContext.LineConfig.CustomOptions.GetValueAsInt("PlcSlot");

            Log.WriteLine($"--------- SiemenssS7 OnCommLineStart ");

            InitLineData();

         
            InitDeviceTags();


        }

        /// <summary>
        /// Performs actions after setting the connection.
        /// 设置连接后执行操作。
        /// </summary>
        public override void OnConnectionSet()
        {
   
        }

        /// <summary>
        /// Performs actions when terminating a communication line.
        /// 在终止通信线路时执行操作。
        /// </summary>
        public override void OnCommLineTerminate()
        {
            Log.WriteLine($"--------- SiemenssS7 OnCommLineTerminate ");
            lineData.ClientHelper.Disconnect();
            FinishSession();
        }

        /// <summary>
        /// Initializes the device data.
        /// </summary>
        public override void InitDeviceData()
        {
            base.InitDeviceData();
        }

        /// <summary>
        /// Initializes the device tags.
        /// 初始化设备标记。
        /// </summary>
        public override void InitDeviceTags()
        {

            Log.WriteLine($"--------- SiemenssS7 InitDeviceTags ");
            DeviceTemplate deviceTemplate = GetDeviceTemplate();

            if (deviceTemplate == null)
                return;

            // create device model
            deviceModel = CreateDeviceModel();
            deviceModel.Addr = (byte)NumAddress;
            List<int> cnlNumList = new List<int>();

            // add model elements and device tags
            foreach (ElemGroupConfig elemGroupConfig in deviceTemplate.ElemGroups)
            {
                bool groupActive = elemGroupConfig.Active;
                bool groupCommands = groupActive && elemGroupConfig.ReadOnlyEnabled;
                ElemGroup elemGroup = null;
                TagGroup tagGroup = new TagGroup(elemGroupConfig.Name) { Hidden = !groupActive };
                BaseTable<Cnl> cnlTable = CommContext.ConfigDatabase?.CnlTable;
                //int elemAddrOffset = 0;

                if (groupActive)
                {
                    elemGroup = deviceModel.CreateElemGroup(elemGroupConfig.DataBlock);
                    elemGroup.Name = elemGroupConfig.Name;
                    //elemGroup.Address = elemGroupConfig.Address;
                    elemGroup.StartTagIdx = DeviceTags.Count;
                }
                 

                
                for (int i=0;i< elemGroupConfig.Elems.Count;i++)
                {
                    int cnlNum = i;
                    ElemConfig elemConfig = elemGroupConfig.Elems[i];
                    // add device tag 
                    DeviceTag deviceTag;

                    if (cnlTable?.GetItem(cnlNum) is Cnl cnl && cnl.Active)
                    {
                        deviceTag = tagGroup.AddTag("", cnl.Name);
                        deviceTag.Cnl = cnl;
                        Log.WriteLine($"--------- SiemenssS7 cnl.TagCode={cnl.TagCode} cnl.Name={cnl.Name} cnl.Active={cnl.Active}");
                    }
                    else
                    {
                        Log.WriteLine($"--------- SiemenssS7 tagGroup.AddTag");
                        deviceTag = tagGroup.AddTag("", Locale.IsRussian ?
                            "Канал " + cnlNum :
                            "Channel " + cnlNum);
                    }

                    deviceTag.SetFormat(GetTagFormat(elemConfig));
                    if (elemConfig.ElemType == ElemType.String)
                    {
                        deviceTag.DataType = TagDataType.Unicode; 
                        deviceTag.Format = TagFormat.String;
                    }

                    // add model element
                    if (groupActive)
                    {
                        Elem elem = elemGroup.CreateElem();
                        elem.Name = elemConfig.Name; 
                        elem.Address = elemConfig.Address;
                        elem.ElemType = elemConfig.ElemType;

                        deviceTag.DataLen = elem.DataLength;
                        deviceTag.Cnl = new Data.Entities.Cnl();
                        elem.DeviceTag = deviceTag;


                        elemGroup.Elems.Add(elem);
                        elemGroup.ElemData.Add(null); 

                        cnlNumList.Add(elemGroup.Elems.Count);
                    }

                    // add model command
                    if (groupCommands && !elemConfig.ReadOnly && !string.IsNullOrEmpty(elemConfig.TagCode))
                    {
                        Log.WriteLine($"--------- SiemenssS7 elemGroupConfigCmds :Address={elemConfig.Address} ElemType={elemConfig.ElemType} ");
                        deviceModel.Cmds.Add(CreateSiemensS7Cmd(elemGroupConfig, elemConfig ));
                    }
                }

                if (groupActive)
                { 
                    deviceModel.ElemGroups.Add(elemGroup);
                }

                DeviceTags.AddGroup(tagGroup); 
            }

            // add model commands
            foreach (CmdConfig cmdConfig in deviceTemplate.Cmds)
            {
                Log.WriteLine($"--------- SiemenssS7 Cmds :Address={cmdConfig.Address} ElemType={cmdConfig.ElemType} ");
                deviceModel.Cmds.Add(CreateSiemensS7Cmd(cmdConfig));
            }

            deviceModel.InitCmdMap();
            CanSendCommands = deviceModel.Cmds.Count > 0;

            publishCnlNums = cnlNumList.ToArray();

            InitSiemensS7Poll();
        }

        /// <summary>
        /// Performs a communication session.
        /// 执行通信会话。
        /// </summary>
        public override void Session()
        {
            LastSessionTime = DateTime.UtcNow;
            LastRequestOK = true;

            Log.WriteLine($"--------- SiemenssS7 Session ");
            Log.WriteLine($"--------- SiemenssS7 Session ElemGroups.Count={deviceModel.ElemGroups.Count}");

            #region 读取点位数据
            if (deviceModel == null)
            {
                Log.WriteLine(Locale.IsRussian ?
                    "Невозможно опросить устройство, потому что модель устройства не определена" :
                    "Unable to poll the device because device model is undefined");
                SleepPollingDelay();
                LastRequestOK = false;
            }
            else if (deviceModel.ElemGroups.Count > 0)
            {
                if (!lineData.ClientHelper.SiemensS7Session.IsConnected)
                {
                    Log.WriteLine($"--------- SiemenssS7 Session Connection Fail");
                    DeviceStatus = DeviceStatus.Error;
                    DeviceData.Invalidate();


                    InitSiemensS7Poll();
                }
                // request element groups
                int elemGroupCnt = deviceModel.ElemGroups.Count;
                int elemGroupIdx = 0;

                while (elemGroupIdx < elemGroupCnt && LastRequestOK)
                {
                    ElemGroup elemGroup = deviceModel.ElemGroups[elemGroupIdx];
                    LastRequestOK = false;
                    int tryNum = 0;
                    Log.WriteLine($"--------- SiemenssS7 ReadDoRequest ");
                    Log.WriteLine($"--------- SiemenssS7 ReadDoRequest ElemGroupName={elemGroup.Name}");
                    //*
                    // 采集轮循方式一：有一个bug：某一组里的点位采集报错，会一直循环采集改组，不会采集其他组数据
                    //*/
                    #region 采集轮循方式一

                    //while (RequestNeeded(ref tryNum))
                    //{
                    //    // perform request
                    //    if (lineData.ClientHelper.ReadDoRequest(elemGroup))
                    //    {
                    //        LastRequestOK = true;

                    //        SetTagData(elemGroup);
                    //    }
                        
                    //    FinishRequest();
                    //    tryNum++;
                    //}

                    //if (LastRequestOK)
                    //{
                    //    // next element group
                    //    elemGroupIdx++;
                    //}
                    //else if (tryNum > 0)
                    //{
                    //    // set tag data as undefined for the current and the next element groups
                    //    while (elemGroupIdx < elemGroupCnt)
                    //    {
                    //        elemGroup = deviceModel.ElemGroups[elemGroupIdx];
                    //        DeviceData.Invalidate(elemGroup.StartTagIdx, elemGroup.Elems.Count);
                    //        elemGroupIdx++;
                    //    }
                    //}
                    #endregion

                    #region 采集轮循方式二

                    while (RequestNeeded(ref tryNum))
                    {
                        // perform request
                        if (lineData.ClientHelper.ReadDoRequest(elemGroup))
                        {
                            LastRequestOK = true;

                            SetTagData(elemGroup);
                        }

                        FinishRequest();
                        tryNum++;

                        if (tryNum > ReqRetries)//组里某个点位报错，本组TryNum轮循后跳出while
                        {
                            LastRequestOK = true;
                        }
                    }

                    if (LastRequestOK)
                    {
                        // next element group
                        elemGroupIdx++;
                    }
                    else if (tryNum > 0)
                    {
                        // set tag data as undefined for the current and the next element groups
                        while (elemGroupIdx < elemGroupCnt)
                        {
                            elemGroup = deviceModel.ElemGroups[elemGroupIdx];
                            DeviceData.Invalidate(elemGroup.StartTagIdx, elemGroup.Elems.Count);
                            elemGroupIdx++;
                        }
                    }
                    #endregion
                }
            }
            else
            {
                Log.WriteLine(Locale.IsRussian ?
                    "Отсутствуют элементы для запроса" :
                    "No elements to request");
                SleepPollingDelay();
            }

            FinishSession();
            #endregion

        }



        /// <summary>
        /// Sends the telecontrol command.
        /// 发送遥控命令。
        /// </summary>
        public override void SendCommand(TeleCommand cmd)
        {
            Log.WriteLine($"--------- SiemenssS7 SendCommand ");
            base.SendCommand(cmd);

            if ((deviceModel.GetCmd(cmd.CmdCode) ?? deviceModel.GetCmd(cmd.CmdNum)) is SiemensS7Cmd siemensS7Cmd)
            {
                // prepare SiemensS7 command
                if (siemensS7Cmd.Multiple )
                {
                    siemensS7Cmd.Value = 0;

                    if (cmd.CmdData != null && cmd.CmdData.Length > 0)
                        siemensS7Cmd.Data = cmd.CmdData;
                    else
                        siemensS7Cmd.SetCmdData(cmd.CmdVal);
                }
                else
                {
                    siemensS7Cmd.Value =  (ushort)cmd.CmdVal;
                    siemensS7Cmd.SetCmdData(cmd.CmdVal);
                } 

                // send command to device
                LastRequestOK = false;
                int tryNum = 0;

                while (RequestNeeded(ref tryNum))
                {
                    double cmdVal = cmd.CmdVal;//纯数字值
                    string cmdData = cmd.GetCmdDataString();//字符串数据


                    object itemVal = null;
                    if (siemensS7Cmd.ElemType == ElemType.S5Time || siemensS7Cmd.ElemType == ElemType.DateTime 
                         || siemensS7Cmd.ElemType == ElemType.DateTimeLong 
                         )
                        itemVal = DateTime.FromOADate(cmdVal); 
                    else if (siemensS7Cmd.ElemType == ElemType.String || siemensS7Cmd.ElemType == ElemType.S7String
                         || siemensS7Cmd.ElemType == ElemType.S7WString || siemensS7Cmd.ElemType == ElemType.Bool
                         )
                        itemVal = cmdData;
                    else
                        itemVal = cmdVal;

                    Log.WriteLine($"--------- SiemenssS7 SendCommand :Address={siemensS7Cmd.Address} ElemType={siemensS7Cmd.ElemType} " +
                        $"cmdData={cmdData} cmdVal={cmdVal}  itemVal={itemVal} ");

                    if (lineData.ClientHelper.SetValue(siemensS7Cmd, itemVal))
                        LastRequestOK = true;

                    FinishRequest();
                    tryNum++;
                }
            }
            else
            {
                Log.WriteLine($"--------- SiemenssS7 SendCommand InvalidCommand");
                LastRequestOK = false;
                Log.WriteLine(CommPhrases.InvalidCommand);
            }

            FinishCommand();
        }
    }
}
