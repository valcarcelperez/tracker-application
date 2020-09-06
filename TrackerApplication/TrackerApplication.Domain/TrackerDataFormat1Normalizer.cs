using System;
using System.Collections.Generic;
using System.Linq;
using TrackerApplication.Domain.TrackerDataFormat1;

namespace TrackerApplication.Domain
{
    public static class TrackerDataFormat1Normalizer
    {
        public static IEnumerable<NormalizedTrackerData.TrackerData> NormalizeTrackerData(TrackerData data)
        {
            var company = new Company { CompanyId = data.PartnerId.ToString(), CompanyName = data.PartnerName };
            return data.Trackers.Select(tracker => CreateTrakerData(company, tracker));
        }

        private static NormalizedTrackerData.TrackerData CreateTrakerData(Company company, Tracker tracker)
        {
            var aggregatedTemperature = AggregateCrumbData("Temperature", tracker);
            var aggregatedHumidty = AggregateCrumbData("Humidty", tracker);

            var normalizedTracker = new NormilizedTracker { TrackerId = tracker.Id, TrackerName = tracker.Model };
            return TrackerDataNormalizer.CreateTrakerData(company, normalizedTracker, aggregatedTemperature, aggregatedHumidty);
        }

        private static AggregatedCrumData AggregateCrumbData(string sensorName, Tracker tracker)
        {
            var result = new AggregatedCrumData();

            if (tracker.Sensors == null || tracker.Sensors.Count == 0)
            {
                return result;
            }

            var sensor = tracker.Sensors.Where(s => s.Name == sensorName).FirstOrDefault();
            if (sensor != null && sensor.Crumbs != null && sensor.Crumbs.Count > 0)
            {
                var aggregated = sensor.Crumbs
                    .GroupBy(item => 1)
                    .Select(crumbs => new
                    {
                        FirstCrumbDtm = crumbs.Min(a => a.CreatedDtm),
                        LastCrumbDtm = crumbs.Max(a => a.CreatedDtm),
                        CrumbCount = crumbs.Count(),
                        AvgValue = Math.Round(crumbs.Average(a => a.Value), 2)
                    })
                    .First();

                result.FirstCrumbDtm = aggregated.FirstCrumbDtm;
                result.LastCrumbDtm = aggregated.LastCrumbDtm;
                result.CrumbCount = aggregated.CrumbCount;
                result.AvgValue = aggregated.AvgValue;
            }

            return result;
        }
    }
}
