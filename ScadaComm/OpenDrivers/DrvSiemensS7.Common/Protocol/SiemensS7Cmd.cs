// Copyright (c) Rapid Software LLC. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Linq;

namespace Scada.Comm.Drivers.DrvSiemensS7.Protocol
{
    /// <summary>
    /// Represents a SiemensS7 command.
    /// <para>Представляет команду SiemensS7.</para>
    /// </summary>
    public class SiemensS7Cmd  
    {
        private string reqDescr; // the command description


        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        public SiemensS7Cmd(DataBlock dataBlock, bool multiple) 
        {

            string[] dataTypeArr = Enum.GetNames(typeof(DataBlock));
            if (!dataTypeArr.Contains<string>(Enum.GetName(typeof(DataBlock), dataBlock)))
            {
                throw new InvalidOperationException(SiemensS7Phrases.IllegalDataBlock);
            }
            //if (dataBlock != DataBlock.S7AreaPE &&
            //    dataBlock != DataBlock.S7AreaPA &&
            //    dataBlock != DataBlock.S7AreaDB &&
            //    dataBlock != DataBlock.S7AreaMK &&
            //    dataBlock != DataBlock.S7AreaTM &&
            //    dataBlock != DataBlock.S7AreaCT)
            //{
            //    throw new InvalidOperationException(SiemensS7Phrases.IllegalDataBlock);
            //}

            Name = "";
            reqDescr = "";
            Multiple = multiple;
            Address = "";
            DataBlock = dataBlock;
            ElemType = ElemType.Undefined;
            ElemCnt = 1;
            ByteOrder = null;
            CmdNum = 0;
            CmdCode = "";
            Value = 0;
            Data = null;

            //SetFuncCode(SiemensS7Utils.GetWriteFuncCode(dataBlock, multiple));
        }

        /// <summary>
        /// Gets or sets the data unit name.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Gets or sets the data unit name.
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Gets the data block.
        /// </summary>
        public DataBlock DataBlock { get; }
        /// <summary>
        /// Gets a description of the request that writes the command.
        /// 获取写入命令的请求的描述。
        /// </summary>
        //public override string ReqDescr
        //{
        //    get
        //    {
        //        if (string.IsNullOrEmpty(reqDescr))
        //            reqDescr = string.Format(SiemensS7Phrases.Command, Name);
        //        return reqDescr;
        //    }
        //}

        /// <summary>
        /// Gets or sets a value indicating whether the command writes multiple elements.
        /// </summary>
        public bool Multiple { get; set; }

        /// <summary>
        /// Gets or sets the command element type.
        /// </summary>
        public ElemType ElemType { get; set; }

        /// <summary>
        /// Gets or sets the number of elements written by the command.
        /// </summary>
        public int ElemCnt { get; set; }

        /// <summary>
        /// Gets or sets the byte order.
        /// </summary>
        public int[] ByteOrder { get; set; }

        /// <summary>
        /// Gets or sets the command number.
        /// </summary>
        public int CmdNum { get; set; }

        /// <summary>
        /// Gets or sets the command code.
        /// </summary>
        public string CmdCode { get; set; }

        /// <summary>
        /// Gets or sets the command value.
        /// </summary>
        public ushort Value { get; set; }

        /// <summary>
        /// Gets or sets the data of the multiple command.
        /// </summary>
        public byte[] Data { get; set; }


        /// <summary>
        /// Initializes the request PDU and calculates the response length.
        /// </summary>
        //public override void InitReqPDU()
        //{
        //    //if (DataBlock == DataBlock.S7AreaMK)
        //    //{
        //    //    // build PDU for custom command
        //    //    int dataLength = Data == null ? 0 : Data.Length;
        //    //    ReqPDU = new byte[1 + dataLength];
        //    //    ReqPDU[0] = FuncCode;
                
        //    //    if (dataLength > 0)
        //    //        Buffer.BlockCopy(Data, 0, ReqPDU, 1, dataLength);

        //    //    RespPduLen = ReqPDU.Length; // assuming echo
        //    //}
        //    //else if (Multiple)
        //    //{
        //    //    // build PDU for WriteMultipleCoils and WriteMultipleRegisters commands
        //    //    int quantity;   // quantity of registers
        //    //    int dataLength; // data length in bytes

