// Copyright (c) Rapid Software LLC. All rights reserved.

using System.Xml;

namespace Scada.Comm.Drivers.DrvOpcClassic.Config
{
    /// <summary>
    /// Represents a monitored item configuration.
    /// <para>Представляет конфигурацию отслеживаемого элемента.</para>
    /// </summary>
    internal class ItemConfig
    {
        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        public ItemConfig()
        {
            Active = true;
            Path = "";
            Name = "";
            TagCode = "";
            DataTypeName = "";
            IsArray = false;
            DataLen = 0;
            Tag = null;
        }


        /// <summary>
        /// Gets or sets a value indicating that the item is active.
        /// </summary>
        public bool Active { get; set; }

        /// <summary>
        /// Gets or sets the path.
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// Gets or sets the tag code associated with the item.
        /// </summary>
        public string TagCode { get; set; }

        /// <summary>
        /// Gets or sets the data type name.
        /// </summary>
        public string DataTypeName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating that the item data type is a string.
        /// </summary>
        public bool IsString
        {
            get
            {
                return DriverUtils.DataTypeEquals(DataTypeName, typeof(string));
            }
        }

        /// <summary>
        /// Gets or sets a value indicating that the item data type is an array.
        /// </summary>
        public bool IsArray { get; set; }

        /// <summary>
        /// Get or sets the data length if the item represents a string or an array.
        /// </summary>
        public int DataLen { get; set; }

        /// <summary>
        /// Gets the normalized data length.
        /// </summary>
        public int DataLength
        {
            get
            {
                return Math.Max(DataLen, 1);
            }
        }

        /// <summary>
        /// Gets or sets the object that contains data related to the item.
        /// </summary>
        public object Tag { get; set; }


        /// <summary>
        /// Loads the configuration from the XML node.
        /// </summary>
        public void LoadFromXml(XmlElement xmlElem)
        {
            ArgumentNullException.ThrowIfNull(xmlElem, nameof(xmlElem));
            Active = xmlElem.GetAttrAsBool("active");
            Path = xmlElem.GetAttrAsString("path");
            Name = xmlElem.GetAttrAsString("name");
            TagCode = xmlElem.GetAttrAsString("tagCode");
            DataTypeName = xmlElem.GetAttrAsString("dataType");
            IsArray = xmlElem.GetAttrAsBool("isArray");
            DataLen = xmlElem.GetAttrAsInt("dataLen");
        }

        /// <summary>
        /// Saves the configuration into the XML node.
        /// </summary>
        public void SaveToXml(XmlElement xmlElem)
        {
            ArgumentNullException.ThrowIfNull(xmlElem, nameof(xmlElem));
            xmlElem.SetAttribute("active", Active);
            xmlElem.SetAttribute("path", Path);
            xmlElem.SetAttribute("name", Name);
            xmlElem.SetAttribute("tagCode", TagCode);
            xmlElem.SetAttribute("dataType", DataTypeName);
            xmlElem.SetAttribute("isArray", IsArray);

            if (IsString || IsArray)
                xmlElem.SetAttribute("dataLen", DataLength);
        }
    }
}
