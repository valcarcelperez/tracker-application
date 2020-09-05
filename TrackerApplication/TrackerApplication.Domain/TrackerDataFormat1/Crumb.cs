using System;
using System.Text.Json.Serialization;

namespace TrackerApplication.Domain.TrackerDataFormat1
{
    public class Crumb
    {
        [JsonConverter(typeof(MMDashDDDashYYYYDateTimeConverter))]
        public DateTime CreatedDtm { get; set; }
        public double Value { get; set; }
    }
}
