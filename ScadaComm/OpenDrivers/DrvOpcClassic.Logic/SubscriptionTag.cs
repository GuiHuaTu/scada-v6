// Copyright (c) Rapid Software LLC. All rights reserved.

using Scada.Comm.Drivers.DrvOpcClassic.Config;

namespace Scada.Comm.Drivers.DrvOpcClassic.Logic
{
    /// <summary>
    /// Represents metadata about a subscription.
    /// <para>Представляет метаданные о подписке.</para>
    /// </summary>
    internal class SubscriptionTag
    {
        /// <summary>
        /// Gets the device logic that added the subscription.
        /// </summary>
        public DevOpcClassicLogic DeviceLogic { get; init; }

        /// <summary>
        /// Gets the subscription configuration.
        /// </summary>
        public SubscriptionConfig SubscriptionConfig { get; init; }

        /// <summary>
        /// Gets or sets the subscription items for synchronous reading.
        /// </summary>
        public Opc.Da.Item[] SubscriptionItems { get; set; }
    }
}
