// Copyright (c) Rapid Software LLC. All rights reserved.

using Scada.Comm.Config;
using Scada.Comm.Devices;

namespace Scada.Comm.Drivers.DrvOpcClassic.Logic
{
    /// <summary>
    /// Implements the driver logic.
    /// <para>Реализует логику драйвера.</para>
    /// </summary>
    public class DrvOpcClassicLogic : DriverLogic
    {
        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        public DrvOpcClassicLogic(ICommContext commContext)
            : base(commContext)
        {
        }

        /// <summary>
        /// Gets the driver code.
        /// </summary>
        public override string Code
        {
            get
            {
                return DriverUtils.DriverCode;
            }
        }

        /// <summary>
        /// Creates a new device.
        /// </summary>
        public override DeviceLogic CreateDevice(ILineContext lineContext, DeviceConfig deviceConfig)
        {
            return new DevOpcClassicLogic(CommContext, lineContext, deviceConfig);
        }
    }
}
