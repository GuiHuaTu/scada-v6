namespace Scada.Comm.Drivers.DrvOpcClassic.View.Forms
{
    partial class FrmNetworkOptions
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.chkIsDefault = new System.Windows.Forms.CheckBox();
            this.gbCredentials = new System.Windows.Forms.GroupBox();
            this.txtDomain = new System.Windows.Forms.TextBox();
            this.lblDomain = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.lblPassword = new System.Windows.Forms.Label();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.lblUsername = new System.Windows.Forms.Label();
            this.gbProxy = new System.Windows.Forms.GroupBox();
            this.txtProxyAddress = new System.Windows.Forms.TextBox();
            this.lblProxyAddress = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.gbCredentials.SuspendLayout();
            this.gbProxy.SuspendLayout();
            this.SuspendLayout();
            // 
            // chkIsDefault
            // 
            this.chkIsDefault.AutoSize = true;
            this.chkIsDefault.Location = new System.Drawing.Point(12, 12);
            this.chkIsDefault.Name = "chkIsDefault";
            this.chkIsDefault.Size = new System.Drawing.Size(128, 19);
            this.chkIsDefault.TabIndex = 0;
            this.chkIsDefault.Text = "Use default options";
            this.chkIsDefault.UseVisualStyleBackColor = true;
            this.chkIsDefault.CheckedChanged += new System.EventHandler(this.chkIsDefault_CheckedChanged);
            // 
            // gbCredentials
            // 
            this.gbCredentials.Controls.Add(this.txtDomain);
            this.gbCredentials.Controls.Add(this.lblDomain);
            this.gbCredentials.Controls.Add(this.txtPassword);
            this.gbCredentials.Controls.Add(this.lblPassword);
            this.gbCredentials.Controls.Add(this.txtUsername);
            this.gbCredentials.Controls.Add(this.lblUsername);
            this.gbCredentials.Location = new System.Drawing.Point(12, 37);
            this.gbCredentials.Name = "gbCredentials";
            this.gbCredentials.Padding = new System.Windows.Forms.Padding(10, 3, 10, 10);
            this.gbCredentials.Size = new System.Drawing.Size(360, 161);
            this.gbCredentials.TabIndex = 1;
            this.gbCredentials.TabStop = false;
            this.gbCredentials.Text = "Credentials";
            // 
            // txtDomain
            // 
            this.txtDomain.Location = new System.Drawing.Point(13, 125);
            this.txtDomain.Name = "txtDomain";
            this.txtDomain.Size = new System.Drawing.Size(334, 23);
            this.txtDomain.TabIndex = 5;
            // 
            // lblDomain
            // 
            this.lblDomain.AutoSize = true;
            this.lblDomain.Location = new System.Drawing.Point(10, 107);
            this.lblDomain.Name = "lblDomain";
            this.lblDomain.Size = new System.Drawing.Size(49, 15);
            this.lblDomain.TabIndex = 4;
            this.lblDomain.Text = "Domain";
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(13, 81);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(334, 23);
            this.txtPassword.TabIndex = 3;
            this.txtPassword.UseSystemPasswordChar = true;
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Location = new System.Drawing.Point(10, 63);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(57, 15);
            this.lblPassword.TabIndex = 2;
            this.lblPassword.Text = "Password";
            // 
            // txtUsername
            // 
            this.txtUsername.Location = new System.Drawing.Point(13, 37);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(334, 23);
            this.txtUsername.TabIndex = 1;
            // 
            // lblUsername
            // 
            this.lblUsername.AutoSize = true;
            this.lblUsername.Location = new System.Drawing.Point(10, 19);
            this.lblUsername.Name = "lblUsername";
            this.lblUsername.Size = new System.Drawing.Size(60, 15);
            this.lblUsername.TabIndex = 0;
            this.lblUsername.Text = "Username";
            // 
            // gbProxy
            // 
            this.gbProxy.Controls.Add(this.txtProxyAddress);
            this.gbProxy.Controls.Add(this.lblProxyAddress);
            this.gbProxy.Location = new System.Drawing.Point(12, 204);
            this.gbProxy.Name = "gbProxy";
            this.gbProxy.Padding = new System.Windows.Forms.Padding(10, 3, 10, 10);
            this.gbProxy.Size = new System.Drawing.Size(360, 73);
            this.gbProxy.TabIndex = 2;
            this.gbProxy.TabStop = false;
            this.gbProxy.Text = "Web Proxy";
            // 
            // txtProxyAddress
            // 
            this.txtProxyAddress.Location = new System.Drawing.Point(13, 37);
            this.txtProxyAddress.Name = "txtProxyAddress";
            this.txtProxyAddress.Size = new System.Drawing.Size(331, 23);
            this.txtProxyAddress.TabIndex = 1;
            // 
            // lblProxyAddress
            // 
            this.lblProxyAddress.AutoSize = true;
            this.lblProxyAddress.Location = new System.Drawing.Point(10, 19);
            this.lblProxyAddress.Name = "lblProxyAddress";
            this.lblProxyAddress.Size = new System.Drawing.Size(49, 15);
            this.lblProxyAddress.TabIndex = 0;
            this.lblProxyAddress.Text = "Address";
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(216, 293);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 3;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(297, 293);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // FrmNetworkOptions
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(384, 328);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.gbProxy);
            this.Controls.Add(this.gbCredentials);
            this.Controls.Add(this.chkIsDefault);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmNetworkOptions";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Network Options";
            this.Load += new System.EventHandler(this.FrmConnectData_Load);
            this.gbCredentials.ResumeLayout(false);
            this.gbCredentials.PerformLayout();
            this.gbProxy.ResumeLayout(false);
            this.gbProxy.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private CheckBox chkIsDefault;
        private GroupBox gbCredentials;
        private TextBox txtDomain;
        private Label lblDomain;
        private TextBox txtPassword;
        private Label lblPassword;
        private TextBox txtUsername;
        private Label lblUsername;
        private GroupBox gbProxy;
        private TextBox txtProxyAddress;
        private Label lblProxyAddress;
        private Button btnOK;
        private Button btnCancel;
    }
}