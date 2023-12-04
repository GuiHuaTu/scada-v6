// Copyright (c) Rapid Software LLC. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;

namespace Scada.Comm.Drivers.DrvSiemensS7.Protocol
{
    /// <summary>
    /// Represents a SiemensS7 device model.
    /// <para>Представляет модель устройства SiemensS7.</para>
    /// </summary>
    public class DeviceModel
    {
        private Dictionary<int, SiemensS7Cmd> cmdByNum;     // the commands accessed by number
        private Dictionary<string, SiemensS7Cmd> cmdByCode; // the commands accessed by code

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        public DeviceModel()
        {
            cmdByNum = null;
            cmdByCode = null;

            Addr = 0;
            ElemGroups = new List<ElemGroup>();
            Cmds = new List<SiemensS7Cmd>();
        }


        /// <summary>
        /// Gets or sets the device address (unit ID).
        /// </summary>
        public byte Addr { get; set; }

        /// <summary>
        /// Gets the element groups.
        /// </summary>
        public List<ElemGroup> ElemGroups { get; private set; }

        /// <summary>
        /// Gets the commands.
        /// </summary>
        public List<SiemensS7Cmd> Cmds { get; private set; }


        /// <summary>
        /// Creates a new group of SiemensS7 elements.
        /// </summary>
        public virtual ElemGroup CreateElemGroup(DataBlock dataBlock)
        {
            return new ElemGroup(dataBlock);
        }

        /// <summary>
        /// Creates a new SiemensS7 command.
        /// </summary>
        public virtual SiemensS7Cmd CreateSiemensS7Cmd(DataBlock dataBlock, bool multiple)
        {
            return new SiemensS7Cmd(dataBlock, multiple);
        }

        /// <summary>
        /// Initializes a command map.
        /// </summary>
        public void InitCmdMap()
        {
            cmdByNum = new Dictionary<int, SiemensS7Cmd>();
            cmdByCode = new Dictionary<string, SiemensS7Cmd>();

            foreach (SiemensS7Cmd cmd in Cmds)
            { 
                if (cmd.CmdNum > 0 && !cmdByNum.ContainsKey(cmd.CmdNum))
                    cmdByNum.Add(cmd.CmdNum, cmd);

                if (!string.IsNullOrEmpty(cmd.CmdCode) && !cmdByCode.ContainsKey(cmd.CmdCode))
                    cmdByCode.Add(cmd.CmdCode, cmd);
            };
        }

        /// <summary>
        /// Gets a command by the specified number.
        /// </summary>
        public SiemensS7Cmd GetCmd(int cmdNum)
        {
            return cmdByNum != null && cmdNum > 0 && cmdByNum.TryGetValue(cmdNum, out SiemensS7Cmd cmd) ? cmd : null;
        }

        /// <summary>
        /// Gets a command by the specified code.
        /// </summary>
        public SiemensS7Cmd GetCmd(string cmdCode)
        {
            return cmdByCode != null && !string.IsNullOrEmpty(cmdCode) && 
                cmdByCode.TryGetValue(cmdCode, out SiemensS7Cmd cmd) ? cmd : null;
        }
    }
}
