// Copyright (c) Rapid Software LLC. All rights reserved.

using Scada.Comm.Drivers.DrvOpcClassic.Config;
using Scada.Forms;

namespace Scada.Comm.Drivers.DrvOpcClassic.View.Forms
{
    /// <summary>
    /// Represents a form for editing network options.
    /// <para>Представляет собой форму для редактирования сетевых параметров.</para>
    /// </summary>
    public partial class FrmNetworkOptions : Form
    {
        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        public FrmNetworkOptions()
        {
            InitializeComponent();
            ConnectionOptions = null;
        }


        /// <summary>
        /// Gets or sets the connection options.
        /// </summary>
        internal OpcConnectionOptions ConnectionOptions { get; set; }


        /// <summary>
        /// Sets the controls according to the options.
        /// </summary>
        private void OptionsToControls()
        {
            if (ConnectionOptions != null)
            {
                chkIsDefault.Checked = ConnectionOptions.NetworkIsDefault && ConnectionOptions.ProxyIsDefault;
                txtUsername.Text = ConnectionOptions.Username;
                txtPassword.Text = ConnectionOptions.Password;
                txtDomain.Text = ConnectionOptions.Domain;
                txtProxyAddress.Text = ConnectionOptions.ProxyAddress;
            }
        }

        /// <summary>
        /// Sets the options according to the controls.
        /// </summary>
        private void ControlsToOptions()
        {
            if (ConnectionOptions != null)
            {
                if (chkIsDefault.Checked)
                {
                    ConnectionOptions.Username = "";
                    ConnectionOptions.Password = "";
                    ConnectionOptions.Domain = "";
                    ConnectionOptions.ProxyAddress = "";
                }
                else
                {
                    ConnectionOptions.Username = txtUsername.Text;
                    ConnectionOptions.Password = txtPassword.Text;
                    ConnectionOptions.Domain = txtDomain.Text;
                    ConnectionOptions.ProxyAddress = txtProxyAddress.Text;
                }
            }
        }


        private void FrmConnectData_Load(object sender, EventArgs e)
        {
            FormTranslator.Translate(this, GetType().FullName);
            OptionsToControls();
            btnOK.Enabled = ConnectionOptions != null;
        }

        private void chkIsDefault_CheckedChanged(object sender, EventArgs e)
        {
            gbCredentials.Enabled = gbProxy.Enabled = !chkIsDefault.Checked;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            ControlsToOptions();
            DialogResult = DialogResult.OK;
        }
    }
}
