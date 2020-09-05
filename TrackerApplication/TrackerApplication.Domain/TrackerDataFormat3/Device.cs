using System;
using System.Collections.Generic;

namespace TrackerApplication.Domain.TrackerDataFormat3
{
    public class Device
    {
        public int ID { get; set; }
        public string DeviceName { get; set; }
        public DateTime StartDateTime { get; set; }
        public List<Datum> Data { get; set; }
    }
}
