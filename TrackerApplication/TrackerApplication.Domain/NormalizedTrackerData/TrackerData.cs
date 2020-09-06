using System.Collections.Generic;

namespace TrackerApplication.Domain.NormalizedTrackerData
{
    public class TrackerData
    {
        public string CompanyId { get; set; }
        public string CompanyName { get; set; }

        public List<Tracker> Trackers { get; set; }
    }

    public class Tracker
    {
        public int? TrackerId { get; set; }
        public string TrackerName { get; set; }
    }
}
