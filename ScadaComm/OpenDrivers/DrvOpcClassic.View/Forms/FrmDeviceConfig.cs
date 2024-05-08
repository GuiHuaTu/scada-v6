// Copyright (c) Rapid Software LLC. All rights reserved.

using Opc;
using OpcCom;
using Scada.Comm.Drivers.DrvOpcClassic.Config;
using Scada.Comm.Drivers.DrvOpcClassic.View.Properties;
using Scada.Forms;
using Scada.Lang;
using System.Xml;

namespace Scada.Comm.Drivers.DrvOpcClassic.View.Forms
{
    /// <summary>
    /// Represents a device configuration form.
    /// <para>Представляет форму конфигурации устройства.</para>
    /// </summary>
    public partial class FrmDeviceConfig : Form
    {
        /// <summary>
        /// Specifies the image keys.
        /// </summary>
        private static class ImageKey
        {
            public const string Empty = "empty.png";
            public const string FolderClosed = "folder_closed.png";
            public const string FolderOpen = "folder_open.png";
            public const string ItemCmd = "item_cmd.png";
            public const string ItemEvent = "item_event.png";
            public const string ItemVar = "item_var.png";
        }

        private readonly AppDirs appDirs;              // the application directories
        private readonly int lineNum;                  // the communication line number
        private readonly int deviceNum;                // the device number
        private readonly OpcLineConfig lineConfig;     // the communication line configuration
        private readonly OpcDeviceConfig deviceConfig; // the device configuration

        private string lineConfigFileName;       // the line configuration file name
        private string deviceConfigFileName;     // the device configuration file name
        private bool lineConfigModified;         // the line configuration has been modified
        private bool deviceConfigModified;       // the device configuration has been modified
        private bool changing;                   // controls are being changed programmatically
        private TreeNode subscriptionsNode;      // the tree node of the data subscriptions
        private TreeNode commandsNode;           // the tree node of the commands
        private TreeNode eventSubscriptionsNode; // the tree node of the event subscriptions
        private Opc.Da.Server daServer;          // the connected OPC DA server
        private Opc.Ae.Server aeServer;          // the connected OPC AE server
        private bool loadedFromXml;              // indicates that the server content is loaded from XML


        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        public FrmDeviceConfig()
        {
            InitializeComponent();

            ctrlSubscription.Top = ctrlItem.Top = ctrlCommand.Top = 
                ctrlEventSubscription.Top = ctrlEventCategory.Top = ctrlEmptyItem.Top;
            ctrlSubscription.Visible = ctrlItem.Visible = ctrlCommand.Visible = 
                ctrlEventSubscription.Visible = ctrlEventCategory.Visible = false;
        }

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        public FrmDeviceConfig(AppDirs appDirs, int lineNum, int deviceNum)
            : this()
        {
            this.appDirs = appDirs ?? throw new ArgumentNullException(nameof(appDirs));
            this.lineNum = lineNum;
            this.deviceNum = deviceNum;
            lineConfig = new OpcLineConfig();
            deviceConfig = new OpcDeviceConfig();

            lineConfigFileName = "";
            deviceConfigFileName = "";
            lineConfigModified = false;
            deviceConfigModified = false;
            changing = false;
            subscriptionsNode = null;
            commandsNode = null;
            eventSubscriptionsNode = null;
            daServer = null;
            aeServer = null;
            loadedFromXml = false;
        }


