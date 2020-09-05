using System;

namespace TrackerApplication.Domain.TrackerDataFormat3
{
    public class Datum
    {
        public string SensorType { get; set; }
        public DateTime DateTime { get; set; }
        public double Value { get; set; }
    }
}
