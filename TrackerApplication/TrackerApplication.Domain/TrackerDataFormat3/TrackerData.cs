using System.Collections.Generic;

namespace TrackerApplication.Domain.TrackerDataFormat3
{
    public class TrackerData
    {
        public int CompanyId { get; set; }
        public string Company { get; set; }
        public List<Device> Devices { get; set; }
    }
}
