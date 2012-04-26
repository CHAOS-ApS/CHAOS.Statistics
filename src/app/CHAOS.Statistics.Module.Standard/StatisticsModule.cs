using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Xsl;
using CHAOS.Statistics.Data.EF;
using CHAOS.Statistics.Data.DTO;
using System.Web;
using System.Data.Objects;
using System.Collections.Generic;
using CHAOS.Portal.Core.Module;



namespace CHAOS.Statistics.Module.Standard
{
    [Module("Statistics")]
    public class StatisticsModule : AModule
    {
        #region Properties

        public string ConnectionString { get; set; }

        public statisticsEntities StatisticsDataContext
        {
            get
            {
                return new statisticsEntities(ConnectionString);
            }
        }
        #endregion

        #region Construction

        public override void Initialize( string configuration )
        {
            ConnectionString = XDocument.Parse(configuration).Root.Attribute("ConnectionString").Value;
        }

        #endregion

        #region SetStats
        [Datatype("StatsObject", "Set")]
        public StatsObject Set(string repositoryIdentifier, string objectIdentifier, int objectTypeID, int objectCollectionID, string channelIdentifier, int channelTypeID, int eventTypeID, string objectTitle, string IP, string city, string country, int userSessionID)
        {
            {
                if (objectIdentifier.Contains(','))
                    throw new StatisticsException("ObjectIdentifier cannot contain a comma");

                if (objectIdentifier.Length > 128)
                    throw new StatisticsException("ObjectIdentifer length cannot exceed 128 chars");

                int returnValue = 0;

                try
                {
                    Convert.ToInt32(StatisticsDataContext.StatsObject_Set(repositoryIdentifier, objectIdentifier, objectTypeID, objectCollectionID, channelIdentifier, channelTypeID, eventTypeID, objectTitle, IP, city, country, userSessionID).Single());
                }
                catch (Exception ex)
                {
                    throw new StatisticsException("StatsObject Set failed. Possible due to uncorrect parameters. " + ex.StackTrace);
                }

                if (returnValue < 0)
                {
                    if (returnValue == -101)
                        throw new StatisticsException("Repository identifier not found");

                    if (returnValue == -102)
                        throw new StatisticsException("Statisticslevel not set correct");

                    if (returnValue == -103)
                        throw new StatisticsException("ObjectCollectionID or ObjectTypeID not found");

                    if (returnValue == -104)
                        throw new StatisticsException("ChannelTypeID not found");

                    if (returnValue == -105)
                        throw new StatisticsException("EventTypeID not found");

                    if (returnValue == -106)
                        throw new StatisticsException("DayStats set failed");

                    if (returnValue == -107)
                        throw new StatisticsException("HourStats set failed");

                    if (returnValue == -108)
                        throw new StatisticsException("DurationStats set failed");
                }
        

                return  ExtentionMethods.ToDTO(returnValue);
            }
        }
        #endregion

        #region DayStats
        [Datatype("DayStats", "Get")]
        public IEnumerable<DayStats> DayStats_Get(int objectCollectionID, string channelIDList, string objectTypeIDList, string eventTypeIDList, string statsObjectIdentifierList, DateTime fromDate, DateTime toDate)
        {
            try
            {
                return ExtentionMethods.ToDTO(StatisticsDataContext.DayStats_Get(objectCollectionID, IsNull(channelIDList), IsNull(objectTypeIDList), IsNull(eventTypeIDList), IsNull(statsObjectIdentifierList), fromDate, toDate).ToList());
            }
            catch(Exception ex)
            {
                throw new StatisticsException("DayStats/Get", ex.InnerException);
            }
        }

        [Datatype("DayStatsTotal", "Get")]
        public DayStatsTotal DayStats_GetTotal(int objectCollectionID, string channelIDList, string objectTypeIDList, string eventTypeIDList, string statsObjectIdentifierList, DateTime fromDate, DateTime toDate)
        {
            return ExtentionMethods.ToDTO(StatisticsDataContext.DayStats_GetTotal(objectCollectionID, IsNull(channelIDList), IsNull(objectTypeIDList), IsNull(eventTypeIDList), IsNull(statsObjectIdentifierList), fromDate, toDate).Single());
        }

