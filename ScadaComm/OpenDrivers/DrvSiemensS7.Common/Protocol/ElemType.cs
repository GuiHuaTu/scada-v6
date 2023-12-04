// Copyright (c) Rapid Software LLC. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

namespace Scada.Comm.Drivers.DrvSiemensS7.Protocol
{
    /// <summary>
    /// Specifies the SiemensS7 element types.
    /// <para>Задаёт типы элементов SiemensS7.</para>
    /// </summary>
    public enum ElemType
    {
        /// <summary>
        /// Undefined.
        /// </summary>
        Undefined,

        /// <summary>
        /// Unsigned 16-bit integer.
        /// </summary>
        UShort,

        /// <summary>
        /// Signed 16-bit integer.
        /// </summary>
        Short,

        /// <summary>
        /// Unsigned 32-bit integer.
        /// </summary>
        UInt,

        /// <summary>
        /// Signed 32-bit integer.
        /// </summary>
        Int,

        /// <summary>
        /// Unsigned 64-bit integer.
        /// </summary>
        ULong,

        /// <summary>
        /// Signed 64-bit integer.
        /// </summary>
        Long,

        /// <summary>
        /// 32-bit floating-point number.
        /// </summary>
        Float,

        /// <summary>
        /// 64-bit floating-point number.
        /// </summary>
        Double,

        /// <summary>
        /// Logical.
        /// </summary>
        Bool,

        /// <summary>
        /// Unsigned 8-bit.
        /// </summary>
        Byte,

        /// <summary>
        /// Unsigned 8-bit.
        /// </summary>
        String,

        S7String,
        S7WString,
        S5Time,
        Counter,
        DateTime,
        DateTimeLong
    }
     

}
