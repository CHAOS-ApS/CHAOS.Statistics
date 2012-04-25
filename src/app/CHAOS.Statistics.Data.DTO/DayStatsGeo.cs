using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CHAOS.Portal.DTO.Standard;
using CHAOS.Serialization;

namespace CHAOS.Statistics.Data.DTO
{
    public class DayStatsGeo : Result
    {
        [Serialize("NumberOfViews")]
        public uint NumberOfViews { get; set; }

        [Serialize("GeoString")]
        public string GeoString { get; set; }


        public DayStatsGeo(uint numberOfViews, string geoString)
        {
            NumberOfViews = numberOfViews;
            GeoString = geoString;
        }

        public DayStatsGeo()
        {

        }
    }
}
