namespace Scada.Comm.Drivers.DrvOpcClassic.View.Controls
{
    partial class CtrlEventCategory
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
            this.gbEventCategory = new System.Windows.Forms.GroupBox();
            this.txtID = new System.Windows.Forms.TextBox();
            this.lblID = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.lblName = new System.Windows.Forms.Label();
            this.gbEventCategory.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbEventCategory
            // 
            this.gbEventCategory.Controls.Add(this.txtID);
            this.gbEventCategory.Controls.Add(this.lblID);
            this.gbEventCategory.Controls.Add(this.txtName);
            this.gbEventCategory.Controls.Add(this.lblName);
            this.gbEventCategory.Location = new System.Drawing.Point(0, 0);
            this.gbEventCategory.Name = "gbEventCategory";
            this.gbEventCategory.Padding = new System.Windows.Forms.Padding(10, 3, 10, 10);
            this.gbEventCategory.Size = new System.Drawing.Size(250, 500);
            this.gbEventCategory.TabIndex = 0;
            this.gbEventCategory.TabStop = false;
            this.gbEventCategory.Text = "Category Parameters";
            // 
            // txtID
            // 
            this.txtID.Location = new System.Drawing.Point(13, 81);
            this.txtID.Name = "txtID";
            this.txtID.ReadOnly = true;
            this.txtID.Size = new System.Drawing.Size(224, 23);
            this.txtID.TabIndex = 3;
            // 
            // lblID
            // 
            this.lblID.AutoSize = true;
            this.lblID.Location = new System.Drawing.Point(10, 63);
            this.lblID.Name = "lblID";
            this.lblID.Size = new System.Drawing.Size(18, 15);
            this.lblID.TabIndex = 2;
            this.lblID.Text = "ID";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(13, 37);
            this.txtName.Name = "txtName";
            this.txtName.ReadOnly = true;
            this.txtName.Size = new System.Drawing.Size(224, 23);
            this.txtName.TabIndex = 1;
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(10, 19);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(39, 15);
            this.lblName.TabIndex = 0;
            this.lblName.Text = "Name";
            // 
            // CtrlEventCategory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gbEventCategory);
            this.Name = "CtrlEventCategory";
            this.Size = new System.Drawing.Size(250, 500);
            this.gbEventCategory.ResumeLayout(false);
            this.gbEventCategory.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbEventCategory;
        private TextBox txtID;
        private Label lblID;
        private TextBox txtName;
        private Label lblName;
    }
}
