// Copyright (c) Rapid Software LLC. All rights reserved.

using Opc;
using OpcCom;
using Scada.Comm.Devices;
using Scada.Comm.Drivers.DrvOpcClassic.Config;
using Scada.Lang;
using Scada.Log;

namespace Scada.Comm.Drivers.DrvOpcClassic.Logic
{
    /// <summary>
    /// Provides helper methods for using OPC client.
    /// <para>Предоставляет вспомогательные методы для клиента OPC.</para>
    /// </summary>
    internal class OpcClientHelper
    {
        /// <summary>
        /// The delay before reconnect.
        /// </summary>
        private static readonly TimeSpan ReconnectDelay = TimeSpan.FromSeconds(5);

        private readonly OpcConnectionOptions connectionOptions;     // the connection options
        private readonly ILog log;                                   // implements logging
        private readonly List<SubscriptionTag> subscrTags;           // metadata about subscriptions
        private readonly List<EventSubscriptionTag> eventSubscrTags; // metadata about event subscriptions

        private DateTime connAttemptDT; // the timestamp of a connection attempt
        private Opc.Da.Server daServer; // communicates an OPC server using the DA specification
        private Opc.Ae.Server aeServer; // communicates an OPC server using the AE specification


        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        public OpcClientHelper(OpcConnectionOptions connectionOptions, ILog log)
        {
            this.connectionOptions = connectionOptions ?? throw new ArgumentNullException(nameof(connectionOptions));
            this.log = log ?? throw new ArgumentNullException(nameof(log));
            subscrTags = new List<SubscriptionTag>();
            eventSubscrTags = new List<EventSubscriptionTag>();

            connAttemptDT = DateTime.MinValue;
            daServer = null;
            aeServer = null;
        }


        /// <summary>
        /// Gets a value indicating whether an OPC DA connection is required.
        /// </summary>
        private bool DaRequired => connectionOptions.DaSpec != Spec.None && subscrTags.Count > 0;

        /// <summary>
        /// Gets a value indicating whether an OPC AE connection is required.
        /// </summary>
        private bool AeRequired => connectionOptions.AeSpec != Spec.None && eventSubscrTags.Count > 0;


        /// <summary>
        /// Gets a value indicating whether a connection the OPC server is established.
        /// </summary>
        public bool IsConnected => (daServer != null || !DaRequired) && (aeServer != null || !AeRequired);

        /// <summary>
        /// Gets the OPC DA server.
        /// </summary>
        public Opc.Da.Server DaSever => daServer;


        /// <summary>
        /// Performs a delay before connecting.
        /// </summary>
        private void DelayConnection()
        {
            DateTime utcNow = DateTime.UtcNow;
            TimeSpan connectDelay = ReconnectDelay - (utcNow - connAttemptDT);

            if (connectDelay > TimeSpan.Zero)
            {
                log.WriteAction(Locale.IsRussian ?
                    "Задержка перед соединением {0} с" :
                    "Delay before connecting {0} sec",
                    connectDelay.TotalSeconds.ToString("N1"));
                Thread.Sleep(connectDelay);
            }

            connAttemptDT = DateTime.UtcNow;
        }

        /// <summary>
        /// Connects the OPC DA server.
        /// </summary>
        private void ConnectToDaServer(ServerEnumerator enumerator, string host, ConnectData connectData)
        {
            Opc.Server[] daServers = enumerator.GetAvailableServers(
                connectionOptions.GetDaSpecification(), host, connectData);

            foreach (Opc.Server server in daServers)
            {
                if (connectionOptions.ServerPath == server.Url.Path)
                {
                    daServer = server as Opc.Da.Server;
                    break;
                }
            }

            if (daServer == null)
            {
                log.WriteLine(Locale.IsRussian ?
                    "OPC DA сервер недоступен или не поддерживает выбранную спецификацию." :
                    "OPC DA server is unavailable or does not support the selected specification.");
            }
            else
            {
                try
                { 
                    daServer.Connect(); 
                }
                catch 
                { 
                    daServer.SafeDispose();
                    daServer = null;
                    throw;
                }

                daServer.ServerShutdown += DaServer_ServerShutdown;
                log.WriteLine(Locale.IsRussian ?
                    "Соединение с OPC DA сервером установлено" :
                    "Connection to the OPC DA server has been established");
            }
        }

