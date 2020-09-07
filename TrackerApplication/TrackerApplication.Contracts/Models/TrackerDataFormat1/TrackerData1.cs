using System.Collections.Generic;

namespace TrackerApplication.Contracts.Models.TrackerDataFormat1
{
    public class TrackerData1
    {
        public int PartnerId { get; set; }
        public string PartnerName { get; set; }
        public List<Tracker1> Trackers { get; set; }
    }
}
