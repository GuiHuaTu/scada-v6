// Copyright (c) Rapid Software LLC. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using S7.Net;

namespace Scada.Comm.Drivers.DrvSiemensS7.Protocol
{
    /// <summary>
    /// Specifies the SiemensS7 data blocks.
    /// <para>Задаёт блоки данных SiemensS7.</para>
    /// </summary>
    public enum DataBlock 
    {
        Input = DataType.Input,
        Output= DataType.Output,
        Memory= DataType.Memory,
        DataBlock= DataType.DataBlock,
        Timer= DataType.Timer,
        Counter= DataType.Counter,
    }
    

}
