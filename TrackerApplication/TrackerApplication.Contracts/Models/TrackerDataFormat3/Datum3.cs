using System;
using System.Text.Json.Serialization;
using TrackerApplication.Contracts.Converters;

namespace TrackerApplication.Contracts.Models.TrackerDataFormat3
{
    public class Datum3
    {
        public string SensorType { get; set; }

        [JsonConverter(typeof(MMDashDDDashYYYYDateTimeConverter))]
        public DateTime DateTime { get; set; }
        
        public double Value { get; set; }
    }
}
