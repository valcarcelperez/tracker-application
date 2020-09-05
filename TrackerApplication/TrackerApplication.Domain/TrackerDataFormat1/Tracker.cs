using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TrackerApplication.Domain.TrackerDataFormat1
{
    public class Tracker
    {
        public int Id { get; set; }
        
        public string Model { get; set; }
        
        [JsonConverter(typeof(MMDashDDDashYYYYDateTimeConverter))]
        public DateTime ShipmentStartDtm { get; set; }
        
        public List<Sensor> Sensors { get; set; }
    }
}
