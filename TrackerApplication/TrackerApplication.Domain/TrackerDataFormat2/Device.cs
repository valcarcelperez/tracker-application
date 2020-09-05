using System;
using System.Collections.Generic;

namespace TrackerApplication.Domain.TrackerDataFormat2
{
    public class Device
    {
        public int DeviceID { get; set; }
        public string Name { get; set; }
        public DateTime StartDateTime { get; set; }
        public List<SensorData> SensorData { get; set; }
    }
}
