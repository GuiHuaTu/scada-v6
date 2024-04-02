﻿// Copyright (c) Rapid Software LLC. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Scada.Comm.Channels;
using Scada.Log;
using System.Globalization;
using S7.Net;
using S7.Net.Types;
using System.Net;
using System;
using Scada.Data.Entities;
using Scada.Lang;
using System.Xml.Linq;
using System.Text; 
using Scada.Comm.Drivers.DrvSiemensS7.Config;
using Scada.Storages;
using Scada.Data.Const;

namespace Scada.Comm.Drivers.DrvSiemensS7.Protocol
{
 
    /// <summary>
    /// Polls devices using SiemensS7 protocol.
    /// <para>Опрос устройств по протоколу SiemensS7.</para>
    /// </summary>
    public class SiemensS7Poll
    {
        /// <summary>
        /// Represetns a request method.
        /// </summary>
        public delegate bool ReadDelegate(ElemGroup elemGroup); 

        /// <summary>
        /// Gets the SiemensS7Session session
        /// </summary>
        public Plc SiemensS7Session { get; protected set; }
        /// <summary>
        /// The communication line log.
        /// </summary>
        protected ILog log;
        private readonly IStorage storage;                    // the application storage

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary> 
        public SiemensS7Poll(SiemensS7ConnectionOptions options, ILog log, IStorage storage)
        {

            SiemensS7Session = null; 
            this.storage = storage ?? throw new ArgumentNullException(nameof(storage));
            this.log = log ?? throw new ArgumentNullException(nameof(log));

            CpuType = options.CpuType;
            PlcIP = options.PlcIP;
            PlcRack = options.PlcRack;
            PlcSlot = options.PlcSlot;

            Timeout = 0;
            TransactionID = 0;
            //ChooseRequestMethod();
            ReadDoRequest = Read;
        }


        public void Connection()
        {

            log.WriteLine($"开始连接S7:CpuType={CpuType},PlcIP={PlcIP},PlcRack={PlcRack},PlcSlot={PlcSlot}" );
            if (SiemensS7Session == null || !SiemensS7Session.IsConnected)
            {
                SiemensS7Session = new Plc((CpuType)Enum.Parse(typeof(CpuType), CpuType), PlcIP, PlcRack, PlcSlot);
            }

            SiemensS7Session.Open();//连接PLC

            if (SiemensS7Session.IsConnected)
            {
                log.WriteLine($"连接S7成功:CpuType={CpuType},PlcIP={PlcIP},PlcRack={PlcRack},PlcSlot={PlcSlot}" );
                log.WriteLine(SiemensS7Phrases.OK);
            }
            else
            {
                log.WriteLine($"连接S7失败:CpuType={CpuType},PlcIP={PlcIP},PlcRack={PlcRack},PlcSlot={PlcSlot}" );
                log.WriteLine(SiemensS7Phrases.CommError);
            }

        }

        public void Disconnect()
        {
            if (SiemensS7Session != null)
            {
                log.WriteLine();

                try
                {
                    if (SiemensS7Session.IsConnected)
                    {
                        log.WriteAction(Locale.IsRussian ?
                            "Отключение от SiemensS7Session-сервера" :
                            "Disconnect from SiemensS7Session ");
                        SiemensS7Session.Close();
                    }
                }
                catch (Exception ex)
                {
                    log.WriteError(ex.BuildErrorMessage(Locale.IsRussian ?
                        "Ошибка при отключении от SiemensS7Session-сервера" :
                        "Error disconnecting SiemensS7Session "));
                }

                SiemensS7Session = null;
            }
        }

        /// <summary>
        /// Gets the data transfer mode.
        /// </summary>
        //public TransMode TransMode { get; }
        /// <summary>
        ///  Gets the SiemensS7Session cpu type.
        /// </summary>
        public string CpuType { get; }
        public string PlcIP { get; }
        public short PlcRack { get; }
        public short PlcSlot { get; }

        /// <summary>
        /// Gets the input buffer.
        /// </summary>
        public byte[] InBuf { get; protected set; }

