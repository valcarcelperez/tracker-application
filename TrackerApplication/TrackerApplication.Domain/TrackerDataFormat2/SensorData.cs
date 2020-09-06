﻿using System;
using System.Text.Json.Serialization;

namespace TrackerApplication.Domain.TrackerDataFormat2
{
    public class SensorData
    {
        public string SensorType { get; set; }

        [JsonConverter(typeof(MMDashDDDashYYYYDateTimeConverter))]
        public DateTime DateTime { get; set; }
        
        public double Value { get; set; }
    }
}
