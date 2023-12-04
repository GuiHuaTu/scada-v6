// Copyright (c) Rapid Software LLC. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using S7.Net;
using Scada.Comm.Config;
using Scada.Comm.Devices;
using Scada.Comm.Drivers.DrvSiemensS7.Config;
using Scada.Comm.Drivers.DrvSiemensS7.Protocol;
using Scada.Comm.Lang;
using Scada.Data.Const;
using Scada.Data.Models;
using Scada.Lang;
using System;
using System.Collections.Generic;

namespace Scada.Comm.Drivers.DrvSiemensS7.Logic
{
    /// <summary>
    /// Implements the device logic.
    /// <para>Реализует логику устройства.</para>
    /// </summary>
    public class DevSiemensS7Logic : DeviceLogic
    {
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

        protected string cpuType;     // the cpu type of the communication line
        protected string plcIP;     // the ip of the communication line
        protected short plcRack;     // the rack of the communication line
        protected short plcSlot;     // the slot of the communication line
        protected DeviceModel deviceModel; // the device model
        protected SiemensS7Poll siemensS7Poll;   // implements device polling




        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        public DevSiemensS7Logic(ICommContext commContext, ILineContext lineContext, DeviceConfig deviceConfig)
            : base(commContext, lineContext, deviceConfig)
        {
            cpuType = Enum.GetName(typeof(VarType), CpuType.S71200);
            lineContext.LineConfig.CustomOptions.TryGetValue("PlcIp",out plcIP);
            plcRack = (short)lineContext.LineConfig.CustomOptions.GetValueAsInt("PlcRack" );
            plcSlot = (short)lineContext.LineConfig.CustomOptions.GetValueAsInt("PlcSlot");
            deviceModel = null;
            siemensS7Poll = null;
        }


        /// <summary>
        /// Gets the shared data key of the template dictionary.
        /// </summary>
        protected virtual string TemplateDictKey => "SiemensS7.Templates";


        /// <summary>
        /// Creates a new SiemensS7 command based on the element configuration.
        /// 基于元素配置创建一个新的SiemensS7命令。
        /// </summary>
        private SiemensS7Cmd CreateSiemensS7Cmd(DeviceTemplateOptions options, 
            ElemGroupConfig elemGroupConfig, ElemConfig elemConfig )
        {
            SiemensS7Cmd siemensS7Cmd = deviceModel.CreateSiemensS7Cmd(elemGroupConfig.DataBlock, elemConfig.Quantity > 1);
            siemensS7Cmd.Name = elemConfig.Name;
            siemensS7Cmd.Address = elemConfig.Address;
            siemensS7Cmd.ElemType = elemConfig.ElemType;
            siemensS7Cmd.ElemCnt = 1;
            siemensS7Cmd.ByteOrder = SiemensS7Utils.ParseByteOrder(elemConfig.ByteOrder) ??
                options.GetDefaultByteOrder(SiemensS7Utils.GetDataLength(elemConfig.ElemType));
            siemensS7Cmd.CmdNum = 0;
            siemensS7Cmd.CmdCode = elemConfig.TagCode;
             
            return siemensS7Cmd;
        }

        /// <summary>
        /// Creates a new SiemensS7 command based on the command configuration.
        /// 基于命令配置创建新的SiemensS7命令
        /// </summary>
        private SiemensS7Cmd CreateSiemensS7Cmd(DeviceTemplateOptions options, CmdConfig cmdConfig)
        {
            SiemensS7Cmd siemensS7Cmd = deviceModel.CreateSiemensS7Cmd(cmdConfig.DataBlock, cmdConfig.Multiple);
            siemensS7Cmd.Name = cmdConfig.Name;
            siemensS7Cmd.Address = cmdConfig.Address;
            siemensS7Cmd.ElemType = cmdConfig.ElemType;
            siemensS7Cmd.ElemCnt = cmdConfig.ElemCnt;
            siemensS7Cmd.ByteOrder = SiemensS7Utils.ParseByteOrder(cmdConfig.ByteOrder) ??
                options.GetDefaultByteOrder(SiemensS7Utils.GetDataLength(cmdConfig.ElemType) * cmdConfig.ElemCnt);
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
            if (deviceModel != null)
            {
                // create polling object
                siemensS7Poll = new SiemensS7Poll(cpuType, plcIP,plcRack,plcSlot)
                {
                    Timeout = PollingOptions.Timeout,
                    Log = Log
                };
            }
        }

