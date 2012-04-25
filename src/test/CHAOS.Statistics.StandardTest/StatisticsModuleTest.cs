using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using CHAOS.Statistics.Module.Standard;
using CHAOS.Statistics.Data.EF;
using System.Data.Objects;
using System.Diagnostics; 

namespace CHAOS.Statistics.StandardTest
{
    [TestFixture]
    public class StatisticsModuleTest :StatisticsBaseTest
    {

        #region SetStats
        [Test]
        public void Should_SetStats()
        {
            var returnValue = StatisticsModule.Set("{17787A86-0A0E-48C0-BA47-1994C2055410}", "My objectIdentifier- test", 1, 1, "My Channel", 3, 1, "My Object", "000.000.000.000", "Test city", "Test nation", 0);

            Assert.Greater(returnValue.ObjectSessionID, 0);
        }

        //[Test]
        //public void Should_SetStats_100()
        //{
        //    Stopwatch timer = new Stopwatch();
        //    timer.Start();
        //    for (int i = 0; i <= 125; i++)
        //    {
        //        int returnValue = StatisticsModule.StatsObject_Set("{17787A86-0A0E-48C0-BA47-1994C2055410}", "My objectIdentifier- test", 1, 1, "My Channel", 3, 1, "My Object", "000.000.000.000", "Test city", "Test nation", 0);
        //    }
        //    timer.Stop();

        //    Assert.Less(timer.Elapsed.TotalSeconds, 8);
        //}

        #endregion

        #region DayStats
        [Test]
        public void Should_Get_DayStats()
        {
            var returnValue = StatisticsModule.Set("{17787A86-0A0E-48C0-BA47-1994C2055410}", "test2403", 1, 1, "My Channel", 3, 1, "My Object", "000.000.000.000", "Test city", "Test nation", 0);

            var result = StatisticsModule.DayStats_Get(1, null, null, new int[] { 1, 2, 3, 4, 5 }, new string[] {"test2403", "test2" }, DateTime.Now.AddDays(-1), DateTime.Now);

            Assert.IsNotEmpty(result.ToList());
        }

        [Test]
        public void Shoud_Get_DayStats_Objects()
        {
            var returnValue = StatisticsModule.Set("{17787A86-0A0E-48C0-BA47-1994C2055410}", "My objectIdentifiertest", 1, 1, "My Channel", 3, 1, "My Object", "000.000.000.000", "Test city", "Test nation", 0);

            var result = StatisticsModule.DayStats_GetObjects(1, null, null, new int[] { 1, 2, 3, 4, 5 }, DateTime.Now.AddDays(-1), DateTime.Now, 0, 25, "ASC");

            Assert.IsNotEmpty(result.ToList());

            var result2 = StatisticsModule.DayStats_GetObjects(1, null, null, new int[] { 1, 2, 3, 4, 5 }, DateTime.Now.AddDays(-1), DateTime.Now, 0, 25, "DESC");

            Assert.IsNotEmpty(result.ToList());

            Assert.AreEqual(result.Count(), result2.Count());

        }

        [Test]
        public void Should_Get_DayStatsTotal()
        {
            var returnValue = StatisticsModule.Set("{17787A86-0A0E-48C0-BA47-1994C2055410}", "My objectIdentifiertest", 1, 1, "My Channel", 3, 1, "My Object", "000.000.000.000", "Test city", "Test nation", 0);

            var result = StatisticsModule.DayStats_GetTotal(1, null, null, new int[] { 1, 2, 3, 4, 5 }, new string[] { "My objectIdentifiertest, test2" }, DateTime.Now.AddDays(-1), DateTime.Now);

            Assert.GreaterOrEqual(result.Count, 0);
        }

        [Test]
        public void Shoud_Get_DayStatsGeo()
        {
            var returnValue = StatisticsModule.Set("{17787A86-0A0E-48C0-BA47-1994C2055410}", "My objectIdentifiertest", 1, 1, "My Channel", 3, 1, "My Object", "000.000.000.000", "Test city", "Test nation", 0);

            var resultCity = StatisticsModule.DayStatsGeo_Get(1, null, null, new int[] { 1, 2, 3, 4, 5 }, new string[] { "My objectIdentifiertest,test" }, DateTime.Now.AddDays(-1), DateTime.Now, 0, 100, "ASC", "City");

            var resultCountry = StatisticsModule.DayStatsGeo_Get(1, null, null, new int[] { 1, 2, 3, 4, 5 }, new string[] { "My objectIdentifiertest,test" }, DateTime.Now.AddDays(-1), DateTime.Now, 0, 100, "ASC", "Country");

            var resultIP = StatisticsModule.DayStatsGeo_Get(1, null, null, new int[] { 1, 2, 3, 4, 5 }, new string[] { "My objectIdentifiertest,test" }, DateTime.Now.AddDays(-1), DateTime.Now, 0, 100, "ASC", "IP");

            Assert.IsNotEmpty(resultCity.ToList());

            Assert.IsNotEmpty(resultCountry.ToList());

            Assert.IsNotEmpty(resultIP.ToList());

        }
        #endregion