        //    //    if (DataBlock == DataBlock.S7AreaPE)
        //    //    {
        //    //        quantity = ElemCnt;
        //    //        dataLength = (ElemCnt % 8 == 0) ? ElemCnt / 8 : ElemCnt / 8 + 1;
        //    //    }
        //    //    else
        //    //    {
        //    //        quantity = ElemCnt * SiemensS7Utils.GetQuantity(ElemType);
        //    //        dataLength = quantity * 2;
        //    //    }

        //    //    ReqPDU = new byte[6 + dataLength];
        //    //    ReqPDU[0] = FuncCode;
        //    //    ReqPDU[1] = (byte)(Address / 256);
        //    //    ReqPDU[2] = (byte)(Address % 256);
        //    //    ReqPDU[3] = (byte)(quantity / 256);
        //    //    ReqPDU[4] = (byte)(quantity % 256);
        //    //    ReqPDU[5] = (byte)dataLength;

        //    //    SiemensS7Utils.ApplyByteOrder(Data, 0, ReqPDU, 6, dataLength, ByteOrder, false);

        //    //    // set response length
        //    //    RespPduLen = 5;
        //    //}
        //    //else
        //    //{
        //    //    // build PDU for WriteSingleCoil and WriteSingleRegister commands
        //    //    int dataLength = DataBlock == DataBlock.S7AreaPE ? 2 : SiemensS7Utils.GetDataLength(ElemType);
        //    //    ReqPDU = new byte[3 + dataLength];
        //    //    ReqPDU[0] = FuncCode;
        //    //    ReqPDU[1] = (byte)(Address / 256);
        //    //    ReqPDU[2] = (byte)(Address % 256);

        //    //    if (DataBlock == DataBlock.S7AreaPE)
        //    //    {
        //    //        ReqPDU[3] = Value > 0 ? (byte)0xFF : (byte)0x00;
        //    //        ReqPDU[4] = 0x00;
        //    //    }
        //    //    else
        //    //    {

        //    //        byte[] data = dataLength == 2 ?
        //    //            new byte[] // standard SiemensS7
        //    //            {
        //    //                (byte)(Value / 256),
        //    //                (byte)(Value % 256)
        //    //            } :
        //    //            Data;

        //    //        SiemensS7Utils.ApplyByteOrder(data, 0, ReqPDU, 3, dataLength, ByteOrder, false);
        //    //    }

        //    //    // set response length
        //    //    RespPduLen = ReqPDU.Length; // echo
        //    //}
        //}

        /// <summary>
        /// Decodes the response PDU.
        /// </summary>
        //public override bool DecodeRespPDU(byte[] buffer, int offset, int length, out string errMsg)
        //{
        //    //if (base.DecodeRespPDU(buffer, offset, length, out errMsg))
        //    //{
        //    //    if (DataBlock == DataBlock.S7AreaMK ||
        //    //        buffer[offset + 1] == ReqPDU[1] && buffer[offset + 2] == ReqPDU[2] &&
        //    //        buffer[offset + 3] == ReqPDU[3] && buffer[offset + 4] == ReqPDU[4])
        //    //    {
        //    //        return true;
        //    //    }
        //    //    else
        //    //    {
        //    //        errMsg = SiemensS7Phrases.InvalidPduData;
        //    //        return false;
        //    //    }
        //    //}
        //    //else
        //    //{
        //    //    return false;
        //    //}
        //    errMsg = "";
        //    return false;
        //}

        /// <summary>
        /// Sets the command data, converted according to the command element type.
        /// </summary>
        public void SetCmdData(double cmdVal)
        {
            bool reverse = true;

            switch (ElemType)
            {
                case ElemType.UShort:
                    Data = BitConverter.GetBytes((ushort)cmdVal);
                    break;
                case ElemType.Short:
                    Data = BitConverter.GetBytes((short)cmdVal);
                    break;
                case ElemType.UInt:
                    Data = BitConverter.GetBytes((uint)cmdVal);
                    break;
                case ElemType.Int:
                    Data = BitConverter.GetBytes((int)cmdVal);
                    break;
                case ElemType.ULong:
                    Data = BitConverter.GetBytes((ulong)cmdVal);
                    break;
                case ElemType.Long:
                    Data = BitConverter.GetBytes((long)cmdVal);
                    break;
                case ElemType.Float:
                    Data = BitConverter.GetBytes((float)cmdVal);
                    break;
                case ElemType.Double:
                    Data = BitConverter.GetBytes(cmdVal);
                    break;
                default:
                    Data = BitConverter.GetBytes(cmdVal);
                    reverse = false;
                    break;
            }

            if (reverse)
                Array.Reverse(Data);
        }
    }
}