        /// <summary>
        /// Sets the data of the element group tags.
        /// </summary>
        private void SetTagData(ElemGroup elemGroup)
        {
            int tagStatus = LastRequestOK ? CnlStatusID.Defined : CnlStatusID.Undefined;
            for (int elemIdx = 0, tagIdx = elemGroup.StartTagIdx + elemIdx, cnt = elemGroup.Elems.Count; 
                elemIdx < cnt; elemIdx++, tagIdx++)
            {
                DeviceData.Set(elemGroup.Elems[elemIdx].DeviceTag, elemGroup.ElemData[elemIdx], tagStatus);
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
        /// Performs actions when starting a communication line.
        /// 启动通信线路时执行操作。
        /// </summary>
        public override void OnCommLineStart()
        {
            cpuType = LineContext.LineConfig.CustomOptions.GetValueAsString("CpuType", Enum.GetName(typeof(VarType), CpuType.S71200));
            
            LineContext.LineConfig.CustomOptions.TryGetValue("PlcIp", out plcIP);
            plcRack = (short)LineContext.LineConfig.CustomOptions.GetValueAsInt("PlcRack");
            plcSlot = (short)LineContext.LineConfig.CustomOptions.GetValueAsInt("PlcSlot");
        }

        /// <summary>
        /// Performs actions after setting the connection.
        /// 设置连接后执行操作。
        /// </summary>
        public override void OnConnectionSet()
        {
            //SiemensS7Session.Open();

            InitSiemensS7Poll();


            // update connection reference
            if (siemensS7Poll != null)
                siemensS7Poll.Connection();
        }

        /// <summary>
        /// Performs actions when terminating a communication line.
        /// 在终止通信线路时执行操作。
        /// </summary>
        public override void OnCommLineTerminate()
        {
            siemensS7Poll.Disconnect();
        }

        /// <summary>
        /// Initializes the device tags.
        /// 初始化设备标记。
        /// </summary>
        public override void InitDeviceTags()
        {
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

                foreach (ElemConfig elemConfig in elemGroupConfig.Elems)
                {
                    // add model element
                    if (groupActive)
                    {
                        Elem elem = elemGroup.CreateElem();
                        elem.Name = elemConfig.Name; 
                        elem.Address = elemConfig.Address;
                        elem.ElemType = elemConfig.ElemType;
                        elem.ByteOrder = SiemensS7Utils.ParseByteOrder(elemConfig.ByteOrder) ??
                            deviceTemplate.Options.GetDefaultByteOrder(SiemensS7Utils.GetDataLength(elemConfig.ElemType));
                        elemGroup.Elems.Add(elem);
                    }

                    // add model command
                    if (groupCommands && !elemConfig.ReadOnly && !string.IsNullOrEmpty(elemConfig.TagCode))
                    {
                        deviceModel.Cmds.Add(
                            CreateSiemensS7Cmd(deviceTemplate.Options, elemGroupConfig, elemConfig ));
                    }

                    // add device tag
                    tagGroup.AddTag(elemConfig.TagCode, elemConfig.Name).SetFormat(GetTagFormat(elemConfig));
                    //elemAddrOffset += elemConfig.Quantity;
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
                deviceModel.Cmds.Add(CreateSiemensS7Cmd(deviceTemplate.Options, cmdConfig));
            }

            deviceModel.InitCmdMap();
            CanSendCommands = deviceModel.Cmds.Count > 0;
            InitSiemensS7Poll();
        }

        /// <summary>
        /// Performs a communication session.
        /// 执行通信会话。
        /// </summary>
        public override void Session()
        {
            base.Session();

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
                // request element groups
                int elemGroupCnt = deviceModel.ElemGroups.Count;
                int elemGroupIdx = 0;

                while (elemGroupIdx < elemGroupCnt && LastRequestOK)
                {
                    ElemGroup elemGroup = deviceModel.ElemGroups[elemGroupIdx];
                    LastRequestOK = false;
                    int tryNum = 0;

                    while (RequestNeeded(ref tryNum))
                    {
                        // perform request
                        if (siemensS7Poll.ReadDoRequest(elemGroup))
                        {
                            LastRequestOK = true;

                            SetTagData(elemGroup);
                        }

                        FinishRequest();
                        tryNum++;
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
        }

        /// <summary>
        /// Sends the telecontrol command.
        /// 发送遥控命令。
        /// </summary>
        public override void SendCommand(TeleCommand cmd)
        {
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
                    if (siemensS7Poll.SetValue(siemensS7Cmd,cmd.GetCmdDataString()))
                        LastRequestOK = true;

                    FinishRequest();
                    tryNum++;
                }
            }
            else
            {
                LastRequestOK = false;
                Log.WriteLine(CommPhrases.InvalidCommand);
            }

            FinishCommand();
        }
    }
}
