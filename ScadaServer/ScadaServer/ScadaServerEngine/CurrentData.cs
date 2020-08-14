﻿/*
 * Copyright 2020 Mikhail Shiryaev
 * 
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 * 
 *     http://www.apache.org/licenses/LICENSE-2.0
 * 
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 * 
 * 
 * Product  : Rapid SCADA
 * Module   : ScadaServerEngine
 * Summary  : Represents current data of the input channels
 * 
 * Author   : Mikhail Shiryaev
 * Created  : 2020
 * Modified : 2020
 */

using Scada.Data.Models;
using System;
using System.Collections.Generic;

namespace Scada.Server.Engine
{
    /// <summary>
    /// Represents current data of the input channels.
    /// <para>Представляет текущие данные входных каналов.</para>
    /// </summary>
    internal class CurrentData : ICalcContext
    {
        private readonly ICnlDataChangeHandler cnlDataChangeHandler; // handles data changes
        private readonly Dictionary<int, CnlTag> cnlTags;            // the metadata about the input channels


        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        public CurrentData(ICnlDataChangeHandler cnlDataChangeHandler, Dictionary<int, CnlTag> cnlTags)
        {
            this.cnlDataChangeHandler = cnlDataChangeHandler ?? throw new ArgumentNullException("cnlDataChangeHandler");
            this.cnlTags = cnlTags ?? throw new ArgumentNullException("cnlTags");

            int cnlCnt = cnlTags.Count;
            CnlData = new CnlData[cnlCnt];
            PrevCnlData = new CnlData[cnlCnt];
            Timestamps = new DateTime[cnlCnt];
            PrevTimestamps = new DateTime[cnlCnt];
        }


        /// <summary>
        /// Gets the current data of the input channels.
        /// </summary>
        public CnlData[] CnlData { get; }

        /// <summary>
        /// Gets the previous data of the input channels.
        /// </summary>
        public CnlData[] PrevCnlData { get; }

        /// <summary>
        /// Gets the current timestamps of the input channels.
        /// </summary>
        public DateTime[] Timestamps { get; }

        /// <summary>
        /// Gets the previous timestamps of the input channels.
        /// </summary>
        public DateTime[] PrevTimestamps { get; }


        /// <summary>
        /// Sets the current input channel data.
        /// </summary>
        public void SetCurCnlData(CnlTag cnlTag, CnlData cnlData, DateTime nowDT)
        {
            int cnlIndex = cnlTag.Index;
            DateTime prevTimestamp = Timestamps[cnlIndex];
            CnlData prevCnlData = CnlData[cnlIndex];
            cnlDataChangeHandler.HandleCurDataChanged(cnlTag, ref cnlData, prevCnlData, nowDT, prevTimestamp);

            PrevTimestamps[cnlIndex] = prevTimestamp;
            PrevCnlData[cnlIndex] = prevCnlData;

            Timestamps[cnlIndex] = nowDT;
            CnlData[cnlIndex] = cnlData;
        }

        /// <summary>
        /// Gets the actual data of the input channel.
        /// </summary>
        CnlData ICalcContext.GetCnlData(int cnlNum)
        {
            return cnlTags.TryGetValue(cnlNum, out CnlTag cnlTag) ?
                CnlData[cnlTag.Index] :
                Data.Models.CnlData.Empty;
        }

        /// <summary>
        /// Gets the previous data of the input channel, if applicable.
        /// </summary>
        CnlData ICalcContext.GetPrevCnlData(int cnlNum)
        {
            return cnlTags.TryGetValue(cnlNum, out CnlTag cnlTag) ?
                PrevCnlData[cnlTag.Index] :
                Data.Models.CnlData.Empty;
        }

        /// <summary>
        /// Gets the actual timestamp of the input channel.
        /// </summary>
        DateTime ICalcContext.GetCnlTime(int cnlNum)
        {
            return cnlTags.TryGetValue(cnlNum, out CnlTag cnlTag) ?
                Timestamps[cnlTag.Index] :
                DateTime.MinValue;
        }

        /// <summary>
        /// Gets the previous timestamp of the input channel, if applicable.
        /// </summary>
        DateTime ICalcContext.GetPrevCnlTime(int cnlNum)
        {
            return cnlTags.TryGetValue(cnlNum, out CnlTag cnlTag) ?
                PrevTimestamps[cnlTag.Index] :
                DateTime.MinValue;
        }

        /// <summary>
        /// Sets the input channel data.
        /// </summary>
        void ICalcContext.SetCnlData(int cnlNum, CnlData cnlData)
        {
            if (cnlTags.TryGetValue(cnlNum, out CnlTag cnlTag))
                SetCurCnlData(cnlTag, cnlData, DateTime.UtcNow);
        }
    }
}
