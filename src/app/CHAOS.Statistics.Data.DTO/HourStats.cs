using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CHAOS.Portal.DTO.Standard;
using CHAOS.Serialization;

namespace CHAOS.Statistics.Data.DTO
{
    public class HourStats : Result
    {
        [Serialize("Count")]
        public uint Count { get; set; }

        [Serialize("Hour")]
        public uint Hour { get; set; }

        public HourStats(uint count, uint hour)
        {
            Count = count;
            Hour = hour;
        }

        public HourStats()
        {

        }
    }
}
