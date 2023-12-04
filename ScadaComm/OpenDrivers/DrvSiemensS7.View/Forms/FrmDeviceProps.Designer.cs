namespace Scada.Comm.Drivers.DrvSiemensS7.View.Forms
{
    partial class FrmDeviceProps
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
            gbDevice = new GroupBox();
            btnBrowseTemplate = new Button();
            btnEditTemplate = new Button();
            txtTemplateFileName = new TextBox();
            lblTemplateFileName = new Label();
            gbCommLine = new GroupBox();
            txt_Plc_Slot = new TextBox();
            lbl_Plc_Slot = new Label();
            txt_Plc_Rack = new TextBox();
            lbl_Plc_Rack = new Label();
            txt_Plc_IP = new TextBox();
            lbl_Ip = new Label();
            cbCpuType = new ComboBox();
            lblCpuType = new Label();
            btnOK = new Button();
            btnCancel = new Button();
            openFileDialog = new OpenFileDialog();
            gbDevice.SuspendLayout();
            gbCommLine.SuspendLayout();
            SuspendLayout();
            // 
            // gbDevice
            // 
            gbDevice.Controls.Add(btnBrowseTemplate);
            gbDevice.Controls.Add(btnEditTemplate);
            gbDevice.Controls.Add(txtTemplateFileName);
            gbDevice.Controls.Add(lblTemplateFileName);
            gbDevice.Location = new Point(19, 262);
            gbDevice.Margin = new Padding(5);
            gbDevice.Name = "gbDevice";
            gbDevice.Padding = new Padding(16, 5, 16, 16);
            gbDevice.Size = new Size(644, 117);
            gbDevice.TabIndex = 1;
            gbDevice.TabStop = false;
            gbDevice.Text = "Device";
            // 
            // btnBrowseTemplate
            // 
            btnBrowseTemplate.Location = new Point(506, 59);
            btnBrowseTemplate.Margin = new Padding(5);
            btnBrowseTemplate.Name = "btnBrowseTemplate";
            btnBrowseTemplate.Size = new Size(118, 37);
            btnBrowseTemplate.TabIndex = 3;
            btnBrowseTemplate.Text = "Browse...";
            btnBrowseTemplate.UseVisualStyleBackColor = true;
            btnBrowseTemplate.Click += btnBrowse_Click;
            // 
            // btnEditTemplate
            // 
            btnEditTemplate.Location = new Point(379, 59);
            btnEditTemplate.Margin = new Padding(5);
            btnEditTemplate.Name = "btnEditTemplate";
            btnEditTemplate.Size = new Size(118, 37);
            btnEditTemplate.TabIndex = 2;
            btnEditTemplate.Text = "Edit";
            btnEditTemplate.UseVisualStyleBackColor = true;
            btnEditTemplate.Click += btnEdit_Click;
            // 
            // txtTemplateFileName
            // 
            txtTemplateFileName.Location = new Point(20, 59);
            txtTemplateFileName.Margin = new Padding(5);
            txtTemplateFileName.Name = "txtTemplateFileName";
            txtTemplateFileName.Size = new Size(347, 30);
            txtTemplateFileName.TabIndex = 1;
            // 
            // lblTemplateFileName
            // 
            lblTemplateFileName.AutoSize = true;
            lblTemplateFileName.Location = new Point(16, 30);
            lblTemplateFileName.Margin = new Padding(5, 0, 5, 0);
            lblTemplateFileName.Name = "lblTemplateFileName";
            lblTemplateFileName.Size = new Size(150, 24);
            lblTemplateFileName.TabIndex = 0;
            lblTemplateFileName.Text = "Device template";
            // 
            // gbCommLine
            // 
            gbCommLine.Controls.Add(txt_Plc_Slot);
            gbCommLine.Controls.Add(lbl_Plc_Slot);
            gbCommLine.Controls.Add(txt_Plc_Rack);
            gbCommLine.Controls.Add(lbl_Plc_Rack);
            gbCommLine.Controls.Add(txt_Plc_IP);
            gbCommLine.Controls.Add(lbl_Ip);
            gbCommLine.Controls.Add(cbCpuType);
            gbCommLine.Controls.Add(lblCpuType);
            gbCommLine.Location = new Point(19, 19);
            gbCommLine.Margin = new Padding(5);
            gbCommLine.Name = "gbCommLine";
            gbCommLine.Padding = new Padding(16, 5, 16, 16);
            gbCommLine.Size = new Size(644, 233);
            gbCommLine.TabIndex = 0;
            gbCommLine.TabStop = false;
            gbCommLine.Text = "Communication Line";
            // 
            // txt_Plc_Slot
            // 
            txt_Plc_Slot.Location = new Point(470, 160);
            txt_Plc_Slot.Name = "txt_Plc_Slot";
            txt_Plc_Slot.Size = new Size(150, 30);
            txt_Plc_Slot.TabIndex = 7;
            txt_Plc_Slot.Text = "0";
            // 
            // lbl_Plc_Slot
            // 
            lbl_Plc_Slot.AutoSize = true;
            lbl_Plc_Slot.Location = new Point(350, 163);
            lbl_Plc_Slot.Name = "lbl_Plc_Slot";
            lbl_Plc_Slot.Size = new Size(83, 24);
            lbl_Plc_Slot.TabIndex = 6;
            lbl_Plc_Slot.Text = "PLC 插槽";
            // 
            // txt_Plc_Rack
            // 
            txt_Plc_Rack.Location = new Point(140, 160);
            txt_Plc_Rack.Name = "txt_Plc_Rack";
            txt_Plc_Rack.Size = new Size(150, 30);
            txt_Plc_Rack.TabIndex = 5;
            txt_Plc_Rack.Text = "0";
            // 
            // lbl_Plc_Rack
            // 
            lbl_Plc_Rack.AutoSize = true;
            lbl_Plc_Rack.Location = new Point(20, 163);
            lbl_Plc_Rack.Name = "lbl_Plc_Rack";
            lbl_Plc_Rack.Size = new Size(83, 24);
            lbl_Plc_Rack.TabIndex = 4;
            lbl_Plc_Rack.Text = "PLC 机架";
            // 
            // txt_Plc_IP
            // 
            txt_Plc_IP.Location = new Point(140, 103);
            txt_Plc_IP.Name = "txt_Plc_IP";
            txt_Plc_IP.Size = new Size(481, 30);
            txt_Plc_IP.TabIndex = 3;
            // 
            // lbl_Ip
            // 
            lbl_Ip.AutoSize = true;
            lbl_Ip.Location = new Point(20, 106);
            lbl_Ip.Name = "lbl_Ip";
            lbl_Ip.Size = new Size(101, 24);
            lbl_Ip.TabIndex = 2;
            lbl_Ip.Text = "IP Address";
            // 
            // cbCpuType
            // 
            cbCpuType.DropDownStyle = ComboBoxStyle.DropDownList;
            cbCpuType.FormattingEnabled = true;
            cbCpuType.Location = new Point(20, 59);
            cbCpuType.Margin = new Padding(5);
            cbCpuType.Name = "cbCpuType";
            cbCpuType.Size = new Size(601, 32);
            cbCpuType.TabIndex = 1;
            // 
            // lblCpuType
            // 
            lblCpuType.AutoSize = true;
            lblCpuType.Location = new Point(16, 30);
            lblCpuType.Margin = new Padding(5, 0, 5, 0);
            lblCpuType.Name = "lblCpuType";
            lblCpuType.Size = new Size(92, 24);
            lblCpuType.TabIndex = 0;
            lblCpuType.Text = "Cpu Type";
            // 
            // btnOK
            // 
            btnOK.Location = new Point(418, 404);
            btnOK.Margin = new Padding(5);
            btnOK.Name = "btnOK";
            btnOK.Size = new Size(118, 37);
            btnOK.TabIndex = 2;
            btnOK.Text = "OK";
            btnOK.UseVisualStyleBackColor = true;
            btnOK.Click += btnOK_Click;
            // 
            // btnCancel
            // 
            btnCancel.DialogResult = DialogResult.Cancel;
            btnCancel.Location = new Point(545, 404);
            btnCancel.Margin = new Padding(5);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(118, 37);
            btnCancel.TabIndex = 3;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            // 
            // openFileDialog
            // 
            openFileDialog.DefaultExt = "*.xml";
            openFileDialog.Filter = "Template Files (*.xml)|*.xml|All Files (*.*)|*.*";
            openFileDialog.FilterIndex = 0;
            // 
            // FrmDeviceProps
            // 
            AcceptButton = btnOK;
            AutoScaleDimensions = new SizeF(11F, 24F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = btnCancel;
            ClientSize = new Size(682, 513);
            Controls.Add(btnCancel);
            Controls.Add(btnOK);
            Controls.Add(gbDevice);
            Controls.Add(gbCommLine);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Margin = new Padding(5);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FrmDeviceProps";
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "Device {0} Properties";
            Load += FrmDevProps_Load;
            gbDevice.ResumeLayout(false);
            gbDevice.PerformLayout();
            gbCommLine.ResumeLayout(false);
            gbCommLine.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.GroupBox gbDevice;
        private System.Windows.Forms.TextBox txtTemplateFileName;
        private System.Windows.Forms.Label lblTemplateFileName;
        private System.Windows.Forms.GroupBox gbCommLine;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ComboBox cbCpuType;
        private System.Windows.Forms.Label lblCpuType;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.Button btnBrowseTemplate;
        private System.Windows.Forms.Button btnEditTemplate;
        private Label lbl_Ip;
        private TextBox txt_Plc_IP;
        private Label lbl_Plc_Rack;
        private TextBox txt_Plc_Slot;
        private Label lbl_Plc_Slot;
        private TextBox txt_Plc_Rack;
    }
}