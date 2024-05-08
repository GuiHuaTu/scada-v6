namespace Scada.Comm.Drivers.DrvOpcClassic.View.Controls
{
    partial class CtrlCommand
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.gbCommand = new System.Windows.Forms.GroupBox();
            this.pbDataTypeWarning = new System.Windows.Forms.PictureBox();
            this.cbDataType = new System.Windows.Forms.ComboBox();
            this.lblDataType = new System.Windows.Forms.Label();
            this.numCmdNum = new System.Windows.Forms.NumericUpDown();
            this.lblCmdNum = new System.Windows.Forms.Label();
            this.txtCmdCode = new System.Windows.Forms.TextBox();
            this.lblCmdCode = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.lblName = new System.Windows.Forms.Label();
            this.txtPath = new System.Windows.Forms.TextBox();
            this.lblPath = new System.Windows.Forms.Label();
            this.gbCommand.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbDataTypeWarning)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCmdNum)).BeginInit();
            this.SuspendLayout();
            // 
            // gbCommand
            // 
            this.gbCommand.Controls.Add(this.pbDataTypeWarning);
            this.gbCommand.Controls.Add(this.cbDataType);
            this.gbCommand.Controls.Add(this.lblDataType);
            this.gbCommand.Controls.Add(this.numCmdNum);
            this.gbCommand.Controls.Add(this.lblCmdNum);
            this.gbCommand.Controls.Add(this.txtCmdCode);
            this.gbCommand.Controls.Add(this.lblCmdCode);
            this.gbCommand.Controls.Add(this.txtName);
            this.gbCommand.Controls.Add(this.lblName);
            this.gbCommand.Controls.Add(this.txtPath);
            this.gbCommand.Controls.Add(this.lblPath);
            this.gbCommand.Location = new System.Drawing.Point(0, 0);
            this.gbCommand.Name = "gbCommand";
            this.gbCommand.Padding = new System.Windows.Forms.Padding(10, 3, 10, 10);
            this.gbCommand.Size = new System.Drawing.Size(250, 500);
            this.gbCommand.TabIndex = 0;
            this.gbCommand.TabStop = false;
            this.gbCommand.Text = "Command Parameters";
            // 
            // pbDataTypeWarning
            // 
            this.pbDataTypeWarning.BackColor = System.Drawing.SystemColors.Window;
            this.pbDataTypeWarning.Image = global::Scada.Comm.Drivers.DrvOpcClassic.View.Properties.Resources.warning;
            this.pbDataTypeWarning.Location = new System.Drawing.Point(201, 216);
            this.pbDataTypeWarning.Name = "pbDataTypeWarning";
            this.pbDataTypeWarning.Size = new System.Drawing.Size(16, 16);
            this.pbDataTypeWarning.TabIndex = 10;
            this.pbDataTypeWarning.TabStop = false;
            // 
            // cbDataType
            // 
            this.cbDataType.FormattingEnabled = true;
            this.cbDataType.Location = new System.Drawing.Point(13, 213);
            this.cbDataType.Name = "cbDataType";
            this.cbDataType.Size = new System.Drawing.Size(224, 23);
            this.cbDataType.TabIndex = 9;
            this.cbDataType.TextChanged += new System.EventHandler(this.cbDataType_TextChanged);
            // 
            // lblDataType
            // 
            this.lblDataType.AutoSize = true;
            this.lblDataType.Location = new System.Drawing.Point(10, 195);
            this.lblDataType.Name = "lblDataType";
            this.lblDataType.Size = new System.Drawing.Size(57, 15);
            this.lblDataType.TabIndex = 8;
            this.lblDataType.Text = "Data type";
            // 
            // numCmdNum
            // 
            this.numCmdNum.Location = new System.Drawing.Point(13, 169);
            this.numCmdNum.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.numCmdNum.Name = "numCmdNum";
            this.numCmdNum.Size = new System.Drawing.Size(120, 23);
            this.numCmdNum.TabIndex = 7;
            this.numCmdNum.ValueChanged += new System.EventHandler(this.numCmdNum_ValueChanged);
            // 
            // lblCmdNum
            // 
            this.lblCmdNum.AutoSize = true;
            this.lblCmdNum.Location = new System.Drawing.Point(10, 151);
            this.lblCmdNum.Name = "lblCmdNum";
            this.lblCmdNum.Size = new System.Drawing.Size(109, 15);
            this.lblCmdNum.TabIndex = 6;
            this.lblCmdNum.Text = "Command number";
            // 
            // txtCmdCode
            // 
            this.txtCmdCode.Location = new System.Drawing.Point(13, 125);
            this.txtCmdCode.Name = "txtCmdCode";
            this.txtCmdCode.Size = new System.Drawing.Size(224, 23);
            this.txtCmdCode.TabIndex = 5;
            this.txtCmdCode.TextChanged += new System.EventHandler(this.txtCmdCode_TextChanged);
            // 
            // lblCmdCode
            // 
            this.lblCmdCode.AutoSize = true;
            this.lblCmdCode.Location = new System.Drawing.Point(10, 107);
            this.lblCmdCode.Name = "lblCmdCode";
            this.lblCmdCode.Size = new System.Drawing.Size(93, 15);
            this.lblCmdCode.TabIndex = 4;
            this.lblCmdCode.Text = "Command code";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(13, 81);
            this.txtName.Name = "txtName";
            this.txtName.ReadOnly = true;
            this.txtName.Size = new System.Drawing.Size(224, 23);
            this.txtName.TabIndex = 3;
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(10, 63);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(39, 15);
            this.lblName.TabIndex = 2;
            this.lblName.Text = "Name";
            // 
            // txtPath
            // 
            this.txtPath.Location = new System.Drawing.Point(13, 37);
            this.txtPath.Name = "txtPath";
            this.txtPath.ReadOnly = true;
            this.txtPath.Size = new System.Drawing.Size(224, 23);
            this.txtPath.TabIndex = 1;
            // 
            // lblPath
            // 
            this.lblPath.AutoSize = true;
            this.lblPath.Location = new System.Drawing.Point(10, 19);
            this.lblPath.Name = "lblPath";
            this.lblPath.Size = new System.Drawing.Size(31, 15);
            this.lblPath.TabIndex = 0;
            this.lblPath.Text = "Path";
            // 
            // CtrlCommand
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gbCommand);
            this.Name = "CtrlCommand";
            this.Size = new System.Drawing.Size(250, 500);
            this.gbCommand.ResumeLayout(false);
            this.gbCommand.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbDataTypeWarning)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCmdNum)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbCommand;
        private System.Windows.Forms.Label lblDataType;
        private System.Windows.Forms.NumericUpDown numCmdNum;
        private System.Windows.Forms.Label lblCmdNum;
        private TextBox txtCmdCode;
        private Label lblCmdCode;
        private TextBox txtName;
        private Label lblName;
        private TextBox txtPath;
        private Label lblPath;
        private ComboBox cbDataType;
        private PictureBox pbDataTypeWarning;
    }
}