        #region HourStats
        [Test]
        public void Should_Get_HourStats()
        {
            var returnValue = StatisticsModule.Set("{17787A86-0A0E-48C0-BA47-1994C2055410}", "My objectIdentifiertest", 1, 1, "My Channel", 3, 1, "My Object", "000.000.000.000", "Test city", "Test nation", 0);

            var result = StatisticsModule.HourStats_Get(1, null, null, new int[] { 1, 2, 3, 4, 5 }, new string[] { "My objectIdentifiertest,test" }, DateTime.Now.AddHours(-1), DateTime.Now);

            Assert.IsNotEmpty(result.ToList());
        }

        [Test]
        public void Should_Get_HourStats_Objects()
        {
            var returnValue = StatisticsModule.Set("{17787A86-0A0E-48C0-BA47-1994C2055410}", "My objectIdentifiertest", 1, 1, "My Channel", 3, 1, "My Object", "000.000.000.000", "Test city", "Test nation", 0);

            var returnValue2 = StatisticsModule.Set("{17787A86-0A0E-48C0-BA47-1994C2055410}", "My objectIdentifiertest99", 1, 1, "My Channel", 3, 1, "My Object", "000.000.000.000", "Test city", "Test nation", 0);

            var result = StatisticsModule.HourStats_GetObjects(1, null, null, new int[] { 1, 2, 3, 4, 5 }, DateTime.Now.AddHours(-1), DateTime.Now, 0, 25, "ASC");

            Assert.IsNotEmpty(result.ToList());

            var result2 = StatisticsModule.HourStats_GetObjects(1, null, null, new int[] { 1, 2, 3, 4, 5 }, DateTime.Now.AddHours(-1), DateTime.Now, 0, 25, "DESC");

            Assert.IsNotEmpty(result.ToList());

            Assert.AreEqual(result.Count(), result2.Count());
        }

        #endregion

        #region Duration Session
        [Test]
        public void Should_Set_DurationSession()
        {
            var objectSessionID = StatisticsModule.Set("{17787A86-0A0E-48C0-BA47-1994C2055410}", "My objectIdentifiertest", 1, 1, "My Channel", 3, 1, "My Object", "000.000.000.000", "Test city", "Test nation", 0);

            int returnValue = StatisticsModule.DurationSession_Set(objectSessionID.ObjectSessionID, 100, 1000);

            int returnValue2 = StatisticsModule.DurationSession_Set(objectSessionID.ObjectSessionID, 100, 2000);

            int returnValue3 = StatisticsModule.DurationSession_Set(objectSessionID.ObjectSessionID, 600, 1000);

            int returnValue4 = StatisticsModule.DurationSession_Set(objectSessionID.ObjectSessionID, 800, 2000);

            // Assert that durationSessionID is returned
            Assert.Greater(returnValue, 0);

            // Assert that durationsession is updated
            Assert.AreEqual(returnValue, returnValue2);

            // Assert that new durationSession is inserted
            Assert.AreNotEqual(returnValue3, returnValue4);
        }

        [Test]
        public void Should_Get_Duration()
        {
            var objectSessionID = StatisticsModule.Set("{17787A86-0A0E-48C0-BA47-1994C2055410}", "My objectIdentifiertest", 1, 1, "My Channel", 3, 1, "My Object", "000.000.000.000", "Test city", "Test nation", 0);

            var returnValue = StatisticsModule.DurationSession_Set(objectSessionID.ObjectSessionID, 100, 1000);

            var result = StatisticsModule.DurationSession_Get(1, null, null, null, null, DateTime.Now.AddDays(-1), DateTime.Now);

            Assert.IsNotEmpty(result.ToList());
        }

        #endregion
    }
}
