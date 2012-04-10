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
using Geckon.Portal.Core.Exception;
using Geckon.Portal.Core.Standard.Extension;
using Geckon.Portal.Core.Standard.Module;
using Geckon.Portal.Data;
using Geckon.Portal.Core.Standard;
using System.Web;
using System.Data.Objects;
using System.Collections.Generic; 


namespace CHAOS.Statistics.Module.Standard
{
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

        public override void Init(XElement config)
        {
            ConnectionString = config.Attribute("ConnectionString").Value;
        }

        #endregion

        #region SetStats
        [Datatype("Statistics", "StatsObject_Set")]
        public int StatsObject_Set(string repositoryIdentifier, string objectIdentifier, int objectTypeID, int objectCollectionID, string channelIdentifier, int channelTypeID, int eventTypeID, string objectTitle, string IP, string city, string country, int userSessionID)
        {
            if(objectIdentifier.Contains(','))
                throw new StatisticsException("ObjectIdentifier cannot contain a comma");

            if (objectIdentifier.Length > 128)
                throw new StatisticsException("ObjectIdentifer length cannot exceed 128 chars"); 

            int returnValue = Convert.ToInt32( StatisticsDataContext.StatsObject_Set(repositoryIdentifier, objectIdentifier, objectTypeID, objectCollectionID, channelIdentifier, channelTypeID, eventTypeID, objectTitle, IP, city, country, userSessionID).Single());

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

            return returnValue;
        }
        #endregion

        #region DayStats
        [Datatype("Statistics", "DayStats_Get")]
        public IEnumerable<DayStats> DayStats_Get(int objectCollectionID, int[] channelIDList, int[] objectTypeIDList, int[] eventTypeIDList, string[] statsObjectIdentifierList, DateTime fromDate, DateTime toDate)
        {
            return ExtentionMethods.ToDTO(StatisticsDataContext.DayStats_Get(objectCollectionID, IsNull(channelIDList), IsNull(objectTypeIDList), IsNull(eventTypeIDList), IsNull(statsObjectIdentifierList), fromDate, toDate).ToList());
        }

        [Datatype("Statistics", "DayStats_GetTotal")]
        public DayStatsTotal DayStats_GetTotal(int objectCollectionID, int[] channelIDList, int[] objectTypeIDList, int[] eventTypeIDList, string[] statsObjectIdentifierList, DateTime fromDate, DateTime  toDate)
        {
            return ExtentionMethods.ToDTO(StatisticsDataContext.DayStats_GetTotal(objectCollectionID, IsNull(channelIDList), IsNull(objectTypeIDList), IsNull(eventTypeIDList), IsNull(statsObjectIdentifierList), fromDate, toDate).Single());
        }

        [Datatype("Statistics", "DayStats_GetObjects")]
        public IEnumerable<DayStatsObject> DayStats_GetObjects(int objectCollectionID, int[] channelIDList, int[] objectTypeIDList, int[] eventTypeIDList, DateTime fromDate, DateTime toDate, int pageIndex, int pageSize, string sortDirection )
        {
            return ExtentionMethods.ToDTO(StatisticsDataContext.DayStats_GetObjects(objectCollectionID, IsNull(channelIDList), IsNull(objectTypeIDList), IsNull(eventTypeIDList), fromDate, toDate, pageIndex, pageSize, sortDirection).ToList());
        }

        [Datatype("Statistics", "DayStatsGeo_Get")]
        public IEnumerable<DayStatsGeo> DayStatsGeo_Get(int objectCollectionID, int[] channelIDList, int[] objectTypeIDList, int[] eventTypeIDList, string[] statsObjectIdentifierList, DateTime fromDate, DateTime toDate, int pageIndex, int pageSize, string sortDirection, string geoType)
        {
            return ExtentionMethods.ToDTO(StatisticsDataContext.DayStatsGeo_Get(objectCollectionID, IsNull(channelIDList), IsNull(objectTypeIDList), IsNull(eventTypeIDList), IsNull(statsObjectIdentifierList), fromDate, toDate, pageIndex, pageSize, sortDirection, geoType).ToList());
        }
        #endregion

        #region Hourstats
        [Datatype("Statistics", "HourStats_Get")]
        public IEnumerable<HourStats> HourStats_Get(int objectCollectionID, int[] channelIDList, int[] objectTypeIDList, int[] eventTypeIDList, string[] statsObjectIdentifierList, DateTime fromDate, DateTime toDate)
        {
            return ExtentionMethods.ToDTO( StatisticsDataContext.HourStats_Get(objectCollectionID, IsNull(channelIDList), IsNull(objectTypeIDList), IsNull(eventTypeIDList), IsNull(statsObjectIdentifierList), fromDate, toDate).ToList());
        }

        [Datatype("Statistics", "HourStats_GetObjects")]
        public IEnumerable<HourStatsObject> HourStats_GetObjects(int objectCollectionID, int[] channelIDList, int[] objectTypeIDList, int[] eventTypeIDList, DateTime fromDate, DateTime toDate, int pageIndex, int pageSize, string sortDirection )
        {
            return ExtentionMethods.ToDTO(StatisticsDataContext.HourStats_GetObjects(objectCollectionID, IsNull(channelIDList), IsNull(objectTypeIDList), IsNull(eventTypeIDList), fromDate, toDate, pageIndex, pageSize, sortDirection).ToList());
        }
        #endregion

        #region DurationStats
        [Datatype("Statistics", "DurationSession_Set")]
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

        [Datatype("Statistics", "DurationSession_Get")]
        public IEnumerable<durationsession_entity> DurationSession_Get(int objectCollectionID, int[] channelIDList, int[] objectTypeIDList, int[] eventTypeIDList, string[] statsObjectIdentifierList, DateTime fromDate, DateTime toDate)
        {
            return StatisticsDataContext.DurationSession_Get(objectCollectionID, IsNull(channelIDList), IsNull(objectTypeIDList), IsNull(eventTypeIDList), IsNull(statsObjectIdentifierList), fromDate, toDate).ToList();
        }



        #endregion

        #region helper methods
        private string IsNull(int[] array)
        {
            if (array == null)
                return null;
            else
                return string.Join(",", array);
        }

        private string IsNull(string[] array)
        {
            if (array == null)
                return null;
            else
                return string.Join(",", array);
        }

        #endregion
    }
}
