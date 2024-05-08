// Copyright (c) Rapid Software LLC. All rights reserved.

using Scada.Comm.Config;
using Scada.Comm.Devices;
using Scada.Comm.Drivers.DrvOpcClassic.Config;
using Scada.Comm.Drivers.DrvOpcClassic.View.Forms;
using Scada.Data.Const;
using Scada.Data.Models;

namespace Scada.Comm.Drivers.DrvOpcClassic.View
{
    /// <summary>
    /// Implements the device user interface.
    /// <para>Реализует пользовательский интерфейс устройства.</para>
    /// </summary>
    internal class DevOpcClassicView : DeviceView
    {
        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        public DevOpcClassicView(DriverView parentView, LineConfig lineConfig, DeviceConfig deviceConfig)
            : base(parentView, lineConfig, deviceConfig)
        {
            CanShowProperties = true;
        }


        /// <summary>
        /// Shows a modal dialog box for editing device properties.
        /// </summary>
        public override bool ShowProperties()
        {
            new FrmDeviceConfig(AppDirs, LineConfig.CommLineNum, DeviceNum).ShowDialog();
            return false;
        }

        /// <summary>
        /// Gets the default polling options for the device.
        /// </summary>
        public override PollingOptions GetPollingOptions()
        {
            return PollingOptions.CreateWithDefaultDelay();
        }

        /// <summary>
        /// Gets the channel prototypes for the device.
        /// </summary>
        public override ICollection<CnlPrototype> GetCnlPrototypes()
        {
            OpcDeviceConfig config = new();

            if (!config.Load(Path.Combine(AppDirs.ConfigDir, OpcDeviceConfig.GetFileName(DeviceNum)),
                out string errMsg))
            {
                throw new ScadaException(errMsg);
            }

            // create channels for subscriptions
            List<CnlPrototype> cnlPrototypes = new();
            int tagNum = 1;
            int eventMask = new EventMask { Enabled = true, StatusChange = true, Command = true }.Value;
            int cmdEventMask = new EventMask { Enabled = true, Command = true }.Value;

            foreach (SubscriptionConfig subscriptionConfig in config.Subscriptions)
            {
                foreach (ItemConfig itemConfig in subscriptionConfig.Items)
                {
                    CnlPrototype cnl = new()
                    {
                        Active = itemConfig.Active,
                        Name = itemConfig.Name,
                        CnlTypeID = CnlTypeID.InputOutput,
                        TagNum = string.IsNullOrEmpty(itemConfig.TagCode) ? tagNum : null,
                        TagCode = itemConfig.TagCode,
                        EventMask = eventMask
                    };

                    if (itemConfig.IsString)
                    {
                        cnl.DataTypeID = DataTypeID.Unicode;
                        cnl.DataLen = DeviceTag.CalcDataLength(itemConfig.DataLength, TagDataType.Unicode);
                        cnl.FormatCode = FormatCode.String;
                    }
                    else if (itemConfig.IsArray)
                    {
                        cnl.DataLen = itemConfig.DataLength;
                    }

                    if (DriverUtils.DataTypeEquals(itemConfig.DataTypeName, typeof(DateTime)))
                        cnl.FormatCode = FormatCode.DateTime;

                    cnlPrototypes.Add(cnl);
                    tagNum++;
                }
            }

            // create channels for commands
            foreach (CommandConfig commandConfig in config.Commands)
            {
                CnlPrototype cnl = new()
                {
                    Name = commandConfig.Name,
                    CnlTypeID = CnlTypeID.Output,
                    TagNum = string.IsNullOrEmpty(commandConfig.CmdCode) ? commandConfig.CmdNum : null,
                    TagCode = commandConfig.CmdCode,
                    EventMask = cmdEventMask
                };

                if (DriverUtils.DataTypeEquals(commandConfig.DataTypeName, typeof(string)))
                    cnl.FormatCode = FormatCode.String;
                else if (DriverUtils.DataTypeEquals(commandConfig.DataTypeName, typeof(DateTime)))
                    cnl.FormatCode = FormatCode.DateTime;

                cnlPrototypes.Add(cnl);
            }

            return cnlPrototypes;
        }
    }
}