        /// <summary>
        /// Gets or sets the reading timeout, ms.
        /// </summary>
        public int Timeout { get; set; }


        /// <summary>
        /// Gets or sets the communication line log.
        /// </summary>
        public ILog Log
        {
            get
            {
                return log;
            }
            set
            {
                log = value ?? LogStub.Instance;
            }
        }

        /// <summary>
        /// Gets the current transaction ID in the TCP mode.
        /// </summary>
        public ushort TransactionID { get; protected set; }

        /// <summary>
        /// Gets the request method.
        /// </summary>
        //public RequestDelegate DoRequest { get; protected set; }
        public ReadDelegate ReadDoRequest { get; protected set; } 

        public bool Read(ElemGroup elemGroup)
        {

            if (elemGroup.Elems.Count > 0)
            {
                int okCount = 0;
                //log.WriteLine($"------------------------------------------------------------------------------------------");
                //log.WriteLine($"--------- SiemenssS7 Read ElemGroupName={elemGroup.Name}:Start");
                //log.WriteLine($"--------- SiemenssS7 Read ElemGroupName={elemGroup.Name}:ElemGroup.Elems.Count={elemGroup.Elems.Count}");
                for (int i = 0; i < elemGroup.Elems.Count; i++)
                {
                    try
                    {
                        //log.WriteLine("");
                        //string val = ReadValue(elemGroup.Elems[i]).ToString();
                        object val = ReadValueV2(elemGroup.Elems[i]) ;

                        //log.WriteLine($"--------- SiemenssS7 ReadValue:ElemName={elemGroup.Elems[i].Name} Address={elemGroup.Elems[i].Address}" +
                        //    $" ElemType={elemGroup.Elems[i].ElemType} ReadValue={val}");

                        //log.WriteLine($"--------- SiemenssS7 Read ElemGroupName={elemGroup.Name}:ElemGroup.ElemData.Count={elemGroup.ElemData.Count}");

                        elemGroup.ElemData[i] = val;

                        elemGroup.CnlStatusID[i] = CnlStatusID.Defined;

                        okCount = okCount + 1;
                    }
                    catch (Exception ex)
                    {
                        elemGroup.CnlStatusID[i] = CnlStatusID.Error;

                        log.WriteLine($"点位信息:{elemGroup.Elems[i].Name}|{elemGroup.Elems[i].ElemType.ToString()}|{elemGroup.Elems[i].Address}，采集失败Error: " + ex.Message);
                    }
                }

                if(okCount == elemGroup.Elems.Count)//采集全部成功
                {
                    //log.WriteLine($"--------- SiemenssS7 Read ElemGroupName={elemGroup.Name}:OK");
                    return true;
                }
                else{
                    //log.WriteLine($"--------- SiemenssS7 Read ElemGroupName={elemGroup.Name}:Err");
                    return false;
                }
            }
            else
            {
                return false;
            }
        }



        public object ReadValue(Elem elem)
        {

