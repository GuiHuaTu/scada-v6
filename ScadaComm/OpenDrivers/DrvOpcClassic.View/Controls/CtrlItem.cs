// Copyright (c) Rapid Software LLC. All rights reserved.

using Scada.Comm.Drivers.DrvOpcClassic.Config;
using Scada.Forms;
using System.ComponentModel;

namespace Scada.Comm.Drivers.DrvOpcClassic.View.Controls
{
    /// <summary>
    /// Represents a control for editing a monitored item.
    /// <para>Представляет элемент управления для редактирования отслеживаемого элемента.</para>
    /// </summary>
    public partial class CtrlItem : UserControl
    {
        private ItemConfig itemConfig;


        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        public CtrlItem()
        {
            InitializeComponent();
            cbDataType.Items.AddRange(DriverUtils.TypeNames);
        }


        /// <summary>
        /// Gets or sets the edited monitored item.
        /// </summary>
        internal ItemConfig ItemConfig
        {
            get
            {
                return itemConfig;
            }
            set
            {
                itemConfig = null;
                ShowItemProps(value);
                itemConfig = value;
            }
        }


        /// <summary>
        /// Shows the monitored item properties.
        /// </summary>
        private void ShowItemProps(ItemConfig itemConfig)
        {
            if (itemConfig != null)
            {
                chkActive.Checked = itemConfig.Active;
                txtPath.Text = itemConfig.Path;
                txtName.Text = itemConfig.Name;
                txtTagCode.Text = itemConfig.TagCode;
                txtTagNum.Text = itemConfig.Tag is ItemConfigTag tag ? tag.TagNumStr : "";
                cbDataType.Text = itemConfig.DataTypeName;
                pbDataTypeWarning.Visible = string.IsNullOrWhiteSpace(itemConfig.DataTypeName);
                chkIsString.Checked = itemConfig.IsString;
                chkIsArray.Checked = itemConfig.IsArray;
                chkIsArray.Enabled = !itemConfig.IsString;
                numDataLen.Enabled = itemConfig.IsString || itemConfig.IsArray;
                numDataLen.SetValue(itemConfig.DataLen);
            }
        }

        /// <summary>
        /// Raises an ObjectChanged event.
        /// </summary>
        private void OnObjectChanged(object changeArgument)
        {
            ObjectChanged?.Invoke(this, new ObjectChangedEventArgs(itemConfig, changeArgument));
        }

        /// <summary>
        /// Refreshes the displayed tag number.
        /// </summary>
        public void RefreshTagNum()
        {
            txtTagNum.Text = itemConfig?.Tag is ItemConfigTag tag ? tag.TagNumStr : "";
        }


        /// <summary>
        /// Occurs when the edited object changes.
        /// </summary>
        [Category("Property Changed")]
        public event EventHandler<ObjectChangedEventArgs> ObjectChanged;


        private void chkActive_CheckedChanged(object sender, EventArgs e)
        {
            if (itemConfig != null)
            {
                itemConfig.Active = chkActive.Checked;
                OnObjectChanged(TreeUpdateTypes.None);
            }
        }

        private void txtTagCode_TextChanged(object sender, EventArgs e)
        {
            if (itemConfig != null)
            {
                itemConfig.TagCode = txtTagCode.Text;
                OnObjectChanged(TreeUpdateTypes.None);
            }
        }

        private void cbDataType_TextChanged(object sender, EventArgs e)
        {
            if (itemConfig != null)
            {
                itemConfig.DataTypeName = cbDataType.Text;
                pbDataTypeWarning.Visible = string.IsNullOrWhiteSpace(itemConfig.DataTypeName);
                OnObjectChanged(TreeUpdateTypes.None);

                chkIsArray.Checked = false;
                numDataLen.SetValue(1);

                if (itemConfig.IsString)
                {
                    chkIsString.Checked = true;
                    chkIsArray.Enabled = false;
                    numDataLen.Enabled = true;
                }
                else
                {
                    chkIsString.Checked = false;
                    chkIsArray.Enabled = true;
                    numDataLen.Enabled = false;
                }
            }
        }

        private void chkIsArray_CheckedChanged(object sender, EventArgs e)
        {
            if (itemConfig != null)
            {
                itemConfig.IsArray = chkIsArray.Checked;
                OnObjectChanged(TreeUpdateTypes.None);

                if (chkIsString.Checked || chkIsArray.Checked)
                {
                    numDataLen.Enabled = true;
                }
                else
                {
                    numDataLen.SetValue(1);
                    numDataLen.Enabled = false;
                }
            }
        }

        private void numArrayLen_ValueChanged(object sender, EventArgs e)
        {
            if (itemConfig != null)
            {
                itemConfig.DataLen = Convert.ToInt32(numDataLen.Value);
                OnObjectChanged(TreeUpdateTypes.None);
            }
        }
    }
}