        /// <summary>
        /// Gets or sets a value indicating whether the line configuration has been modified.
        /// </summary>
        private bool LineConfigModified
        {
            get
            {
                return lineConfigModified;
            }
            set
            {
                lineConfigModified = value;
                btnSave.Enabled = Modified;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the device configuration has been modified.
        /// </summary>
        private bool DeviceConfigModified
        {
            get
            {
                return deviceConfigModified;
            }
            set
            {
                deviceConfigModified = value;
                btnSave.Enabled = Modified;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the configuration has been modified.
        /// </summary>
        private bool Modified => LineConfigModified || DeviceConfigModified;


        /// <summary>
        /// Takes the tree view images and loads them into an image list.
        /// </summary>
        private void TakeTreeViewImages()
        {
            // loading images from resources instead of storing in image list prevents them from corruption
            ilTree.Images.Add(ImageKey.Empty, Resources.empty);
            ilTree.Images.Add(ImageKey.FolderClosed, Resources.folder_closed);
            ilTree.Images.Add(ImageKey.FolderOpen, Resources.folder_open);
            ilTree.Images.Add(ImageKey.ItemCmd, Resources.item_cmd);
            ilTree.Images.Add(ImageKey.ItemEvent, Resources.item_event);
            ilTree.Images.Add(ImageKey.ItemVar, Resources.item_var);
        }

        /// <summary>
        /// Sets the controls according to the configuration.
        /// </summary>
        private void ConfigToControls()
        {
            changing = true;
            ShowConnectionOptions();
            FillDeviceTree();
            changing = false;
        }

        /// <summary>
        /// Shows the connection options of the line configuration.
        /// </summary>
        private void ShowConnectionOptions()
        {
            OpcConnectionOptions connectionOptions = lineConfig.ConnectionOptions;
            chkHost.Checked = !string.IsNullOrEmpty(connectionOptions.Host);
            txtHost.Enabled = chkHost.Checked;
            txtHost.Text = connectionOptions.Host;
            txtServerPath.Text = connectionOptions.ServerPath;
            txtDaSpec.Text = connectionOptions.DaSpec.ToString(Locale.IsRussian);
            txtAeSpec.Text = connectionOptions.AeSpec.ToString(Locale.IsRussian);
        }

        /// <summary>
        /// Fills the device tree.
        /// </summary>
        private void FillDeviceTree()
        {
            try
            {
                tvDevice.BeginUpdate();
                tvDevice.Nodes.Clear();

                subscriptionsNode = TreeViewExtensions.CreateNode(DriverPhrases.SubscriptionsNode, 
                    ImageKey.FolderClosed);
                commandsNode = TreeViewExtensions.CreateNode(DriverPhrases.CommandsNode, 
                    ImageKey.FolderClosed);
                eventSubscriptionsNode = TreeViewExtensions.CreateNode(DriverPhrases.EventSubscriptionsNode, 
                    ImageKey.FolderClosed);
                int tagNum = 1;

                foreach (SubscriptionConfig subscriptionConfig in deviceConfig.Subscriptions)
                {
                    TreeNode subscriptionNode = CreateSubscriptionNode(subscriptionConfig);
                    subscriptionsNode.Nodes.Add(subscriptionNode);

                    foreach (ItemConfig itemConfig in subscriptionConfig.Items)
                    {
                        subscriptionNode.Nodes.Add(CreateItemNode(itemConfig));
                        itemConfig.Tag = new ItemConfigTag(tagNum);
                        tagNum++;
                    }
                }

                foreach (CommandConfig commandConfig in deviceConfig.Commands)
                {
                    commandsNode.Nodes.Add(CreateCommandNode(commandConfig));
                }

                foreach (EventSubscriptionConfig eventSubscriptionConfig in deviceConfig.EventSubscriptions)
                {
                    TreeNode eventSubscriptionNode = CreateEventSubscriptionNode(eventSubscriptionConfig);
                    eventSubscriptionsNode.Nodes.Add(eventSubscriptionNode);

                    foreach (EventCategoryConfig eventCategory in eventSubscriptionConfig.Categories)
                    {
                        eventSubscriptionNode.Nodes.Add(CreateEventCategoryNode(eventCategory));
                    }
                }

                tvDevice.Nodes.Add(subscriptionsNode);
                tvDevice.Nodes.Add(commandsNode);
                tvDevice.Nodes.Add(eventSubscriptionsNode);

                subscriptionsNode.Expand();
                commandsNode.Expand();
                eventSubscriptionsNode.Expand();
            }
            finally
            {
                tvDevice.EndUpdate();
            }
        }

        /// <summary>
        /// Creates a new subscription node according to the subscription configuration.
        /// </summary>
        private static TreeNode CreateSubscriptionNode(SubscriptionConfig subscriptionConfig)
        {
            return TreeViewExtensions.CreateNode(
                GetDisplayName(subscriptionConfig.DisplayName, DriverPhrases.UnnamedSubscription),
                ImageKey.FolderClosed,
                subscriptionConfig);
        }

        /// <summary>
        /// Creates a new monitored item node according to the item configuration.
        /// </summary>
        private static TreeNode CreateItemNode(ItemConfig itemConfig)
        {
            return TreeViewExtensions.CreateNode(
                GetDisplayName(itemConfig.Name, DriverPhrases.UnnamedItem),
                ImageKey.ItemVar,
                itemConfig);
        }

        /// <summary>
        /// Creates a new command node according to the command configuration.
        /// </summary>
        private static TreeNode CreateCommandNode(CommandConfig commandConfig)
        {
            return TreeViewExtensions.CreateNode(
                GetDisplayName(commandConfig.Name, DriverPhrases.UnnamedCommand),
                ImageKey.ItemCmd,
                commandConfig);
        }

        /// <summary>
        /// Creates a new event subscription node according to the event subscription configuration.
        /// </summary>
        private static TreeNode CreateEventSubscriptionNode(EventSubscriptionConfig eventSubscriptionConfig)
        {
            return TreeViewExtensions.CreateNode(
                GetDisplayName(eventSubscriptionConfig.DisplayName, DriverPhrases.UnnamedSubscription),
                ImageKey.FolderClosed,
                eventSubscriptionConfig);
        }

        /// <summary>
        /// Creates a new event category node.
        /// </summary>
        private static TreeNode CreateEventCategoryNode(EventCategoryConfig eventCategory)
        {
            return TreeViewExtensions.CreateNode(
                GetDisplayName(eventCategory.Name, DriverPhrases.UnnamedItem),
                ImageKey.ItemEvent, 
                eventCategory);
        }

        /// <summary>
        /// Returns the specified display name or the default name.
        /// </summary>
        private static string GetDisplayName(string displayName, string defaultName)
        {
            return string.IsNullOrEmpty(displayName) ? defaultName : displayName;
        }

        /// <summary>
        /// Sets the node image as open or closed folder.
        /// </summary>
        private static void SetFolderImage(TreeNode treeNode)
        {
            if (treeNode.ImageKey.StartsWith("folder_"))
                treeNode.SetImageKey(treeNode.IsExpanded ? ImageKey.FolderOpen : ImageKey.FolderClosed);
        }

        /// <summary>
        /// Sets the enabled property of the server browsing buttons.
        /// </summary>
        private void SetServerButtonsEnabled()
        {
            OpcConnectionOptions connectionOptions = lineConfig.ConnectionOptions;
            btnConnect.Enabled =
                daServer == null && connectionOptions.DaSpec != Spec.None ||
                aeServer == null && connectionOptions.AeSpec != Spec.None;
            btnDisconnect.Enabled = daServer != null || aeServer != null;
        }

        /// <summary>
        /// Sets the enabled property of the buttons that manipulate the device tree.
        /// </summary>
        private void SetDeviceButtonsEnabled()
        {
            object serverNodeTag = tvServer.SelectedNode?.Tag;
            btnAddItem.Enabled = serverNodeTag is Opc.Da.BrowseElement browseElem && browseElem.IsVariable() ||
                serverNodeTag is Opc.Ae.Category;

            bool deviceNodeTagDefined = tvDevice.SelectedNode?.Tag != null;
            btnMoveUpItem.Enabled = deviceNodeTagDefined && tvDevice.SelectedNode.PrevNode != null;
            btnMoveDownItem.Enabled = deviceNodeTagDefined && tvDevice.SelectedNode.NextNode != null;
            btnDeleteItem.Enabled = deviceNodeTagDefined;
        }

        /// <summary>
        /// Update signals if 2 elements are reversed.
        /// </summary>
        private void SwapSignals(TreeNode treeNode1, TreeNode treeNode2)
        {
            if (treeNode1?.Tag is ItemConfig itemConfig1 &&
                treeNode2?.Tag is ItemConfig itemConfig2 &&
                itemConfig1.Tag is ItemConfigTag itemConfigTag1 &&
                itemConfig2.Tag is ItemConfigTag itemConfigTag2)
            {
                (itemConfigTag1.TagNum, itemConfigTag2.TagNum) = (itemConfigTag2.TagNum, itemConfigTag1.TagNum);
                ctrlItem.RefreshTagNum();
            }
        }

        /// <summary>
        /// Update tag numbers starting from the specified node.
        /// </summary>
        private void UpdateTagNums(TreeNode startNode)
        {
            TreeNode startSubscrNode = startNode?.FindClosest(typeof(SubscriptionConfig));

            if (startSubscrNode != null)
            {
                // define initial tag number
                int tagNum = 1;
                TreeNode subscrNode = startSubscrNode.PrevNode;

                while (subscrNode != null)
                {
                    if (subscrNode.LastNode?.Tag is ItemConfig itemConfig &&
                        itemConfig.Tag is ItemConfigTag tag)
                    {
                        tagNum = tag.TagNum + 1;
                        break;
                    }

                    subscrNode = subscrNode.PrevNode;
                }

                // recalculate tag numbers
                subscrNode = startSubscrNode;

                while (subscrNode != null)
                {
                    foreach (TreeNode itemNode in subscrNode.Nodes)
                    {
                        if (itemNode.Tag is ItemConfig itemConfig &&
                            itemConfig.Tag is ItemConfigTag tag)
                        {
                            tag.TagNum = tagNum;
                            tagNum++;
                        }
                    }

                    subscrNode = subscrNode.NextNode;
                }

                ctrlItem.RefreshTagNum();
            }
        }

        /// <summary>
        /// Connects to the OPC server.
        /// </summary>
        private bool Connect()
        {
            bool connectOK = true;
            OpcConnectionOptions connectionOptions = lineConfig.ConnectionOptions;

            ServerEnumerator enumerator = new();
            string host = string.IsNullOrEmpty(connectionOptions.Host) ? null : connectionOptions.Host;
            ConnectData connectData = connectionOptions.GetConnectData();

            if (daServer == null && connectionOptions.DaSpec != Spec.None &&
                !ConnectToDaServer(enumerator, host, connectData))
            {
                connectOK = false;
            }

            if (aeServer == null && connectionOptions.AeSpec != Spec.None &&
                !ConnectToAeServer(enumerator, host, connectData))
            {
                connectOK = false;
            }

            SetServerButtonsEnabled();
            return connectOK;
        }

        /// <summary>
        /// Connects the OPC DA server.
        /// </summary>
        private bool ConnectToDaServer(ServerEnumerator enumerator, string host, ConnectData connectData)
        {
            try
            {
                OpcConnectionOptions connectionOptions = lineConfig.ConnectionOptions;
                Opc.Server[] daServers = enumerator.GetAvailableServers(
                    connectionOptions.GetDaSpecification(), host, connectData);

                foreach (Opc.Server server in daServers)
                {
                    if (connectionOptions.ServerPath == server.Url.Path)
                    {
                        daServer = server as Opc.Da.Server;
                        break;
                    }
                }

                if (daServer == null)
                {
                    ScadaUiUtils.ShowError(DriverPhrases.DaServerUnavailable);
                    return false;
                }
                else
                {
                    try
                    {
                        daServer.Connect();
                        daServer.ServerShutdown += Server_ServerShutdown;
                        return true;
                    }
                    catch
                    {
                        daServer.SafeDispose();
                        daServer = null;
                        throw;
                    }
                }
            }
            catch (Exception ex)
            {
                ScadaUiUtils.ShowError(ex.BuildErrorMessage(DriverPhrases.ConnectDaServerError));
                return false;
            }
        }

        /// <summary>
        /// Connects the OPC AE server.
        /// </summary>
        private bool ConnectToAeServer(ServerEnumerator enumerator, string host, ConnectData connectData)
        {
            try
            {
                OpcConnectionOptions connectionOptions = lineConfig.ConnectionOptions;
                Opc.Server[] aeServers = enumerator.GetAvailableServers(
                    connectionOptions.GetAeSpecification(), host, connectData);

                foreach (Opc.Server server in aeServers)
                {
                    if (connectionOptions.ServerPath == server.Url.Path)
                    {
                        aeServer = server as Opc.Ae.Server;
                        break;
                    }
                }

                if (aeServer == null)
                {
                    ScadaUiUtils.ShowError(DriverPhrases.AeServerUnavailable);
                    return false;
                }
                else
                {
                    try
                    {
                        aeServer.Connect();
                        aeServer.ServerShutdown += Server_ServerShutdown;
                        return true;
                    }
                    catch
                    {
                        aeServer.SafeDispose();
                        aeServer = null;
                        throw;
                    }
                }
            }
            catch (Exception ex)
            {
                ScadaUiUtils.ShowError(ex.BuildErrorMessage(DriverPhrases.ConnectAeServerError));
                return false;
            }
        }
        
        /// <summary>
        /// Handles a shutdown event of the OPC server.
        /// </summary>
        private void Server_ServerShutdown(string reason)
        {
            if (InvokeRequired)
            {
                BeginInvoke(Server_ServerShutdown, reason);
            }
            else
            {
                btnDisconnect_Click(null, null);
            }
        }

        /// <summary>
        /// Disconnects from the OPC server.
        /// </summary>
        private void Disconnect()
        {
            if (daServer != null)
            {
                try
                {
                    if (daServer.IsConnected)
                        daServer.Disconnect();
                }
                catch (Exception ex)
                {
                    ScadaUiUtils.ShowError(ex.BuildErrorMessage(DriverPhrases.DisconnectDaServerError));
                }

                daServer.SafeDispose();
                daServer = null;
            }

            if (aeServer != null)
            {
                try
                {
                    if (aeServer.IsConnected)
                        aeServer.Disconnect();
                }
                catch (Exception ex)
                {
                    ScadaUiUtils.ShowError(ex.BuildErrorMessage(DriverPhrases.DisconnectAeServerError));
                }

                aeServer.SafeDispose();
                aeServer = null;
            }

            tvServer.Nodes.Clear();
            SetServerButtonsEnabled();
            SetDeviceButtonsEnabled();
        }

        /// <summary>
        /// Browses the OPC server.
        /// </summary>
        private void BrowseServer()
        {
            try
            {
                loadedFromXml = false;
                tvServer.BeginUpdate();
                tvServer.Nodes.Clear();

                if (daServer != null)
                {
                    TreeNode daServerNode = TreeViewExtensions.CreateNode(DriverPhrases.DaServerNode, 
                        ImageKey.FolderClosed);
                    tvServer.Nodes.Add(daServerNode);

                    BrowseDaNode(daServerNode, true);
                    daServerNode.Expand();
                }

                if (aeServer != null)
                {
                    TreeNode aeServerNode = TreeViewExtensions.CreateNode(DriverPhrases.AeServerNode,
                        ImageKey.FolderClosed);
                    tvServer.Nodes.Add(aeServerNode);

                    BrowseAeNode(aeServerNode);
                    aeServerNode.Expand();
                }
            }
            catch (Exception ex)
            {
                ScadaUiUtils.ShowError(ex.BuildErrorMessage(DriverPhrases.BrowseServerError));
            }
            finally
            {
                tvServer.EndUpdate();
            }
        }

        /// <summary>
        /// Browses the OPC DA node.
        /// </summary>
        private void BrowseDaNode(TreeNode node, bool throwOnError)
        {
            try
            {
                ItemIdentifier itemID = node.Tag is Opc.Da.BrowseElement parentElem ?
                    new ItemIdentifier(parentElem.ItemPath, parentElem.ItemName) : null;
                Opc.Da.BrowseFilters filters = new();
                Opc.Da.BrowseElement[] elems = daServer.Browse(itemID, filters, out Opc.Da.BrowsePosition pos);

                if (elems != null)
                {
                    foreach (Opc.Da.BrowseElement elem in elems)
                    {
                        AddDaNode(node, elem);
                    }
                }

                while (pos != null)
                {
                    elems = daServer.BrowseNext(ref pos);

                    if (elems != null)
                    {
                        foreach (Opc.Da.BrowseElement elem in elems)
                        {
                            AddDaNode(node, elem);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                if (throwOnError)
                    throw;
                else
                    ScadaUiUtils.ShowError(ex.BuildErrorMessage(DriverPhrases.BrowseServerError));
            }
        }

        /// <summary>
        /// Adds an OPC DA node to the parent node.
        /// </summary>
        private static void AddDaNode(TreeNode parentNode, Opc.Da.BrowseElement browseElem)
        {
            TreeNode elemNode = new(browseElem.Name) { Tag = browseElem };
            elemNode.SetImageKey(browseElem.IsVariable() ? ImageKey.ItemVar : ImageKey.FolderClosed);

            if (browseElem.HasChildren)
                elemNode.Nodes.Add(TreeViewExtensions.CreateNode(DriverPhrases.EmptyNode, ImageKey.Empty));

            parentNode.Nodes.Add(elemNode);
        }

        /// <summary>
        /// Browses the OPC AE node.
        /// </summary>
        private void BrowseAeNode(TreeNode node)
        {
            List<Opc.Ae.Category> categoryList = new();
            categoryList.AddRange(aeServer.QueryEventCategories((int)Opc.Ae.EventType.Simple));
            categoryList.AddRange(aeServer.QueryEventCategories((int)Opc.Ae.EventType.Tracking));
            categoryList.AddRange(aeServer.QueryEventCategories((int)Opc.Ae.EventType.Condition));

            foreach (Opc.Ae.Category category in categoryList)
            {
                node.Nodes.Add(TreeViewExtensions.CreateNode(category.Name, ImageKey.ItemEvent, category));
            }
        }

        /// <summary>
        /// Loads the OPC server content from the XML file.
        /// </summary>
        private void LoadServerContent(string fileName)
        {
            try
            {
                loadedFromXml = true;
                tvServer.BeginUpdate();
                tvServer.Nodes.Clear();

                XmlDocument xmlDoc = new();
                xmlDoc.Load(fileName);

                if (xmlDoc.DocumentElement.SelectSingleNode("OpcDa") is XmlNode opcDaNode)
                {
                    TreeNode daServerNode = TreeViewExtensions.CreateNode(DriverPhrases.DaServerNode,
                        ImageKey.FolderClosed);
                    tvServer.Nodes.Add(daServerNode);

                    foreach (XmlElement browseXmlElem in opcDaNode.SelectNodes("BrowseElement"))
                    {
                        AddDaNode(daServerNode, browseXmlElem);
                    }

                    daServerNode.Expand();
                }
            }
            catch (Exception ex)
            {
                ScadaUiUtils.ShowError(ex.BuildErrorMessage(DriverPhrases.LoadServerContentError));
            }
            finally
            {
                tvServer.EndUpdate();
            }
        }

        /// <summary>
        /// Adds an OPC DA node to the parent node.
        /// </summary>
        private static void AddDaNode(TreeNode parentNode, XmlElement browseXmlElem)
        {
            Opc.Da.BrowseElement browseElem = new()
            {
                Name = browseXmlElem.GetAttrAsString("name"),
                ItemName = browseXmlElem.GetAttrAsString("itemName"),
                ItemPath = browseXmlElem.GetAttrAsString("itemPath"),
                IsItem = browseXmlElem.GetAttrAsBool("isItem"),
                HasChildren = browseXmlElem.GetAttrAsBool("hasChildren")
            };

            TreeNode elemNode = new(browseElem.Name) { Tag = browseElem };
            elemNode.SetImageKey(browseElem.IsVariable() ? ImageKey.ItemVar : ImageKey.FolderClosed);
            parentNode.Nodes.Add(elemNode);

            foreach (XmlElement xmlElem in browseXmlElem.SelectNodes("BrowseElement"))
            {
                AddDaNode(elemNode, xmlElem);
            }
        }

        /// <summary>
        /// Adds a new item to the configuration.
        /// </summary>
        private bool AddItem(TreeNode serverNode)
        {
            if (serverNode?.Tag is Opc.Da.BrowseElement browseElem && browseElem.IsVariable())
            {
                if (TreeViewExtensions.GetTopParentNode(tvDevice.SelectedNode) == commandsNode)
                    AddCommand(browseElem);
                else
                    AddDaItem(browseElem);

                return true;
            }
            else if (serverNode?.Tag is Opc.Ae.Category category)
            {
                AddEventCategory(category);
                return true;
            }

            return false;
        }

        /// <summary>
        /// Adds a data item to the device configuration.
        /// </summary>
        private void AddDaItem(Opc.Da.BrowseElement browseElem)
        {
            // create new monitored item
            ItemConfig itemConfig = new()
            {
                Path = browseElem.ItemPath,
                Name = browseElem.ItemName,
                TagCode = browseElem.ItemName,
                Tag = new ItemConfigTag(0)
            };

            if (GetDataType(browseElem, loadedFromXml, out string dataTypeName, out bool isArray))
            {
                itemConfig.DataTypeName = dataTypeName;
                itemConfig.IsArray = isArray;
            }

            // find subscription
            TreeNode deviceNode = tvDevice.SelectedNode;
            TreeNode subscriptionNode = deviceNode?.FindClosest(typeof(SubscriptionConfig)) ??
                subscriptionsNode.LastNode;
            SubscriptionConfig subscriptionConfig;

            // add new subscription
            if (subscriptionNode == null)
            {
                subscriptionConfig = new SubscriptionConfig();
                subscriptionNode = CreateSubscriptionNode(subscriptionConfig);
                tvDevice.Insert(subscriptionsNode, subscriptionNode,
                    deviceConfig.Subscriptions, subscriptionConfig);
            }
            else
            {
                subscriptionConfig = (SubscriptionConfig)subscriptionNode.Tag;
            }

            // add monitored item
            TreeNode itemNode = CreateItemNode(itemConfig);
            tvDevice.Insert(subscriptionNode, itemNode, subscriptionConfig.Items, itemConfig);
            UpdateTagNums(itemNode);
            DeviceConfigModified = true;
        }

        /// <summary>
        /// Adds a command to the device configuration.
        /// </summary>
        private void AddCommand(Opc.Da.BrowseElement browseElem)
        {
            CommandConfig commandConfig = new()
            {
                Path = browseElem.ItemPath,
                Name = browseElem.ItemName,
                CmdCode = browseElem.ItemName
            };

            if (GetDataType(browseElem, loadedFromXml, out string dataTypeName, out _))
                commandConfig.DataTypeName = dataTypeName;

            tvDevice.Insert(commandsNode, CreateCommandNode(commandConfig), deviceConfig.Commands, commandConfig);
            DeviceConfigModified = true;
        }

        /// <summary>
        /// Adds an event category to the device configuration.
        /// </summary>
        private void AddEventCategory(Opc.Ae.Category category)
        {
            // create new category
            EventCategoryConfig eventCategoryConfig = new()
            {
                Name = category.Name,
                ID = category.ID
            };

            // find subscription
            TreeNode deviceNode = tvDevice.SelectedNode;
            TreeNode eventSubscriptionNode = deviceNode?.FindClosest(typeof(EventSubscriptionConfig)) ??
                eventSubscriptionsNode.LastNode;
            EventSubscriptionConfig eventSubscriptionConfig;

            // add new subscription
            if (eventSubscriptionNode == null)
            {
                eventSubscriptionConfig = new EventSubscriptionConfig();
                eventSubscriptionNode = CreateEventSubscriptionNode(eventSubscriptionConfig);
                tvDevice.Insert(eventSubscriptionsNode, eventSubscriptionNode,
                    deviceConfig.EventSubscriptions, eventSubscriptionConfig);
            }
            else
            {
                eventSubscriptionConfig = (EventSubscriptionConfig)eventSubscriptionNode.Tag;
            }

            // add monitored item
            TreeNode eventCategoryNode = CreateEventCategoryNode(eventCategoryConfig);
            tvDevice.Insert(eventSubscriptionNode, eventCategoryNode, 
                eventSubscriptionConfig.Categories, eventCategoryConfig);
            DeviceConfigModified = true;
        }

        /// <summary>
        /// Gets the data type of the element.
        /// </summary>
        private bool GetDataType(Opc.Da.BrowseElement browseElem, bool silent, 
            out string dataTypeName, out bool isArray)
        {
            try
            {
                if (daServer == null || !daServer.IsConnected)
                    throw new ScadaException(DriverPhrases.ServerNotConnected);

                Opc.Da.Item item = new(new ItemIdentifier(browseElem.ItemPath, browseElem.ItemName));
                Opc.Da.ItemValueResult res = daServer.Read(new Opc.Da.Item[] { item })[0];

                if (res.ResultID.Failed() || res.Value == null)
                    throw new ScadaException(DriverPhrases.UnableToReadData);

                System.Type dataType = res.Value.GetType();
                System.Type elemType = dataType.IsArray ? dataType.GetElementType() : dataType;
                dataTypeName = elemType.FullName;
                isArray = dataType.IsArray && elemType != typeof(string); // string array not supported
                return true;
            }
            catch (Exception ex)
            {
                if (!silent)
                    ScadaUiUtils.ShowError(ex.BuildErrorMessage(DriverPhrases.GetDataTypeError));

                dataTypeName = "";
                isArray = false;
                return false;
            }
        }

        /// <summary>
        /// Saves the line and device configuration.
        /// </summary>
        private bool SaveConfig()
        {
            bool saveOK = true;

            // save line configuration
            if (LineConfigModified)
            {
                if (lineConfig.Save(lineConfigFileName, out string errMsg))
                {
                    LineConfigModified = false;
                }
                else
                {
                    saveOK = false;
                    ScadaUiUtils.ShowError(errMsg);
                }
            }

            // save device configuration
            if (DeviceConfigModified)
            {
                if (deviceConfig.Save(deviceConfigFileName, out string errMsg))
                {
                    DeviceConfigModified = false;
                }
                else
                {
                    saveOK = false;
                    ScadaUiUtils.ShowError(errMsg);
                }
            }

            return saveOK;
        }


        private void FrmDeviceConfig_Load(object sender, EventArgs e)
        {
            // translate form
            FormTranslator.Translate(this, GetType().FullName, new FormTranslatorOptions { ToolTip = toolTip });
            FormTranslator.Translate(ctrlEmptyItem, ctrlEmptyItem.GetType().FullName);
            FormTranslator.Translate(ctrlSubscription, ctrlSubscription.GetType().FullName);
            FormTranslator.Translate(ctrlItem, ctrlItem.GetType().FullName);
            FormTranslator.Translate(ctrlCommand, ctrlCommand.GetType().FullName);
            FormTranslator.Translate(ctrlEventSubscription, ctrlEventSubscription.GetType().FullName);
            FormTranslator.Translate(ctrlEventCategory, ctrlEventCategory.GetType().FullName);
            Text = string.Format(Text, deviceNum);
            openFileDialog.SetFilter(DriverPhrases.XmlFileFilter);

            // load configuration
            lineConfigFileName = Path.Combine(appDirs.ConfigDir, OpcLineConfig.GetFileName(lineNum));
            deviceConfigFileName = Path.Combine(appDirs.ConfigDir, OpcDeviceConfig.GetFileName(deviceNum));

            if (File.Exists(lineConfigFileName) && !lineConfig.Load(lineConfigFileName, out string errMsg))
                ScadaUiUtils.ShowError(errMsg);

            if (File.Exists(deviceConfigFileName) && !deviceConfig.Load(deviceConfigFileName, out errMsg))
                ScadaUiUtils.ShowError(errMsg);

            // display configuration
            TakeTreeViewImages();
            ConfigToControls();
            SetServerButtonsEnabled();
            SetDeviceButtonsEnabled();
            LineConfigModified = false;
            DeviceConfigModified = false;
        }

        private void FrmDeviceConfig_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Modified)
            {
                DialogResult result = MessageBox.Show(CommonPhrases.SaveConfigConfirm,
                    CommonPhrases.QuestionCaption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                switch (result)
                {
                    case DialogResult.Yes:
                        e.Cancel = !SaveConfig();
                        break;

                    case DialogResult.No:
                        break;

                    default:
                        e.Cancel = true;
                        break;
                }
            }
        }

        private void FrmDeviceConfig_FormClosed(object sender, FormClosedEventArgs e)
        {
            Disconnect();
        }


        private void chkHost_CheckedChanged(object sender, EventArgs e)
        {
            if (!changing)
                txtHost.Enabled = chkHost.Checked;
        }

        private void txtHost_TextChanged(object sender, EventArgs e)
        {
            if (!changing)
            {
                lineConfig.ConnectionOptions.Host = txtHost.Text;
                LineConfigModified = true;
            }
        }

        private void btnShowNetworkOptions_Click(object sender, EventArgs e)
        {
            // show network options form
            FrmNetworkOptions frmNetworkOptions = new() { ConnectionOptions = lineConfig.ConnectionOptions };

            if (frmNetworkOptions.ShowDialog() == DialogResult.OK)
                LineConfigModified = true;
        }

        private void btnSelectServer_Click(object sender, EventArgs e)
        {
            // show select server form
            FrmServerSelect frmServerSelect = new() { ConnectionOptions = lineConfig.ConnectionOptions };

            if (frmServerSelect.ShowDialog() == DialogResult.OK)
            {
                LineConfigModified = true;
                changing = true;
                ShowConnectionOptions();
                changing = false;

                Disconnect();

                if (!string.IsNullOrEmpty(lineConfig.ConnectionOptions.ServerPath) && Connect())
                    BrowseServer();
            }
        }


        private void btnConnect_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(lineConfig.ConnectionOptions.ServerPath))
                ScadaUiUtils.ShowError(DriverPhrases.ServerNotSelected);
            else if (Connect())
                BrowseServer();
        }

        private void btnDisconnect_Click(object sender, EventArgs e)
        {
            Disconnect();
        }

        private void btnOpenFile_Click(object sender, EventArgs e)
        {
            // open server content
            openFileDialog.FileName = "";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                Disconnect();
                LoadServerContent(openFileDialog.FileName);
            }
        }

        private void tvServer_AfterSelect(object sender, TreeViewEventArgs e)
        {
            SetDeviceButtonsEnabled();
        }

        private void tvServer_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            // fill child nodes
            TreeNode node = e.Node;

            if (daServer != null && node.Tag is Opc.Da.BrowseElement && 
                node.Nodes.Count == 1 && node.Nodes[0].Tag == null)
            {
                tvServer.BeginUpdate();
                node.Nodes.Clear();
                BrowseDaNode(node, false);
                tvServer.EndUpdate();
            }
        }

        private void tvServer_AfterExpand(object sender, TreeViewEventArgs e)
        {
            SetFolderImage(e.Node);
        }

        private void tvServer_AfterCollapse(object sender, TreeViewEventArgs e)
        {
            SetFolderImage(e.Node);
        }

        private void tvServer_KeyDown(object sender, KeyEventArgs e)
        {
            TreeNode selectedNode = tvServer.SelectedNode;

            if (e.KeyCode == Keys.Enter && AddItem(selectedNode))
            {
                // go to the next node
                if (selectedNode.NextNode != null)
                    tvServer.SelectedNode = selectedNode.NextNode;
                else if (selectedNode.Parent?.NextNode != null)
                    tvServer.SelectedNode = selectedNode.Parent.NextNode;
            }
        }

        private void tvServer_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                AddItem(tvServer.SelectedNode);
        }


        private void btnAddSubscription_Click(object sender, EventArgs e)
        {
            if (TreeViewExtensions.GetTopParentNode(tvDevice.SelectedNode) == eventSubscriptionsNode)
            {
                // add new event subscription
                EventSubscriptionConfig eventSubscriptionConfig = new();
                TreeNode subscriptionNode = CreateEventSubscriptionNode(eventSubscriptionConfig);
                tvDevice.Insert(eventSubscriptionsNode, subscriptionNode, 
                    deviceConfig.EventSubscriptions, eventSubscriptionConfig);
                ctrlEventSubscription.SetFocus();
                DeviceConfigModified = true;
            }
            else
            {
                // add new data subscription
                SubscriptionConfig subscriptionConfig = new();
                TreeNode subscriptionNode = CreateSubscriptionNode(subscriptionConfig);
                tvDevice.Insert(subscriptionsNode, subscriptionNode, 
                    deviceConfig.Subscriptions, subscriptionConfig);
                ctrlSubscription.SetFocus();
                DeviceConfigModified = true;
            }
        }

        private void btnAddItem_Click(object sender, EventArgs e)
        {
            AddItem(tvServer.SelectedNode);
        }

        private void btnMoveUpItem_Click(object sender, EventArgs e)
        {
            // move up the selected item
            TreeNode selectedNode = tvDevice.SelectedNode;
            object deviceNodeTag = selectedNode?.Tag;

            if (deviceNodeTag is SubscriptionConfig)
            {
                tvDevice.MoveUpSelectedNode(deviceConfig.Subscriptions);
                UpdateTagNums(selectedNode);
            }
            else if (deviceNodeTag is ItemConfig)
            {
                if (selectedNode.Parent.Tag is SubscriptionConfig subscriptionConfig)
                {
                    tvDevice.MoveUpSelectedNode(subscriptionConfig.Items);
                    SwapSignals(selectedNode, selectedNode.NextNode);
                }
            }
            else if (deviceNodeTag is CommandConfig)
            {
                tvDevice.MoveUpSelectedNode(deviceConfig.Commands);
            }
            else if (deviceNodeTag is EventSubscriptionConfig)
            {
                tvDevice.MoveUpSelectedNode(deviceConfig.EventSubscriptions);
            }
            else if (deviceNodeTag is EventCategoryConfig)
            {
                if (selectedNode.Parent.Tag is EventSubscriptionConfig eventSubscriptionConfig)
                    tvDevice.MoveUpSelectedNode(eventSubscriptionConfig.Categories);
            }

            DeviceConfigModified = true;
        }

        private void btnMoveDownItem_Click(object sender, EventArgs e)
        {
            // move down the selected item
            TreeNode selectedNode = tvDevice.SelectedNode;
            object deviceNodeTag = tvDevice.SelectedNode?.Tag;

            if (deviceNodeTag is SubscriptionConfig)
            {
                tvDevice.MoveDownSelectedNode(deviceConfig.Subscriptions);
                UpdateTagNums(selectedNode);
            }
            else if (deviceNodeTag is ItemConfig)
            {
                if (selectedNode.Parent.Tag is SubscriptionConfig subscriptionConfig)
                {
                    tvDevice.MoveDownSelectedNode(subscriptionConfig.Items);
                    SwapSignals(selectedNode, selectedNode.PrevNode);
                }
            }
            else if (deviceNodeTag is CommandConfig)
            {
                tvDevice.MoveDownSelectedNode(deviceConfig.Commands);
            }
            else if (deviceNodeTag is EventSubscriptionConfig)
            {
                tvDevice.MoveDownSelectedNode(deviceConfig.EventSubscriptions);
            }
            else if (deviceNodeTag is EventCategoryConfig)
            {
                if (selectedNode.Parent.Tag is EventSubscriptionConfig eventSubscriptionConfig)
                    tvDevice.MoveDownSelectedNode(eventSubscriptionConfig.Categories);
            }

            DeviceConfigModified = true;
        }

        private void btnDeleteItem_Click(object sender, EventArgs e)
        {
            // delete the selected item
            TreeNode selectedNode = tvDevice.SelectedNode;
            object deviceNodeTag = selectedNode?.Tag;

            if (deviceNodeTag is SubscriptionConfig)
            {
                TreeNode nextSubscrNode = selectedNode.NextNode;
                tvDevice.RemoveNode(selectedNode, deviceConfig.Subscriptions);
                UpdateTagNums(nextSubscrNode);
            }
            else if (deviceNodeTag is ItemConfig)
            {
                if (selectedNode.Parent.Tag is SubscriptionConfig subscriptionConfig)
                {
                    TreeNode subscrNode = selectedNode.Parent;
                    tvDevice.RemoveNode(selectedNode, subscriptionConfig.Items);
                    UpdateTagNums(subscrNode);
                }
            }
            else if (deviceNodeTag is CommandConfig)
            {
                tvDevice.RemoveNode(selectedNode, deviceConfig.Commands);
            }
            else if (deviceNodeTag is EventSubscriptionConfig)
            {
                tvDevice.RemoveNode(selectedNode, deviceConfig.EventSubscriptions);
            }
            else if (deviceNodeTag is EventCategoryConfig)
            {
                if (selectedNode.Parent.Tag is EventSubscriptionConfig eventSubscriptionConfig)
                    tvDevice.RemoveNode(selectedNode, eventSubscriptionConfig.Categories);
            }

            DeviceConfigModified = true;
        }

        private void tvDevice_AfterSelect(object sender, TreeViewEventArgs e)
        {
            SetDeviceButtonsEnabled();

            // show parameters of the selected item
            ctrlEmptyItem.Visible = false;
            ctrlSubscription.Visible = false;
            ctrlItem.Visible = false;
            ctrlCommand.Visible = false;
            ctrlEventSubscription.Visible = false;
            ctrlEventCategory.Visible = false;
            object deviceNodeTag = e.Node?.Tag;

            if (deviceNodeTag is SubscriptionConfig subscriptionConfig)
            {
                ctrlSubscription.SubscriptionConfig = subscriptionConfig;
                ctrlSubscription.Visible = true;
            }
            else if (deviceNodeTag is ItemConfig itemConfig)
            {
                ctrlItem.ItemConfig = itemConfig;
                ctrlItem.Visible = true;
            }
            else if (deviceNodeTag is CommandConfig commandConfig)
            {
                ctrlCommand.CommandConfig = commandConfig;
                ctrlCommand.Visible = true;
            }
            else if (deviceNodeTag is EventSubscriptionConfig eventSubscriptionConfig)
            {
                ctrlEventSubscription.SubscriptionConfig = eventSubscriptionConfig;
                ctrlEventSubscription.Visible = true;
            }
            else if (deviceNodeTag is EventCategoryConfig eventCategory)
            {
                ctrlEventCategory.EventCategoryConfig = eventCategory;
                ctrlEventCategory.Visible = true;
            }
            else
            {
                ctrlEmptyItem.Visible = true;
            }
        }

        private void tvDevice_AfterExpand(object sender, TreeViewEventArgs e)
        {
            SetFolderImage(e.Node);
        }

        private void tvDevice_AfterCollapse(object sender, TreeViewEventArgs e)
        {
            SetFolderImage(e.Node);
        }
        
        private void ctrlItem_ObjectChanged(object sender, ObjectChangedEventArgs e)
        {
            DeviceConfigModified = true;
            TreeNode selectedNode = tvDevice.SelectedNode;
            TreeUpdateTypes treeUpdateTypes = (TreeUpdateTypes)e.ChangeArgument;

            if (treeUpdateTypes.HasFlag(TreeUpdateTypes.CurrentNode))
            {
                if (e.ChangedObject is SubscriptionConfig subscriptionConfig)
                {
                    selectedNode.Text = GetDisplayName(subscriptionConfig.DisplayName,
                        DriverPhrases.UnnamedSubscription);
                }
                else if (e.ChangedObject is EventSubscriptionConfig eventSubscriptionConfig)
                {
                    selectedNode.Text = GetDisplayName(eventSubscriptionConfig.DisplayName,
                        DriverPhrases.UnnamedSubscription);
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveConfig();
        }
    }
}
