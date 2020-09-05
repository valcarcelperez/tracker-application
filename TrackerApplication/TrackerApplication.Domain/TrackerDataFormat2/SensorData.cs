using System;

namespace TrackerApplication.Domain.TrackerDataFormat2
{
    public class SensorData
    {
        public string SensorType { get; set; }
        public DateTime DateTime { get; set; }
        public double Value { get; set; }
    }
}
