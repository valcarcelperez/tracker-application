using System.Collections.Generic;

namespace TrackerApplication.Domain.TrackerDataFormat2
{
    public class TrackerData
    {
        public int CompanyId { get; set; }
        public string Company { get; set; }
        public List<Device> Devices { get; set; }
    }
}
