namespace Scada.Comm.Drivers.DrvOpcClassic.View.Controls
{
    partial class CtrlEventSubscription
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
            this.gbSubscription = new System.Windows.Forms.GroupBox();
            this.numLowSeverity = new System.Windows.Forms.NumericUpDown();
            this.lblLowSeverity = new System.Windows.Forms.Label();
            this.numHighSeverity = new System.Windows.Forms.NumericUpDown();
            this.lblHighSeverity = new System.Windows.Forms.Label();
            this.chkConditionEvents = new System.Windows.Forms.CheckBox();
            this.chkTrackingEvents = new System.Windows.Forms.CheckBox();
            this.chkSimpleEvents = new System.Windows.Forms.CheckBox();
            this.numMaxSize = new System.Windows.Forms.NumericUpDown();
            this.lblMaxSize = new System.Windows.Forms.Label();
            this.numKeepAlive = new System.Windows.Forms.NumericUpDown();
            this.lblKeepAlive = new System.Windows.Forms.Label();
            this.numUpdateRate = new System.Windows.Forms.NumericUpDown();
            this.lblUpdateRate = new System.Windows.Forms.Label();
            this.txtDisplayName = new System.Windows.Forms.TextBox();
            this.lblDisplayName = new System.Windows.Forms.Label();
            this.chkActive = new System.Windows.Forms.CheckBox();
            this.gbSubscription.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numLowSeverity)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numHighSeverity)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numKeepAlive)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUpdateRate)).BeginInit();
            this.SuspendLayout();
            // 
            // gbSubscription
            // 
            this.gbSubscription.Controls.Add(this.numLowSeverity);
            this.gbSubscription.Controls.Add(this.lblLowSeverity);
            this.gbSubscription.Controls.Add(this.numHighSeverity);
            this.gbSubscription.Controls.Add(this.lblHighSeverity);
            this.gbSubscription.Controls.Add(this.chkConditionEvents);
            this.gbSubscription.Controls.Add(this.chkTrackingEvents);
            this.gbSubscription.Controls.Add(this.chkSimpleEvents);
            this.gbSubscription.Controls.Add(this.numMaxSize);
            this.gbSubscription.Controls.Add(this.lblMaxSize);
            this.gbSubscription.Controls.Add(this.numKeepAlive);
            this.gbSubscription.Controls.Add(this.lblKeepAlive);
            this.gbSubscription.Controls.Add(this.numUpdateRate);
            this.gbSubscription.Controls.Add(this.lblUpdateRate);
            this.gbSubscription.Controls.Add(this.txtDisplayName);
            this.gbSubscription.Controls.Add(this.lblDisplayName);
            this.gbSubscription.Controls.Add(this.chkActive);
            this.gbSubscription.Location = new System.Drawing.Point(0, 0);
            this.gbSubscription.Name = "gbSubscription";
            this.gbSubscription.Padding = new System.Windows.Forms.Padding(10, 3, 10, 10);
            this.gbSubscription.Size = new System.Drawing.Size(250, 500);
            this.gbSubscription.TabIndex = 0;
            this.gbSubscription.TabStop = false;
            this.gbSubscription.Text = "Subscription Parameters";
            // 
            // numLowSeverity
            // 
            this.numLowSeverity.Location = new System.Drawing.Point(13, 357);
            this.numLowSeverity.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numLowSeverity.Name = "numLowSeverity";
            this.numLowSeverity.Size = new System.Drawing.Size(120, 23);
            this.numLowSeverity.TabIndex = 15;
            this.numLowSeverity.ValueChanged += new System.EventHandler(this.numLowSeverity_ValueChanged);
            // 
            // lblLowSeverity
            // 
            this.lblLowSeverity.AutoSize = true;
            this.lblLowSeverity.Location = new System.Drawing.Point(10, 339);
            this.lblLowSeverity.Name = "lblLowSeverity";
            this.lblLowSeverity.Size = new System.Drawing.Size(72, 15);
            this.lblLowSeverity.TabIndex = 14;
            this.lblLowSeverity.Text = "Low severity";
            // 
            // numHighSeverity
            // 
            this.numHighSeverity.Location = new System.Drawing.Point(13, 313);
            this.numHighSeverity.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numHighSeverity.Name = "numHighSeverity";
            this.numHighSeverity.Size = new System.Drawing.Size(120, 23);
            this.numHighSeverity.TabIndex = 13;
            this.numHighSeverity.ValueChanged += new System.EventHandler(this.numHighSeverity_ValueChanged);
            // 
            // lblHighSeverity
            // 
            this.lblHighSeverity.AutoSize = true;
            this.lblHighSeverity.Location = new System.Drawing.Point(10, 295);
            this.lblHighSeverity.Name = "lblHighSeverity";
            this.lblHighSeverity.Size = new System.Drawing.Size(76, 15);
            this.lblHighSeverity.TabIndex = 12;
            this.lblHighSeverity.Text = "High severity";
            // 
            // chkConditionEvents
            // 
            this.chkConditionEvents.AutoSize = true;
            this.chkConditionEvents.Location = new System.Drawing.Point(13, 273);
            this.chkConditionEvents.Name = "chkConditionEvents";
            this.chkConditionEvents.Size = new System.Drawing.Size(116, 19);
            this.chkConditionEvents.TabIndex = 11;
            this.chkConditionEvents.Text = "Condition events";
            this.chkConditionEvents.UseVisualStyleBackColor = true;
            this.chkConditionEvents.CheckedChanged += new System.EventHandler(this.chkConditionEvents_CheckedChanged);
            // 
            // chkTrackingEvents
            // 
            this.chkTrackingEvents.AutoSize = true;
            this.chkTrackingEvents.Location = new System.Drawing.Point(13, 248);
            this.chkTrackingEvents.Name = "chkTrackingEvents";
            this.chkTrackingEvents.Size = new System.Drawing.Size(107, 19);
            this.chkTrackingEvents.TabIndex = 10;
            this.chkTrackingEvents.Text = "Tracking events";
            this.chkTrackingEvents.UseVisualStyleBackColor = true;
            this.chkTrackingEvents.CheckedChanged += new System.EventHandler(this.chkTrackingEvents_CheckedChanged);
            // 
            // chkSimpleEvents
            // 
            this.chkSimpleEvents.AutoSize = true;
            this.chkSimpleEvents.Location = new System.Drawing.Point(13, 223);
            this.chkSimpleEvents.Name = "chkSimpleEvents";
            this.chkSimpleEvents.Size = new System.Drawing.Size(99, 19);
            this.chkSimpleEvents.TabIndex = 9;
            this.chkSimpleEvents.Text = "Simple events";
            this.chkSimpleEvents.UseVisualStyleBackColor = true;
            this.chkSimpleEvents.CheckedChanged += new System.EventHandler(this.chkSimpleEvents_CheckedChanged);
            // 
            // numMaxSize
            // 
            this.numMaxSize.Location = new System.Drawing.Point(13, 194);
            this.numMaxSize.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numMaxSize.Name = "numMaxSize";
            this.numMaxSize.Size = new System.Drawing.Size(120, 23);
            this.numMaxSize.TabIndex = 8;
            this.numMaxSize.ValueChanged += new System.EventHandler(this.numMaxSize_ValueChanged);
            // 
            // lblMaxSize
            // 
            this.lblMaxSize.AutoSize = true;
            this.lblMaxSize.Location = new System.Drawing.Point(10, 176);
            this.lblMaxSize.Name = "lblMaxSize";
            this.lblMaxSize.Size = new System.Drawing.Size(84, 15);
            this.lblMaxSize.TabIndex = 7;
            this.lblMaxSize.Text = "Maximum size";
            // 
            // numKeepAlive
            // 
            this.numKeepAlive.Location = new System.Drawing.Point(13, 150);
            this.numKeepAlive.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numKeepAlive.Name = "numKeepAlive";
            this.numKeepAlive.Size = new System.Drawing.Size(120, 23);
            this.numKeepAlive.TabIndex = 6;
            this.numKeepAlive.ValueChanged += new System.EventHandler(this.numKeepAlive_ValueChanged);
            // 
            // lblKeepAlive
            // 
            this.lblKeepAlive.AutoSize = true;
            this.lblKeepAlive.Location = new System.Drawing.Point(10, 132);
            this.lblKeepAlive.Name = "lblKeepAlive";
            this.lblKeepAlive.Size = new System.Drawing.Size(82, 15);
            this.lblKeepAlive.TabIndex = 5;
            this.lblKeepAlive.Text = "Keep alive, ms";
            // 
            // numUpdateRate
            // 
            this.numUpdateRate.Location = new System.Drawing.Point(13, 106);
            this.numUpdateRate.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numUpdateRate.Name = "numUpdateRate";
            this.numUpdateRate.Size = new System.Drawing.Size(120, 23);
            this.numUpdateRate.TabIndex = 4;
            this.numUpdateRate.ValueChanged += new System.EventHandler(this.numUpdateRate_ValueChanged);
            // 
            // lblUpdateRate
            // 
            this.lblUpdateRate.AutoSize = true;
            this.lblUpdateRate.Location = new System.Drawing.Point(10, 88);
            this.lblUpdateRate.Name = "lblUpdateRate";
            this.lblUpdateRate.Size = new System.Drawing.Size(90, 15);
            this.lblUpdateRate.TabIndex = 3;
            this.lblUpdateRate.Text = "Update rate, ms";
            // 
            // txtDisplayName
            // 
            this.txtDisplayName.Location = new System.Drawing.Point(13, 62);
            this.txtDisplayName.Name = "txtDisplayName";
            this.txtDisplayName.Size = new System.Drawing.Size(224, 23);
            this.txtDisplayName.TabIndex = 2;
            this.txtDisplayName.TextChanged += new System.EventHandler(this.txtDisplayName_TextChanged);
            // 
            // lblDisplayName
            // 
            this.lblDisplayName.AutoSize = true;
            this.lblDisplayName.Location = new System.Drawing.Point(10, 44);
            this.lblDisplayName.Name = "lblDisplayName";
            this.lblDisplayName.Size = new System.Drawing.Size(78, 15);
            this.lblDisplayName.TabIndex = 1;
            this.lblDisplayName.Text = "Display name";
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
            // CtrlEventSubscription
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gbSubscription);
            this.Name = "CtrlEventSubscription";
            this.Size = new System.Drawing.Size(250, 500);
            this.gbSubscription.ResumeLayout(false);
            this.gbSubscription.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numLowSeverity)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numHighSeverity)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numKeepAlive)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUpdateRate)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbSubscription;
        private System.Windows.Forms.CheckBox chkActive;
        private System.Windows.Forms.NumericUpDown numUpdateRate;
        private System.Windows.Forms.Label lblUpdateRate;
        private System.Windows.Forms.TextBox txtDisplayName;
        private System.Windows.Forms.Label lblDisplayName;
        private NumericUpDown numKeepAlive;
        private Label lblKeepAlive;
        private NumericUpDown numMaxSize;
        private Label lblMaxSize;
        private CheckBox chkConditionEvents;
        private CheckBox chkTrackingEvents;
        private CheckBox chkSimpleEvents;
        private NumericUpDown numLowSeverity;
        private Label lblLowSeverity;
        private NumericUpDown numHighSeverity;
        private Label lblHighSeverity;
    }
}
