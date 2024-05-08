// Copyright (c) Rapid Software LLC. All rights reserved.

using Scada.Comm.Devices;
using System.Xml;

namespace Scada.Comm.Drivers.DrvOpcClassic.Config
{
    /// <summary>
    /// Represents a configuration of an OPC device.
    /// <para>Представляет конфигурацию устройства OPC.</para>
    /// </summary>
    internal class OpcDeviceConfig : DeviceConfigBase
    {
        /// <summary>
        /// Gets the subscriptions.
        /// </summary>
        public List<SubscriptionConfig> Subscriptions { get; private set; }

        /// <summary>
        /// Gets the commands.
        /// </summary>
        public List<CommandConfig> Commands { get; private set; }

        /// <summary>
        /// Gets the event subscriptions.
        /// </summary>
        public List<EventSubscriptionConfig> EventSubscriptions { get; private set; }


        /// <summary>
        /// Sets the default values.
        /// </summary>
        protected override void SetToDefault()
        {
            Subscriptions = new List<SubscriptionConfig>();
            Commands = new List<CommandConfig>();
            EventSubscriptions = new List<EventSubscriptionConfig>();
        }

        /// <summary>
        /// Loads the configuration from the specified reader.
        /// </summary>
        protected override void Load(TextReader reader)
        {
            XmlDocument xmlDoc = new();
            xmlDoc.Load(reader);
            XmlElement rootElem = xmlDoc.DocumentElement;

            if (rootElem.SelectSingleNode("Subscriptions") is XmlNode subscriptionsNode)
            {
                foreach (XmlElement subscriptionElem in subscriptionsNode.SelectNodes("Subscription"))
                {
                    SubscriptionConfig subscriptionConfig = new();
                    subscriptionConfig.LoadFromXml(subscriptionElem);
                    Subscriptions.Add(subscriptionConfig);
                }
            }

            if (rootElem.SelectSingleNode("Commands") is XmlNode commandsNode)
            {
                foreach (XmlElement commandElem in commandsNode.SelectNodes("Command"))
                {
                    CommandConfig commandConfig = new();
                    commandConfig.LoadFromXml(commandElem);
                    Commands.Add(commandConfig);
                }
            }

            if (rootElem.SelectSingleNode("EventSubscriptions") is XmlNode eventSubscriptionsNode)
            {
                foreach (XmlElement subscriptionElem in eventSubscriptionsNode.SelectNodes("Subscription"))
                {
                    EventSubscriptionConfig eventSubscriptionConfig = new();
                    eventSubscriptionConfig.LoadFromXml(subscriptionElem);
                    EventSubscriptions.Add(eventSubscriptionConfig);
                }
            }
        }

        /// <summary>
        /// Saves the configuration to the specified writer.
        /// </summary>
        protected override void Save(TextWriter writer)
        {
            XmlDocument xmlDoc = new();
            XmlDeclaration xmlDecl = xmlDoc.CreateXmlDeclaration("1.0", "utf-8", null);
            xmlDoc.AppendChild(xmlDecl);

            XmlElement rootElem = xmlDoc.CreateElement("OpcDeviceConfig");
            xmlDoc.AppendChild(rootElem);

            XmlElement subscriptionsElem = rootElem.AppendElem("Subscriptions");
            XmlElement commandsElem = rootElem.AppendElem("Commands");
            XmlElement eventSubscriptionsElem = rootElem.AppendElem("EventSubscriptions");

            foreach (SubscriptionConfig subscriptionConfig in Subscriptions)
            {
                subscriptionConfig.SaveToXml(subscriptionsElem.AppendElem("Subscription"));
            }

            foreach (CommandConfig commandConfig in Commands)
            {
                commandConfig.SaveToXml(commandsElem.AppendElem("Command"));
            }

            foreach (EventSubscriptionConfig eventSubscriptionConfig in EventSubscriptions)
            {
                eventSubscriptionConfig.SaveToXml(eventSubscriptionsElem.AppendElem("Subscription"));
            }

            xmlDoc.Save(writer);
        }

        /// <summary>
        /// Gets the short name of the device configuration file.
        /// </summary>
        public static string GetFileName(int deviceNum)
        {
            return GetFileName(DriverUtils.DriverCode, deviceNum);
        }
    }
}
