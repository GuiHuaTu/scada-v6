// Copyright (c) Rapid Software LLC. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Scada.Comm.Drivers.DrvSiemensS7.Protocol;
//using Scada.ComponentModel;
using System.Net;

namespace Scada.Comm.Drivers.DrvSiemensS7.Config
{
    /// <summary>
    /// Represents an element configuration.
    /// <para>Представляет конфигурацию элемента.</para>
    /// </summary>
    public class ElemConfig
    {
        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        public ElemConfig()
        {
            Name = "";
            TagCode = "";
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
        /// Gets or sets the element name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the tag code associated with the element.
        /// </summary>
        public string TagCode { get; set; }

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
 
    }
}
