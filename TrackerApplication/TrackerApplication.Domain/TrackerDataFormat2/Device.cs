using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TrackerApplication.Domain.TrackerDataFormat2
{
    public class Device
    {
        public int DeviceID { get; set; }
     
        public string Name { get; set; }
        
        [JsonConverter(typeof(MMDashDDDashYYYYDateTimeConverter))]
        public DateTime StartDateTime { get; set; }
    
        public List<SensorData> SensorData { get; set; }
    }
}
