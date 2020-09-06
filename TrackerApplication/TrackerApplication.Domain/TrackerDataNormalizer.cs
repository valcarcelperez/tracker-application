using System.Collections.Generic;
using System.Linq;

namespace TrackerApplication.Domain
{
    public static class TrackerDataNormalizer
    {
        public static IEnumerable<NormalizedTrackerData.TrackerData> NormalizeTrackerData(TrackerDataFormat1.TrackerData data)
        {
            var companyId = data.PartnerId.ToString();
            var companyName = data.PartnerName;

            return data.Trackers.Select(tracker => CreateTrakerData(companyId, companyName, tracker));
        }

        private static NormalizedTrackerData.TrackerData CreateTrakerData(string companyId, string companyName, TrackerDataFormat1.Tracker tracker)
        {
            return new NormalizedTrackerData.TrackerData
            {
                CompanyId = companyId,
                CompanyName = companyName,
                TrackerId = tracker.Id,
                TrackerName = tracker.Model
            };
        }
    }
}
