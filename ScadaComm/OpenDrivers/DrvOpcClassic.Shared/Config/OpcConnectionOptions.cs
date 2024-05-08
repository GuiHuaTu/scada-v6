// Copyright (c) Rapid Software LLC. All rights reserved.

using Opc;
using System.Net;
using System.Xml;

namespace Scada.Comm.Drivers.DrvOpcClassic.Config
{
    /// <summary>
    /// Represents options for connecting to an OPC server.
    /// <para>Представляет параметры подключения к OPC-серверу.</para>
    /// </summary>
    internal class OpcConnectionOptions
    {
        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        public OpcConnectionOptions()
        {
            Host = "";
            ServerPath = "";
            DaSpec = Spec.DA30;
            AeSpec = Spec.None;
            Username = "";
            Password = "";
            Domain = "";
            ProxyAddress = "";
        }


        /// <summary>
        /// Gets or sets the host.
        /// </summary>
        public string Host { get; set; }

        /// <summary>
        /// Gets or sets the server path.
        /// </summary>
        /// <remarks>
        /// Path is a part of server URL. 
        /// Server URL examples:
        /// opcda://localhost/path
        /// opcae://localhost/path
        /// </remarks>
        public string ServerPath { get; set; }

        /// <summary>
        /// Gets or sets the specification of the OPC DA server.
        /// </summary>
        public Spec DaSpec { get; set; }

        /// <summary>
        /// Gets or sets the specification of the OPC AE server.
        /// </summary>
        public Spec AeSpec { get; set; }

        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the domain.
        /// </summary>
        public string Domain { get; set; }

        /// <summary>
        /// Gets or sets the web proxy address.
        /// </summary>
        public string ProxyAddress { get; set; }

        /// <summary>
        /// Gets a value indicating whether the network credentials are not set.
        /// </summary>
        public bool NetworkIsDefault
        {
            get
            {
                return
                    string.IsNullOrEmpty(Username) &&
                    string.IsNullOrEmpty(Password) &&
                    string.IsNullOrEmpty(Domain);
            }
        }

        /// <summary>
        /// Gets a value indicating whether the proxy is not set.
        /// </summary>
        public bool ProxyIsDefault
        {
            get
            {
                return string.IsNullOrEmpty(ProxyAddress);
            }
        }


        /// <summary>
        /// Loads the options from the XML node.
        /// </summary>
        public void LoadFromXml(XmlNode xmlNode)
        {
            ArgumentNullException.ThrowIfNull(xmlNode, nameof(xmlNode));
            Host = xmlNode.GetChildAsString("Host");
            ServerPath = xmlNode.GetChildAsString("ServerPath");
            DaSpec = xmlNode.GetChildAsEnum("DaSpec", DaSpec);
            AeSpec = xmlNode.GetChildAsEnum("AeSpec", AeSpec);
            Username = xmlNode.GetChildAsString("Username");
            Password = ScadaUtils.Decrypt(xmlNode.GetChildAsString("Password"));
            Domain = xmlNode.GetChildAsString("Domain");
            ProxyAddress = xmlNode.GetChildAsString("ProxyAddress");
        }

        /// <summary>
        /// Saves the options into the XML node.
        /// </summary>
        public void SaveToXml(XmlElement xmlElem)
        {
            ArgumentNullException.ThrowIfNull(xmlElem, nameof(xmlElem));
            xmlElem.AppendElem("Host", Host);
            xmlElem.AppendElem("ServerPath", ServerPath);
            xmlElem.AppendElem("DaSpec", DaSpec);
            xmlElem.AppendElem("AeSpec", AeSpec);
            xmlElem.AppendElem("Username", Username);
            xmlElem.AppendElem("Password", ScadaUtils.Encrypt(Password));
            xmlElem.AppendElem("Domain", Domain);
            xmlElem.AppendElem("ProxyAddress", ProxyAddress);
        }


        /// <summary>
        /// Gets the OPC DA specification.
        /// </summary>
        public Specification GetDaSpecification()
        {
            return DaSpec switch
            {
                Spec.DA10 => Specification.COM_DA_10,
                Spec.DA20 => Specification.COM_DA_20,
                Spec.DA30 => Specification.COM_DA_30,
                _ => throw new ScadaException("Invalid DA specification.")
            };
        }

        /// <summary>
        /// Gets the OPC AE specification.
        /// </summary>
        public Specification GetAeSpecification()
        {
            return AeSpec switch
            {
                Spec.AE10 => Specification.COM_AE_10,
                _ => throw new ScadaException("Invalid AE specification.")
            };
        }

        /// <summary>
        /// Gets the connection arguments.
        /// </summary>
        public ConnectData GetConnectData()
        {
            NetworkCredential credentials = NetworkIsDefault ? null : new NetworkCredential(Username, Password, Domain);
            WebProxy proxy = ProxyIsDefault ? null : new WebProxy(ProxyAddress);
            return credentials == null && proxy == null ? null : new ConnectData(credentials, proxy);
        }
    }
}
