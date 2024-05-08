namespace Scada.Comm.Drivers.DrvOpcClassic.View.Forms
{
    partial class FrmServerSelect
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
            this.lblHost = new System.Windows.Forms.Label();
            this.txtHost = new System.Windows.Forms.TextBox();
            this.lblServer = new System.Windows.Forms.Label();
            this.cbServer = new System.Windows.Forms.ComboBox();
            this.gbDaSpec = new System.Windows.Forms.GroupBox();
            this.rbDa30 = new System.Windows.Forms.RadioButton();
            this.rbDa20 = new System.Windows.Forms.RadioButton();
            this.rbDa10 = new System.Windows.Forms.RadioButton();
            this.rbDaNone = new System.Windows.Forms.RadioButton();
            this.gbAeSpec = new System.Windows.Forms.GroupBox();
            this.rbAe10 = new System.Windows.Forms.RadioButton();
            this.rbAeNone = new System.Windows.Forms.RadioButton();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.gbDaSpec.SuspendLayout();
            this.gbAeSpec.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblHost
            // 
            this.lblHost.AutoSize = true;
            this.lblHost.Location = new System.Drawing.Point(9, 9);
            this.lblHost.Name = "lblHost";
            this.lblHost.Size = new System.Drawing.Size(32, 15);
            this.lblHost.TabIndex = 0;
            this.lblHost.Text = "Host";
            // 
            // txtHost
            // 
            this.txtHost.Location = new System.Drawing.Point(12, 27);
            this.txtHost.Name = "txtHost";
            this.txtHost.ReadOnly = true;
            this.txtHost.Size = new System.Drawing.Size(360, 23);
            this.txtHost.TabIndex = 1;
            // 
            // lblServer
            // 
            this.lblServer.AutoSize = true;
            this.lblServer.Location = new System.Drawing.Point(9, 53);
            this.lblServer.Name = "lblServer";
            this.lblServer.Size = new System.Drawing.Size(65, 15);
            this.lblServer.TabIndex = 2;
            this.lblServer.Text = "OPC server";
            // 
            // cbServer
            // 
            this.cbServer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbServer.FormattingEnabled = true;
            this.cbServer.Location = new System.Drawing.Point(12, 71);
            this.cbServer.Name = "cbServer";
            this.cbServer.Size = new System.Drawing.Size(360, 23);
            this.cbServer.TabIndex = 3;
            this.cbServer.SelectedIndexChanged += new System.EventHandler(this.cbServer_SelectedIndexChanged);
            // 
            // gbDaSpec
            // 
            this.gbDaSpec.Controls.Add(this.rbDa30);
            this.gbDaSpec.Controls.Add(this.rbDa20);
            this.gbDaSpec.Controls.Add(this.rbDa10);
            this.gbDaSpec.Controls.Add(this.rbDaNone);
            this.gbDaSpec.Location = new System.Drawing.Point(12, 100);
            this.gbDaSpec.Name = "gbDaSpec";
            this.gbDaSpec.Padding = new System.Windows.Forms.Padding(10, 3, 10, 10);
            this.gbDaSpec.Size = new System.Drawing.Size(177, 129);
            this.gbDaSpec.TabIndex = 4;
            this.gbDaSpec.TabStop = false;
            this.gbDaSpec.Text = "DA Specification";
            // 
            // rbDa30
            // 
            this.rbDa30.AutoSize = true;
            this.rbDa30.Location = new System.Drawing.Point(13, 97);
            this.rbDa30.Name = "rbDa30";
            this.rbDa30.Size = new System.Drawing.Size(112, 19);
            this.rbDa30.TabIndex = 3;
            this.rbDa30.TabStop = true;
            this.rbDa30.Text = "Data Access 3.00";
            this.rbDa30.UseVisualStyleBackColor = true;
            // 
            // rbDa20
            // 
            this.rbDa20.AutoSize = true;
            this.rbDa20.Location = new System.Drawing.Point(13, 72);
            this.rbDa20.Name = "rbDa20";
            this.rbDa20.Size = new System.Drawing.Size(114, 19);
            this.rbDa20.TabIndex = 2;
            this.rbDa20.TabStop = true;
            this.rbDa20.Text = "Data Access 2.XX";
            this.rbDa20.UseVisualStyleBackColor = true;
            // 
            // rbDa10
            // 
            this.rbDa10.AutoSize = true;
            this.rbDa10.Location = new System.Drawing.Point(13, 47);
            this.rbDa10.Name = "rbDa10";
            this.rbDa10.Size = new System.Drawing.Size(112, 19);
            this.rbDa10.TabIndex = 1;
            this.rbDa10.TabStop = true;
            this.rbDa10.Text = "Data Access 1.0a";
            this.rbDa10.UseVisualStyleBackColor = true;
            // 
            // rbDaNone
            // 
            this.rbDaNone.AutoSize = true;
            this.rbDaNone.Location = new System.Drawing.Point(13, 22);
            this.rbDaNone.Name = "rbDaNone";
            this.rbDaNone.Size = new System.Drawing.Size(54, 19);
            this.rbDaNone.TabIndex = 0;
            this.rbDaNone.TabStop = true;
            this.rbDaNone.Text = "None";
            this.rbDaNone.UseVisualStyleBackColor = true;
            // 
            // gbAeSpec
            // 
            this.gbAeSpec.Controls.Add(this.rbAe10);
            this.gbAeSpec.Controls.Add(this.rbAeNone);
            this.gbAeSpec.Location = new System.Drawing.Point(195, 100);
            this.gbAeSpec.Name = "gbAeSpec";
            this.gbAeSpec.Padding = new System.Windows.Forms.Padding(10, 3, 10, 10);
            this.gbAeSpec.Size = new System.Drawing.Size(177, 129);
            this.gbAeSpec.TabIndex = 5;
            this.gbAeSpec.TabStop = false;
            this.gbAeSpec.Text = "AE Specification";
            // 
            // rbAe10
            // 
            this.rbAe10.AutoSize = true;
            this.rbAe10.Location = new System.Drawing.Point(13, 47);
            this.rbAe10.Name = "rbAe10";
            this.rbAe10.Size = new System.Drawing.Size(148, 19);
            this.rbAe10.TabIndex = 1;
            this.rbAe10.TabStop = true;
            this.rbAe10.Text = "Alarms and Events 1.XX";
            this.rbAe10.UseVisualStyleBackColor = true;
            // 
            // rbAeNone
            // 
            this.rbAeNone.AutoSize = true;
            this.rbAeNone.Location = new System.Drawing.Point(13, 22);
            this.rbAeNone.Name = "rbAeNone";
            this.rbAeNone.Size = new System.Drawing.Size(54, 19);
            this.rbAeNone.TabIndex = 0;
            this.rbAeNone.TabStop = true;
            this.rbAeNone.Text = "None";
            this.rbAeNone.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(216, 245);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 6;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(297, 245);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // FrmServerSelect
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(384, 280);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.gbAeSpec);
            this.Controls.Add(this.gbDaSpec);
            this.Controls.Add(this.cbServer);
            this.Controls.Add(this.lblServer);
            this.Controls.Add(this.txtHost);
            this.Controls.Add(this.lblHost);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmServerSelect";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Select OPC Server";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmServerSelect_FormClosed);
            this.Load += new System.EventHandler(this.FrmServerSelect_Load);
            this.gbDaSpec.ResumeLayout(false);
            this.gbDaSpec.PerformLayout();
            this.gbAeSpec.ResumeLayout(false);
            this.gbAeSpec.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label lblHost;
        private TextBox txtHost;
        private Label lblServer;
        private ComboBox cbServer;
        private GroupBox gbDaSpec;
        private RadioButton rbDa30;
        private RadioButton rbDa20;
        private RadioButton rbDa10;
        private RadioButton rbDaNone;
        private GroupBox gbAeSpec;
        private RadioButton rbAe10;
        private RadioButton rbAeNone;
        private Button btnOK;
        private Button btnCancel;
    }
}