            if (SiemensS7Session != null && SiemensS7Session.IsConnected)
            {
                var plcData = new PLCAddress(elem.Address);
                VarType useType = GetS7VarTypeByElemType(elem.ElemType);

                //log.WriteLine($"--------- SiemenssS7 ReadValue:ElemName={elem.Name} Address={elem.Address} ElemType={elem.ElemType} VarType={useType}");
                switch (useType)
                {
                    case VarType.Bit:
                        bool result = Convert.ToBoolean(SiemensS7Session.Read(elem.Address));
                        return result ? 1 : 0;
                        break;
                    case VarType.Byte:
                        return SiemensS7Session.Read(elem.Address);
                        break;
                    case VarType.Word:
                        return SiemensS7Session.Read(elem.Address);
                        break;
                    case VarType.DWord:
                        return SiemensS7Session.Read(elem.Address);
                        break;
                    case VarType.Int:
                        return SiemensS7Session.Read(plcData.DataType, plcData.DbNumber, plcData.StartByte, VarType.Int, 1);
                        break;
                    case VarType.DInt:
                        return SiemensS7Session.Read(plcData.DataType, plcData.DbNumber, plcData.StartByte, VarType.DInt, 1);
                        break;
                    case VarType.Real:
                        return SiemensS7Session.Read(plcData.DataType, plcData.DbNumber, plcData.StartByte, VarType.Real, 1);
                        break;
                    case VarType.LReal:
                        return SiemensS7Session.Read(plcData.DataType, plcData.DbNumber, plcData.StartByte, VarType.LReal, 1);
                        break;
                    case VarType.String:
                        byte count = (byte)SiemensS7Session.Read(plcData.DataType, plcData.DbNumber, plcData.StartByte + 1, VarType.Byte, 1);
                        return SiemensS7Session.Read(plcData.DataType, plcData.DbNumber, plcData.StartByte + 2, VarType.String, count);
                        break;
                    case VarType.S7String:
                        byte S7StringCount = (byte)SiemensS7Session.Read(plcData.DataType, plcData.DbNumber, plcData.StartByte + 1, VarType.Byte, 1);
                        return SiemensS7Session.Read(plcData.DataType, plcData.DbNumber, plcData.StartByte, VarType.S7String, S7StringCount);
                        break;
                    case VarType.S7WString:
                        short S7WStringCount = ((short)SiemensS7Session.Read(plcData.DataType, plcData.DbNumber, plcData.StartByte + 2, VarType.Int, 1));
                        return SiemensS7Session.Read(plcData.DataType, plcData.DbNumber, plcData.StartByte, VarType.S7WString, S7WStringCount);
                        break; 
                    case VarType.Counter:
                        break;
                    case VarType.DateTime:
                        return SiemensS7Session.Read(plcData.DataType, plcData.DbNumber, plcData.StartByte, VarType.DateTime, 1);
                        break;
                    case VarType.DateTimeLong:
                        return SiemensS7Session.Read(plcData.DataType, plcData.DbNumber, plcData.StartByte, VarType.DateTimeLong, 1);
                        break;
                    default:
                        throw new Exception("读取数据类型不存在");
                        break;
                }
                return null;
            }
            else
            {
                return null;
            }

        }



        public object ReadValueV2(Elem elem)
        {

            if (SiemensS7Session != null && SiemensS7Session.IsConnected)
            {

                var DataItem = new PLCAddress(elem.Address);
         
                #region ElemType
                switch (elem.ElemType)
                {
                    case ElemType.Bool:
                        return (bool)SiemensS7Session.Read(elem.Address);
                    case ElemType.Byte:
                        return (byte)SiemensS7Session.Read(elem.Address);
                    case ElemType.UShort:
                        return (ushort)SiemensS7Session.Read(elem.Address);
                    case ElemType.Short:
                        return ((ushort)SiemensS7Session.Read(elem.Address)).ConvertToShort();
                    case ElemType.UInt:
                        return (uint)SiemensS7Session.Read(elem.Address);
                    case ElemType.Int:
                        return ((uint)SiemensS7Session.Read(elem.Address)).ConvertToInt();
                    case ElemType.ULong:
                        return Convert.ToUInt64(SiemensS7Session.Read(elem.Address));
                    case ElemType.Long:
                        return Convert.ToInt64(SiemensS7Session.Read(elem.Address));
                    case ElemType.Float:
                        return ((uint)SiemensS7Session.Read(elem.Address)).ConvertToFloat();
                    case ElemType.Double:
                        return (SiemensS7Session.Read(elem.Address));
                    case ElemType.String:
                        StringClass tc2Generic = SiemensS7Session.ReadClass<StringClass>(DataItem.DbNumber);
                        return tc2Generic.StringVariable;
                    case ElemType.DateTime:
                        //var DataType = elem.DataItem.DataType;
                        //var db = elem.DataItem.DB;
                        //var offset = elem.DataItem.StartByteAdr;
                        var DataType = DataItem.DataType;
                        var db = DataItem.DbNumber;
                        var offset = DataItem.StartByte;
                        var value = SiemensS7Session.Read( DataType, db, offset, VarType.DateTimeLong, 1);
                        return (System.DateTime)value;
                    default:
                        throw new Exception("读取数据类型不存在");
                }
                #endregion

            }
            else
            {
                return null;
            }

        }



