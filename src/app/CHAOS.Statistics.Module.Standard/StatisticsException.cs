using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace CHAOS.Statistics.Module.Standard
{
    public class StatisticsException : Exception
    {
        //public StatisticsException(string message);
        //protected StatisticsException(SerializationInfo info, StreamingContext context);
        //public StatisticsException(string message, Exception innerException);

        public StatisticsException(string message, Exception innerException): base(message, innerException){}

        public StatisticsException(string message) : base(message) { }

    }
}
