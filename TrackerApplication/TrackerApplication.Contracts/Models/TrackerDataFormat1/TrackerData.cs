using System.Collections.Generic;

namespace TrackerApplication.Contracts.Models.TrackerDataFormat1
{
    public class TrackerData
    {
        public int PartnerId { get; set; }
        public string PartnerName { get; set; }
        public List<Tracker> Trackers { get; set; }
    }
}
