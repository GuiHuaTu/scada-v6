// Copyright (c) Rapid Software LLC. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Scada.Comm.Drivers.DrvSiemensS7.Protocol;
using System;
using System.Xml;

namespace Scada.Comm.Drivers.DrvSiemensS7.Config
{
    /// <summary>
    /// Represents a command configuration.
    /// <para>Представляет конфигурацию команды.</para>
    /// </summary>
    public class CmdConfig  
    {
        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        public CmdConfig() 
        {
            DataBlock = DataBlock.Input;
            Multiple = false;
            CustomFuncCode = 0;
            ElemType = ElemType.Bool;
            ElemCnt = 1;
            ByteOrder = "";
            CmdNum = 0;
            CmdCode = ""; 
            IsReadCmd = true;
            Address = "";
        }
        public bool IsReadCmd { get; set; }
        /// <summary>
        /// Gets the maximum number of elements.
        /// </summary>
        public int MaxElemCnt
        {
            get
            {
                return GetMaxElemCnt(DataBlock);
            }
        }
        /// <summary>
        /// Gets the maximum number of elements depending on the data block.
        /// </summary>
        public virtual int GetMaxElemCnt(DataBlock dataBlock)
        {
            //return dataBlock == DataBlock.Input || dataBlock == DataBlock.Output 
            //    ? 2000
            //    : 125;
            return 2000;
        }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the data block.
        /// </summary>
        public DataBlock DataBlock { get; set; }

        /// <summary>
        /// Gets or sets the zero-based address of the start element.
        /// </summary>
        public string Address { get; set; }


        /// <summary>
        /// Gets or sets a value indicating whether the command writes multiple elements.
        /// </summary>
        public bool Multiple { get; set; }

        /// <summary>
        /// Gets or sets the custom function code.
        /// </summary>
        public int CustomFuncCode { get; set; }

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
        public string ByteOrder { get; set; }

        /// <summary>
        /// Gets or sets the command number.
        /// </summary>
        public int CmdNum { get; set; }

        /// <summary>
        /// Gets or sets the command code.
        /// </summary>
        public string CmdCode { get; set; }

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
        /// Gets a value indicating whether the data type selection is applicable for the command.
        /// </summary>
        public  bool ElemTypeEnabled
        {
            get
            {
                return DataBlock == DataBlock.Input && Multiple;
            }
        }
         
        /// <summary>
        /// Gets a value indicating whether the byte order is applicable for the data unit.
        /// </summary>
        public bool ByteOrderEnabled
        {
            get
            {
                return DataBlock == DataBlock.Input || DataBlock == DataBlock.Output;
            }
        }


        /// <summary>
        /// Gets the default element type.
        /// </summary>
        public ElemType DefaultElemType
        {
            get
            {
                return  DataBlock == DataBlock.Input || DataBlock == DataBlock.Counter
                        ? ElemType.Bool
                        : ElemType.UShort;
            }
        }


        /// <summary>
        /// Loads the configuration from the XML node.
        /// </summary>
        public void LoadFromXml(XmlElement xmlElem)
        {
            if (xmlElem == null)
                throw new ArgumentNullException(nameof(xmlElem));

            DataBlock = xmlElem.GetAttrAsEnum("dataBlock", xmlElem.GetAttrAsEnum<DataBlock>("tableType"));
            Multiple = xmlElem.GetAttrAsBool("multiple");
            CustomFuncCode = xmlElem.GetAttrAsInt("funcCode");
            Address = xmlElem.GetAttrAsString("address");
            ElemType = xmlElem.GetAttrAsEnum("elemType", DefaultElemType);
            ElemCnt = xmlElem.GetAttrAsInt("elemCnt", 1);
            ByteOrder = xmlElem.GetAttrAsString("byteOrder");
            CmdNum = xmlElem.GetAttrAsInt("cmdNum");
            CmdCode = xmlElem.GetAttrAsString("cmdCode");
            Name = xmlElem.GetAttrAsString("name");
        }

        /// <summary>
        /// Saves the configuration into the XML node.
        /// </summary>
        public void SaveToXml(XmlElement xmlElem)
        {
            if (xmlElem == null)
                throw new ArgumentNullException(nameof(xmlElem));

            xmlElem.SetAttribute("dataBlock", DataBlock);

            if (DataBlock == DataBlock.Input)
            {
                xmlElem.SetAttribute("funcCode", CustomFuncCode);
                xmlElem.SetAttribute("address", Address);
                xmlElem.SetAttribute("elemType", ElemType.ToString().ToLowerInvariant());
            }
            else
            {
                xmlElem.SetAttribute("multiple", Multiple);
                xmlElem.SetAttribute("address", Address);

                //if (ElemTypeEnabled)
                    xmlElem.SetAttribute("elemType", ElemType.ToString().ToLowerInvariant());

                if (Multiple)
                    xmlElem.SetAttribute("elemCnt", ElemCnt);

                if (ByteOrderEnabled && !string.IsNullOrEmpty(ByteOrder))
                    xmlElem.SetAttribute("byteOrder", ByteOrder);
            }

            xmlElem.SetAttribute("cmdNum", CmdNum);
            xmlElem.SetAttribute("cmdCode", CmdCode);
            xmlElem.SetAttribute("name", Name);
        }
    }
}
