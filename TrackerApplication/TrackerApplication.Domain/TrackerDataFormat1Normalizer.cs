using System;
using System.Collections.Generic;
using System.Linq;
using TrackerApplication.Domain.TrackerDataFormat1;

namespace TrackerApplication.Domain
{
    public static class TrackerDataFormat1Normalizer
    {
        public static IEnumerable<Contracts.Models.TrackerData> NormalizeTrackerData(TrackerData data)
        {
            var company = new Company { CompanyId = data.PartnerId.ToString(), CompanyName = data.PartnerName };
            return data.Trackers.Select(tracker => CreateTrakerData(company, tracker));
        }

        private static Contracts.Models.TrackerData CreateTrakerData(Company company, Tracker tracker)
        {
            var aggregatedTemperature = AggregateCrumbData("Temperature", tracker);
            var aggregatedHumidty = AggregateCrumbData("Humidty", tracker);

            var normalizedTracker = new NormilizedTracker { TrackerId = tracker.Id, TrackerName = tracker.Model };
            return TrackerDataNormalizer.CreateTrakerData(company, normalizedTracker, aggregatedTemperature, aggregatedHumidty);
        }

        private static AggregatedCrumData AggregateCrumbData(string sensorName, Tracker tracker)
        {
            if (tracker.Sensors == null || tracker.Sensors.Count == 0)
            {
                return new AggregatedCrumData();
            }

            var sensor = tracker.Sensors.Where(s => s.Name == sensorName).FirstOrDefault();
            if (sensor != null && sensor.Crumbs != null && sensor.Crumbs.Count > 0)
            {
                return sensor.Crumbs
                    .GroupBy(item => 1)
                    .Select(crumbs => new AggregatedCrumData
                    {
                        FirstCrumbDtm = crumbs.Min(a => a.CreatedDtm),
                        LastCrumbDtm = crumbs.Max(a => a.CreatedDtm),
                        CrumbCount = crumbs.Count(),
                        AvgValue = Math.Round(crumbs.Average(a => a.Value), 2)
                    })
                    .First();
            }

            return new AggregatedCrumData();
        }
    }
}
