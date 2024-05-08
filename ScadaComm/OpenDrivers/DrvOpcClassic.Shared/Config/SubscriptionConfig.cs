// Copyright (c) Rapid Software LLC. All rights reserved.

using System.Xml;

namespace Scada.Comm.Drivers.DrvOpcClassic.Config
{
    /// <summary>
    /// Represents a subscription configuration.
    /// <para>Представляет конфигурацию подписки.</para>
    /// </summary>
    internal class SubscriptionConfig
    {
        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        public SubscriptionConfig()
        {
            Active = true;
            DisplayName = "";
            UpdateRate = 1000;
            KeepAlive = 0;
            Deadband = 0.0;
            ReadSync = false;
            Items = new List<ItemConfig>();
        }


        /// <summary>
        /// Gets or sets a value indicating that the subscription is active.
        /// </summary>
        public bool Active { get; set; }

        /// <summary>
        /// Gets or sets the display name.
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// Gets or sets the rate at which the server checks of updates to send to the client, ms.
        /// </summary>
        public int UpdateRate { get; set; }

        /// <summary>
        /// Gets or sets the maximum period between updates sent to the client, ms.
        /// </summary>
        public int KeepAlive { get; set; }

        /// <summary>
        /// Gets or sets the minimum percentage change required to trigger a data update for an item.
        /// </summary>
        public double Deadband { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to read values synchronously.
        /// </summary>
        public bool ReadSync { get; set; }

        /// <summary>
        /// Gets the monitored items of the subscription.
        /// </summary>
        public List<ItemConfig> Items { get; private set; }


        /// <summary>
        /// Loads the configuration from the XML node.
        /// </summary>
        public void LoadFromXml(XmlElement xmlElem)
        {
            ArgumentNullException.ThrowIfNull(xmlElem, nameof(xmlElem));
            Active = xmlElem.GetAttrAsBool("active", Active);
            DisplayName = xmlElem.GetAttrAsString("displayName", DisplayName);
            UpdateRate = xmlElem.GetAttrAsInt("updateRate", UpdateRate);
            KeepAlive = xmlElem.GetAttrAsInt("keepAlive", KeepAlive);
            Deadband = xmlElem.GetAttrAsDouble("deadband", Deadband);
            ReadSync = xmlElem.GetAttrAsBool("readSync", ReadSync);

            foreach (XmlElement itemElem in xmlElem.SelectNodes("Item"))
            {
                ItemConfig itemConfig = new();
                itemConfig.LoadFromXml(itemElem);
                Items.Add(itemConfig);
            }
        }

        /// <summary>
        /// Saves the configuration into the XML node.
        /// </summary>
        public void SaveToXml(XmlElement xmlElem)
        {
            ArgumentNullException.ThrowIfNull(xmlElem, nameof(xmlElem));
            xmlElem.SetAttribute("active", Active);
            xmlElem.SetAttribute("displayName", DisplayName);
            xmlElem.SetAttribute("updateRate", UpdateRate);
            xmlElem.SetAttribute("keepAlive", KeepAlive);
            xmlElem.SetAttribute("deadband", Deadband);
            xmlElem.SetAttribute("readSync", ReadSync);

            foreach (ItemConfig itemConfig in Items)
            {
                itemConfig.SaveToXml(xmlElem.AppendElem("Item"));
            }
        }
    }
}