        public bool SetValue(SiemensS7Cmd siemensS7Cmd, object value)
        {
            try
            {
                if (SiemensS7Session != null && SiemensS7Session.IsConnected)
                {
                    var plcData = new PLCAddress(siemensS7Cmd.Address);
                    VarType useType = GetS7VarTypeByElemType(siemensS7Cmd.ElemType);

                    switch (useType)
                    {
                        case VarType.Bit:
                            SiemensS7Session.Write(plcData.DataType, plcData.DbNumber, plcData.StartByte, Convert.ToBoolean(value), plcData.BitNumber);
                            break;
                        case VarType.Byte:
                            SiemensS7Session.Write(plcData.DataType, plcData.DbNumber, plcData.StartByte, Convert.ToByte(value));
                            break;
                        case VarType.Word:
                            SiemensS7Session.Write(plcData.DataType, plcData.DbNumber, plcData.StartByte, Convert.ToUInt16(value));
                            break;
                        case VarType.DWord:
                            SiemensS7Session.Write(plcData.DataType, plcData.DbNumber, plcData.StartByte, Convert.ToUInt32(value));
                            break;
                        case VarType.Int:
                            SiemensS7Session.Write(plcData.DataType, plcData.DbNumber, plcData.StartByte, Convert.ToInt16(value));
                            break;
                        case VarType.DInt:
                            SiemensS7Session.Write(plcData.DataType, plcData.DbNumber, plcData.StartByte, Convert.ToInt32(value));
                            break;
                        case VarType.Real:
                            SiemensS7Session.Write(plcData.DataType, plcData.DbNumber, plcData.StartByte, Convert.ToSingle(value));
                            break;
                        case VarType.LReal:
                            SiemensS7Session.Write(plcData.DataType, plcData.DbNumber, plcData.StartByte, Convert.ToDouble(value));
                            break;
                        case VarType.String:
                            //byte Count = Convert.ToByte(plcInfo.ByteCount == 0 ? 255 : plcInfo.ByteCount);
                            byte Count = 255;
                            byte UsingCount = Convert.ToByte(value.ToString().Length);
                            byte[] stringArr = new byte[value.ToString().Length + 2];
                            stringArr[0] = Count;
                            stringArr[1] = UsingCount;
                            byte[] OstringArr = Encoding.ASCII.GetBytes(value.ToString());
                            for (int i = 0; i < OstringArr.Length; i++)
                            {
                                stringArr[2 + i] = OstringArr[i];
                            }
                            SiemensS7Session.Write(plcData.DataType, plcData.DbNumber, plcData.StartByte, stringArr);
                            break;
                        case VarType.S7String:
                            SiemensS7Session.Write(plcData.DataType, plcData.DbNumber, plcData.StartByte, value.ToString());
                            break;
                        case VarType.S7WString:
                            //short wsCount = Convert.ToInt16(plcInfo.ByteCount == 0 ? 255 : plcInfo.ByteCount);
                            short wsCount = 255;
                            short wsUsingCount = Convert.ToInt16(value.ToString().Length);
                            byte[] wsstringArr = new byte[value.ToString().Length * 2 + 4];
                            wsstringArr[0] = Convert.ToByte(wsCount >> 8);
                            wsstringArr[1] = Convert.ToByte(wsCount & 0x00FF);
                            wsstringArr[2] = Convert.ToByte(wsUsingCount >> 8);
                            wsstringArr[3] = Convert.ToByte(wsUsingCount & 0x00FF);
                            byte[] wsOstringArr = Encoding.BigEndianUnicode.GetBytes(value.ToString());
                            for (int i = 0; i < wsOstringArr.Length; i++)
                            {
                                wsstringArr[4 + i] = wsOstringArr[i];
                            }
                            SiemensS7Session.Write(plcData.DataType, plcData.DbNumber, plcData.StartByte, wsstringArr);
                            break; 
                        //case VarType.Counter:
                        //    break;
                        //case VarType.DateTime:
                        //    showStr = SiemensS7Session.Read(plcData.DataType, plcData.DbNumber, plcData.StartByte, VarType.DateTime, 1).ToString();
                        //    break;
                        //case VarType.DateTimeLong:
                        //    showStr = SiemensS7Session.Read(plcData.DataType, plcData.DbNumber, plcData.StartByte, VarType.DateTimeLong, 1).ToString();
                        //    break;
                        default:
                            break;
                    }
                }

                log.WriteLine(SiemensS7Phrases.OK);
                return true;
            }
            catch (Exception ex)
            {
                log.WriteLine("Error: " + ex.Message);
                return false;
            }

        }



