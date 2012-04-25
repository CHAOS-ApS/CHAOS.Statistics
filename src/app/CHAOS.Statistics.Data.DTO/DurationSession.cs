using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CHAOS.Portal.DTO.Standard;
using CHAOS.Serialization;

namespace CHAOS.Statistics.Data.DTO
{
    public class DurationSession : Result
    {
        [Serialize("Duration")]
        public uint Duration { get; set; }

        [Serialize("StatsObjectSessionDate")]
        public DateTime StatsObjectSessionDate { get; set; }


        public DurationSession(uint duration, DateTime statsObjectSessionDate)
        {
            Duration = duration;
            StatsObjectSessionDate = statsObjectSessionDate;
        }

        public DurationSession()
        {

        }

    }
}
