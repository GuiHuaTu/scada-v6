// Copyright (c) Rapid Software LLC. All rights reserved.

using Opc;
using OpcCom;
using Scada.Comm.Drivers.DrvOpcClassic.Config;
using Scada.Forms;

namespace Scada.Comm.Drivers.DrvOpcClassic.View.Forms
{
    /// <summary>
    /// Represents a form for selecting an OPC server.
    /// <para>Представляет форму для выбора OPC-сервера.</para>
    /// </summary>
    public partial class FrmServerSelect : Form
    {
        /// <summary>
        /// Represents an item in the OPC server list.
        /// </summary>
        private class ServerItem : IDisposable
        {
            public ServerItem()
            {
                IsEmpty = true;
                Name = "";
                Path = "";
            }
            public ServerItem(Opc.Server server)
            {
                ArgumentNullException.ThrowIfNull(server, nameof(server));
                IsEmpty = false;
                Name = server.Name;
                Path = server.Url.Path;
            }

            public bool IsEmpty { get; }
            public string Name { get; }
            public string Path { get; }
            public Opc.Da.Server Da10Server { get; set; } = null;
            public Opc.Da.Server Da20Server { get; set; } = null;
            public Opc.Da.Server Da30Server { get; set; } = null;
            public Opc.Ae.Server Ae10Server { get; set; } = null;

            public void Dispose()
            {
                Da10Server?.Dispose();
                Da20Server?.Dispose();
                Da30Server?.Dispose();
                Ae10Server?.Dispose();
            }
            public override string ToString() => Name;
        }


        private readonly Dictionary<string, ServerItem> serverItems; // the server items accessed by path


        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        public FrmServerSelect()
        {
            InitializeComponent();

            serverItems = new Dictionary<string, ServerItem>();
            ConnectionOptions = null;
        }


        /// <summary>
        /// Gets or sets the connection options.
        /// </summary>
        internal OpcConnectionOptions ConnectionOptions { get; set; }


        /// <summary>
        /// Fills the server list.
        /// </summary>
        private bool FillServers()
        {
            try
            {
                string host = string.IsNullOrEmpty(ConnectionOptions.Host) ? null : ConnectionOptions.Host;
                ConnectData connectData = ConnectionOptions.GetConnectData();
                ServerEnumerator enumerator = new();
                Opc.Server[] da10Servers = enumerator.GetAvailableServers(Specification.COM_DA_10, host, connectData);
                Opc.Server[] da20Servers = enumerator.GetAvailableServers(Specification.COM_DA_20, host, connectData);
                Opc.Server[] da30Servers = enumerator.GetAvailableServers(Specification.COM_DA_30, host, connectData);
                Opc.Server[] ae10Servers = enumerator.GetAvailableServers(Specification.COM_AE_10, host, connectData);

                foreach (Opc.Server server in da10Servers)
                {
                    if (server is Opc.Da.Server daServer)
                    {
                        ServerItem serverItem = GetOrCreateServerItem(server);
                        serverItem.Da10Server = daServer;
                    }
                }

                foreach (Opc.Server server in da20Servers)
                {
                    if (server is Opc.Da.Server daServer)
                    {
                        ServerItem serverItem = GetOrCreateServerItem(server);
                        serverItem.Da20Server = daServer;
                    }
                }

                foreach (Opc.Server server in da30Servers)
                {
                    if (server is Opc.Da.Server daServer)
                    {
                        ServerItem serverItem = GetOrCreateServerItem(server);
                        serverItem.Da30Server = daServer;
                    }
                }

                foreach (Opc.Server server in ae10Servers)
                {
                    if (server is Opc.Ae.Server aeServer)
                    {
                        ServerItem serverItem = GetOrCreateServerItem(server);
                        serverItem.Ae10Server = aeServer;
                    }
                }

                try
                {
                    cbServer.BeginUpdate();
                    cbServer.Items.Clear();
                    cbServer.Items.Add(new ServerItem()); // add empty item
                    ServerItem selectedItem = null;

                    foreach (ServerItem serverItem in serverItems.Values)
                    {
                        if (ConnectionOptions.ServerPath == serverItem.Path)
                            selectedItem = serverItem;

                        cbServer.Items.Add(serverItem);
                    }

                    if (selectedItem == null)
                    {
                        cbServer.SelectedIndex = 0;
                    }
                    else
                    {
                        cbServer.SelectedItem = selectedItem;

                        if (ConnectionOptions.DaSpec == Spec.DA30)
                            rbDa30.Checked = true;
                        else if (ConnectionOptions.DaSpec == Spec.DA20)
                            rbDa20.Checked = true;
                        else if (ConnectionOptions.DaSpec == Spec.DA10)
                            rbDa10.Checked = true;
                        else
                            rbDaNone.Checked = true;

                        if (ConnectionOptions.AeSpec == Spec.AE10)
                            rbAe10.Checked = true;
                        else
                            rbAeNone.Checked = true;
                    }
                }
                finally
                {
                    cbServer.EndUpdate();
                }

                return true;
            }
            catch (Exception ex)
            {
                ScadaUiUtils.ShowError(ex.BuildErrorMessage(DriverPhrases.FillServersError));
                return false;
            }
        }