        /// <summary>
        /// Connects the OPC AE server.
        /// </summary>
        private void ConnectToAeServer(ServerEnumerator enumerator, string host, ConnectData connectData)
        {
            Opc.Server[] aeServers = enumerator.GetAvailableServers(
                connectionOptions.GetAeSpecification(), host, connectData);

            foreach (Opc.Server server in aeServers)
            {
                if (connectionOptions.ServerPath == server.Url.Path)
                {
                    aeServer = server as Opc.Ae.Server;
                    break;
                }
            }

            if (aeServer == null)
            {
                log.WriteLine(Locale.IsRussian ?
                    "OPC AE сервер недоступен или не поддерживает выбранную спецификацию." :
                    "OPC AE server is unavailable or does not support the selected specification.");
            }
            else
            {
                try
                { 
                    aeServer.Connect(); 
                }
                catch 
                { 
                    aeServer.SafeDispose();
                    aeServer = null;
                    throw;
                }

                aeServer.ServerShutdown += AeServer_ServerShutdown;
                log.WriteLine(Locale.IsRussian ?
                    "Соединение с OPC AE сервером установлено" :
                    "Connection to the OPC AE server has been established");
            }
        }

        /// <summary>
        /// Create subscriptions to the OPC DA server.
        /// </summary>
        private void CreateDaSubscriptions()
        {
            Opc.Da.Server daServerRef = daServer ?? // copy reference
                throw new InvalidOperationException(Locale.IsRussian ?
                    "Сервер OPC DA не определён." :
                    "OPC DA server is undefined.");

            foreach (SubscriptionTag subscriptionTag in subscrTags)
            {
                SubscriptionConfig subscriptionConfig = subscriptionTag.SubscriptionConfig;
                log.WriteLine(Locale.IsRussian ?
                    "Создание подписки \"{0}\" для устройства {1}{2}" :
                    "Create subscription \"{0}\" for the device {1}{2}",
                    subscriptionConfig.DisplayName, subscriptionTag.DeviceLogic.Title,
                    subscriptionConfig.ReadSync ? " (sync)" : " (async)");

                // create subscription items
                List<Opc.Da.Item> items = new();

                foreach (ItemConfig itemConfig in subscriptionConfig.Items)
                {
                    if (itemConfig.Active)
                    {
                        items.Add(new Opc.Da.Item(new ItemIdentifier(itemConfig.Path, itemConfig.Name))
                        {
                            ClientHandle = new ItemTag
                            {
                                DeviceTag = itemConfig.Tag as DeviceTag,
                                ItemConfig = itemConfig
                            }
                        });
                    }
                }

                subscriptionTag.SubscriptionItems = items.ToArray();

                // create subscription
                if (!subscriptionConfig.ReadSync)
                {
                    Opc.Da.SubscriptionState subscriptionState = new()
                    {
                        Name = subscriptionConfig.DisplayName,
                        ClientHandle = subscriptionTag,
                        UpdateRate = subscriptionConfig.UpdateRate,
                        KeepAlive = subscriptionConfig.KeepAlive,
                        Deadband = (float)subscriptionConfig.Deadband,
                    };

                    Opc.Da.Subscription subscription = (Opc.Da.Subscription)daServerRef.CreateSubscription(subscriptionState);
                    subscription.DataChanged += Subscription_DataChanged;

                    if (subscriptionTag.SubscriptionItems.Length > 0)
                        subscription.AddItems(subscriptionTag.SubscriptionItems);
                }
            }
        }

