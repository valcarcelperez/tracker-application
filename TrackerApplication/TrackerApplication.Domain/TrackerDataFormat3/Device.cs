using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TrackerApplication.Domain.TrackerDataFormat3
{
    public class Device
    {
        public int ID { get; set; }
        
        public string DeviceName { get; set; }

        [JsonConverter(typeof(MMDashDDDashYYYYDateTimeConverter))]
        public DateTime StartDateTime { get; set; }
        
        public List<Datum> Data { get; set; }
    }
}