        public bool SetValueV2(SiemensS7Cmd elem, object value)
        {
            try
            {
                if (SiemensS7Session != null && SiemensS7Session.IsConnected)
                {
                    var DataItem = new PLCAddress(elem.Address);
                
                    #region ElemType
                    switch (elem.ElemType)
                    {
                        case ElemType.Bool:
                            SiemensS7Session.Write(elem.Address, value);
                            break;
                        case ElemType.Byte:
                            SiemensS7Session.Write(elem.Address, value);
                            break;
                        case ElemType.UShort:
                            SiemensS7Session.Write(elem.Address, (ushort)value);
                            break;
                        case ElemType.Short:
                            SiemensS7Session.Write(elem.Address, ((short)value).ConvertToUshort());
                            break;
                        case ElemType.UInt:
                            SiemensS7Session.Write(elem.Address, (uint)value);
                            break;
                        case ElemType.Int:
                            SiemensS7Session.Write(elem.Address, (int)value);
                            break;
                        case ElemType.ULong:
                            SiemensS7Session.Write(elem.Address, ((ulong)value));
                            break;
                        case ElemType.Long:
                            SiemensS7Session.Write(elem.Address, ((long)value));
                            break;
                        case ElemType.Float:
                            SiemensS7Session.Write(elem.Address, ((float)value).ConvertToUInt());
                            break;
                        case ElemType.Double:
                            SiemensS7Session.Write(elem.Address, (double)value);
                            break;
                        case ElemType.String:
                            StringClass str = new StringClass();
                            str.StringVariable = (string)value;
                            SiemensS7Session.WriteClass(str, DataItem.DbNumber);
                            break;
                        case ElemType.DateTime:
                            //var DataType = elem.DataItem.DataType;
                            //var db = elem.DataItem.DB;
                            //var offset = elem.DataItem.StartByteAdr;
                            var DataType = DataItem.DataType;
                            var db = DataItem.DbNumber;
                            var offset = DataItem.StartByte;
                            SiemensS7Session.WriteBytes(DataType, db, offset, S7.Net.Types.DateTimeLong.ToByteArray((System.DateTime)value));
                            break;
                        default:
                            throw new Exception("写入数据类型不存在");
                    }
                    #endregion
                }

                log.WriteLine("OK");
                return true;
            }
            catch (Exception ex)
            {
                log.WriteLine("Error: " + ex.Message);
                return false;
            }

        }



        //rapid scada  elemType  change to S7.net VarType
        public VarType GetS7VarTypeByElemType(ElemType elemType)
        {
            switch (elemType)
            {
                case ElemType.Bool:
                    return VarType.Bit;
                case ElemType.Byte:
                    return VarType.Byte;
                    break;
                case ElemType.UShort:
                    return VarType.Word;
                    break;
                case ElemType.Long:
                    return VarType.LReal;
                    break;
                case ElemType.ULong:
                    return VarType.LReal;
                    break;
                case ElemType.Short:
                    return VarType.Int;
                    break;
                case ElemType.UInt:
                    return VarType.DWord;
                    break;
                case ElemType.Int:
                    return VarType.DInt;
                    break;
                case ElemType.Float:
                    return VarType.Real;
                    break;
                case ElemType.Double:
                    return VarType.LReal;
                    break;
                case ElemType.String:
                    return VarType.String;
                    break;
                case ElemType.S7String://2 byte
                    return VarType.S7String;
                    break;
                case ElemType.S7WString://4 byte
                    return VarType.S7WString;
                    break; 
                case ElemType.Counter:
                    return VarType.Counter;
                    break;
                case ElemType.DateTime:
                    return VarType.DateTime;
                    break;
                case ElemType.DateTimeLong:
                    return VarType.DateTimeLong;
                    break;
                default:
                    throw new Exception("数据类型不存在");
                    break;

            }
        }


