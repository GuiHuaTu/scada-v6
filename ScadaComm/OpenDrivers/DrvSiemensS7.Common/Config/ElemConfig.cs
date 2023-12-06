// Copyright (c) Rapid Software LLC. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Scada.Comm.Devices;
using Scada.Comm.Drivers.DrvSiemensS7.Protocol;
using Scada.Data.Const;
using Scada.Data.Entities;

//using Scada.ComponentModel;
using System.Net;

namespace Scada.Comm.Drivers.DrvSiemensS7.Config
{
    /// <summary>
    /// Represents an element configuration.
    /// <para>Представляет конфигурацию элемента.</para>
    /// </summary>
    public class ElemConfig:Cnl
    {
        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        public ElemConfig()
        {
            Active = true;
            CnlTypeID = Data.Const.CnlTypeID.Input;
            ElemType = ElemType.Undefined;
            Address = "";
            ByteOrder = "";
            ReadOnly = false;
            IsBitMask = false;
        }

        /// <summary>
        /// Gets or sets the number of the published channel.
        /// </summary>
        //[DisplayName, Category, Description]
        //public int CnlNum { get; set; } = 0;

        /// <summary>
        /// Gets or sets the tag address with the element.
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Gets or sets the element type.
        /// </summary>
        public ElemType ElemType { get; set; }

        /// <summary>
        /// Gets or sets the byte order.
        /// </summary>
        public string ByteOrder { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the element is read only.
        /// </summary>
        public bool ReadOnly { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the element represents a bit mask.
        /// </summary>
        public bool IsBitMask { get; set; }

        /// <summary>
        /// Gets the quantity of addresses.
        /// </summary>
        public virtual int Quantity
        {
            get
            {
                return SiemensS7Utils.GetQuantity(ElemType);
            }
        }

        /// <summary>
        /// Converts the channel prototype to a device tag.
        /// </summary>
        public DeviceTag ToDeviceTag()
        {
            return new DeviceTag(TagCode, Name)
            {
                DataType = (TagDataType)(DataTypeID ?? 0),
                DataLen = DataLen ?? 1,
                Format = GetTagFormat()
            };
        }


        /// <summary>
        /// Gets or sets the channel format code.
        /// </summary>
        public string FormatCode { get; set; }

        /// <summary>
        /// Gets a tag format by the format code of the channel prototype.
        /// </summary>
        private TagFormat GetTagFormat()
        {
            if (string.IsNullOrEmpty(FormatCode))
                return null;

            switch (FormatCode)
            {
                case Data.Const.FormatCode.N0:
                    return TagFormat.IntNumber;

                case Data.Const.FormatCode.X:
                case Data.Const.FormatCode.X2:
                case Data.Const.FormatCode.X4:
                case Data.Const.FormatCode.X8:
                    return TagFormat.HexNumber;

                case Data.Const.FormatCode.DateTime:
                case Data.Const.FormatCode.Date:
                case Data.Const.FormatCode.Time:
                    return TagFormat.DateTime;

                case Data.Const.FormatCode.String:
                    return TagFormat.String;

                case Data.Const.FormatCode.OffOn:
                    return TagFormat.OffOn;

                default:
                    return null;
            }
        }

    }
}