        /// <summary>
        /// Create subscriptions to the OPC AE server.
        /// </summary>
        private void CreateAeSubscriptions()
        {
            Opc.Ae.Server aeServerRef = aeServer ?? // copy reference
                throw new InvalidOperationException(Locale.IsRussian ?
                    "Сервер OPC AE не определён." :
                    "OPC AE server is undefined.");

            foreach (EventSubscriptionTag subscriptionTag in eventSubscrTags)
            {
                EventSubscriptionConfig subscriptionConfig = subscriptionTag.SubscriptionConfig;
                log.WriteLine(Locale.IsRussian ?
                    "Создание подписки на события \"{0}\" для устройства {1}" :
                    "Create event subscription \"{0}\" for the device {1}",
                    subscriptionConfig.DisplayName, subscriptionTag.DeviceLogic.Title);

                Opc.Ae.SubscriptionState subscriptionState = new()
                {
                    Name = subscriptionConfig.DisplayName,
                    ClientHandle = subscriptionTag,
                    BufferTime = subscriptionConfig.UpdateRate,
                    KeepAlive = subscriptionConfig.KeepAlive,
                    MaxSize = subscriptionConfig.MaxSize,
                };

                Opc.Ae.SubscriptionFilters filters = new()
                {
                    EventTypes = 0
                };

                if (subscriptionConfig.SimpleEvents)
                    filters.EventTypes |= (int)Opc.Ae.EventType.Simple;
                if (subscriptionConfig.TrackingEvents)
                    filters.EventTypes |= (int)Opc.Ae.EventType.Tracking;
                if (subscriptionConfig.ConditionEvents)
                    filters.EventTypes |= (int)Opc.Ae.EventType.Condition;

                filters.HighSeverity = subscriptionConfig.HighSeverity;
                filters.LowSeverity = subscriptionConfig.LowSeverity;
                filters.Categories.AddRange(subscriptionConfig.Categories.Select(c => c.ID).Distinct().ToArray());

                Opc.Ae.Subscription subscription = (Opc.Ae.Subscription)aeServerRef.CreateSubscription(subscriptionState);
                subscription.SetFilters(filters);
                subscription.EventChanged += Subscription_EventChanged;
            }
        }

        /// <summary>
        /// Handles a shutdown event of the DA server.
        /// </summary>
        private void DaServer_ServerShutdown(string reason)
        {
            log.WriteLine();
            log.WriteAction(Locale.IsRussian ?
                "OPC DA сервер отключен. Причина: {0}" :
                "OPC DA server is shutdown. Reason: {0}",
                string.IsNullOrEmpty(reason) ? "-" : reason);
            daServer.SafeDispose();
            daServer = null;
        }

        /// <summary>
        /// Handles a shutdown event of the AE server.
        /// </summary>
        private void AeServer_ServerShutdown(string reason)
        {
            log.WriteLine();
            log.WriteAction(Locale.IsRussian ?
                "OPC AE сервер отключен. Причина: {0}" :
                "OPC AE server is shutdown. Reason: {0}",
                string.IsNullOrEmpty(reason) ? "-" : reason);
            aeServer.SafeDispose();
            aeServer = null;
        }

        /// <summary>
        /// Handles new data received from the DA server.
        /// </summary>
        private void Subscription_DataChanged(object subscriptionHandle, object requestHandle, 
            Opc.Da.ItemValueResult[] values)
        {
            if (subscriptionHandle is SubscriptionTag subscriptionTag)
            {
                subscriptionTag.DeviceLogic.ProcessDataChanges(subscriptionTag, values);
            }
            else
            {
                log.WriteError(Locale.IsRussian ?
                    "Получены данные по неизвестной подписке" :
                    "Received data for unknown subscription");
            }
        }

        /// <summary>
        /// Handles new event received from the AE server.
        /// </summary>
        private void Subscription_EventChanged(Opc.Ae.EventNotification[] notifications, bool refresh, bool lastRefresh)
        {
            if (notifications != null)
            {
                foreach (Opc.Ae.EventNotification notification in notifications)
                {
                    if (notification.ClientHandle is EventSubscriptionTag subscriptionTag)
                    {
                        subscriptionTag.DeviceLogic.ProcessEvent(subscriptionTag, notification);
                    }
                    else
                    {
                        log.WriteError(Locale.IsRussian ?
                            "Получено событие по неизвестной подписке" :
                            "Received event for unknown subscription");
                    }
                }
            }
        }


