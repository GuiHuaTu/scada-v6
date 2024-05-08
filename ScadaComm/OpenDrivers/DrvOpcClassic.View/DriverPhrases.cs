// Copyright (c) Rapid Software LLC. All rights reserved.

using Scada.Lang;

namespace Scada.Comm.Drivers.DrvOpcClassic.View
{
    /// <summary>
    /// The phrases used by the driver.
    /// <para>Фразы, используемые драйвером.</para>
    /// </summary>
    internal static class DriverPhrases
    {
        // Scada.Comm.Drivers.DrvOpcClassic.View.Forms.FrmDeviceConfig
        public static string XmlFileFilter { get; private set; }
        public static string ServerNotSelected { get; private set; }
        public static string DaServerUnavailable { get; private set; }
        public static string AeServerUnavailable { get; private set; }
        public static string ConnectDaServerError { get; private set; }
        public static string ConnectAeServerError { get; private set; }
        public static string DisconnectDaServerError { get; private set; }
        public static string DisconnectAeServerError { get; private set; }
        public static string BrowseServerError { get; private set; }
        public static string LoadServerContentError { get; private set; }
        public static string ServerNotConnected { get; private set; }
        public static string UnableToReadData { get; private set; }
        public static string GetDataTypeError { get; private set; }
        public static string DaServerNode { get; private set; }
        public static string AeServerNode { get; private set; }
        public static string EmptyNode { get; private set; }
        public static string SubscriptionsNode { get; private set; }
        public static string CommandsNode { get; private set; }
        public static string EventSubscriptionsNode { get; private set; }
        public static string UnnamedSubscription { get; private set; }
        public static string UnnamedItem { get; private set; }
        public static string UnnamedCommand { get; private set; }

        // Scada.Comm.Drivers.DrvOpcClassic.View.Forms.FrmServerSelect
        public static string FillServersError { get; private set; }
        public static string DisposeServersError { get; private set; }

        public static void Init()
        {
            LocaleDict dict = Locale.GetDictionary("Scada.Comm.Drivers.DrvOpcClassic.View.Forms.FrmDeviceConfig");
            XmlFileFilter = dict[nameof(XmlFileFilter)];
            ServerNotSelected = dict[nameof(ServerNotSelected)];
            DaServerUnavailable = dict[nameof(DaServerUnavailable)];
            AeServerUnavailable = dict[nameof(AeServerUnavailable)];
            ConnectDaServerError = dict[nameof(ConnectDaServerError)];
            ConnectAeServerError = dict[nameof(ConnectAeServerError)];
            DisconnectDaServerError = dict[nameof(DisconnectDaServerError)];
            DisconnectAeServerError = dict[nameof(DisconnectAeServerError)];
            ServerNotConnected = dict[nameof(ServerNotConnected)];
            UnableToReadData = dict[nameof(UnableToReadData)];
            BrowseServerError = dict[nameof(BrowseServerError)];
            LoadServerContentError = dict[nameof(LoadServerContentError)];
            GetDataTypeError = dict[nameof(GetDataTypeError)];
            DaServerNode = dict[nameof(DaServerNode)];
            AeServerNode = dict[nameof(AeServerNode)];
            EmptyNode = dict[nameof(EmptyNode)];
            SubscriptionsNode = dict[nameof(SubscriptionsNode)];
            CommandsNode = dict[nameof(CommandsNode)];
            EventSubscriptionsNode = dict[nameof(EventSubscriptionsNode)];
            UnnamedSubscription = dict[nameof(UnnamedSubscription)];
            UnnamedItem = dict[nameof(UnnamedItem)];
            UnnamedCommand = dict[nameof(UnnamedCommand)];

            dict = Locale.GetDictionary("Scada.Comm.Drivers.DrvOpcClassic.View.Forms.FrmServerSelect");
            FillServersError = dict[nameof(FillServersError)];
            DisposeServersError = dict[nameof(DisposeServersError)];
        }
    }
}
