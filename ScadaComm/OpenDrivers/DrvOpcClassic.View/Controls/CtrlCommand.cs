// Copyright (c) Rapid Software LLC. All rights reserved.

using Scada.Comm.Drivers.DrvOpcClassic.Config;
using Scada.Forms;
using System.ComponentModel;

namespace Scada.Comm.Drivers.DrvOpcClassic.View.Controls
{
    /// <summary>
    /// Represents a control for editing a command.
    /// <para>Представляет элемент управления для редактирования команды.</para>
    /// </summary>
    public partial class CtrlCommand : UserControl
    {
        private CommandConfig commandConfig;


        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        public CtrlCommand()
        {
            InitializeComponent();
            cbDataType.Items.AddRange(DriverUtils.TypeNames);
        }


        /// <summary>
        /// Gets or sets the edited command.
        /// </summary>
        internal CommandConfig CommandConfig
        {
            get
            {
                return commandConfig;
            }
            set
            {
                commandConfig = null;
                ShowCommandProps(value);
                commandConfig = value;
            }
        }


        /// <summary>
        /// Shows the command properties.
        /// </summary>
        private void ShowCommandProps(CommandConfig commandConfig)
        {
            if (commandConfig != null)
            {
                txtPath.Text = commandConfig.Path;
                txtName.Text = commandConfig.Name;
                txtCmdCode.Text = commandConfig.CmdCode;
                numCmdNum.SetValue(commandConfig.CmdNum);
                cbDataType.Text = commandConfig.DataTypeName;
                pbDataTypeWarning.Visible = string.IsNullOrWhiteSpace(commandConfig.DataTypeName);
            }
        }

        /// <summary>
        /// Raises an ObjectChanged event.
        /// </summary>
        private void OnObjectChanged(object changeArgument)
        {
            ObjectChanged?.Invoke(this, new ObjectChangedEventArgs(commandConfig, changeArgument));
        }


        /// <summary>
        /// Occurs when the edited object changes.
        /// </summary>
        [Category("Property Changed")]
        public event EventHandler<ObjectChangedEventArgs> ObjectChanged;


        private void txtCmdCode_TextChanged(object sender, EventArgs e)
        {
            if (commandConfig != null)
            {
                commandConfig.CmdCode = txtCmdCode.Text;
                OnObjectChanged(TreeUpdateTypes.None);
            }
        }

        private void numCmdNum_ValueChanged(object sender, EventArgs e)
        {
            if (commandConfig != null)
            {
                commandConfig.CmdNum = Convert.ToInt32(numCmdNum.Value);
                OnObjectChanged(TreeUpdateTypes.None);
            }
        }

        private void cbDataType_TextChanged(object sender, EventArgs e)
        {
            if (commandConfig != null)
            {
                commandConfig.DataTypeName = cbDataType.Text;
                pbDataTypeWarning.Visible = string.IsNullOrWhiteSpace(commandConfig.DataTypeName);
                OnObjectChanged(TreeUpdateTypes.None);
            }
        }
    }
}