        /// <summary>
        /// Connects to the OPC server.
        /// </summary>
        public bool Connect()
        {
            try
            {
                log.WriteLine();
                DelayConnection();

                if (string.IsNullOrEmpty(connectionOptions.ServerPath))
                {
                    throw new ScadaException(Locale.IsRussian ?
                        "Путь к серверу не определён." : 
                        "Server path is undefined.");
                }

                log.WriteAction(Locale.IsRussian ?
                    "Соединение с {0}" :
                    "Connect to {0}",
                    connectionOptions.ServerPath);

                ServerEnumerator enumerator = new();
                string host = string.IsNullOrEmpty(connectionOptions.Host) ? null : connectionOptions.Host;
                ConnectData connectData = connectionOptions.GetConnectData();

                if (daServer == null && DaRequired)
                    ConnectToDaServer(enumerator, host, connectData);

                if (aeServer == null && AeRequired)
                    ConnectToAeServer(enumerator, host, connectData);

                return true;
            }
            catch (Exception ex)
            {
                log.WriteError(ex.BuildErrorMessage(Locale.IsRussian ?
                    "Ошибка при соединении с OPC-сервером" :
                    "Error connecting OPC server"));
                return false;
            }
        }

        /// <summary>
        /// Disconnects from the OPC server.
        /// </summary>
        public void Disconnect()
        {
            if (daServer != null || aeServer != null)
                log.WriteLine();

            if (daServer != null)
            {
                try 
                {
                    if (daServer.IsConnected)
                    {
                        log.WriteAction(Locale.IsRussian ?
                            "Отключение от OPC DA сервера" :
                            "Disconnect from OPC DA server");
                        daServer.Disconnect();
                    }
                }
                catch (Exception ex)
                {
                    log.WriteError(ex.BuildErrorMessage(Locale.IsRussian ?
                        "Ошибка при отключении от OPC DA сервера" :
                        "Error disconnecting OPC DA server"));
                }

                daServer.SafeDispose();
                daServer = null;
            }

            if (aeServer != null)
            {
                try 
                {
                    if (aeServer.IsConnected)
                    {
                        log.WriteAction(Locale.IsRussian ?
                            "Отключение от OPC AE сервера" :
                            "Disconnect from OPC AE server");
                        aeServer.Disconnect();
                    }
                }
                catch (Exception ex)
                {
                    log.WriteError(ex.BuildErrorMessage(Locale.IsRussian ?
                        "Ошибка при отключении от OPC AE сервера" :
                        "Error disconnecting OPC AE server"));
                }

                aeServer.SafeDispose();
                aeServer = null;
            }
        }

        /// <summary>
        /// Adds DA subscribtions according to the device configuration.
        /// </summary>
        public List<SubscriptionTag> AddDaSubscriptions(DevOpcClassicLogic deviceLogic, OpcDeviceConfig deviceConfig)
        {
            List<SubscriptionTag> addedTags = new();

            foreach (SubscriptionConfig subscriptionConfig in deviceConfig.Subscriptions)
            {
                if (subscriptionConfig.Active)
                {
                    SubscriptionTag subscriptionTag = new()
                    {
                        DeviceLogic = deviceLogic,
                        SubscriptionConfig = subscriptionConfig
                    };

                    addedTags.Add(subscriptionTag);
                    subscrTags.Add(subscriptionTag);
                }
            }

            return addedTags;
        }

        /// <summary>
        /// Adds AE subscribtions according to the device configuration.
        /// </summary>
        public void AddAeSubscriptions(DevOpcClassicLogic deviceLogic, OpcDeviceConfig deviceConfig)
        {
            foreach (EventSubscriptionConfig subscriptionConfig in deviceConfig.EventSubscriptions)
            {
                if (subscriptionConfig.Active)
                {
                    eventSubscrTags.Add(new EventSubscriptionTag
                    {
                        DeviceLogic = deviceLogic,
                        SubscriptionConfig = subscriptionConfig
                    });
                }
            }
        }

        /// <summary>
        /// Creates the previously added subscriptions on the OPC server.
        /// </summary>
        public bool CreateSubscriptions()
        {
            try
            {
                log.WriteLine();
                log.WriteAction(Locale.IsRussian ?
                    "Создание подписок" :
                    "Create subscriptions");

                if (DaRequired)
                    CreateDaSubscriptions();

                if (AeRequired)
                    CreateAeSubscriptions();

                return true;
            }
            catch (Exception ex)
            {
                log.WriteLine(ex.BuildErrorMessage(Locale.IsRussian ?
                    "Ошибка при создании подписок" :
                    "Error creating subscriptions"));
                return false;
            }
        }
    }
}
