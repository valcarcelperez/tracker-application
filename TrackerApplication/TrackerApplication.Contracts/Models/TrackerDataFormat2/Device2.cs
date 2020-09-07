using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TrackerApplication.Contracts.Models.TrackerDataFormat2
{
    public class Device2
    {
        public int DeviceID { get; set; }
     
        public string Name { get; set; }
        
        [JsonConverter(typeof(MMDashDDDashYYYYDateTimeConverter))]
        public DateTime StartDateTime { get; set; }
    
        public List<SensorData2> SensorData { get; set; }
    }
}
