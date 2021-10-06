﻿/*
 * Copyright 2021 Rapid Software LLC
 * 
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 * 
 *     http://www.apache.org/licenses/LICENSE-2.0
 * 
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 * 
 * 
 * Product  : Rapid SCADA
 * Module   : ScadaAdminCommon
 * Summary  : Represents configuration transfer options
 * 
 * Author   : Mikhail Shiryaev
 * Created  : 2018
 * Modified : 2021
 */

using Scada.Agent;
using Scada.Protocol;
using System;
using System.Xml;

namespace Scada.Admin.Deployment
{
    /// <summary>
    /// Represents configuration transfer options.
    /// <para>Представляет параметры передачи конфигурации.</para>
    /// </summary>
    public abstract class TransferOptions
    {
        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        public TransferOptions()
        {
            IncludeBase = true;
            IncludeView = true;
            IncludeServer = true;
            IncludeComm = true;
            IncludeWeb = true;
            IgnoreRegKeys = false;
        }


        /// <summary>
        /// Gets or sets a value indicating whether to transfer the configuration database.
        /// </summary>
        public bool IncludeBase { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to transfer views.
        /// </summary>
        public bool IncludeView { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to transfer Server configuration.
        /// </summary>
        public bool IncludeServer { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to transfer Communicator configuration.
        /// </summary>
        public bool IncludeComm { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to transfer Webstation configuration.
        /// </summary>
        public bool IncludeWeb { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to ignore registration keys.
        /// </summary>
        public bool IgnoreRegKeys { get; set; }


        /// <summary>
        /// Loads the settings from the XML node.
        /// </summary>
        public virtual void LoadFromXml(XmlNode xmlNode)
        {
            if (xmlNode == null)
                throw new ArgumentNullException(nameof(xmlNode));

            IncludeBase = xmlNode.GetChildAsBool("IncludeBase");
            IncludeView = xmlNode.GetChildAsBool("IncludeInterface");
            IncludeServer = xmlNode.GetChildAsBool("IncludeServer");
            IncludeComm = xmlNode.GetChildAsBool("IncludeComm");
            IncludeWeb = xmlNode.GetChildAsBool("IncludeWeb");
            IgnoreRegKeys = xmlNode.GetChildAsBool("IgnoreRegKeys");
        }

        /// <summary>
        /// Saves the settings into the XML node.
        /// </summary>
        public virtual void SaveToXml(XmlElement xmlElem)
        {
            if (xmlElem == null)
                throw new ArgumentNullException(nameof(xmlElem));

            xmlElem.AppendElem("IncludeBase", IncludeBase);
            xmlElem.AppendElem("IncludeInterface", IncludeView);
            xmlElem.AppendElem("IncludeServer", IncludeServer);
            xmlElem.AppendElem("IncludeComm", IncludeComm);
            xmlElem.AppendElem("IncludeWeb", IncludeWeb);
            xmlElem.AppendElem("IgnoreRegKeys", IgnoreRegKeys);
        }

        /// <summary>
        /// Convert this transfer options to Agent transfer options.
        /// </summary>
        public ConfigTransferOptions ToConfigTransferOpions()
        {
            ConfigTransferOptions options = new();

            if (IncludeBase)
                options.ConfigParts |= ConfigParts.Base;
            if (IncludeView)
                options.ConfigParts |= ConfigParts.View;
            if (IncludeServer)
                options.ConfigParts |= ConfigParts.Server;
            if (IncludeComm)
                options.ConfigParts |= ConfigParts.Comm;
            if (IncludeWeb)
                options.ConfigParts |= ConfigParts.Web;

            if (IgnoreRegKeys)
            {
                options.IgnoredPaths.Add(new RelativePath(TopFolder.Server, AppFolder.Config, "*_Reg.xml"));
                options.IgnoredPaths.Add(new RelativePath(TopFolder.Comm, AppFolder.Config, "*_Reg.xml"));
                options.IgnoredPaths.Add(new RelativePath(TopFolder.Web, AppFolder.Config, "*_Reg.xml"));
            }

            return options;
        }
    }
}