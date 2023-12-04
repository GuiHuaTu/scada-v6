namespace Scada.Comm.Drivers.DrvSiemensS7.View.Controls
{
    partial class CtrlElemGroup
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
            gbElemGroup = new GroupBox();
            chkGrActive = new CheckBox();
            lblGrElemCnt = new Label();
            numGrElemCnt = new NumericUpDown();
            txtGrName = new TextBox();
            lblGrName = new Label();
            lblGrDataBlock = new Label();
            cbGrDataBlock = new ComboBox();
            gbElemGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numGrElemCnt).BeginInit();
            SuspendLayout();
            // 
            // gbElemGroup
            // 
            gbElemGroup.Controls.Add(chkGrActive);
            gbElemGroup.Controls.Add(lblGrElemCnt);
            gbElemGroup.Controls.Add(numGrElemCnt);
            gbElemGroup.Controls.Add(txtGrName);
            gbElemGroup.Controls.Add(lblGrName);
            gbElemGroup.Controls.Add(lblGrDataBlock);
            gbElemGroup.Controls.Add(cbGrDataBlock);
            gbElemGroup.Location = new Point(0, 0);
            gbElemGroup.Margin = new Padding(5);
            gbElemGroup.Name = "gbElemGroup";
            gbElemGroup.Padding = new Padding(16, 5, 16, 16);
            gbElemGroup.Size = new Size(471, 437);
            gbElemGroup.TabIndex = 0;
            gbElemGroup.TabStop = false;
            gbElemGroup.Text = "Element Group Parameters";
            // 
            // chkGrActive
            // 
            chkGrActive.AutoSize = true;
            chkGrActive.Location = new Point(20, 35);
            chkGrActive.Margin = new Padding(5);
            chkGrActive.Name = "chkGrActive";
            chkGrActive.Size = new Size(89, 28);
            chkGrActive.TabIndex = 0;
            chkGrActive.Text = "Active";
            chkGrActive.UseVisualStyleBackColor = true;
            chkGrActive.CheckedChanged += chkGrActive_CheckedChanged;
            // 
            // lblGrElemCnt
            // 
            lblGrElemCnt.AutoSize = true;
            lblGrElemCnt.Location = new Point(16, 350);
            lblGrElemCnt.Margin = new Padding(5, 0, 5, 0);
            lblGrElemCnt.Name = "lblGrElemCnt";
            lblGrElemCnt.Size = new Size(134, 24);
            lblGrElemCnt.TabIndex = 10;
            lblGrElemCnt.Text = "Element count";
            // 
            // numGrElemCnt
            // 
            numGrElemCnt.Location = new Point(20, 379);
            numGrElemCnt.Margin = new Padding(5);
            numGrElemCnt.Maximum = new decimal(new int[] { 2000, 0, 0, 0 });
            numGrElemCnt.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numGrElemCnt.Name = "numGrElemCnt";
            numGrElemCnt.Size = new Size(189, 30);
            numGrElemCnt.TabIndex = 11;
            numGrElemCnt.Value = new decimal(new int[] { 1, 0, 0, 0 });
            numGrElemCnt.ValueChanged += numGrElemCnt_ValueChanged;
            // 
            // txtGrName
            // 
            txtGrName.Location = new Point(20, 99);
            txtGrName.Margin = new Padding(5);
            txtGrName.Name = "txtGrName";
            txtGrName.Size = new Size(428, 30);
            txtGrName.TabIndex = 2;
            txtGrName.TextChanged += txtGrName_TextChanged;
            // 
            // lblGrName
            // 
            lblGrName.AutoSize = true;
            lblGrName.Location = new Point(16, 70);
            lblGrName.Margin = new Padding(5, 0, 5, 0);
            lblGrName.Name = "lblGrName";
            lblGrName.Size = new Size(62, 24);
            lblGrName.TabIndex = 1;
            lblGrName.Text = "Name";
            // 
            // lblGrDataBlock
            // 
            lblGrDataBlock.AutoSize = true;
            lblGrDataBlock.Location = new Point(16, 141);
            lblGrDataBlock.Margin = new Padding(5, 0, 5, 0);
            lblGrDataBlock.Name = "lblGrDataBlock";
            lblGrDataBlock.Size = new Size(143, 24);
            lblGrDataBlock.TabIndex = 3;
            lblGrDataBlock.Text = "Data Area Type";
            // 
            // cbGrDataBlock
            // 
            cbGrDataBlock.DropDownStyle = ComboBoxStyle.DropDownList;
            cbGrDataBlock.FormattingEnabled = true;
            cbGrDataBlock.Items.AddRange(new object[] { "Input", "Output", "Memory", "DataBlock", "Timer", "Counter" });
            cbGrDataBlock.Location = new Point(20, 170);
            cbGrDataBlock.Margin = new Padding(5);
            cbGrDataBlock.Name = "cbGrDataBlock";
            cbGrDataBlock.Size = new Size(428, 32);
            cbGrDataBlock.TabIndex = 4;
            cbGrDataBlock.SelectedIndexChanged += cbGrDataBlock_SelectedIndexChanged;
            // 
            // CtrlElemGroup
            // 
            AutoScaleDimensions = new SizeF(11F, 24F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(gbElemGroup);
            Margin = new Padding(5);
            Name = "CtrlElemGroup";
            Size = new Size(471, 437);
            gbElemGroup.ResumeLayout(false);
            gbElemGroup.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numGrElemCnt).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.GroupBox gbElemGroup;
        private System.Windows.Forms.CheckBox chkGrActive;
        private System.Windows.Forms.Label lblGrElemCnt;
        private System.Windows.Forms.NumericUpDown numGrElemCnt;
        private System.Windows.Forms.TextBox txtGrName;
        private System.Windows.Forms.Label lblGrName;
        private System.Windows.Forms.Label lblGrDataBlock;
        private System.Windows.Forms.ComboBox cbGrDataBlock;
    }
}
