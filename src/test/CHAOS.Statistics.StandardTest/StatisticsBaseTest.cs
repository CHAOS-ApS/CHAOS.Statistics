using System;
using System.Xml.Linq;
using Geckon.Portal.Test;
using NUnit.Framework;

namespace CHAOS.Statistics.StandardTest
{
    public class StatisticsBaseTest : TestBase
    {
        public CHAOS.Statistics.Module.Standard.StatisticsModule StatisticsModule { get; set; }

        [SetUp]
        public void SetUp()
        {
            //base.SetUp();

            StatisticsModule = new CHAOS.Statistics.Module.Standard.StatisticsModule();
            StatisticsModule.Initialize("<Settings ConnectionString=\"metadata=res://*/StatisticsDB.csdl|res://*/StatisticsDB.ssdl|res://*/StatisticsDB.msl;provider=MySql.Data.MySqlClient;provider connection string=&quot;server=localhost;User Id=root;password=pbvu7000;database=statistics&quot;\" providerName=\"System.Data.EntityClient\"/>");
        }

    }
}
    