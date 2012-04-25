using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CHAOS.Portal.DTO.Standard;
using CHAOS.Serialization;

namespace CHAOS.Statistics.Data.DTO
{
    public class DayStats : Result
    {
        [Serialize("Timestamp")]
        public DateTime Timestamp { get; set; }

        [Serialize("Count")]
        public uint Count { get; set; }

        public DayStats(DateTime timestamp, uint count)
        {
            Timestamp = timestamp;
            Count = count;
        }

        public DayStats()
        {

        }
    }
}
