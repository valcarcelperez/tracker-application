using System.Collections.Generic;

namespace TrackerApplication.Contracts.Models.TrackerDataFormat2
{
    public class TrackerData2
    {
        public int CompanyId { get; set; }
        public string Company { get; set; }
        public List<Device2> Devices { get; set; }
    }
}
