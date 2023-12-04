// Copyright (c) Rapid Software LLC. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using S7.Net;
using System;
using System.Xml;

namespace Scada.Comm.Drivers.DrvSiemensS7.Config
{
    /// <summary>
    /// Represents the OPC server connection options.
    /// <para>Представляет параметры соединения с OPC-сервером.</para>
    /// </summary>
    public class SiemensS7ConnectionOptions
    {
        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        public SiemensS7ConnectionOptions()
        {
            PlcIP = "";
            PlcRack = 0;
            PlcSlot = 0;
        }


        /// <summary>
        /// Gets or sets the server URL.
        /// </summary>
        public string PlcIP { get; set; } 
        public short PlcRack { get; set; }
        public short PlcSlot { get; set; }




        /// <summary>
        /// Loads the options from the XML node.
        /// </summary>
        public void LoadFromXml(XmlNode xmlNode)
        { 
            PlcIP = xmlNode.GetChildAsString("PlcIP");
            PlcRack = Convert.ToInt16(xmlNode.GetChildAsString("PlcRack"));
            PlcSlot = Convert.ToInt16(xmlNode.GetChildAsString("PlcSlot"));
        }

        /// <summary>
        /// Saves the options into the XML node.
        /// </summary>
        public void SaveToXml(XmlElement xmlElem)
        {
            if(xmlElem == null) 
            {
                throw new  ArgumentNullException() ;
            }

            xmlElem.AppendElem("PlcIP", PlcIP);
            xmlElem.AppendElem("PlcRack", PlcRack);
            xmlElem.AppendElem("PlcSlot", PlcSlot);

        }

    }
}
