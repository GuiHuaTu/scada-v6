// Copyright (c) Rapid Software LLC. All rights reserved.

using Scada.Comm.Drivers.DrvOpcClassic.Config;

namespace Scada.Comm.Drivers.DrvOpcClassic.Logic
{
    /// <summary>
    /// Represents metadata about an event subscription.
    /// <para>Представляет метаданные о подписке на события.</para>
    /// </summary>
    internal class EventSubscriptionTag : ICloneable
    {
        /// <summary>
        /// Gets the device logic that added the subscription.
        /// </summary>
        public DevOpcClassicLogic DeviceLogic { get; init; }

        /// <summary>
        /// Gets the subscription configuration.
        /// </summary>
        public EventSubscriptionConfig SubscriptionConfig { get; init; }


        /// <summary>
        /// Creates a new object that is a copy of the current instance.
        /// Required to create Opc.Ae.Subscription.
        /// </summary>
        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
