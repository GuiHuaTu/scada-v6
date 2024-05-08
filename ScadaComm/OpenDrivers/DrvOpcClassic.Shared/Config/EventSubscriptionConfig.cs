// Copyright (c) Rapid Software LLC. All rights reserved.

using System.Xml;

namespace Scada.Comm.Drivers.DrvOpcClassic.Config
{
    /// <summary>
    /// Represents an event subscription configuration.
    /// <para>Представляет конфигурацию подписки на события.</para>
    /// </summary>
    internal class EventSubscriptionConfig
    {
        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        public EventSubscriptionConfig()
        {
            Active = true;
            DisplayName = "";
            UpdateRate = 1000;
            KeepAlive = 0;
            MaxSize = 0;
            SimpleEvents = true;
            TrackingEvents = true;
            ConditionEvents = true;
            HighSeverity = 1000;
            LowSeverity = 1;
            Categories = new List<EventCategoryConfig>();
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
        /// Gets or sets the maximum rate at which the server sends event notifications, ms.
        /// </summary>
        public int UpdateRate { get; set; }

        /// <summary>
        /// Gets or sets the maximum period between updates sent to the client, ms.
        /// </summary>
        public int KeepAlive { get; set; }

        /// <summary>
        /// Gets or sets the requested maximum number of events that are sent in a single callback.
        /// </summary>
        public int MaxSize { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to receive events of the simple type.
        /// </summary>
        public bool SimpleEvents { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to receive events of the tracking type.
        /// </summary>
        public bool TrackingEvents { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to receive events of the condition type.
        /// </summary>
        public bool ConditionEvents { get; set; }

        /// <summary>
        /// Gets or sets the highest severity of events to send to the client.
        /// </summary>
        public int HighSeverity { get; set; }

        /// <summary>
        /// Gets or sets the lowest severity of events to send to the client.
        /// </summary>
        public int LowSeverity { get; set; }

        /// <summary>
        /// Gets the categories of events to send to the client. 
        /// List keys contain category names, values contain category IDs.
        /// </summary>
        public List<EventCategoryConfig> Categories { get; private set; }
        
        
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
            MaxSize = xmlElem.GetAttrAsInt("maxSize", MaxSize);
            SimpleEvents = xmlElem.GetAttrAsBool("simpleEvents", SimpleEvents);
            TrackingEvents = xmlElem.GetAttrAsBool("trackingEvents", TrackingEvents);
            ConditionEvents = xmlElem.GetAttrAsBool("conditionEvents", ConditionEvents);
            HighSeverity = xmlElem.GetAttrAsInt("highSeverity", HighSeverity);
            LowSeverity = xmlElem.GetAttrAsInt("lowSeverity", LowSeverity);

            foreach (XmlElement categoryElem in xmlElem.SelectNodes("Category"))
            {
                EventCategoryConfig category = new();
                category.LoadFromXml(categoryElem);
                Categories.Add(category);
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
            xmlElem.SetAttribute("maxSize", MaxSize);
            xmlElem.SetAttribute("simpleEvents", SimpleEvents);
            xmlElem.SetAttribute("trackingEvents", TrackingEvents);
            xmlElem.SetAttribute("conditionEvents", ConditionEvents);
            xmlElem.SetAttribute("highSeverity", HighSeverity);
            xmlElem.SetAttribute("lowSeverity", LowSeverity);

            foreach (EventCategoryConfig category in Categories)
            {
                category.SaveToXml(xmlElem.AppendElem("Category"));
            }
        }
    }
}
