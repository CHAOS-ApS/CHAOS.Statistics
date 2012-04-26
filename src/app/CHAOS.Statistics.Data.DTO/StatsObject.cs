using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CHAOS.Portal.DTO.Standard;
using CHAOS.Serialization;

namespace CHAOS.Statistics.Data.DTO
{
    public class StatsObject : Result
    {
        [Serialize("ObjectSessionID")]
        public int ObjectSessionID { get; set; }

        public StatsObject(int objectSessionID)
        {
            ObjectSessionID = objectSessionID;
        }

        public StatsObject()
        {

        }
    }
}