        /// <summary>
        /// Sets the request method according to the data transfer mode.
        /// </summary>
        //protected void ChooseRequestMethod()
        //{
        //    switch (TransMode)
        //    {
        //        case TransMode.RTU:
        //            DoRequest = RtuRequest;
        //            break;

        //        case TransMode.ASCII:
        //            DoRequest = AsciiRequest;
        //            break;
        //        default: // TransMode.TCP
        //            DoRequest = TcpRequest;
        //            break;
        //    }
        //}

        ///// <summary>
        ///// Performs a request in the PDU mode.
        ///// </summary>
        //protected bool RtuRequest(DataUnit dataUnit)
        //{
        //    bool result = false;

        //    // send request
        //    log.WriteLine(dataUnit.ReqDescr);
        //    Connection.Write(dataUnit.ReqADU, 0, dataUnit.ReqADU.Length, ProtocolFormat.Hex, out string logText);
        //    log.WriteLine(logText);

        //    // receive response
        //    // partial read to calculate PDU length
        //    const int FirstCount = 2;
        //    int readCnt = Connection.Read(InBuf, 0, FirstCount, Timeout, ProtocolFormat.Hex, out logText);
        //    log.WriteLine(logText);

        //    if (readCnt != FirstCount)
        //    {
        //        log.WriteLine(SiemensS7Phrases.CommError);
        //    }
        //    else if (InBuf[0] != dataUnit.ReqADU[0]) // validate device address
        //    {
        //        log.WriteLine(SiemensS7Phrases.InvalidDevAddr);
        //    }
        //    else if (!(InBuf[1] == dataUnit.FuncCode || InBuf[1] == dataUnit.ExcFuncCode)) // validate function code
        //    {
        //        log.WriteLine(SiemensS7Phrases.InvalidPduFuncCode);
        //    }
        //    else
        //    {
        //        int pduLen;
        //        int count;

        //        if (InBuf[1] == dataUnit.FuncCode)
        //        {
        //            pduLen = dataUnit.RespPduLen;
        //            count = dataUnit.RespAduLen - FirstCount;
        //        }
        //        else // exception received
        //        {
        //            pduLen = 2;
        //            count = 3;
        //        }

        //        // read rest of response
        //        readCnt = Connection.Read(InBuf, FirstCount, count, Timeout, ProtocolFormat.Hex, out logText);
        //        log.WriteLine(logText);

        //        if (readCnt != count)
        //        {
        //            log.WriteLine(SiemensS7Phrases.CommError);
        //        }
        //        else if (InBuf[pduLen + 1] + InBuf[pduLen + 2] * 256 != SiemensS7Utils.CRC16(InBuf, 0, pduLen + 1))
        //        {
        //            log.WriteLine(SiemensS7Phrases.CrcError);
        //        }
        //        else if (dataUnit.DecodeRespPDU(InBuf, 1, pduLen, out string errMsg))
        //        {
        //            log.WriteLine(SiemensS7Phrases.OK);
        //            result = true;
        //        }
        //        else
        //        {
        //            log.WriteLine(errMsg);
        //        }
        //    }

        //    return result;
        //}

        ///// <summary>
        ///// Performs a request in the ASCII mode.
        ///// </summary>
        //protected bool AsciiRequest(DataUnit dataUnit)
        //{
        //    bool result = false;

        //    // send request
        //    log.WriteLine(dataUnit.ReqDescr);
        //    Connection.WriteLine(dataUnit.ReqStr, out string logText);
        //    log.WriteLine(logText);

        //    // receive response
        //    string line = Connection.ReadLine(Timeout, out logText);
        //    log.WriteLine(logText);
        //    int lineLen = line == null ? 0 : line.Length;

