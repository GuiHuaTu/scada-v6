﻿// Copyright (c) Rapid Software LLC. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Scada.Comm.Drivers.DrvSiemensS7.Config;
using Scada.Comm.Drivers.DrvSiemensS7.Protocol;
using Scada.Forms;
using System.ComponentModel;

namespace Scada.Comm.Drivers.DrvSiemensS7.View.Controls
{
    /// <summary>
    /// Represents a control for editing a SiemensS7 element.
    /// <para>Представляет элемент управления для редактирования элемента SiemensS7.</para>
    /// </summary>
    public partial class CtrlElem : UserControl
    {
        private ElemTag elemTag; // the element metadata


        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        public CtrlElem()
        {
            InitializeComponent();
            elemTag = null;
        }


        /// <summary>
        /// Gets or sets the element for editing.
        /// </summary>
        public ElemTag ElemTag
        {
            get
            {
                return elemTag;
            }
            set
            {
                elemTag = null; // to avoid ObjectChanged event
                ShowElemConfig(value);
                elemTag = value;
            }
        }


        /// <summary>
        /// Shows the element configuration.
        /// </summary>
        private void ShowElemConfig(ElemTag elemTag)
        {
            if (elemTag == null)
            {
                txtElemName.Text = "";
                txtElemTagCode.Text = "";
                pnlElemTagCodeWarn.Visible = false;
                txtElemTagNum.Text = "";
                txtElemAddress.Text = "";
                rbBool.Checked = true;
                //txtElemByteOrder.Text = "";
                chkElemReadOnly.Checked = false;
                gbElem.Enabled = false;
            }
            else
            {
                ElemGroupConfig elemGroup = elemTag.ElemGroup;
                ElemConfig elem = elemTag.Elem;

                txtElemName.Text = elem.Name;
                txtElemTagCode.Text = elem.TagCode;
                pnlElemTagCodeWarn.Visible = string.IsNullOrEmpty(elem.TagCode);
                txtElemTagNum.Text = elemTag.TagNum.ToString();
                txtElemAddress.Text = elemTag.Elem.Address ;

                //if (elem.ElemType == ElemType.Bool)
                //{
                //    rbUShort.Enabled = rbShort.Enabled = rbUInt.Enabled = rbInt.Enabled =
                //        rbULong.Enabled = rbLong.Enabled = rbFloat.Enabled = rbDouble.Enabled = false;
                //    rbBool.Enabled = true;
                //}
                //else
                //{
                //    rbUShort.Enabled = rbShort.Enabled = rbUInt.Enabled = rbInt.Enabled =
                //        rbULong.Enabled = rbLong.Enabled = rbFloat.Enabled = rbDouble.Enabled = true;
                //    rbBool.Enabled = false;
                //}

                switch (elem.ElemType)
                {
                    case ElemType.UShort:
                        rbUShort.Checked = true;
                        break;
                    case ElemType.Short:
                        rbShort.Checked = true;
                        break;
                    case ElemType.UInt:
                        rbUInt.Checked = true;
                        break;
                    case ElemType.Int:
                        rbInt.Checked = true;
                        break;
                    case ElemType.ULong:
                        rbULong.Checked = true;
                        break;
                    case ElemType.Long:
                        rbLong.Checked = true;
                        break;
                    case ElemType.Float:
                        rbFloat.Checked = true;
                        break;
                    case ElemType.Double:
                        rbDouble.Checked = true;
                        break;
                    case ElemType.Bool:
                        rbBool.Checked = true;
                        break;
                    case ElemType.Byte:
                        rbByte.Checked = true;
                        break;
                    case ElemType.String:
                        rb_String.Checked = true;
                        break;
                    case ElemType.S7String:
                        rb_S7String.Checked = true;
                        break;
                    case ElemType.S7WString:
                        rb_S7WString.Checked = true;
                        break;
                    case ElemType.S5Time:
                        rb_S5Time.Checked = true;
                        break;
                    case ElemType.Counter:
                        rb_Counter.Checked = true;
                        break;
                    case ElemType.DateTime:
                        rb_DateTime.Checked = true;
                        break;
                    case ElemType.DateTimeLong:
                        rb_DateTimeLong.Checked = true;
                        break;
                }

                //txtElemByteOrder.Text = elem.ByteOrder;
                //txtElemByteOrder.Enabled = elemGroup.ByteOrderEnabled;
                chkElemReadOnly.Checked = elem.ReadOnly;
                chkElemReadOnly.Enabled = elemGroup.ReadOnlyEnabled;
                //chkElemIsBitMask.Checked = elem.IsBitMask;
                //chkElemIsBitMask.Enabled = elemGroup.BitMaskEnabled;
                gbElem.Enabled = true;
            }
        }

