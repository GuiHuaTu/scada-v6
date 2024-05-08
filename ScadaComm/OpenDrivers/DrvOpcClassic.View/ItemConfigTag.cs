// Copyright (c) Rapid Software LLC. All rights reserved.

namespace Scada.Comm.Drivers.DrvOpcClassic.View
{
    /// <summary>
    /// Represents an object associated with a monitored item configuration.
    /// <para>Представляет объект, связанный с конфигурацией отслеживаемого элемента.</para>
    /// </summary>
    internal class ItemConfigTag
    {
        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        public ItemConfigTag(int tagNum)
        {
            TagNum = tagNum;
        }


        /// <summary>
        /// Gets or sets the tag number.
        /// </summary>
        public int TagNum { get; set; }

        /// <summary>
        /// Gets a string representation of the tag number.
        /// </summary>
        public string TagNumStr => TagNum.ToString();
    }
}