        //    if (lineLen >= 3)
        //    {
        //        int aduLen = (lineLen - 1) / 2;

        //        if (aduLen == dataUnit.RespAduLen && lineLen % 2 == 1)
        //        {
        //            // receive response ADU
        //            byte[] aduBuf = new byte[aduLen];
        //            bool parseOK = true;

        //            for (int i = 0, j = 1; i < aduLen && parseOK; i++, j += 2)
        //            {
        //                try
        //                {
        //                    aduBuf[i] = byte.Parse(line.Substring(j, 2), NumberStyles.HexNumber);
        //                }
        //                catch
        //                {
        //                    log.WriteLine(SiemensS7Phrases.InvalidSymbol);
        //                    parseOK = false;
        //                }
        //            }

        //            if (parseOK)
        //            {
        //                if (aduBuf[aduLen - 1] == SiemensS7Utils.LRC(aduBuf, 0, aduLen - 1))
        //                {
        //                    // decode response
        //                    if (dataUnit.DecodeRespPDU(aduBuf, 1, aduLen - 2, out string errMsg))
        //                    {
        //                        log.WriteLine(SiemensS7Phrases.OK);
        //                        result = true;
        //                    }
        //                    else
        //                    {
        //                        log.WriteLine(errMsg);
        //                    }
        //                }
        //                else
        //                {
        //                    log.WriteLine(SiemensS7Phrases.LrcError);
        //                }
        //            }
        //        }
        //        else
        //        {
        //            log.WriteLine(SiemensS7Phrases.InvalidAduLength);
        //        }
        //    }
        //    else
        //    {
        //        log.WriteLine(SiemensS7Phrases.CommError);
        //    }

        //    return result;
        //}

        ///// <summary>
        ///// Performs a request in the TCP mode.
        ///// </summary>
        //protected bool TcpRequest(DataUnit dataUnit)
        //{
        //    bool result = false;

        //    // specify transaction ID
        //    if (++TransactionID == 0)
        //        TransactionID = 1;

        //    dataUnit.ReqADU[0] = (byte)(TransactionID / 256);
        //    dataUnit.ReqADU[1] = (byte)(TransactionID % 256);

        //    // send request
        //    log.WriteLine(dataUnit.ReqDescr);
        //    Connection.Write(dataUnit.ReqADU, 0, dataUnit.ReqADU.Length, ProtocolFormat.Hex, out string logText);
        //    log.WriteLine(logText);

        //    // receive response
        //    // read MBAP header
        //    int readCnt = Connection.Read(InBuf, 0, 7, Timeout, ProtocolFormat.Hex, out logText);
        //    log.WriteLine(logText);

        //    if (readCnt == 7)
        //    {
        //        int pduLen = InBuf[4] * 256 + InBuf[5] - 1;

        //        if (InBuf[0] == dataUnit.ReqADU[0] && InBuf[1] == dataUnit.ReqADU[1] && // transaction ID
        //            InBuf[2] == 0 && InBuf[3] == 0 && pduLen > 0 &&                     // protocol ID
        //            InBuf[6] == dataUnit.ReqADU[6])                                     // unit ID
        //        {
        //            // read PDU
        //            readCnt = Connection.Read(InBuf, 7, pduLen, Timeout, ProtocolFormat.Hex, out logText);
        //            log.WriteLine(logText);

        //            if (readCnt == pduLen)
        //            {
        //                // decode response
        //                if (dataUnit.DecodeRespPDU(InBuf, 7, pduLen, out string errMsg))
        //                {
        //                    log.WriteLine(SiemensS7Phrases.OK);
        //                    result = true;
        //                }
        //                else
        //                {
        //                    log.WriteLine(errMsg);
        //                }
        //            }
        //            else
        //            {
        //                log.WriteLine(SiemensS7Phrases.CommError);
        //            }
        //        }
        //        else
        //        {
        //            log.WriteLine(SiemensS7Phrases.InvalidMbap);
        //        }
        //    }
        //    else
        //    {
        //        log.WriteLine(SiemensS7Phrases.CommError);
        //    }

        //    return result;
        //}
    }
}