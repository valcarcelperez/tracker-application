using System;
using System.Text.Json.Serialization;

namespace TrackerApplication.Domain.TrackerDataFormat3
{
    public class Datum
    {
        public string SensorType { get; set; }

        [JsonConverter(typeof(MMDashDDDashYYYYDateTimeConverter))]
        public DateTime DateTime { get; set; }
        
        public double Value { get; set; }
    }
}
