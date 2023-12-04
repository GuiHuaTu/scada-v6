namespace Scada.Comm.Drivers.DrvSiemensS7.View.Controls
{
    partial class CtrlCmd
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
            gbCmd = new GroupBox();
            pnlCmdElem = new Panel();
            numCmdElemCnt = new NumericUpDown();
            lblCmdElemCnt = new Label();
            cbCmdElemType = new ComboBox();
            lblCmdElemType = new Label();
            numCmdAddress = new TextBox();
            lblCmdAddress = new Label();
            chkCmdMultiple = new CheckBox();
            cbCmdDataBlock = new ComboBox();
            lblCmdDataBlock = new Label();
            numCmdNum = new NumericUpDown();
            lblCmdNum = new Label();
            pnlCmdCodeWarn = new Panel();
            lblCmdCodeWarn = new Label();
            pbCmdCodeWarn = new PictureBox();
            txtCmdCode = new TextBox();
            lblCmdCode = new Label();
            txtCmdName = new TextBox();
            lblCmdName = new Label();
            gbCmd.SuspendLayout();
            pnlCmdElem.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numCmdElemCnt).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numCmdNum).BeginInit();
            pnlCmdCodeWarn.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pbCmdCodeWarn).BeginInit();
            SuspendLayout();
            // 
            // gbCmd
            // 
            gbCmd.Controls.Add(pnlCmdElem);
            gbCmd.Controls.Add(chkCmdMultiple);
            gbCmd.Controls.Add(cbCmdDataBlock);
            gbCmd.Controls.Add(lblCmdDataBlock);
            gbCmd.Controls.Add(numCmdNum);
            gbCmd.Controls.Add(lblCmdNum);
            gbCmd.Controls.Add(pnlCmdCodeWarn);
            gbCmd.Controls.Add(txtCmdCode);
            gbCmd.Controls.Add(lblCmdCode);
            gbCmd.Controls.Add(txtCmdName);
            gbCmd.Controls.Add(lblCmdName);
            gbCmd.Location = new Point(0, 0);
            gbCmd.Margin = new Padding(5);
            gbCmd.Name = "gbCmd";
            gbCmd.Padding = new Padding(16, 5, 16, 16);
            gbCmd.Size = new Size(471, 654);
            gbCmd.TabIndex = 0;
            gbCmd.TabStop = false;
            gbCmd.Text = "Command Parameters";
            // 
            // pnlCmdElem
            // 
            pnlCmdElem.Controls.Add(numCmdElemCnt);
            pnlCmdElem.Controls.Add(lblCmdElemCnt);
            pnlCmdElem.Controls.Add(cbCmdElemType);
            pnlCmdElem.Controls.Add(lblCmdElemType);
            pnlCmdElem.Controls.Add(numCmdAddress);
            pnlCmdElem.Controls.Add(lblCmdAddress);
            pnlCmdElem.Location = new Point(16, 355);
            pnlCmdElem.Margin = new Padding(5);
            pnlCmdElem.Name = "pnlCmdElem";
            pnlCmdElem.Size = new Size(435, 278);
            pnlCmdElem.TabIndex = 14;
            // 
            // numCmdElemCnt
            // 
            numCmdElemCnt.Location = new Point(227, 106);
            numCmdElemCnt.Margin = new Padding(5);
            numCmdElemCnt.Maximum = new decimal(new int[] { 2000, 0, 0, 0 });
            numCmdElemCnt.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numCmdElemCnt.Name = "numCmdElemCnt";
            numCmdElemCnt.Size = new Size(204, 30);
            numCmdElemCnt.TabIndex = 6;
            numCmdElemCnt.Value = new decimal(new int[] { 1, 0, 0, 0 });
            numCmdElemCnt.ValueChanged += numCmdElemCnt_ValueChanged;
            // 
            // lblCmdElemCnt
            // 
            lblCmdElemCnt.AutoSize = true;
            lblCmdElemCnt.Location = new Point(223, 77);
            lblCmdElemCnt.Margin = new Padding(5, 0, 5, 0);
            lblCmdElemCnt.Name = "lblCmdElemCnt";
            lblCmdElemCnt.Size = new Size(134, 24);
            lblCmdElemCnt.TabIndex = 5;
            lblCmdElemCnt.Text = "Element count";
            // 
            // cbCmdElemType
            // 
            cbCmdElemType.DropDownStyle = ComboBoxStyle.DropDownList;
            cbCmdElemType.FormattingEnabled = true;
            cbCmdElemType.Items.AddRange(new object[] { "bool (1 bit)", "byte (1 byte)", "double (8 bytes)", "float (4 bytes)", "int (4 bytes)", "long (8 bytes)", "S7String", "S7WString", "short (2 bytes)", "string", "uint (4 bytes)", "ulong (8 bytes)", "Undefined", "ushort (2 bytes)" });
            cbCmdElemType.Location = new Point(9, 106);
            cbCmdElemType.Margin = new Padding(5);
            cbCmdElemType.Name = "cbCmdElemType";
            cbCmdElemType.Size = new Size(208, 32);
            cbCmdElemType.TabIndex = 4;
            cbCmdElemType.SelectedIndexChanged += cbCmdElemType_SelectedIndexChanged;
            // 
            // lblCmdElemType
            // 
            lblCmdElemType.AutoSize = true;
            lblCmdElemType.Location = new Point(4, 77);
            lblCmdElemType.Margin = new Padding(5, 0, 5, 0);
            lblCmdElemType.Name = "lblCmdElemType";
            lblCmdElemType.Size = new Size(124, 24);
            lblCmdElemType.TabIndex = 3;
            lblCmdElemType.Text = "Element type";
            // 
            // numCmdAddress
            // 
            numCmdAddress.Location = new Point(7, 36);
            numCmdAddress.Margin = new Padding(5);
            numCmdAddress.Name = "numCmdAddress";
            numCmdAddress.Size = new Size(211, 30);
            numCmdAddress.TabIndex = 1;
            // 
            // lblCmdAddress
            // 
            lblCmdAddress.AutoSize = true;
            lblCmdAddress.Location = new Point(7, 7);
            lblCmdAddress.Margin = new Padding(5, 0, 5, 0);
            lblCmdAddress.Name = "lblCmdAddress";
            lblCmdAddress.Size = new Size(152, 24);
            lblCmdAddress.TabIndex = 0;
            lblCmdAddress.Text = "Element address";
            // 
            // chkCmdMultiple
            // 
            chkCmdMultiple.AutoSize = true;
            chkCmdMultiple.Location = new Point(20, 317);
            chkCmdMultiple.Margin = new Padding(5);
            chkCmdMultiple.Name = "chkCmdMultiple";
            chkCmdMultiple.Size = new Size(109, 28);
            chkCmdMultiple.TabIndex = 9;
            chkCmdMultiple.Text = "Multiple";
            chkCmdMultiple.UseVisualStyleBackColor = true;
            chkCmdMultiple.CheckedChanged += chkCmdMultiple_CheckedChanged;
            // 
            // cbCmdDataBlock
            // 
            cbCmdDataBlock.DropDownStyle = ComboBoxStyle.DropDownList;
            cbCmdDataBlock.FormattingEnabled = true;
            cbCmdDataBlock.Items.AddRange(new object[] { "Input", "Output", "Memory", "DataBlock", "Timer", "Counter" });
            cbCmdDataBlock.Location = new Point(20, 270);
            cbCmdDataBlock.Margin = new Padding(5);
            cbCmdDataBlock.Name = "cbCmdDataBlock";
            cbCmdDataBlock.Size = new Size(428, 32);
            cbCmdDataBlock.TabIndex = 8;
            cbCmdDataBlock.SelectedIndexChanged += cbCmdDataBlock_SelectedIndexChanged;
            // 
            // lblCmdDataBlock
            // 
            lblCmdDataBlock.AutoSize = true;
            lblCmdDataBlock.Location = new Point(16, 242);
            lblCmdDataBlock.Margin = new Padding(5, 0, 5, 0);
            lblCmdDataBlock.Name = "lblCmdDataBlock";
            lblCmdDataBlock.Size = new Size(143, 24);
            lblCmdDataBlock.TabIndex = 7;
            lblCmdDataBlock.Text = "Data Area Type";
            // 
            // numCmdNum
            // 
            numCmdNum.Location = new Point(20, 200);
            numCmdNum.Margin = new Padding(5);
            numCmdNum.Maximum = new decimal(new int[] { int.MaxValue, 0, 0, 0 });
            numCmdNum.Name = "numCmdNum";
            numCmdNum.Size = new Size(211, 30);
            numCmdNum.TabIndex = 6;
            numCmdNum.ValueChanged += numCmdNum_ValueChanged;
            // 
            // lblCmdNum
            // 
            lblCmdNum.AutoSize = true;
            lblCmdNum.Location = new Point(16, 171);
            lblCmdNum.Margin = new Padding(5, 0, 5, 0);
            lblCmdNum.Name = "lblCmdNum";
            lblCmdNum.Size = new Size(173, 24);
            lblCmdNum.TabIndex = 5;
            lblCmdNum.Text = "Command number";
            // 
            // pnlCmdCodeWarn
            // 
            pnlCmdCodeWarn.Controls.Add(lblCmdCodeWarn);
            pnlCmdCodeWarn.Controls.Add(pbCmdCodeWarn);
            pnlCmdCodeWarn.Location = new Point(240, 130);
            pnlCmdCodeWarn.Margin = new Padding(5);
            pnlCmdCodeWarn.Name = "pnlCmdCodeWarn";
            pnlCmdCodeWarn.Size = new Size(211, 37);
            pnlCmdCodeWarn.TabIndex = 4;
            // 
            // lblCmdCodeWarn
            // 
            lblCmdCodeWarn.AutoSize = true;
            lblCmdCodeWarn.ForeColor = Color.Red;
            lblCmdCodeWarn.Location = new Point(30, 6);
            lblCmdCodeWarn.Margin = new Padding(5, 0, 5, 0);
            lblCmdCodeWarn.Name = "lblCmdCodeWarn";
            lblCmdCodeWarn.Size = new Size(116, 24);
            lblCmdCodeWarn.TabIndex = 0;
            lblCmdCodeWarn.Text = "Fill out code";
            // 
            // pbCmdCodeWarn
            // 
            pbCmdCodeWarn.Image = Properties.Resources.warning;
            pbCmdCodeWarn.Location = new Point(0, 5);
            pbCmdCodeWarn.Margin = new Padding(5);
            pbCmdCodeWarn.Name = "pbCmdCodeWarn";
            pbCmdCodeWarn.Size = new Size(25, 26);
            pbCmdCodeWarn.TabIndex = 0;
            pbCmdCodeWarn.TabStop = false;
            // 
            // txtCmdCode
            // 
            txtCmdCode.Location = new Point(20, 130);
            txtCmdCode.Margin = new Padding(5);
            txtCmdCode.Name = "txtCmdCode";
            txtCmdCode.Size = new Size(208, 30);
            txtCmdCode.TabIndex = 3;
            txtCmdCode.TextChanged += txtCmdCode_TextChanged;
            // 
            // lblCmdCode
            // 
            lblCmdCode.AutoSize = true;
            lblCmdCode.Location = new Point(16, 101);
            lblCmdCode.Margin = new Padding(5, 0, 5, 0);
            lblCmdCode.Name = "lblCmdCode";
            lblCmdCode.Size = new Size(147, 24);
            lblCmdCode.TabIndex = 2;
            lblCmdCode.Text = "Command code";
            // 
            // txtCmdName
            // 
            txtCmdName.Location = new Point(20, 59);
            txtCmdName.Margin = new Padding(5);
            txtCmdName.Name = "txtCmdName";
            txtCmdName.Size = new Size(428, 30);
            txtCmdName.TabIndex = 1;
            txtCmdName.TextChanged += txtCmdName_TextChanged;
            // 
            // lblCmdName
            // 
            lblCmdName.AutoSize = true;
            lblCmdName.Location = new Point(16, 30);
            lblCmdName.Margin = new Padding(5, 0, 5, 0);
            lblCmdName.Name = "lblCmdName";
            lblCmdName.Size = new Size(62, 24);
            lblCmdName.TabIndex = 0;
            lblCmdName.Text = "Name";
            // 
            // CtrlCmd
            // 
            AutoScaleDimensions = new SizeF(11F, 24F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(gbCmd);
            Margin = new Padding(5);
            Name = "CtrlCmd";
            Size = new Size(471, 654);
            gbCmd.ResumeLayout(false);
            gbCmd.PerformLayout();
            pnlCmdElem.ResumeLayout(false);
            pnlCmdElem.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numCmdElemCnt).EndInit();
            ((System.ComponentModel.ISupportInitialize)numCmdNum).EndInit();
            pnlCmdCodeWarn.ResumeLayout(false);
            pnlCmdCodeWarn.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pbCmdCodeWarn).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.GroupBox gbCmd;
        private System.Windows.Forms.CheckBox chkCmdMultiple;
        private System.Windows.Forms.Label lblCmdElemCnt;
        private System.Windows.Forms.NumericUpDown numCmdElemCnt;
        private System.Windows.Forms.TextBox txtCmdName;
        private System.Windows.Forms.Label lblCmdName;
        private System.Windows.Forms.Label lblCmdNum;
        private System.Windows.Forms.NumericUpDown numCmdNum;
        private System.Windows.Forms.TextBox numCmdAddress;
        private System.Windows.Forms.Label lblCmdAddress;
        private System.Windows.Forms.Label lblCmdDataBlock;
        private System.Windows.Forms.ComboBox cbCmdDataBlock;
        private System.Windows.Forms.Label lblCmdElemType;
        private System.Windows.Forms.ComboBox cbCmdElemType;
        private System.Windows.Forms.TextBox txtCmdCode;
        private System.Windows.Forms.Label lblCmdCode;
        private System.Windows.Forms.Panel pnlCmdCodeWarn;
        private System.Windows.Forms.Label lblCmdCodeWarn;
        private System.Windows.Forms.PictureBox pbCmdCodeWarn;
        private System.Windows.Forms.Panel pnlCmdElem;
    }
}
