// Copyright (c) Rapid Software LLC. All rights reserved.

using System.Xml;

namespace Scada.Comm.Drivers.DrvOpcClassic.Config
{
    /// <summary>
    /// Represents a command configuration.
    /// <para>Представляет конфигурацию команды.</para>
    /// </summary>
    internal class CommandConfig
    {
        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        public CommandConfig()
        {
            Path = "";
            Name = "";
            CmdNum = 0;
            CmdCode = "";
            DataTypeName = "";
        }


        /// <summary>
        /// Gets or sets the path.
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the command number.
        /// </summary>
        public int CmdNum { get; set; }

        /// <summary>
        /// Gets or sets the command code.
        /// </summary>
        public string CmdCode { get; set; }

        /// <summary>
        /// Gets or sets the data type name.
        /// </summary>
        public string DataTypeName { get; set; }
        
        
        /// <summary>
        /// Loads the configuration from the XML node.
        /// </summary>
        public void LoadFromXml(XmlElement xmlElem)
        {
            ArgumentNullException.ThrowIfNull(xmlElem, nameof(xmlElem));
            Path = xmlElem.GetAttrAsString("path");
            Name = xmlElem.GetAttrAsString("name");
            CmdNum = xmlElem.GetAttrAsInt("cmdNum");
            CmdCode = xmlElem.GetAttrAsString("cmdCode");
            DataTypeName = xmlElem.GetAttrAsString("dataType");
        }

        /// <summary>
        /// Saves the configuration into the XML node.
        /// </summary>
        public void SaveToXml(XmlElement xmlElem)
        {
            ArgumentNullException.ThrowIfNull(xmlElem, nameof(xmlElem));
            xmlElem.SetAttribute("path", Path);
            xmlElem.SetAttribute("name", Name);
            xmlElem.SetAttribute("cmdNum", CmdNum);
            xmlElem.SetAttribute("cmdCode", CmdCode);
            xmlElem.SetAttribute("dataType", DataTypeName);
        }
    }
}
