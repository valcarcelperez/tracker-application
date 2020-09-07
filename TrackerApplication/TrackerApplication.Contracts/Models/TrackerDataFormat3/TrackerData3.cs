using System.Collections.Generic;

namespace TrackerApplication.Contracts.Models.TrackerDataFormat3
{
    public class TrackerData3
    {
        public int CompanyId { get; set; }
        public string Company { get; set; }
        public List<Device3> Devices { get; set; }
    }
}
