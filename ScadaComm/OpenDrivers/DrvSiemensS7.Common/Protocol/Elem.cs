// Copyright (c) Rapid Software LLC. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Scada.Comm.Devices;
using System.Net;

namespace Scada.Comm.Drivers.DrvSiemensS7.Protocol
{
    /// <summary>
    /// Represents a SiemensS7 element (register).
    /// <para>Представляет элемент (регистр) SiemensS7.</para>
    /// </summary>
    public class Elem 
    {
        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        public Elem() 
        {
            Name = "";
            ElemType = ElemType.Undefined; 
            Address = "";
            ByteOrder = null;
            Aux = null;
        }

        /// <summary>
        /// Gets the device tag corresponding to the item.
        /// </summary>
        public DeviceTag DeviceTag { get; set; }

        /// <summary>
        /// Gets or sets the element name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the element type.
        /// </summary>
        public ElemType ElemType { get; set; }

        /// <summary>
        /// Gets or sets the tag address with the element.
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Gets or sets the byte order.
        /// </summary>
        public int[] ByteOrder { get; set; }

        /// <summary>
        /// Gets or sets the auxiliary object.
        /// </summary>
        public object Aux { get; set; }

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
        /// Gets the element data length.
        /// </summary>
        public virtual int DataLength
        {
            get
            {
                return SiemensS7Utils.GetDataLength(ElemType);
            }
        }

        //public override string ReqDescr => throw new System.NotImplementedException();
    }
}