        /// <summary>
        /// Raises an ObjectChanged event.
        /// </summary>
        private void OnObjectChanged(object changeArgument)
        {
            ObjectChanged?.Invoke(this, new ObjectChangedEventArgs(elemTag, changeArgument));
        }

        /// <summary>
        /// Sets input focus to the control.
        /// </summary>
        public void SetFocus()
        {
            txtElemName.Select();
        }


        /// <summary>
        /// Occurs when the edited object changes.
        /// </summary>
        [Category("Property Changed")]
        public event EventHandler<ObjectChangedEventArgs> ObjectChanged;


        private void txtElemName_TextChanged(object sender, EventArgs e)
        {
            // update element name
            if (elemTag != null)
            {
                bool updateTagCode = elemTag.Elem.Name == elemTag.Elem.TagCode;
                elemTag.Elem.Name = txtElemName.Text;
                OnObjectChanged(TreeUpdateTypes.CurrentNode);

                if (updateTagCode)
                    txtElemTagCode.Text = txtElemName.Text;
            }
        }

        private void txtElemTagCode_TextChanged(object sender, EventArgs e)
        {
            // update tag code
            if (elemTag != null)
            {
                elemTag.Elem.TagCode = txtElemTagCode.Text;
                pnlElemTagCodeWarn.Visible = txtElemTagCode.Text == "";
                OnObjectChanged(TreeUpdateTypes.None);
            }
        }

        private void rbType_CheckedChanged(object sender, EventArgs e)
        {
            // update element type
            if (elemTag != null && ((RadioButton)sender).Checked)
            {
                ElemType elemType;

                if (rbUShort.Checked)
                    elemType = ElemType.UShort;
                else if (rbShort.Checked)
                    elemType = ElemType.Short;
                else if (rbUInt.Checked)
                    elemType = ElemType.UInt;
                else if (rbInt.Checked)
                    elemType = ElemType.Int;
                else if (rbULong.Checked)
                    elemType = ElemType.ULong;
                else if (rbLong.Checked)
                    elemType = ElemType.Long;
                else if (rbFloat.Checked)
                    elemType = ElemType.Float;
                else if (rbDouble.Checked)
                    elemType = ElemType.Double;
                else if (rbByte.Checked)
                    elemType = ElemType.Byte;
                else if (rb_String.Checked)
                    elemType = ElemType.String;
                else if (rb_S7String.Checked)
                    elemType = ElemType.S7String;
                else if (rb_S7WString.Checked)
                    elemType = ElemType.S7WString;
                else if (rb_S5Time.Checked)
                    elemType = ElemType.S5Time;
                else if (rb_Counter.Checked)
                    elemType = ElemType.Counter;
                else if (rb_DateTime.Checked)
                    elemType = ElemType.DateTime;
                else if (rb_DateTimeLong.Checked)
                    elemType = ElemType.DateTimeLong;
                else
                    elemType = ElemType.Bool;

                elemTag.Elem.ElemType = elemType;
                //txtElemAddress.Text = elemTag.AddressRange;
                OnObjectChanged(TreeUpdateTypes.CurrentNode | TreeUpdateTypes.NextSiblings);
            }
        }

        private void txtByteOrder_TextChanged(object sender, EventArgs e)
        {
            // update byte order
            if (elemTag != null)
            {
                //elemTag.Elem.ByteOrder = txtElemByteOrder.Text;
                OnObjectChanged(TreeUpdateTypes.None);
            }
        }

        private void chkElemReadOnly_CheckedChanged(object sender, EventArgs e)
        {
            // update read only
            if (elemTag != null)
            {
                elemTag.Elem.ReadOnly = chkElemReadOnly.Checked;
                OnObjectChanged(TreeUpdateTypes.None);
            }
        }

        private void chkElemIsBitMask_CheckedChanged(object sender, EventArgs e)
        {
            // update bit mask
            if (elemTag != null)
            {
                //elemTag.Elem.IsBitMask = chkElemIsBitMask.Checked;
                OnObjectChanged(TreeUpdateTypes.None);
            }
        }

        private void txtElemAddress_TextChanged(object sender, EventArgs e)
        { 
            // update element address
            if (elemTag != null)
            {
                elemTag.Elem.Address = txtElemAddress.Text;
                OnObjectChanged(TreeUpdateTypes.CurrentNode | TreeUpdateTypes.NextSiblings);
            }
        }
    }
}
