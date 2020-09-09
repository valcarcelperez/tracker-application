using System;
using System.Text.Json.Serialization;
using TrackerApplication.Contracts.Converters;

namespace TrackerApplication.Contracts.Models.TrackerDataFormat2
{
    public class SensorData2
    {
        public string SensorType { get; set; }

        [JsonConverter(typeof(MMDashDDDashYYYYDateTimeConverter))]
        public DateTime DateTime { get; set; }
        
        public double Value { get; set; }
    }
}
