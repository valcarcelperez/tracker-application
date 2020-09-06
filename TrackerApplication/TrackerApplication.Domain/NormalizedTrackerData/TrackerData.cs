using System;

namespace TrackerApplication.Domain.NormalizedTrackerData
{
    public class TrackerData
    {
        public string CompanyId { get; set; }
        public string CompanyName { get; set; }
        public int? TrackerId { get; set; }
        public string TrackerName { get; set; }
        public DateTime? FirstCrumbDtm { get; set; }
        public DateTime? LastCrumbDtm { get; set; }
    }
}
