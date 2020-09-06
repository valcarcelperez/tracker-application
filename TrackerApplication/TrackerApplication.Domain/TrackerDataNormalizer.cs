using System.Linq;

namespace TrackerApplication.Domain
{
    public static class TrackerDataNormalizer
    {
        public static NormalizedTrackerData.TrackerData NormalizeTrackerData(TrackerDataFormat1.TrackerData data)
        {
            return new NormalizedTrackerData.TrackerData
            {
                CompanyId = data.PartnerId.ToString(),
                CompanyName = data.PartnerName,
                Trackers = data.Trackers.Select(a => NormalizeTrackerData(a)).ToList()
            };
        }

        public static NormalizedTrackerData.Tracker NormalizeTrackerData(TrackerDataFormat1.Tracker data)
        {
            return new NormalizedTrackerData.Tracker
            {
                TrackerId = data.Id,
                TrackerName = data.Model
            };
        }
    }
}
