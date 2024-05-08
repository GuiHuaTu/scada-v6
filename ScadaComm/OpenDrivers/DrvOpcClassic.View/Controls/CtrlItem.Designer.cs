namespace Scada.Comm.Drivers.DrvOpcClassic.View.Controls
{
    partial class CtrlItem
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
            this.gbItem = new System.Windows.Forms.GroupBox();
            this.numDataLen = new System.Windows.Forms.NumericUpDown();
            this.lblDataLen = new System.Windows.Forms.Label();
            this.chkIsArray = new System.Windows.Forms.CheckBox();
            this.chkIsString = new System.Windows.Forms.CheckBox();
            this.cbDataType = new System.Windows.Forms.ComboBox();
            this.lblDataType = new System.Windows.Forms.Label();
            this.txtTagNum = new System.Windows.Forms.TextBox();
            this.lblTagNum = new System.Windows.Forms.Label();
            this.txtTagCode = new System.Windows.Forms.TextBox();
            this.lblTagCode = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.lblName = new System.Windows.Forms.Label();
            this.txtPath = new System.Windows.Forms.TextBox();
            this.lblPath = new System.Windows.Forms.Label();
            this.chkActive = new System.Windows.Forms.CheckBox();
            this.pbDataTypeWarning = new System.Windows.Forms.PictureBox();
            this.gbItem.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numDataLen)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbDataTypeWarning)).BeginInit();
            this.SuspendLayout();
            // 
            // gbItem
            // 
            this.gbItem.Controls.Add(this.numDataLen);
            this.gbItem.Controls.Add(this.lblDataLen);
            this.gbItem.Controls.Add(this.chkIsArray);
            this.gbItem.Controls.Add(this.chkIsString);
            this.gbItem.Controls.Add(this.pbDataTypeWarning);
            this.gbItem.Controls.Add(this.cbDataType);
            this.gbItem.Controls.Add(this.lblDataType);
            this.gbItem.Controls.Add(this.txtTagNum);
            this.gbItem.Controls.Add(this.lblTagNum);
            this.gbItem.Controls.Add(this.txtTagCode);
            this.gbItem.Controls.Add(this.lblTagCode);
            this.gbItem.Controls.Add(this.txtName);
            this.gbItem.Controls.Add(this.lblName);
            this.gbItem.Controls.Add(this.txtPath);
            this.gbItem.Controls.Add(this.lblPath);
            this.gbItem.Controls.Add(this.chkActive);
            this.gbItem.Location = new System.Drawing.Point(0, 0);
            this.gbItem.Name = "gbItem";
            this.gbItem.Padding = new System.Windows.Forms.Padding(10, 3, 10, 10);
            this.gbItem.Size = new System.Drawing.Size(250, 500);
            this.gbItem.TabIndex = 0;
            this.gbItem.TabStop = false;
            this.gbItem.Text = "Item Parameters";
            // 
            // numDataLen
            // 
            this.numDataLen.Location = new System.Drawing.Point(13, 332);
            this.numDataLen.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numDataLen.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numDataLen.Name = "numDataLen";
            this.numDataLen.Size = new System.Drawing.Size(120, 23);
            this.numDataLen.TabIndex = 14;
            this.numDataLen.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numDataLen.ValueChanged += new System.EventHandler(this.numArrayLen_ValueChanged);
            // 
            // lblDataLen
            // 
            this.lblDataLen.AutoSize = true;
            this.lblDataLen.Location = new System.Drawing.Point(10, 314);
            this.lblDataLen.Name = "lblDataLen";
            this.lblDataLen.Size = new System.Drawing.Size(118, 15);
            this.lblDataLen.TabIndex = 13;
            this.lblDataLen.Text = "String or array length";
            // 
            // chkIsArray
            // 
            this.chkIsArray.AutoSize = true;
            this.chkIsArray.Location = new System.Drawing.Point(13, 292);
            this.chkIsArray.Name = "chkIsArray";
            this.chkIsArray.Size = new System.Drawing.Size(63, 19);
            this.chkIsArray.TabIndex = 12;
            this.chkIsArray.Text = "Is array";
            this.chkIsArray.UseVisualStyleBackColor = true;
            this.chkIsArray.CheckedChanged += new System.EventHandler(this.chkIsArray_CheckedChanged);
            // 
            // chkIsString
            // 
            this.chkIsString.AutoSize = true;
            this.chkIsString.Enabled = false;
            this.chkIsString.Location = new System.Drawing.Point(13, 267);
            this.chkIsString.Name = "chkIsString";
            this.chkIsString.Size = new System.Drawing.Size(67, 19);
            this.chkIsString.TabIndex = 11;
            this.chkIsString.Text = "Is string";
            this.chkIsString.UseVisualStyleBackColor = true;
            // 
            // cbDataType
            // 
            this.cbDataType.FormattingEnabled = true;
            this.cbDataType.Location = new System.Drawing.Point(13, 235);
            this.cbDataType.Name = "cbDataType";
            this.cbDataType.Size = new System.Drawing.Size(224, 23);
            this.cbDataType.TabIndex = 10;
            this.cbDataType.TextChanged += new System.EventHandler(this.cbDataType_TextChanged);
            // 
            // lblDataType
            // 
            this.lblDataType.AutoSize = true;
            this.lblDataType.Location = new System.Drawing.Point(10, 220);
            this.lblDataType.Name = "lblDataType";
            this.lblDataType.Size = new System.Drawing.Size(57, 15);
            this.lblDataType.TabIndex = 9;
            this.lblDataType.Text = "Data type";
            // 
            // txtTagNum
            // 
            this.txtTagNum.Location = new System.Drawing.Point(13, 194);
            this.txtTagNum.Name = "txtTagNum";
            this.txtTagNum.ReadOnly = true;
            this.txtTagNum.Size = new System.Drawing.Size(120, 23);
            this.txtTagNum.TabIndex = 8;
            // 
            // lblTagNum
            // 
            this.lblTagNum.AutoSize = true;
            this.lblTagNum.Location = new System.Drawing.Point(10, 176);
            this.lblTagNum.Name = "lblTagNum";
            this.lblTagNum.Size = new System.Drawing.Size(70, 15);
            this.lblTagNum.TabIndex = 7;
            this.lblTagNum.Text = "Tag number";
            // 
            // txtTagCode
            // 
            this.txtTagCode.Location = new System.Drawing.Point(13, 150);
            this.txtTagCode.Name = "txtTagCode";
            this.txtTagCode.Size = new System.Drawing.Size(224, 23);
            this.txtTagCode.TabIndex = 6;
            this.txtTagCode.TextChanged += new System.EventHandler(this.txtTagCode_TextChanged);
            // 
            // lblTagCode
            // 
            this.lblTagCode.AutoSize = true;
            this.lblTagCode.Location = new System.Drawing.Point(10, 132);
            this.lblTagCode.Name = "lblTagCode";
            this.lblTagCode.Size = new System.Drawing.Size(54, 15);
            this.lblTagCode.TabIndex = 5;
            this.lblTagCode.Text = "Tag code";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(13, 106);
            this.txtName.Name = "txtName";
            this.txtName.ReadOnly = true;
            this.txtName.Size = new System.Drawing.Size(224, 23);
            this.txtName.TabIndex = 4;
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(10, 88);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(39, 15);
            this.lblName.TabIndex = 3;
            this.lblName.Text = "Name";
            // 
            // txtPath
            // 
            this.txtPath.Location = new System.Drawing.Point(13, 62);
            this.txtPath.Name = "txtPath";
            this.txtPath.ReadOnly = true;
            this.txtPath.Size = new System.Drawing.Size(224, 23);
            this.txtPath.TabIndex = 2;
            // 
            // lblPath
            // 
            this.lblPath.AutoSize = true;
            this.lblPath.Location = new System.Drawing.Point(10, 44);
            this.lblPath.Name = "lblPath";
            this.lblPath.Size = new System.Drawing.Size(31, 15);
            this.lblPath.TabIndex = 1;
            this.lblPath.Text = "Path";
            // 
            // chkActive
            // 
            this.chkActive.AutoSize = true;
            this.chkActive.Location = new System.Drawing.Point(13, 22);
            this.chkActive.Name = "chkActive";
            this.chkActive.Size = new System.Drawing.Size(59, 19);
            this.chkActive.TabIndex = 0;
            this.chkActive.Text = "Active";
            this.chkActive.UseVisualStyleBackColor = true;
            this.chkActive.CheckedChanged += new System.EventHandler(this.chkActive_CheckedChanged);
            // 
            // pbDataTypeWarning
            // 
            this.pbDataTypeWarning.BackColor = System.Drawing.SystemColors.Window;
            this.pbDataTypeWarning.Image = global::Scada.Comm.Drivers.DrvOpcClassic.View.Properties.Resources.warning;
            this.pbDataTypeWarning.Location = new System.Drawing.Point(201, 238);
            this.pbDataTypeWarning.Name = "pbDataTypeWarning";
            this.pbDataTypeWarning.Size = new System.Drawing.Size(16, 16);
            this.pbDataTypeWarning.TabIndex = 15;
            this.pbDataTypeWarning.TabStop = false;
            // 
            // CtrlItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gbItem);
            this.Name = "CtrlItem";
            this.Size = new System.Drawing.Size(250, 500);
            this.gbItem.ResumeLayout(false);
            this.gbItem.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numDataLen)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbDataTypeWarning)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbItem;
        private System.Windows.Forms.TextBox txtPath;
        private System.Windows.Forms.Label lblPath;
        private System.Windows.Forms.CheckBox chkActive;
        private System.Windows.Forms.CheckBox chkIsArray;
        private System.Windows.Forms.Label lblDataLen;
        private System.Windows.Forms.NumericUpDown numDataLen;
        private System.Windows.Forms.Label lblTagNum;
        private System.Windows.Forms.TextBox txtTagNum;
        private TextBox txtTagCode;
        private Label lblTagCode;
        private CheckBox chkIsString;
        private Label lblDataType;
        private TextBox txtName;
        private Label lblName;
        private ComboBox cbDataType;
        private PictureBox pbDataTypeWarning;
    }
}
