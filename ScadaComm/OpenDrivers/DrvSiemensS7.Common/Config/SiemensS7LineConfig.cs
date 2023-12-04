// Copyright (c) Rapid Software LLC. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using S7.Net;
using Scada.Config;
using System.IO;
using System.Xml;

namespace Scada.Comm.Drivers.DrvSiemensS7.Config
{
    /// <summary>
    /// Represents a configuration of an OPC communication line.
    /// <para>Представляет конфигурацию линии связи OPC.</para>
    /// </summary>
    public class SiemensS7LineConfig : ConfigBase
    {
        /// <summary>
        /// Gets the connection options.
        /// </summary>
        public SiemensS7ConnectionOptions ConnectionOptions { get; private set; }


        /// <summary>
        /// Sets the default values.
        /// </summary>
        protected override void SetToDefault()
        {
            ConnectionOptions = new SiemensS7ConnectionOptions();
        }

        /// <summary>
        /// Loads the configuration from the specified reader.
        /// </summary>
        protected override void Load(TextReader reader)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(reader);
            XmlElement rootElem = xmlDoc.DocumentElement;

            if (rootElem.SelectSingleNode("ConnectionOptions") is XmlNode connectionOptionsNode)
                ConnectionOptions.LoadFromXml(connectionOptionsNode);
        }

        /// <summary>
        /// Saves the configuration to the specified writer.
        /// </summary>
        protected override void Save(TextWriter writer)
        {
            XmlDocument xmlDoc = new XmlDocument();
            XmlDeclaration xmlDecl = xmlDoc.CreateXmlDeclaration("1.0", "utf-8", null);
            xmlDoc.AppendChild(xmlDecl);

            XmlElement rootElem = xmlDoc.CreateElement("SiemensS7LineConfig");
            xmlDoc.AppendChild(rootElem);

            ConnectionOptions.SaveToXml(rootElem.AppendElem("ConnectionOptions"));
            xmlDoc.Save(writer);
        }

        /// <summary>
        /// Gets the short name of the line configuration file.
        /// </summary>
        public static string GetFileName(int lineNum)
        {
            return $"{DriverUtils.DriverCode}_line{lineNum:D3}.xml";
        }
    }
}
