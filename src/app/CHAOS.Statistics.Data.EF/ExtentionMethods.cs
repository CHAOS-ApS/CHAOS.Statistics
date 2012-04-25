﻿using System.Collections.Generic;
using System.Linq;
using System;

namespace CHAOS.Statistics.Data.EF
{
    public static class ExtentionMethods
    {

        #region StatsObject
        public static DTO.StatsObject ToDTO(this int objectSesstionID)
        {
            return new DTO.StatsObject(objectSesstionID);
        }

        #endregion

        #region DayStats
        public static IEnumerable<DTO.DayStats> ToDTO(this IEnumerable<daystats_getinfo> daystatsGetInfos)
        {
            return daystatsGetInfos.Select(item => ToDTO(item)); 
        }

        public static DTO.DayStats ToDTO(this daystats_getinfo daystatsGetInfos)
        {
            return new DTO.DayStats(daystatsGetInfos.Timestamp, Convert.ToUInt32(daystatsGetInfos.NumberOfViews));
        }
        #endregion

        #region DayStatsTotal

        public static IEnumerable<DTO.DayStatsTotal> ToDTO(this IEnumerable<daystats_gettotal_entity> daystatsGetTotalInfos)
        {
            return daystatsGetTotalInfos.Select(item => ToDTO(item));
        }

        public static DTO.DayStatsTotal ToDTO(this daystats_gettotal_entity daystatsGetTotalInfos)
        {
            return new DTO.DayStatsTotal(Convert.ToUInt32(daystatsGetTotalInfos.NumberOfViews));
        }
        #endregion

        #region DayStatsObject
        public static IEnumerable<DTO.DayStatsObject> ToDTO(this IEnumerable<daystats_getobject_entity> daystatsGetObjectInfos)
        {
            return daystatsGetObjectInfos.Select(item => ToDTO(item));
        }

        public static DTO.DayStatsObject ToDTO(this daystats_getobject_entity daystatsGetObjectInfos)
        {
            return new DTO.DayStatsObject(
                Convert.ToUInt32(daystatsGetObjectInfos.NumberOfViews),
                Convert.ToUInt32(daystatsGetObjectInfos.StatsObjectID),
                daystatsGetObjectInfos.Identifier,
                Convert.ToUInt32(daystatsGetObjectInfos.ObjectTypeID),
                daystatsGetObjectInfos.ObjectTitle
                );
        }
        #endregion

        #region DayStatsGeo
        public static IEnumerable<DTO.DayStatsGeo> ToDTO(this IEnumerable<daystatsgeo_entity> daystatsGeoGetInfos)
        {
            return daystatsGeoGetInfos.Select(item => ToDTO(item));
        }

        public static DTO.DayStatsGeo ToDTO(this daystatsgeo_entity daystatsGeoGetInfos)
        {
            return new DTO.DayStatsGeo(
                Convert.ToUInt32(daystatsGeoGetInfos.NumberOfViews),
                daystatsGeoGetInfos.GeoString
                );
        }
        #endregion

        #region HourStats
        public static IEnumerable<DTO.HourStats> ToDTO(this IEnumerable<hourstats_entity> hourStatsInfos)
        {
            return hourStatsInfos.Select(item => ToDTO(item));
        }

        public static DTO.HourStats ToDTO(this hourstats_entity hourStatsInfos)
        {
            return new DTO.HourStats(
                Convert.ToUInt32(hourStatsInfos.Count),
                Convert.ToUInt32( hourStatsInfos.Hour)
                );
        }
        #endregion

        #region HourStatsObject
        public static IEnumerable<DTO.HourStatsObject> ToDTO(this IEnumerable<hourstats_getobjects_entity> hourstatsGetObjectInfos)
        {
            return hourstatsGetObjectInfos.Select(item => ToDTO(item));
        }

        public static DTO.HourStatsObject ToDTO(this hourstats_getobjects_entity daystatsGetObjectInfos)
        {
            return new DTO.HourStatsObject(
                Convert.ToUInt32(daystatsGetObjectInfos.NumberOfViews),
                Convert.ToUInt32(daystatsGetObjectInfos.StatsObjectID),
                daystatsGetObjectInfos.Identifier,
                Convert.ToUInt32(daystatsGetObjectInfos.ObjectTypeID),
                daystatsGetObjectInfos.ObjectTitle
                );
        }


        #endregion


    }
}
