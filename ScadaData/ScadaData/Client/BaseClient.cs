﻿/*
 * Copyright 2020 Mikhail Shiryaev
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
 * Module   : ScadaData
 * Summary  : Represents the base class for TCP clients which interacts with a server
 * 
 * Author   : Mikhail Shiryaev
 * Created  : 2020
 * Modified : 2020
 */

using Scada.Protocol;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Scada.Client
{
    /// <summary>
    /// Represents the base class for TCP clients which interacts with a server.
    /// <para>Представляет базовый класс TCP-клиентов, которые взаимодействуют с сервером.</para>
    /// </summary>
    public abstract class BaseClient
    {
        /// <summary>
        /// The connection options.
        /// </summary>
        protected readonly ConnectionOptions connectionOptions;
        /// <summary>
        /// The input data buffer.
        /// </summary>
        protected readonly byte[] inBuf;
        /// <summary>
        /// The output data buffer.
        /// </summary>
        protected readonly byte[] outBuf;

        /// <summary>
        /// The client connection.
        /// </summary>
        protected TcpClient tcpClient;
        /// <summary>
        /// The stream for network access.
        /// </summary>
        protected NetworkStream netStream;


        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        public BaseClient(ConnectionOptions connectionOptions)
        {
            this.connectionOptions = connectionOptions ?? throw new ArgumentNullException("connectionOptions");
            inBuf = new byte[ProtocolUtils.BufferLenght];
            outBuf = new byte[ProtocolUtils.BufferLenght];

            tcpClient = null;
            netStream = null;

            SessionID = 0;
            ServerName = "";
        }

        
        /// <summary>
        /// Gets the session ID.
        /// </summary>
        public long SessionID { get; protected set; }

        /// <summary>
        /// Gets the server name and version.
        /// </summary>
        public string ServerName { get; protected set; }


        /// <summary>
        /// Connects and authenticates with the server.
        /// </summary>
        protected void Connect()
        {
            // create connection
            tcpClient = new TcpClient
            {
                SendTimeout = connectionOptions.Timeout,
                ReceiveTimeout = connectionOptions.Timeout
            };

            // connect
            if (IPAddress.TryParse(connectionOptions.Host, out IPAddress address))
                tcpClient.Connect(address, connectionOptions.Port);
            else
                tcpClient.Connect(connectionOptions.Host, connectionOptions.Port);

            netStream = tcpClient.GetStream();
        }
        
        /// <summary>
        /// Restores a connection with the server.
        /// </summary>
        protected void RestoreConnection()
        {

        }

        /// <summary>
        /// Gets the server and the session status.
        /// </summary>
        public void GetStatus(out bool serverIsReady, out bool userIsLoggedIn)
        {
            RestoreConnection();

            DataPacket request = new DataPacket
            {
                TransactionID = 0,
                DataLength = 10,
                SessionID = SessionID,
                FunctionID = FunctionID.GetStatus,
                Buffer = inBuf
            };

            netStream.Write(request.Buffer, 0, request.BufferLength);

            serverIsReady = true;
            userIsLoggedIn = true;
        }
    }
}
