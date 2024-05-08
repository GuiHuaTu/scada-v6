// Copyright (c) Rapid Software LLC. All rights reserved.

using Scada.Comm.Devices;
using Scada.Comm.Drivers.DrvOpcClassic.Config;

namespace Scada.Comm.Drivers.DrvOpcClassic.Logic
{
    /// <summary>
    /// Represents metadata about a monitored item.
    /// <para>Представляет метаданные об отслеживаемом элементе.</para>
    /// </summary>
    internal class ItemTag
    {
        /// <summary>
        /// Gets the device tag corresponding to the item.
        /// </summary>
        public DeviceTag DeviceTag { get; init; }

        /// <summary>
        /// Gets the item configuration.
        /// </summary>
        public ItemConfig ItemConfig { get; init; }
    }
}
