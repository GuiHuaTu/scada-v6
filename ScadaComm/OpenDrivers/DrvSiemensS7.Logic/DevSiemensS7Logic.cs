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
using Scada.Log;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using static System.Runtime.CompilerServices.RuntimeHelpers;

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
            public SiemensS7Poll ClientHelper { get; set; }
            public override string ToString() => CommPhrases.SharedObject;
        }

        /// <summary>
        /// Represents metadata about a device tag.
        /// </summary>
        private class DeviceTagMeta
        {
            public Type ActualDataType { get; set; }
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
            SiemensS7Cmd siemensS7Cmd = deviceModel.CreateSiemensS7Cmd(elemGroupConfig.DataBlock, elemGroupConfig.Multiple);
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
            if (lineData.ClientHelper.SiemensS7Session == null)
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
             //Log.WriteLine($"--------- SiemenssS7 SetTagData ");
            int tagStatus = LastRequestOK ? CnlStatusID.Defined : CnlStatusID.Undefined;

             Log.WriteLine($"--------- SiemenssS7 SetTagData Status={tagStatus}");

            //Log.WriteLine($"--------- SiemenssS7 SetTagData  elemGroup.Elems.Count={elemGroup.Elems.Count}  elemGroup.ElemData={elemGroup.ElemData.Count}");
            for(int i = 0;i < elemGroup.Elems.Count;i++)
            {
                DeviceTag deviceTag = elemGroup.Elems[i].DeviceTag; 
                ElemType elemType = elemGroup.Elems[i].ElemType;
                object val = elemGroup.ElemData[i];
                int stat = elemGroup.CnlStatusID[i];

                Log.WriteLine($"--------- SiemenssS7 SetTagData deviceTag index={deviceTag.Index} Code={deviceTag.Code}");
                Log.WriteLine($"--------- SiemenssS7 SetTagData CnlStatusID={stat} | val={val}");
                Log.WriteLine($"--------- SiemenssS7 SetTagData");

                if (stat == CnlStatusID.Error)
                {
                    DeviceData.Invalidate(deviceTag.Index);
                }
                else
                {
                    if (deviceTag.Aux is DeviceTagMeta tagMeta && val != null)
                        tagMeta.ActualDataType = val.GetType();

                    if (val is string strVal)
                    {
                        Log.WriteLine($"--------- SiemenssS7 SetTagData 1");
                        deviceTag.DataType = TagDataType.Unicode;
                        deviceTag.Format = TagFormat.String;
                        DeviceData.SetUnicode(deviceTag.Index, strVal, stat);
                    }
                    else if (val is DateTime dtVal)
                    {
                        Log.WriteLine($"--------- SiemenssS7 SetTagData 2");
                        deviceTag.DataType = TagDataType.Double;
                        deviceTag.Format = TagFormat.DateTime;
                        DeviceData.SetDateTime(deviceTag.Index, dtVal, stat);
                    }
                    else
                    {
                        Log.WriteLine($"--------- SiemenssS7 SetTagData 3");
                        deviceTag.DataType = TagDataType.Double;
                        deviceTag.Format = TagFormat.FloatNumber;
                        DeviceData.Set(deviceTag.Index, Convert.ToDouble(val), stat); 
                    }

                }
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
                lineConfig.ConnectionOptions.CpuType = cpuType;
                lineConfig.ConnectionOptions.PlcIP = plcIP;
                lineConfig.ConnectionOptions.PlcRack= plcRack;
                lineConfig.ConnectionOptions.PlcSlot= plcSlot; 

                Log.WriteLine($"S7连接配置:CpuType={lineConfig.ConnectionOptions.CpuType},PlcIP={lineConfig.ConnectionOptions.PlcIP},PlcRack={lineConfig.ConnectionOptions.PlcRack}" +
                    $",PlcSlot={lineConfig.ConnectionOptions.PlcSlot}" );
                lineData = new SiemensS7LineData()
                {
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

            // add model elements and device tags
            foreach (ElemGroupConfig elemGroupConfig in deviceTemplate.ElemGroups)
            {
                bool groupActive = elemGroupConfig.Active;
                bool groupCommands = groupActive && elemGroupConfig.ReadOnlyEnabled;
                ElemGroup elemGroup = null;
                TagGroup tagGroup = new TagGroup(elemGroupConfig.Name) { Hidden = !groupActive };
                //int elemAddrOffset = 0;

                if (groupActive)
                {
                    elemGroup = deviceModel.CreateElemGroup(elemGroupConfig.DataBlock);
                    elemGroup.Name = elemGroupConfig.Name;
                    //elemGroup.Address = elemGroupConfig.Address;
                    elemGroup.StartTagIdx = DeviceTags.Count;
                }

                for (int i = 0; i < elemGroupConfig.Elems.Count; i++)
                {
                    ElemConfig elemConfig = elemGroupConfig.Elems[i];

                    // add device tag 
                    DeviceTag deviceTag;


                    //Log.WriteLine($"--------- SiemenssS7 tagGroup.AddTag：{elemConfig.TagCode} {elemConfig.Name}  ");
                    //deviceTag = tagGroup.AddTag(elemConfig.TagCode, elemConfig.Name);
                    deviceTag = elemConfig.ToDeviceTag();

                    deviceTag.SetFormat(GetTagFormat(elemConfig));
                    if (elemConfig.ElemType == ElemType.String)
                    {
                        deviceTag.DataType = TagDataType.Unicode;
                        deviceTag.Format = TagFormat.String;
                        deviceTag.DataLen = DeviceTag.CalcDataLength(1, TagDataType.Unicode);
                    }
                    if (elemConfig.ElemType == ElemType.Float || elemConfig.ElemType == ElemType.Double)
                    {
                        deviceTag.DataType = TagDataType.Double;
                        deviceTag.Format = TagFormat.FloatNumber;
                        deviceTag.DataLen = DeviceTag.CalcDataLength(1, TagDataType.Unicode);
                    }
                    if (elemConfig.ElemType == ElemType.Int 
                         || elemConfig.ElemType == ElemType.UInt
                         || elemConfig.ElemType == ElemType.Short
                         || elemConfig.ElemType == ElemType.UShort
                         || elemConfig.ElemType == ElemType.Long
                         || elemConfig.ElemType == ElemType.ULong
                         )
                    {
                        deviceTag.DataType = TagDataType.Int64;
                        deviceTag.Format = TagFormat.IntNumber;
                        deviceTag.DataLen = DeviceTag.CalcDataLength(1, TagDataType.Unicode);
                    }
                    else
                    {  
                        deviceTag.DataType = TagDataType.Unicode;
                        deviceTag.Format = TagFormat.String;
                        deviceTag.DataLen = DeviceTag.CalcDataLength(1, TagDataType.Unicode);
                    }

                    // add model element
                    if (groupActive)
                    {
                        Elem elem = elemGroup.CreateElem();
                        elem.Name = elemConfig.Name;
                        elem.Address = elemConfig.Address;
                        elem.ElemType = elemConfig.ElemType;

                        //deviceTag.DataLen = elem.DataLength;

                        elem.DeviceTag = deviceTag;

                        //Log.WriteLine($"--------- SiemenssS7 SetTagData deviceTag.TagNum={deviceTag.TagNum} deviceTag.Code={deviceTag.Code} " +
                        //    $"deviceTag.Index={deviceTag.Index}  deviceTag.DataLength={deviceTag.DataLength}  ");

                        elemGroup.Elems.Add(elem);
                        elemGroup.ElemData.Add(new object());
                        elemGroup.CnlStatusID.Add(0);

                    }

                    tagGroup.DeviceTags.Add(deviceTag);

                    // add model command
                    if (groupCommands && !elemConfig.ReadOnly && !string.IsNullOrEmpty(elemConfig.TagCode))
                    {
                        //Log.WriteLine($"--------- SiemenssS7 elemGroupConfigCmds :Address={elemConfig.Address} ElemType={elemConfig.ElemType} ");
                        deviceModel.Cmds.Add(CreateSiemensS7Cmd(elemGroupConfig, elemConfig));
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
                //Log.WriteLine($"--------- SiemenssS7 Cmds :Address={cmdConfig.Address} ElemType={cmdConfig.ElemType} ");
                deviceModel.Cmds.Add(CreateSiemensS7Cmd(cmdConfig));
            }

            deviceModel.InitCmdMap();
            CanSendCommands = deviceModel.Cmds.Count > 0;

            InitSiemensS7Poll();
        }

        /// <summary>
        /// Initializes the device data.
        /// </summary>
        public override void InitDeviceData()
        {
            base.InitDeviceData();
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
        /// Sends the telecontrol command.
        /// 发送遥控命令。
        /// </summary>
        public override void SendCommand(TeleCommand cmd)
        {
            Log.WriteLine($"--------- SiemenssS7 SendCommand ");
            base.SendCommand(cmd);
            Log.WriteLine($"--------- SiemenssS7 SendCommand  cmds.count={deviceModel.Cmds.Count}  CmdCode={cmd.CmdCode}   CmdNum={cmd.CmdNum}");

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
                         || siemensS7Cmd.ElemType == ElemType.S7WString  
                         )
                        itemVal = cmdData;
                    else if (siemensS7Cmd.ElemType == ElemType.Bool
                         )
                        if (!string.IsNullOrEmpty(cmdData) && double.IsNaN( cmdVal ))
                        {
                            itemVal = cmdData == "True" || cmdData == "true" || cmdData == "1" ? 1:0;
                        }
                        else
                        {
                            itemVal = cmdVal;
                        }
                    else
                        itemVal = cmdVal;

                    Log.WriteLine($"--------- SiemenssS7 SendCommand :Address={siemensS7Cmd.Address} ElemType={siemensS7Cmd.ElemType} " +
                        $"cmdData={cmdData} cmdVal={cmdVal}  itemVal={itemVal} ");

                    if (lineData.ClientHelper.SetValueV2(siemensS7Cmd, itemVal))
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

        /// <summary>
        /// Performs a communication session.
        /// 执行通信会话。
        /// </summary>
        public override void Session()
        {
            LastSessionTime = DateTime.UtcNow;
            LastRequestOK = true;

            //Log.WriteLine($"--------- SiemenssS7 Session ");
            //Log.WriteLine($"--------- SiemenssS7 Session ElemGroups.Count={deviceModel.ElemGroups.Count}");

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
                    //Log.WriteLine($"--------- SiemenssS7 ReadDoRequest ");
                    //Log.WriteLine($"--------- SiemenssS7 ReadDoRequest ElemGroupName={elemGroup.Name}");
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
                        else //组里某个点位报错，其他点位值也正常显示，继续采集其他不影响其他组，
                        {
                            LastRequestOK = true; //继续采集其他不影响其他组
                            SetTagData(elemGroup);
                        }
                        //foreach (var elem in elemGroup.Elems )
                        //{ 
                        //    Object val =  lineData.ClientHelper.ReadValue(elem);
                        //    //Log.WriteLine($"--------- SiemenssS7 ReadValue:ElemName={elem.Name} Address={elem.Address} ElemType={elem.ElemType} Var={val}");

                        //    if (elem.DeviceTag.Aux is DeviceTagMeta tagMeta && val != null)
                        //        tagMeta.ActualDataType = val.GetType();


                        //    LastRequestOK = true;
                        //    int tagStatus = LastRequestOK ? CnlStatusID.Defined : CnlStatusID.Undefined;

                        //    DeviceData.Set(elem.DeviceTag.Code, Convert.ToDouble(val));
                        //}


                        FinishRequest();
                        tryNum++;

                        if (tryNum >= ReqRetries)//组里某个点位报错，本组尝试多次采集轮循后跳出while,继续下一组采集
                        {
                            LastRequestOK = true;
                        }
                    }

                    if (LastRequestOK)
                    {
                        // next element group
                        elemGroupIdx++;
                    }
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


    }
}
