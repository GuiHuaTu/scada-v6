namespace Scada.Comm.Drivers.DrvSiemensS7.View.Forms
{
    partial class FrmDeviceTemplate
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
            components = new System.ComponentModel.Container();
            TreeNode treeNode1 = new TreeNode("Element groups");
            TreeNode treeNode2 = new TreeNode("Commands");
            treeView = new TreeView();
            cmsTree = new ContextMenuStrip(components);
            miCollapseElemGroups = new ToolStripMenuItem();
            miCloneElemConfig = new ToolStripMenuItem();
            ilTree = new ImageList(components);
            toolStrip = new ToolStrip();
            btnNew = new ToolStripButton();
            btnOpen = new ToolStripButton();
            btnSave = new ToolStripButton();
            btnSaveAs = new ToolStripButton();
            toolStripSeparator1 = new ToolStripSeparator();
            btnAddElemGroup = new ToolStripButton();
            btnAddElem = new ToolStripButton();
            btnAddCmd = new ToolStripButton();
            btnMoveUp = new ToolStripButton();
            btnMoveDown = new ToolStripButton();
            btnDelete = new ToolStripButton();
            toolStripSeparator2 = new ToolStripSeparator();
            btnEditOptions = new ToolStripButton();
            btnEditOptionsExt = new ToolStripButton();
            btnValidate = new ToolStripButton();
            gbTemplate = new GroupBox();
            openFileDialog = new OpenFileDialog();
            saveFileDialog = new SaveFileDialog();
            ctrlElemGroup = new Controls.CtrlElemGroup();
            ctrlElem = new Controls.CtrlElem();
            ctrlCmd = new Controls.CtrlCmd();
            cmsTree.SuspendLayout();
            toolStrip.SuspendLayout();
            gbTemplate.SuspendLayout();
            SuspendLayout();
            // 
            // treeView
            // 
            treeView.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            treeView.ContextMenuStrip = cmsTree;
            treeView.HideSelection = false;
            treeView.ImageIndex = 0;
            treeView.ImageList = ilTree;
            treeView.Location = new Point(20, 35);
            treeView.Margin = new Padding(5, 5, 5, 5);
            treeView.Name = "treeView";
            treeNode1.ImageKey = "(по умолчанию)";
            treeNode1.Name = "elemGroupsNode";
            treeNode1.SelectedImageKey = "(по умолчанию)";
            treeNode1.Text = "Element groups";
            treeNode2.ImageKey = "(по умолчанию)";
            treeNode2.Name = "commandsNode";
            treeNode2.SelectedImageKey = "(по умолчанию)";
            treeNode2.Text = "Commands";
            treeView.Nodes.AddRange(new TreeNode[] { treeNode1, treeNode2 });
            treeView.SelectedImageIndex = 0;
            treeView.ShowRootLines = false;
            treeView.Size = new Size(428, 743);
            treeView.TabIndex = 0;
            treeView.AfterSelect += treeView_AfterSelect;
            treeView.NodeMouseClick += treeView_NodeMouseClick;
            // 
            // cmsTree
            // 
            cmsTree.ImageScalingSize = new Size(24, 24);
            cmsTree.Items.AddRange(new ToolStripItem[] { miCollapseElemGroups, miCloneElemConfig });
            cmsTree.Name = "cmsTree";
            cmsTree.Size = new Size(315, 68);
            cmsTree.Opening += cmsTree_Opening;
            // 
            // miCollapseElemGroups
            // 
            miCollapseElemGroups.Image = Properties.Resources.collapse_all;
            miCollapseElemGroups.Name = "miCollapseElemGroups";
            miCollapseElemGroups.Size = new Size(314, 32);
            miCollapseElemGroups.Text = "Collapse Element Groups";
            miCollapseElemGroups.Click += miCollapseElemGroups_Click;
            // 
            // miCloneElemConfig
            // 
            miCloneElemConfig.Image = Properties.Resources.clone;
            miCloneElemConfig.Name = "miCloneElemConfig";
            miCloneElemConfig.Size = new Size(314, 32);
            miCloneElemConfig.Text = "Clone Element Parameters";
            miCloneElemConfig.Click += miCloneElemConfig_Click;
            // 
            // ilTree
            // 
            ilTree.ColorDepth = ColorDepth.Depth24Bit;
            ilTree.ImageSize = new Size(16, 16);
            ilTree.TransparentColor = Color.Transparent;
            // 
            // toolStrip
            // 
            toolStrip.ImageScalingSize = new Size(24, 24);
            toolStrip.Items.AddRange(new ToolStripItem[] { btnNew, btnOpen, btnSave, btnSaveAs, toolStripSeparator1, btnAddElemGroup, btnAddElem, btnAddCmd, btnMoveUp, btnMoveDown, btnDelete, toolStripSeparator2, btnEditOptions, btnEditOptionsExt, btnValidate });
            toolStrip.Location = new Point(0, 0);
            toolStrip.Name = "toolStrip";
            toolStrip.Padding = new Padding(0, 0, 3, 0);
            toolStrip.Size = new Size(990, 33);
            toolStrip.TabIndex = 0;
            toolStrip.Text = "toolStrip1";
            // 
            // btnNew
            // 
            btnNew.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btnNew.Image = Properties.Resources.blank;
            btnNew.ImageTransparentColor = Color.Magenta;
            btnNew.Name = "btnNew";
            btnNew.Size = new Size(34, 28);
            btnNew.ToolTipText = "New Template";
            btnNew.Click += btnNew_Click;
            // 
            // btnOpen
            // 
            btnOpen.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btnOpen.Image = Properties.Resources.open;
            btnOpen.ImageTransparentColor = Color.Magenta;
            btnOpen.Name = "btnOpen";
            btnOpen.Size = new Size(34, 28);
            btnOpen.ToolTipText = "Open Template";
            btnOpen.Click += btnOpen_Click;
            // 
            // btnSave
            // 
            btnSave.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btnSave.Image = Properties.Resources.save;
            btnSave.ImageTransparentColor = Color.Magenta;
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(34, 28);
            btnSave.ToolTipText = "Save Template";
            btnSave.Click += btnSave_Click;
            // 
            // btnSaveAs
            // 
            btnSaveAs.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btnSaveAs.Image = Properties.Resources.save_as;
            btnSaveAs.ImageTransparentColor = Color.Magenta;
            btnSaveAs.Name = "btnSaveAs";
            btnSaveAs.Size = new Size(34, 28);
            btnSaveAs.ToolTipText = "Save Template As";
            btnSaveAs.Click += btnSave_Click;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(6, 33);
            // 
            // btnAddElemGroup
            // 
            btnAddElemGroup.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btnAddElemGroup.Image = Properties.Resources.group;
            btnAddElemGroup.ImageTransparentColor = Color.Magenta;
            btnAddElemGroup.Name = "btnAddElemGroup";
            btnAddElemGroup.Size = new Size(34, 28);
            btnAddElemGroup.ToolTipText = "Add Element Group";
            btnAddElemGroup.Click += btnAddElemGroup_Click;
            // 
            // btnAddElem
            // 
            btnAddElem.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btnAddElem.Image = Properties.Resources.elem;
            btnAddElem.ImageTransparentColor = Color.Magenta;
            btnAddElem.Name = "btnAddElem";
            btnAddElem.Size = new Size(34, 28);
            btnAddElem.ToolTipText = "Add Element";
            btnAddElem.Click += btnAddElem_Click;
            // 
            // btnAddCmd
            // 
            btnAddCmd.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btnAddCmd.Image = Properties.Resources.cmd;
            btnAddCmd.ImageTransparentColor = Color.Magenta;
            btnAddCmd.Name = "btnAddCmd";
            btnAddCmd.Size = new Size(34, 28);
            btnAddCmd.ToolTipText = "Add Command";
            btnAddCmd.Click += btnAddCmd_Click;
            // 
            // btnMoveUp
            // 
            btnMoveUp.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btnMoveUp.Image = Properties.Resources.move_up;
            btnMoveUp.ImageTransparentColor = Color.Magenta;
            btnMoveUp.Name = "btnMoveUp";
            btnMoveUp.Size = new Size(34, 28);
            btnMoveUp.ToolTipText = "Move Up";
            btnMoveUp.Click += btnMoveUp_Click;
            // 
            // btnMoveDown
            // 
            btnMoveDown.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btnMoveDown.Image = Properties.Resources.move_down;
            btnMoveDown.ImageTransparentColor = Color.Magenta;
            btnMoveDown.Name = "btnMoveDown";
            btnMoveDown.Size = new Size(34, 28);
            btnMoveDown.ToolTipText = "Move Down";
            btnMoveDown.Click += btnMoveDown_Click;
            // 
            // btnDelete
            // 
            btnDelete.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btnDelete.Image = Properties.Resources.delete;
            btnDelete.ImageTransparentColor = Color.Magenta;
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(34, 28);
            btnDelete.ToolTipText = "Delete";
            btnDelete.Click += btnDelete_Click;
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new Size(6, 33);
            // 
            // btnEditOptions
            // 
            btnEditOptions.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btnEditOptions.Image = Properties.Resources.options;
            btnEditOptions.ImageTransparentColor = Color.Magenta;
            btnEditOptions.Name = "btnEditOptions";
            btnEditOptions.Size = new Size(34, 28);
            btnEditOptions.ToolTipText = "Edit Template Options";
            btnEditOptions.Click += btnEditOptions_Click;
            // 
            // btnEditOptionsExt
            // 
            btnEditOptionsExt.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btnEditOptionsExt.Image = Properties.Resources.options_extended;
            btnEditOptionsExt.ImageTransparentColor = Color.Magenta;
            btnEditOptionsExt.Name = "btnEditOptionsExt";
            btnEditOptionsExt.Size = new Size(34, 28);
            btnEditOptionsExt.ToolTipText = "Edit Extended Options";
            btnEditOptionsExt.Click += btnEditOptionsExt_Click;
            // 
            // btnValidate
            // 
            btnValidate.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btnValidate.Image = Properties.Resources.validate;
            btnValidate.ImageTransparentColor = Color.Magenta;
            btnValidate.Name = "btnValidate";
            btnValidate.Size = new Size(34, 28);
            btnValidate.ToolTipText = "Validate Template";
            btnValidate.Click += btnValidate_Click;
            // 
            // gbTemplate
            // 
            gbTemplate.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            gbTemplate.Controls.Add(treeView);
            gbTemplate.Location = new Point(19, 45);
            gbTemplate.Margin = new Padding(5, 5, 5, 5);
            gbTemplate.Name = "gbTemplate";
            gbTemplate.Padding = new Padding(16, 5, 16, 16);
            gbTemplate.Size = new Size(471, 802);
            gbTemplate.TabIndex = 1;
            gbTemplate.TabStop = false;
            gbTemplate.Text = "Device Template";
            // 
            // openFileDialog
            // 
            openFileDialog.DefaultExt = "*.xml";
            openFileDialog.Filter = "Template Files (*.xml)|*.xml|All Files (*.*)|*.*";
            openFileDialog.FilterIndex = 0;
            // 
            // saveFileDialog
            // 
            saveFileDialog.DefaultExt = "*.xml";
            saveFileDialog.Filter = "Template Files (*.xml)|*.xml|All Files (*.*)|*.*";
            saveFileDialog.FilterIndex = 0;
            // 
            // ctrlElemGroup
            // 
            ctrlElemGroup.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            ctrlElemGroup.ElemGroup = null;
            ctrlElemGroup.Location = new Point(500, 45);
            ctrlElemGroup.Margin = new Padding(8, 8, 8, 8);
            ctrlElemGroup.Name = "ctrlElemGroup";
            ctrlElemGroup.Size = new Size(471, 437);
            ctrlElemGroup.TabIndex = 2;
            ctrlElemGroup.TemplateOptions = null;
            ctrlElemGroup.ObjectChanged += ctrlElemGroup_ObjectChanged;
            // 
            // ctrlElem
            // 
            ctrlElem.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            ctrlElem.ElemTag = null;
            ctrlElem.Location = new Point(500, 112);
            ctrlElem.Margin = new Padding(8, 8, 8, 8);
            ctrlElem.Name = "ctrlElem";
            ctrlElem.Size = new Size(471, 711);
            ctrlElem.TabIndex = 3;
            ctrlElem.ObjectChanged += ctrlElem_ObjectChanged;
            // 
            // ctrlCmd
            // 
            ctrlCmd.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            ctrlCmd.Cmd = null;
            ctrlCmd.Location = new Point(500, 192);
            ctrlCmd.Margin = new Padding(8, 8, 8, 8);
            ctrlCmd.Name = "ctrlCmd";
            ctrlCmd.Size = new Size(471, 654);
            ctrlCmd.TabIndex = 4;
            ctrlCmd.TemplateOptions = null;
            ctrlCmd.ObjectChanged += ctrlCmd_ObjectChanged;
            // 
            // FrmDeviceTemplate
            // 
            AutoScaleDimensions = new SizeF(11F, 24F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(990, 866);
            Controls.Add(ctrlElemGroup);
            Controls.Add(ctrlElem);
            Controls.Add(ctrlCmd);
            Controls.Add(gbTemplate);
            Controls.Add(toolStrip);
            Margin = new Padding(5, 5, 5, 5);
            MaximizeBox = false;
            MinimizeBox = false;
            MinimumSize = new Size(1003, 686);
            Name = "FrmDeviceTemplate";
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "MODBUS. Device Template Editor";
            FormClosing += FrmDevTemplate_FormClosing;
            Load += FrmDevTemplate_Load;
            cmsTree.ResumeLayout(false);
            toolStrip.ResumeLayout(false);
            toolStrip.PerformLayout();
            gbTemplate.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.TreeView treeView;
        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripButton btnSave;
        private System.Windows.Forms.ToolStripButton btnOpen;
        private System.Windows.Forms.ToolStripButton btnNew;
        private System.Windows.Forms.ToolStripButton btnAddElemGroup;
        private System.Windows.Forms.ToolStripButton btnAddCmd;
        private System.Windows.Forms.ToolStripButton btnMoveUp;
        private System.Windows.Forms.ToolStripButton btnMoveDown;
        private System.Windows.Forms.ToolStripButton btnDelete;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ImageList ilTree;
        private System.Windows.Forms.GroupBox gbTemplate;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.ToolStripButton btnSaveAs;
        private System.Windows.Forms.ToolStripButton btnAddElem;
        private Scada.Comm.Drivers.DrvSiemensS7.View.Controls.CtrlCmd ctrlCmd;
        private Scada.Comm.Drivers.DrvSiemensS7.View.Controls.CtrlElem ctrlElem;
        private Scada.Comm.Drivers.DrvSiemensS7.View.Controls.CtrlElemGroup ctrlElemGroup;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton btnEditOptions;
        private System.Windows.Forms.ToolStripButton btnEditOptionsExt;
        private System.Windows.Forms.ToolStripButton btnValidate;
        private System.Windows.Forms.ContextMenuStrip cmsTree;
        private System.Windows.Forms.ToolStripMenuItem miCollapseElemGroups;
        private System.Windows.Forms.ToolStripMenuItem miCloneElemConfig;
    }
}