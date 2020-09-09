using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using TrackerApplication.Contracts.Converters;

namespace TrackerApplication.Contracts.Models.TrackerDataFormat1
{
    public class Tracker1
    {
        public int Id { get; set; }
        
        public string Model { get; set; }
        
        [JsonConverter(typeof(MMDashDDDashYYYYDateTimeConverter))]
        public DateTime ShipmentStartDtm { get; set; }
        
        public List<Sensor1> Sensors { get; set; }
    }
}
