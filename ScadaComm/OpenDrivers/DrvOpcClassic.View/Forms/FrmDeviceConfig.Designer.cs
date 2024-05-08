namespace Scada.Comm.Drivers.DrvOpcClassic.View.Forms
{
    partial class FrmDeviceConfig
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
            this.components = new System.ComponentModel.Container();
            this.gbConnectionOptions = new System.Windows.Forms.GroupBox();
            this.lblConnectionInfo = new System.Windows.Forms.Label();
            this.pbConnectionInfo = new System.Windows.Forms.PictureBox();
            this.btnSelectServer = new System.Windows.Forms.Button();
            this.txtAeSpec = new System.Windows.Forms.TextBox();
            this.lblAeSpec = new System.Windows.Forms.Label();
            this.txtDaSpec = new System.Windows.Forms.TextBox();
            this.lblDaSpec = new System.Windows.Forms.Label();
            this.txtServerPath = new System.Windows.Forms.TextBox();
            this.lblServerPath = new System.Windows.Forms.Label();
            this.btnShowNetworkOptions = new System.Windows.Forms.Button();
            this.txtHost = new System.Windows.Forms.TextBox();
            this.chkHost = new System.Windows.Forms.CheckBox();
            this.lblHost = new System.Windows.Forms.Label();
            this.pnlBottom = new System.Windows.Forms.Panel();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.gbServerBrowse = new System.Windows.Forms.GroupBox();
            this.tvServer = new System.Windows.Forms.TreeView();
            this.ilTree = new System.Windows.Forms.ImageList(this.components);
            this.btnOpenFile = new System.Windows.Forms.Button();
            this.btnDisconnect = new System.Windows.Forms.Button();
            this.btnConnect = new System.Windows.Forms.Button();
            this.gbDevice = new System.Windows.Forms.GroupBox();
            this.tvDevice = new System.Windows.Forms.TreeView();
            this.btnDeleteItem = new System.Windows.Forms.Button();
            this.btnMoveDownItem = new System.Windows.Forms.Button();
            this.btnMoveUpItem = new System.Windows.Forms.Button();
            this.btnAddItem = new System.Windows.Forms.Button();
            this.btnAddSubscription = new System.Windows.Forms.Button();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.ctrlSubscription = new Scada.Comm.Drivers.DrvOpcClassic.View.Controls.CtrlSubscription();
            this.ctrlItem = new Scada.Comm.Drivers.DrvOpcClassic.View.Controls.CtrlItem();
            this.ctrlCommand = new Scada.Comm.Drivers.DrvOpcClassic.View.Controls.CtrlCommand();
            this.ctrlEventSubscription = new Scada.Comm.Drivers.DrvOpcClassic.View.Controls.CtrlEventSubscription();
            this.ctrlEmptyItem = new Scada.Comm.Drivers.DrvOpcClassic.View.Controls.CtrlEmptyItem();
            this.ctrlEventCategory = new Scada.Comm.Drivers.DrvOpcClassic.View.Controls.CtrlEventCategory();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.gbConnectionOptions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbConnectionInfo)).BeginInit();
            this.pnlBottom.SuspendLayout();
            this.gbServerBrowse.SuspendLayout();
            this.gbDevice.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbConnectionOptions
            // 
            this.gbConnectionOptions.Controls.Add(this.lblConnectionInfo);
            this.gbConnectionOptions.Controls.Add(this.pbConnectionInfo);
            this.gbConnectionOptions.Controls.Add(this.btnSelectServer);
            this.gbConnectionOptions.Controls.Add(this.txtAeSpec);
            this.gbConnectionOptions.Controls.Add(this.lblAeSpec);
            this.gbConnectionOptions.Controls.Add(this.txtDaSpec);
            this.gbConnectionOptions.Controls.Add(this.lblDaSpec);
            this.gbConnectionOptions.Controls.Add(this.txtServerPath);
            this.gbConnectionOptions.Controls.Add(this.lblServerPath);
            this.gbConnectionOptions.Controls.Add(this.btnShowNetworkOptions);
            this.gbConnectionOptions.Controls.Add(this.txtHost);
            this.gbConnectionOptions.Controls.Add(this.chkHost);
            this.gbConnectionOptions.Controls.Add(this.lblHost);
            this.gbConnectionOptions.Location = new System.Drawing.Point(12, 12);
            this.gbConnectionOptions.Name = "gbConnectionOptions";
            this.gbConnectionOptions.Padding = new System.Windows.Forms.Padding(10, 3, 10, 10);
            this.gbConnectionOptions.Size = new System.Drawing.Size(860, 139);
            this.gbConnectionOptions.TabIndex = 0;
            this.gbConnectionOptions.TabStop = false;
            this.gbConnectionOptions.Text = "Connection Options";
            // 
            // lblConnectionInfo
            // 
            this.lblConnectionInfo.AutoSize = true;
            this.lblConnectionInfo.ForeColor = System.Drawing.SystemColors.GrayText;
            this.lblConnectionInfo.Location = new System.Drawing.Point(35, 111);
            this.lblConnectionInfo.Name = "lblConnectionInfo";
            this.lblConnectionInfo.Size = new System.Drawing.Size(330, 15);
            this.lblConnectionInfo.TabIndex = 11;
            this.lblConnectionInfo.Text = "Connection options are common to the communication line.";
            // 
            // pbConnectionInfo
            // 
            this.pbConnectionInfo.Image = global::Scada.Comm.Drivers.DrvOpcClassic.View.Properties.Resources.info;
            this.pbConnectionInfo.Location = new System.Drawing.Point(13, 110);
            this.pbConnectionInfo.Name = "pbConnectionInfo";
            this.pbConnectionInfo.Size = new System.Drawing.Size(16, 16);
            this.pbConnectionInfo.TabIndex = 10;
            this.pbConnectionInfo.TabStop = false;
            // 
            // btnSelectServer
            // 
            this.btnSelectServer.Location = new System.Drawing.Point(757, 81);
            this.btnSelectServer.Name = "btnSelectServer";
            this.btnSelectServer.Size = new System.Drawing.Size(90, 23);
            this.btnSelectServer.TabIndex = 10;
            this.btnSelectServer.Text = "Select...";
            this.btnSelectServer.UseVisualStyleBackColor = true;
            this.btnSelectServer.Click += new System.EventHandler(this.btnSelectServer_Click);
            // 
            // txtAeSpec
            // 
            this.txtAeSpec.Location = new System.Drawing.Point(611, 81);
            this.txtAeSpec.Name = "txtAeSpec";
            this.txtAeSpec.ReadOnly = true;
            this.txtAeSpec.Size = new System.Drawing.Size(140, 23);
            this.txtAeSpec.TabIndex = 9;
            // 
            // lblAeSpec
            // 
            this.lblAeSpec.AutoSize = true;
            this.lblAeSpec.Location = new System.Drawing.Point(608, 63);
            this.lblAeSpec.Name = "lblAeSpec";
            this.lblAeSpec.Size = new System.Drawing.Size(91, 15);
            this.lblAeSpec.TabIndex = 8;
            this.lblAeSpec.Text = "AE specification";
            // 
            // txtDaSpec
            // 
            this.txtDaSpec.Location = new System.Drawing.Point(465, 81);
            this.txtDaSpec.Name = "txtDaSpec";
            this.txtDaSpec.ReadOnly = true;
            this.txtDaSpec.Size = new System.Drawing.Size(140, 23);
            this.txtDaSpec.TabIndex = 7;
            // 
            // lblDaSpec
            // 
            this.lblDaSpec.AutoSize = true;
            this.lblDaSpec.Location = new System.Drawing.Point(462, 63);
            this.lblDaSpec.Name = "lblDaSpec";
            this.lblDaSpec.Size = new System.Drawing.Size(93, 15);
            this.lblDaSpec.TabIndex = 6;
            this.lblDaSpec.Text = "DA specification";
            // 
            // txtServerPath
            // 
            this.txtServerPath.Location = new System.Drawing.Point(13, 81);
            this.txtServerPath.Name = "txtServerPath";
            this.txtServerPath.ReadOnly = true;
            this.txtServerPath.Size = new System.Drawing.Size(446, 23);
            this.txtServerPath.TabIndex = 5;
            // 
            // lblServerPath
            // 
            this.lblServerPath.AutoSize = true;
            this.lblServerPath.Location = new System.Drawing.Point(10, 63);
            this.lblServerPath.Name = "lblServerPath";
            this.lblServerPath.Size = new System.Drawing.Size(65, 15);
            this.lblServerPath.TabIndex = 4;
            this.lblServerPath.Text = "OPC server";
            // 
            // btnShowNetworkOptions
            // 
            this.btnShowNetworkOptions.Location = new System.Drawing.Point(757, 37);
            this.btnShowNetworkOptions.Name = "btnShowNetworkOptions";
            this.btnShowNetworkOptions.Size = new System.Drawing.Size(90, 23);
            this.btnShowNetworkOptions.TabIndex = 3;
            this.btnShowNetworkOptions.Text = "Options";
            this.btnShowNetworkOptions.UseVisualStyleBackColor = true;
            this.btnShowNetworkOptions.Click += new System.EventHandler(this.btnShowNetworkOptions_Click);
            // 
            // txtHost
            // 
            this.txtHost.Location = new System.Drawing.Point(34, 37);
            this.txtHost.Name = "txtHost";
            this.txtHost.Size = new System.Drawing.Size(717, 23);
            this.txtHost.TabIndex = 2;
            this.txtHost.TextChanged += new System.EventHandler(this.txtHost_TextChanged);
            // 
            // chkHost
            // 
            this.chkHost.AutoSize = true;
            this.chkHost.Location = new System.Drawing.Point(13, 41);
            this.chkHost.Name = "chkHost";
            this.chkHost.Size = new System.Drawing.Size(15, 14);
            this.chkHost.TabIndex = 1;
            this.chkHost.UseVisualStyleBackColor = true;
            this.chkHost.CheckedChanged += new System.EventHandler(this.chkHost_CheckedChanged);
            // 
            // lblHost
            // 
            this.lblHost.AutoSize = true;
            this.lblHost.Location = new System.Drawing.Point(10, 19);
            this.lblHost.Name = "lblHost";
            this.lblHost.Size = new System.Drawing.Size(74, 15);
            this.lblHost.TabIndex = 0;
            this.lblHost.Text = "Remote host";
            // 
            // pnlBottom
            // 
            this.pnlBottom.Controls.Add(this.btnClose);
            this.pnlBottom.Controls.Add(this.btnSave);
            this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBottom.Location = new System.Drawing.Point(0, 663);
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.Size = new System.Drawing.Size(884, 45);
            this.pnlBottom.TabIndex = 9;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(716, 10);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(797, 10);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            // 
            // gbServerBrowse
            // 
            this.gbServerBrowse.Controls.Add(this.tvServer);
            this.gbServerBrowse.Controls.Add(this.btnOpenFile);
            this.gbServerBrowse.Controls.Add(this.btnDisconnect);
            this.gbServerBrowse.Controls.Add(this.btnConnect);
            this.gbServerBrowse.Location = new System.Drawing.Point(12, 157);
            this.gbServerBrowse.Name = "gbServerBrowse";
            this.gbServerBrowse.Padding = new System.Windows.Forms.Padding(10, 3, 10, 10);
            this.gbServerBrowse.Size = new System.Drawing.Size(299, 500);
            this.gbServerBrowse.TabIndex = 1;
            this.gbServerBrowse.TabStop = false;
            this.gbServerBrowse.Text = "Server Browse";
            // 
            // tvServer
            // 
            this.tvServer.HideSelection = false;
            this.tvServer.ImageIndex = 0;
            this.tvServer.ImageList = this.ilTree;
            this.tvServer.Location = new System.Drawing.Point(13, 51);
            this.tvServer.Name = "tvServer";
            this.tvServer.SelectedImageIndex = 0;
            this.tvServer.Size = new System.Drawing.Size(273, 436);
            this.tvServer.TabIndex = 3;
            this.tvServer.AfterCollapse += new System.Windows.Forms.TreeViewEventHandler(this.tvServer_AfterCollapse);
            this.tvServer.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.tvServer_BeforeExpand);
            this.tvServer.AfterExpand += new System.Windows.Forms.TreeViewEventHandler(this.tvServer_AfterExpand);
            this.tvServer.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvServer_AfterSelect);
            this.tvServer.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tvServer_NodeMouseDoubleClick);
            this.tvServer.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tvServer_KeyDown);
            // 
            // ilTree
            // 
            this.ilTree.ColorDepth = System.Windows.Forms.ColorDepth.Depth24Bit;
            this.ilTree.ImageSize = new System.Drawing.Size(16, 16);
            this.ilTree.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // btnOpenFile
            // 
            this.btnOpenFile.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnOpenFile.Image = global::Scada.Comm.Drivers.DrvOpcClassic.View.Properties.Resources.open;
            this.btnOpenFile.Location = new System.Drawing.Point(71, 22);
            this.btnOpenFile.Name = "btnOpenFile";
            this.btnOpenFile.Size = new System.Drawing.Size(23, 23);
            this.btnOpenFile.TabIndex = 2;
            this.toolTip.SetToolTip(this.btnOpenFile, "Open File");
            this.btnOpenFile.UseVisualStyleBackColor = true;
            this.btnOpenFile.Click += new System.EventHandler(this.btnOpenFile_Click);
            // 
            // btnDisconnect
            // 
            this.btnDisconnect.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnDisconnect.Image = global::Scada.Comm.Drivers.DrvOpcClassic.View.Properties.Resources.disconnect;
            this.btnDisconnect.Location = new System.Drawing.Point(42, 22);
            this.btnDisconnect.Name = "btnDisconnect";
            this.btnDisconnect.Size = new System.Drawing.Size(23, 23);
            this.btnDisconnect.TabIndex = 1;
            this.toolTip.SetToolTip(this.btnDisconnect, "Disconnect");
            this.btnDisconnect.UseVisualStyleBackColor = true;
            this.btnDisconnect.Click += new System.EventHandler(this.btnDisconnect_Click);
            // 
            // btnConnect
            // 
            this.btnConnect.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnConnect.Image = global::Scada.Comm.Drivers.DrvOpcClassic.View.Properties.Resources.connect;
            this.btnConnect.Location = new System.Drawing.Point(13, 22);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(23, 23);
            this.btnConnect.TabIndex = 0;
            this.toolTip.SetToolTip(this.btnConnect, "Connect");
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // gbDevice
            // 
            this.gbDevice.Controls.Add(this.tvDevice);
            this.gbDevice.Controls.Add(this.btnDeleteItem);
            this.gbDevice.Controls.Add(this.btnMoveDownItem);
            this.gbDevice.Controls.Add(this.btnMoveUpItem);
            this.gbDevice.Controls.Add(this.btnAddItem);
            this.gbDevice.Controls.Add(this.btnAddSubscription);
            this.gbDevice.Location = new System.Drawing.Point(317, 157);
            this.gbDevice.Name = "gbDevice";
            this.gbDevice.Padding = new System.Windows.Forms.Padding(10, 3, 10, 10);
            this.gbDevice.Size = new System.Drawing.Size(299, 500);
            this.gbDevice.TabIndex = 2;
            this.gbDevice.TabStop = false;
            this.gbDevice.Text = "Device Configuration";
            // 
            // tvDevice
            // 
            this.tvDevice.HideSelection = false;
            this.tvDevice.ImageIndex = 0;
            this.tvDevice.ImageList = this.ilTree;
            this.tvDevice.Location = new System.Drawing.Point(13, 51);
            this.tvDevice.Name = "tvDevice";
            this.tvDevice.SelectedImageIndex = 0;
            this.tvDevice.Size = new System.Drawing.Size(273, 436);
            this.tvDevice.TabIndex = 5;
            this.tvDevice.AfterCollapse += new System.Windows.Forms.TreeViewEventHandler(this.tvDevice_AfterCollapse);
            this.tvDevice.AfterExpand += new System.Windows.Forms.TreeViewEventHandler(this.tvDevice_AfterExpand);
            this.tvDevice.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvDevice_AfterSelect);
            // 
            // btnDeleteItem
            // 
            this.btnDeleteItem.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnDeleteItem.Image = global::Scada.Comm.Drivers.DrvOpcClassic.View.Properties.Resources.delete;
            this.btnDeleteItem.Location = new System.Drawing.Point(129, 22);
            this.btnDeleteItem.Name = "btnDeleteItem";
            this.btnDeleteItem.Size = new System.Drawing.Size(23, 23);
            this.btnDeleteItem.TabIndex = 4;
            this.toolTip.SetToolTip(this.btnDeleteItem, "Delete");
            this.btnDeleteItem.UseVisualStyleBackColor = true;
            this.btnDeleteItem.Click += new System.EventHandler(this.btnDeleteItem_Click);
            // 
            // btnMoveDownItem
            // 
            this.btnMoveDownItem.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnMoveDownItem.Image = global::Scada.Comm.Drivers.DrvOpcClassic.View.Properties.Resources.move_down;
            this.btnMoveDownItem.Location = new System.Drawing.Point(100, 22);
            this.btnMoveDownItem.Name = "btnMoveDownItem";
            this.btnMoveDownItem.Size = new System.Drawing.Size(23, 23);
            this.btnMoveDownItem.TabIndex = 3;
            this.toolTip.SetToolTip(this.btnMoveDownItem, "Move Down");
            this.btnMoveDownItem.UseVisualStyleBackColor = true;
            this.btnMoveDownItem.Click += new System.EventHandler(this.btnMoveDownItem_Click);
            // 
            // btnMoveUpItem
            // 
            this.btnMoveUpItem.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnMoveUpItem.Image = global::Scada.Comm.Drivers.DrvOpcClassic.View.Properties.Resources.move_up;
            this.btnMoveUpItem.Location = new System.Drawing.Point(71, 22);
            this.btnMoveUpItem.Name = "btnMoveUpItem";
            this.btnMoveUpItem.Size = new System.Drawing.Size(23, 23);
            this.btnMoveUpItem.TabIndex = 2;
            this.toolTip.SetToolTip(this.btnMoveUpItem, "Move Up");
            this.btnMoveUpItem.UseVisualStyleBackColor = true;
            this.btnMoveUpItem.Click += new System.EventHandler(this.btnMoveUpItem_Click);
            // 
            // btnAddItem
            // 
            this.btnAddItem.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnAddItem.Image = global::Scada.Comm.Drivers.DrvOpcClassic.View.Properties.Resources.add;
            this.btnAddItem.Location = new System.Drawing.Point(42, 22);
            this.btnAddItem.Name = "btnAddItem";
            this.btnAddItem.Size = new System.Drawing.Size(23, 23);
            this.btnAddItem.TabIndex = 0;
            this.toolTip.SetToolTip(this.btnAddItem, "Add Selected Item");
            this.btnAddItem.UseVisualStyleBackColor = true;
            this.btnAddItem.Click += new System.EventHandler(this.btnAddItem_Click);
            // 
            // btnAddSubscription
            // 
            this.btnAddSubscription.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnAddSubscription.Image = global::Scada.Comm.Drivers.DrvOpcClassic.View.Properties.Resources.folder_add;
            this.btnAddSubscription.Location = new System.Drawing.Point(13, 22);
            this.btnAddSubscription.Name = "btnAddSubscription";
            this.btnAddSubscription.Size = new System.Drawing.Size(23, 23);
            this.btnAddSubscription.TabIndex = 1;
            this.toolTip.SetToolTip(this.btnAddSubscription, "Add Subscription");
            this.btnAddSubscription.UseVisualStyleBackColor = true;
            this.btnAddSubscription.Click += new System.EventHandler(this.btnAddSubscription_Click);
            // 
            // ctrlSubscription
            // 
            this.ctrlSubscription.Location = new System.Drawing.Point(622, 220);
            this.ctrlSubscription.Name = "ctrlSubscription";
            this.ctrlSubscription.Size = new System.Drawing.Size(250, 500);
            this.ctrlSubscription.TabIndex = 4;
            this.ctrlSubscription.ObjectChanged += new System.EventHandler<Scada.Forms.ObjectChangedEventArgs>(this.ctrlItem_ObjectChanged);
            // 
            // ctrlItem
            // 
            this.ctrlItem.Location = new System.Drawing.Point(622, 270);
            this.ctrlItem.Name = "ctrlItem";
            this.ctrlItem.Size = new System.Drawing.Size(250, 500);
            this.ctrlItem.TabIndex = 5;
            this.ctrlItem.ObjectChanged += new System.EventHandler<Scada.Forms.ObjectChangedEventArgs>(this.ctrlItem_ObjectChanged);
            // 
            // ctrlCommand
            // 
            this.ctrlCommand.Location = new System.Drawing.Point(622, 320);
            this.ctrlCommand.Name = "ctrlCommand";
            this.ctrlCommand.Size = new System.Drawing.Size(250, 500);
            this.ctrlCommand.TabIndex = 6;
            this.ctrlCommand.ObjectChanged += new System.EventHandler<Scada.Forms.ObjectChangedEventArgs>(this.ctrlItem_ObjectChanged);
            // 
            // ctrlEventSubscription
            // 
            this.ctrlEventSubscription.Location = new System.Drawing.Point(622, 370);
            this.ctrlEventSubscription.Name = "ctrlEventSubscription";
            this.ctrlEventSubscription.Size = new System.Drawing.Size(250, 500);
            this.ctrlEventSubscription.TabIndex = 7;
            this.ctrlEventSubscription.ObjectChanged += new System.EventHandler<Scada.Forms.ObjectChangedEventArgs>(this.ctrlItem_ObjectChanged);
            // 
            // ctrlEmptyItem
            // 
            this.ctrlEmptyItem.Location = new System.Drawing.Point(622, 157);
            this.ctrlEmptyItem.Name = "ctrlEmptyItem";
            this.ctrlEmptyItem.Size = new System.Drawing.Size(250, 500);
            this.ctrlEmptyItem.TabIndex = 12;
            // 
            // ctrlEventCategory
            // 
            this.ctrlEventCategory.Location = new System.Drawing.Point(622, 420);
            this.ctrlEventCategory.Name = "ctrlEventCategory";
            this.ctrlEventCategory.Size = new System.Drawing.Size(250, 500);
            this.ctrlEventCategory.TabIndex = 8;
            // 
            // openFileDialog
            // 
            this.openFileDialog.Filter = "XML Files (*.xml)|*.xml|All Files (*.*)|*.*";
            // 
            // FrmDeviceConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(884, 708);
            this.Controls.Add(this.pnlBottom);
            this.Controls.Add(this.ctrlEventCategory);
            this.Controls.Add(this.ctrlEventSubscription);
            this.Controls.Add(this.ctrlCommand);
            this.Controls.Add(this.ctrlItem);
            this.Controls.Add(this.ctrlSubscription);
            this.Controls.Add(this.ctrlEmptyItem);
            this.Controls.Add(this.gbDevice);
            this.Controls.Add(this.gbServerBrowse);
            this.Controls.Add(this.gbConnectionOptions);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmDeviceConfig";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Device {0} Properties - OPC Classic";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmDeviceConfig_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmDeviceConfig_FormClosed);
            this.Load += new System.EventHandler(this.FrmDeviceConfig_Load);
            this.gbConnectionOptions.ResumeLayout(false);
            this.gbConnectionOptions.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbConnectionInfo)).EndInit();
            this.pnlBottom.ResumeLayout(false);
            this.gbServerBrowse.ResumeLayout(false);
            this.gbDevice.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private GroupBox gbConnectionOptions;
        private Label lblHost;
        private CheckBox chkHost;
        private TextBox txtHost;
        private Button btnShowNetworkOptions;
        private Label lblServerPath;
        private Button btnSelectServer;
        private Panel pnlBottom;
        private Button btnSave;
        private Button btnClose;
        private GroupBox gbServerBrowse;
        private TreeView tvServer;
        private Button btnOpenFile;
        private GroupBox gbDevice;
        private TreeView tvDevice;
        private Button btnDeleteItem;
        private Button btnMoveDownItem;
        private Button btnMoveUpItem;
        private Button btnAddItem;
        private Button btnAddSubscription;
        private PictureBox pbConnectionInfo;
        private Label lblConnectionInfo;
        private ToolTip toolTip;
        private ImageList ilTree;
        private Controls.CtrlSubscription ctrlSubscription;
        private Controls.CtrlItem ctrlItem;
        private Controls.CtrlCommand ctrlCommand;
        private Controls.CtrlEventSubscription ctrlEventSubscription;
        private Controls.CtrlEmptyItem ctrlEmptyItem;
        private Controls.CtrlEventCategory ctrlEventCategory;
        private Button btnDisconnect;
        private Button btnConnect;
        private TextBox txtAeSpec;
        private Label lblAeSpec;
        private TextBox txtDaSpec;
        private TextBox txtServerPath;
        private Label lblDaSpec;
        private OpenFileDialog openFileDialog;
    }
}