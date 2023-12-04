// Copyright (c) Rapid Software LLC. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Scada.Comm.Drivers.DrvSiemensS7.Config;
using Scada.Comm.Drivers.DrvSiemensS7.Protocol;

namespace Scada.Comm.Drivers.DrvSiemensS7.View
{
    /// <summary>
    /// Represents metadata about a SiemensS7 element.
    /// <para>Представляет метаданные элемента SiemensS7.</para>
    /// </summary>
    public class ElemTag
    {
        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        public ElemTag(DeviceTemplateOptions options, ElemGroupConfig elemGroupConfig, ElemConfig elemConfig)
        {
            TemplateOptions = options ?? throw new ArgumentNullException(nameof(options));
            ElemGroup = elemGroupConfig ?? throw new ArgumentNullException(nameof(elemGroupConfig));
            Elem = elemConfig ?? throw new ArgumentNullException(nameof(elemConfig)); 
            TagNum = 0;
        }


        /// <summary>
        /// Gets the device template options.
        /// </summary>
        public DeviceTemplateOptions TemplateOptions { get; }

        /// <summary>
        /// Gets the element group configuration.
        /// </summary>
        public ElemGroupConfig ElemGroup { get; }

        /// <summary>
        /// Gets the element configuration.
        /// </summary>
        public ElemConfig Elem { get; } 

        /// <summary>
        /// Gets or sets the device tag number.
        /// </summary>
        public int TagNum { get; set; }
         

        /// <summary>
        /// Gets the element node text for the tree view.
        /// </summary>
        public string NodeText
        {
            get
            {
                return $"{(string.IsNullOrEmpty(Elem.Name) ? DriverPhrases.UnnamedElem : Elem.Name)} ({Elem.Address})";
            }
        }


        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        public override string ToString()
        {
            return NodeText;
        }
    }
}