        [Datatype("DayStatsObject", "Get")]
        public IEnumerable<DayStatsObject> DayStats_GetObjects(int objectCollectionID, string channelIDList, string objectTypeIDList, string eventTypeIDList, DateTime fromDate, DateTime toDate, int? pageIndex, int? pageSize, string sortDirection )
        {
            if (pageIndex == null)
                pageIndex = 0;

            if (pageSize == null)
                pageSize = 20;

            if (string.IsNullOrEmpty(sortDirection))
                sortDirection = "DESC";

            return ExtentionMethods.ToDTO(StatisticsDataContext.DayStats_GetObjects(objectCollectionID, IsNull(channelIDList), IsNull(objectTypeIDList), IsNull(eventTypeIDList), fromDate, toDate, pageIndex, pageSize, sortDirection).ToList());
        }

        [Datatype("DayStatsGeo", "Get")]
        public IEnumerable<DayStatsGeo> DayStatsGeo_Get(int objectCollectionID, string channelIDList, string objectTypeIDList, string eventTypeIDList, string statsObjectIdentifierList, DateTime fromDate, DateTime toDate, int? pageIndex, int? pageSize, string sortDirection, string geoType)
        {
            if (pageIndex == null)
                pageIndex = 0;

            if (pageSize == null)
                pageSize = 20;

            if (string.IsNullOrEmpty(sortDirection))
                sortDirection = "DESC";

            if (string.IsNullOrEmpty(geoType))
                geoType = "City";

            return ExtentionMethods.ToDTO(StatisticsDataContext.DayStatsGeo_Get(objectCollectionID, IsNull(channelIDList), IsNull(objectTypeIDList), IsNull(eventTypeIDList), IsNull(statsObjectIdentifierList), fromDate, toDate, pageIndex, pageSize, sortDirection, geoType).ToList());
        }
        #endregion

        #region Hourstats
        [Datatype("HourStats", "Get")]
        public IEnumerable<HourStats> HourStats_Get(int objectCollectionID, string channelIDList, string objectTypeIDList, string eventTypeIDList, string statsObjectIdentifierList, DateTime fromDate, DateTime toDate)
        {
            return ExtentionMethods.ToDTO( StatisticsDataContext.HourStats_Get(objectCollectionID, IsNull(channelIDList), IsNull(objectTypeIDList), IsNull(eventTypeIDList), IsNull(statsObjectIdentifierList), fromDate, toDate).ToList());
        }

        [Datatype("HourStatsObject", "Get")]
        public IEnumerable<HourStatsObject> HourStats_GetObjects(int objectCollectionID, string channelIDList, string objectTypeIDList, string eventTypeIDList, DateTime fromDate, DateTime toDate, int? pageIndex, int? pageSize, string sortDirection )
        {
            if (pageIndex == null)
                pageIndex = 0;

            if (pageSize == null)
                pageSize = 20;

            if(string.IsNullOrEmpty(sortDirection))
                sortDirection = "DESC";

            return ExtentionMethods.ToDTO(StatisticsDataContext.HourStats_GetObjects(objectCollectionID, IsNull(channelIDList), IsNull(objectTypeIDList), IsNull(eventTypeIDList), fromDate, toDate, pageIndex, pageSize, sortDirection).ToList());
        }
        #endregion

        #region DurationStats
        [Datatype("DurationStatsSession", "Set")]
        public int DurationSession_Set (int objectSessionID, long startValue, long endValue)
        {
            int returnValue = Convert.ToInt32( StatisticsDataContext.DurationSession_Set(objectSessionID, startValue, endValue).Single());

            if (returnValue < 0)
            {
                if (returnValue == -100)
                    throw new StatisticsException("End value is smaller than start value");

                if (returnValue == -101)
                    throw new StatisticsException("ObjectSessionID not found");
            }

            return returnValue;
        }

        [Datatype("DurationStatsSession", "Get")]
        public IEnumerable<durationsession_entity> DurationSession_Get(int objectCollectionID, string channelIDList, string objectTypeIDList, string eventTypeIDList, string statsObjectIdentifierList, DateTime fromDate, DateTime toDate)
        {
            return StatisticsDataContext.DurationSession_Get(objectCollectionID, IsNull(channelIDList), IsNull(objectTypeIDList), IsNull(eventTypeIDList), IsNull(statsObjectIdentifierList), fromDate, toDate).ToList();
        }



        #endregion

        #region helper methods

        //private string IsNull(int array)
        //{
        //    if (array == null)
        //        return null;
        //    else
        //        return string.Join(",", array);
        //}

        private string IsNull(string array)
        {
            if (array == null)
                return null;
            else
                return string.Join(",", array);
        }

        #endregion
    }
}
