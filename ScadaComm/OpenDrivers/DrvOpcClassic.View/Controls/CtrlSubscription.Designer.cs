namespace Scada.Comm.Drivers.DrvOpcClassic.View.Controls
{
    partial class CtrlSubscription
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
            gbSubscription = new GroupBox();
            chkReadSync = new CheckBox();
            numDeadband = new NumericUpDown();
            lblDeadband = new Label();
            numKeepAlive = new NumericUpDown();
            lblKeepAlive = new Label();
            numUpdateRate = new NumericUpDown();
            lblUpdateRate = new Label();
            txtDisplayName = new TextBox();
            lblDisplayName = new Label();
            chkActive = new CheckBox();
            gbSubscription.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numDeadband).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numKeepAlive).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numUpdateRate).BeginInit();
            SuspendLayout();
            // 
            // gbSubscription
            // 
            gbSubscription.Controls.Add(chkReadSync);
            gbSubscription.Controls.Add(numDeadband);
            gbSubscription.Controls.Add(lblDeadband);
            gbSubscription.Controls.Add(numKeepAlive);
            gbSubscription.Controls.Add(lblKeepAlive);
            gbSubscription.Controls.Add(numUpdateRate);
            gbSubscription.Controls.Add(lblUpdateRate);
            gbSubscription.Controls.Add(txtDisplayName);
            gbSubscription.Controls.Add(lblDisplayName);
            gbSubscription.Controls.Add(chkActive);
            gbSubscription.Location = new Point(0, 0);
            gbSubscription.Name = "gbSubscription";
            gbSubscription.Padding = new Padding(10, 3, 10, 10);
            gbSubscription.Size = new Size(250, 500);
            gbSubscription.TabIndex = 0;
            gbSubscription.TabStop = false;
            gbSubscription.Text = "Subscription Parameters";
            // 
            // chkReadSync
            // 
            chkReadSync.AutoSize = true;
            chkReadSync.Location = new Point(13, 225);
            chkReadSync.Name = "chkReadSync";
            chkReadSync.Size = new Size(132, 19);
            chkReadSync.TabIndex = 9;
            chkReadSync.Text = "Read synchronously";
            chkReadSync.UseVisualStyleBackColor = true;
            chkReadSync.CheckedChanged += chkReadSync_CheckedChanged;
            // 
            // numDeadband
            // 
            numDeadband.DecimalPlaces = 2;
            numDeadband.Location = new Point(13, 194);
            numDeadband.Name = "numDeadband";
            numDeadband.Size = new Size(120, 23);
            numDeadband.TabIndex = 8;
            numDeadband.ValueChanged += numDeadband_ValueChanged;
            // 
            // lblDeadband
            // 
            lblDeadband.AutoSize = true;
            lblDeadband.Location = new Point(10, 176);
            lblDeadband.Name = "lblDeadband";
            lblDeadband.Size = new Size(77, 15);
            lblDeadband.TabIndex = 7;
            lblDeadband.Text = "Deadband, %";
            // 
            // numKeepAlive
            // 
            numKeepAlive.Location = new Point(13, 150);
            numKeepAlive.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            numKeepAlive.Name = "numKeepAlive";
            numKeepAlive.Size = new Size(120, 23);
            numKeepAlive.TabIndex = 6;
            numKeepAlive.ValueChanged += numKeepAlive_ValueChanged;
            // 
            // lblKeepAlive
            // 
            lblKeepAlive.AutoSize = true;
            lblKeepAlive.Location = new Point(10, 132);
            lblKeepAlive.Name = "lblKeepAlive";
            lblKeepAlive.Size = new Size(82, 15);
            lblKeepAlive.TabIndex = 5;
            lblKeepAlive.Text = "Keep alive, ms";
            // 
            // numUpdateRate
            // 
            numUpdateRate.Location = new Point(13, 106);
            numUpdateRate.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            numUpdateRate.Name = "numUpdateRate";
            numUpdateRate.Size = new Size(120, 23);
            numUpdateRate.TabIndex = 4;
            numUpdateRate.ValueChanged += numUpdateRate_ValueChanged;
            // 
            // lblUpdateRate
            // 
            lblUpdateRate.AutoSize = true;
            lblUpdateRate.Location = new Point(10, 88);
            lblUpdateRate.Name = "lblUpdateRate";
            lblUpdateRate.Size = new Size(90, 15);
            lblUpdateRate.TabIndex = 3;
            lblUpdateRate.Text = "Update rate, ms";
            // 
            // txtDisplayName
            // 
            txtDisplayName.Location = new Point(13, 62);
            txtDisplayName.Name = "txtDisplayName";
            txtDisplayName.Size = new Size(224, 23);
            txtDisplayName.TabIndex = 2;
            txtDisplayName.TextChanged += txtDisplayName_TextChanged;
            // 
            // lblDisplayName
            // 
            lblDisplayName.AutoSize = true;
            lblDisplayName.Location = new Point(10, 44);
            lblDisplayName.Name = "lblDisplayName";
            lblDisplayName.Size = new Size(78, 15);
            lblDisplayName.TabIndex = 1;
            lblDisplayName.Text = "Display name";
            // 
            // chkActive
            // 
            chkActive.AutoSize = true;
            chkActive.Location = new Point(13, 22);
            chkActive.Name = "chkActive";
            chkActive.Size = new Size(59, 19);
            chkActive.TabIndex = 0;
            chkActive.Text = "Active";
            chkActive.UseVisualStyleBackColor = true;
            chkActive.CheckedChanged += chkActive_CheckedChanged;
            // 
            // CtrlSubscription
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(gbSubscription);
            Name = "CtrlSubscription";
            Size = new Size(250, 500);
            gbSubscription.ResumeLayout(false);
            gbSubscription.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numDeadband).EndInit();
            ((System.ComponentModel.ISupportInitialize)numKeepAlive).EndInit();
            ((System.ComponentModel.ISupportInitialize)numUpdateRate).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox gbSubscription;
        private CheckBox chkActive;
        private NumericUpDown numUpdateRate;
        private Label lblUpdateRate;
        private TextBox txtDisplayName;
        private Label lblDisplayName;
        private NumericUpDown numKeepAlive;
        private Label lblKeepAlive;
        private Label lblDeadband;
        private NumericUpDown numDeadband;
        private CheckBox chkReadSync;
    }
}
