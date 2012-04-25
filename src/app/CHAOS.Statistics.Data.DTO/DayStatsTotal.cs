using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CHAOS.Portal.DTO.Standard;
using CHAOS.Serialization;

namespace CHAOS.Statistics.Data.DTO
{
    public class DayStatsTotal : Result
    {
        [Serialize("Count")]
        public uint Count { get; set; }

        public DayStatsTotal(uint count)
        {
            Count = count;
        }

        public DayStatsTotal()
        {

        }
    }
}
