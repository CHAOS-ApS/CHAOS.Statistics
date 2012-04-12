﻿using Geckon.Portal.Core.Standard.Extension;
using System;

namespace CHAOS.Portal.StatisticsExtention.Standard
{
    public class StatisticsExtention : AExtension
    {
        #region SetStats
        public void StatsObject_Set(string repositoryIdentifier, string objectIdentifier, int objectTypeID, int objectCollectionID, string channelIdentifier, int channelTypeID, int eventTypeID, string objectTitle, string IP, string city, string country, int userSessionID)
        {
        }
        #endregion

        #region DayStats

        public void DayStats_Get(int objectCollectionID, int[] channelIDList, int[] objectTypeIDList, int[] eventTypeIDList, string[] statsObjectIdentifierList, DateTime fromDate, DateTime toDate)
        {
        }

        public void DayStats_GetTotal(int objectCollectionID, int[] channelIDList, int[] objectTypeIDList, int[] eventTypeIDList, string[] statsObjectIdentifierList, DateTime fromDate, DateTime toDate)
        {
        }

        public void DayStats_GetObjects(int objectCollectionID, int[] channelIDList, int[] objectTypeIDList, int[] eventTypeIDList, DateTime fromDate, DateTime toDate, int pageIndex, int pageSize, string sortDirection)
        {
        }


        public void DayStatsGeo_Get(int objectCollectionID, int[] channelIDList, int[] objectTypeIDList, int[] eventTypeIDList, string[] statsObjectIdentifierList, DateTime fromDate, DateTime toDate, int pageIndex, int pageSize, string sortDirection, string geoType)
        {
        }
        #endregion

        #region Hourstats

        public void HourStats_Get(int objectCollectionID, int[] channelIDList, int[] objectTypeIDList, int[] eventTypeIDList, string[] statsObjectIdentifierList, DateTime fromDate, DateTime toDate)
        {
        }

        public void HourStats_GetObjects(int objectCollectionID, int[] channelIDList, int[] objectTypeIDList, int[] eventTypeIDList, DateTime fromDate, DateTime toDate, int pageIndex, int pageSize, string sortDirection)
        {
        }
        #endregion

        #region DurationStats
        public void DurationSession_Set(int objectSessionID, long startValue, long endValue)
        {
        }

        public void DurationSession_Get(int objectCollectionID, int[] channelIDList, int[] objectTypeIDList, int[] eventTypeIDList, string[] statsObjectIdentifierList, DateTime fromDate, DateTime toDate)
        {
        }

        #endregion
    }
}