        /// <summary>
        /// Gets the existing or creates a new server item.
        /// </summary>
        private ServerItem GetOrCreateServerItem(Opc.Server server)
        {
            if (!serverItems.TryGetValue(server.Url.Path, out ServerItem serverItem))
            {
                serverItem = new ServerItem(server);
                serverItems.Add(serverItem.Path, serverItem);
            }

            return serverItem;
        }

        /// <summary>
        /// Dispose the servers and clears the server list.
        /// </summary>
        private void DisposeServers()
        {
            try
            {
                foreach (ServerItem serverItem in serverItems.Values)
                {
                    serverItem.Dispose();
                }

                serverItems.Clear();
                cbServer.Items.Clear();
            }
            catch (Exception ex)
            {
                ScadaUiUtils.ShowError(ex.BuildErrorMessage(DriverPhrases.DisposeServersError));
            }
        }

        /// <summary>
        /// Gets the selected DA specification.
        /// </summary>
        private Spec GetDaSpec()
        {
            if (rbDa10.Checked)
                return Spec.DA10;
            else if (rbDa20.Checked)
                return Spec.DA20;
            else if (rbDa30.Checked)
                return Spec.DA30;
            else
                return Spec.None;
        }

        /// <summary>
        /// Gets the selected AE specification.
        /// </summary>
        private Spec GetAeSpec()
        {
            return rbAe10.Checked ? Spec.AE10 : Spec.None;
        }


        private void FrmServerSelect_Load(object sender, EventArgs e)
        {
            FormTranslator.Translate(this, GetType().FullName);
            ActiveControl = cbServer;

            if (ConnectionOptions == null)
            {
                btnOK.Enabled = false;
            }
            else
            {
                txtHost.Text = ConnectionOptions.Host;
                btnOK.Enabled = FillServers();
            }
        }

        private void FrmServerSelect_FormClosed(object sender, FormClosedEventArgs e)
        {
            DisposeServers();
        }

        private void cbServer_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbServer.SelectedItem is ServerItem serverItem)
            {
                if (serverItem.IsEmpty)
                {
                    gbDaSpec.Enabled = false;
                    rbDaNone.Checked = true;

                    gbAeSpec.Enabled = false;
                    rbAeNone.Checked = true;
                }
                else
                {
                    // DA specification
                    gbDaSpec.Enabled = true;
                    rbDa10.Enabled = serverItem.Da10Server != null;
                    rbDa20.Enabled = serverItem.Da20Server != null;
                    rbDa30.Enabled = serverItem.Da30Server != null;

                    if (serverItem.Da30Server != null)
                        rbDa30.Checked = true;
                    else if (serverItem.Da20Server != null)
                        rbDa20.Checked = true;
                    else if (serverItem.Da10Server != null)
                        rbDa10.Checked = true;
                    else
                        rbDaNone.Checked = true;

                    // AE specification
                    gbAeSpec.Enabled = true;
                    rbAe10.Enabled = serverItem.Ae10Server != null;

                    if (serverItem.Ae10Server == null)
                        rbAeNone.Checked = true;
                    else
                        rbAe10.Checked = true;
                }
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (cbServer.SelectedItem is ServerItem serverItem)
            {
                if (serverItem.IsEmpty)
                {
                    ConnectionOptions.ServerPath = "";
                    ConnectionOptions.DaSpec = Spec.None;
                    ConnectionOptions.AeSpec = Spec.None;
                }
                else
                {
                    ConnectionOptions.ServerPath = serverItem.Path;
                    ConnectionOptions.DaSpec = GetDaSpec();
                    ConnectionOptions.AeSpec = GetAeSpec();
                }
            }

            DialogResult = DialogResult.OK;
        }
    }
}
