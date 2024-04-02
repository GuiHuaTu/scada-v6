﻿// Copyright (c) Rapid Software LLC. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Scada.Data.Const;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Linq;

namespace Scada.Comm.Drivers.DrvSiemensS7.Protocol
{
    /// <summary>
    /// Represents a group of SiemensS7 elements.
    /// <para>Представляет группу элементов SiemensS7.</para>
    /// </summary>
    public class ElemGroup 
    {
        private string reqDescr; // the request description


        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        public ElemGroup(DataBlock dataBlock) 
        {
            Name = "";
            DataBlock = dataBlock;
            reqDescr = "";
            Elems = new List<Elem>();
            ElemData = new List<object>();
            CnlStatusID = new List<int>();
            StartTagIdx = -1; 
        }

        /// <summary>
        /// Gets or sets the data unit name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets the data block.
        /// </summary>
        public DataBlock DataBlock { get; }

        /// <summary>
        /// Gets a description of the request that reads the element group.
        /// </summary>
        //public override string ReqDescr
        //{
        //    get
        //    {
        //        if (string.IsNullOrEmpty(reqDescr))
        //            reqDescr = string.Format(SiemensS7Phrases.Request, Name);
        //        return reqDescr;
        //    }
        //}

        /// <summary>
        /// Gets the elements of the group.
        /// </summary>
        public List<Elem> Elems { get; private set; }

        /// <summary>
        /// Gets the raw data of the group elements.
        /// </summary>
        //public byte[][] ElemData { get; private set; } 

        public List<object> ElemData { get; private set; }
 
        /// <summary>
        /// 每个点位读取状态
        /// </summary>
        public List<int> CnlStatusID { get; private set; }
        
        /// <summary>
        /// Gets or sets the device tag index that corresponds to the start element.
        /// </summary>
        public int StartTagIdx { get; set; }


        /// <summary>
        /// Initializes the request PDU and calculates the response length.
        /// </summary>
        //public override void InitReqPDU()
        //{
        //    // get total request amount
        //    int totalQuantity = 0;
        //    int totalDataLength = 0;

        //    foreach (Elem elem in Elems)
        //    {
        //        totalQuantity += elem.Quantity;
        //        totalDataLength += elem.DataLength;
        //    }

        //    // build PDU
        //    ReqPDU = new byte[5];
        //    ReqPDU[0] = FuncCode;
        //    ReqPDU[1] = (byte)(Address / 256);
        //    ReqPDU[2] = (byte)(Address % 256);
        //    ReqPDU[3] = (byte)(totalQuantity / 256);
        //    ReqPDU[4] = (byte)(totalQuantity % 256);

        //    // calculate response length
        //    if (DataBlock == DataBlock.S7AreaPE || DataBlock == DataBlock.S7AreaPA)
        //    {
        //        int n = totalQuantity / 8;
        //        if ((totalQuantity % 8) > 0)
        //            n++;
        //        RespPduLen = 2 + n;
        //        RespByteCnt = (byte)n;
        //    }
        //    else
        //    {
        //        RespPduLen = 2 + totalDataLength;
        //        RespByteCnt = (byte)totalDataLength;
        //    }

        //    // initialize array of element values
        //    int elemCnt = Elems.Count;
        //    ElemData = new byte[elemCnt][];

        //    for (int i = 0; i < elemCnt; i++)
        //    {
        //        Elem elem = Elems[i];
        //        byte[] elemVal = new byte[elem.DataLength];
        //        Array.Clear(elemVal, 0, elemVal.Length);
        //        ElemData[i] = elemVal;
        //    }
        //}

        /// <summary>
        /// Decodes the response PDU.
        /// </summary>
        //public override bool DecodeRespPDU(byte[] buffer, int offset, int length, out string errMsg)
        //{
        //    if (base.DecodeRespPDU(buffer, offset, length, out errMsg))
        //    {
        //        if (buffer[offset + 1] == RespByteCnt)
        //        {
        //            int len = ElemData.Length;
        //            int byteNum = offset + 2;

        //            if (DataBlock == DataBlock.S7AreaPE || DataBlock == DataBlock.S7AreaPA)
        //            {
        //                int bitNum = 0;
        //                for (int elemInd = 0; elemInd < len; elemInd++)
        //                {
        //                    ElemData[elemInd][0] = ((buffer[byteNum] >> bitNum) & 0x01) > 0 ? (byte)1 : (byte)0;

        //                    if (++bitNum == 8)
        //                    {
        //                        bitNum = 0;
        //                        byteNum++;
        //                    }
        //                }
        //            }
        //            else
        //            {
        //                for (int elemInd = 0; elemInd < len; elemInd++)
        //                {
        //                    byte[] elemVal = ElemData[elemInd];
        //                    int elemDataLen = Elems[elemInd].DataLength;

        //                    // copy the read bytes in reverse order
        //                    for (int i = elemDataLen - 1, j = byteNum; i >= 0; i--, j++)
        //                    {
        //                        elemVal[i] = buffer[j];
        //                    }

        //                    byteNum += elemDataLen;
        //                }
        //            }

        //            return true;
        //        }
        //        else
        //        {
        //            errMsg = SiemensS7Phrases.InvalidPduData;
        //            return false;
        //        }
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}

        /// <summary>
        /// Creates a new SiemensS7 element.
        /// </summary>
        public virtual Elem CreateElem()
        {
            return new Elem();
        }

        /// <summary>
        /// Gets the element value according to its type, converted to double.
        /// </summary>
        public double GetElemVal(ElemType elemType ,object elemData)
        {
            byte[] buf = Serialize(elemData);

            // calculate value
            switch (elemType)
            {
                case ElemType.UShort:
                    return BitConverter.ToUInt16(buf, 0);
                case ElemType.Short:
                    return BitConverter.ToInt16(buf, 0);
                case ElemType.UInt:
                    return BitConverter.ToUInt32(buf, 0);
                case ElemType.Int:
                    return BitConverter.ToInt32(buf, 0);
                case ElemType.ULong: // possible data loss
                    return BitConverter.ToUInt64(buf, 0);
                case ElemType.Long:  // possible data loss
                    return BitConverter.ToInt64(buf, 0);
                case ElemType.Float:
                    return BitConverter.ToSingle(buf, 0);
                case ElemType.Double:
                    return BitConverter.ToDouble(buf, 0);
                case ElemType.Bool:
                    return buf[0] > 0 ? 1.0 : 0.0;


                case ElemType.Byte:

                case ElemType.String:

                case ElemType.S7String:

                case ElemType.S7WString:

                case ElemType.S5Time:

                case ElemType.Counter:

                case ElemType.DateTime:

                case ElemType.DateTimeLong:

                default:
                    return 0.0;
            }
        } 
         
        /// <summary>
        /// 序列化
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static byte[] Serialize(object obj)
        {
            BinaryFormatter bf = new BinaryFormatter();
            MemoryStream stream = new MemoryStream();
            bf.Serialize(stream, obj);
            byte[] datas = stream.ToArray();
            stream.Dispose();
            return datas;
        }
        /// <summary>
        /// 反序列化
        /// </summary>
        /// <param name="datas"></param>
        /// <returns></returns>
        public static object Deserialize(byte[] datas)
        {
            BinaryFormatter bf = new BinaryFormatter();
            MemoryStream stream = new MemoryStream(datas, 0, datas.Length);
            object obj = bf.Deserialize(stream);
            stream.Dispose();
            return obj;
        }
    }
}