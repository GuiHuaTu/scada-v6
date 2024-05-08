// Copyright (c) Rapid Software LLC. All rights reserved.

using System.Xml;

namespace Scada.Comm.Drivers.DrvOpcClassic.Config
{
    /// <summary>
    /// Represents an event category configuration.
    /// <para>Представляет конфигурацию категории события.</para>
    /// </summary>
    internal class EventCategoryConfig
    {
        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        public EventCategoryConfig()
        {
            Name = "";
            ID = 0;
        }


        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the category ID.
        /// </summary>
        public int ID { get; set; }


        /// <summary>
        /// Loads the configuration from the XML node.
        /// </summary>
        public void LoadFromXml(XmlElement xmlElem)
        {
            ArgumentNullException.ThrowIfNull(xmlElem, nameof(xmlElem));
            Name = xmlElem.GetAttrAsString("name");
            ID = xmlElem.GetAttrAsInt("id");
        }

        /// <summary>
        /// Saves the configuration into the XML node.
        /// </summary>
        public void SaveToXml(XmlElement xmlElem)
        {
            ArgumentNullException.ThrowIfNull(xmlElem, nameof(xmlElem));
            xmlElem.SetAttribute("name", Name);
            xmlElem.SetAttribute("id", ID);
        }
    }
}
