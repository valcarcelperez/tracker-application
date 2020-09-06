using System;
using System.Collections.Generic;
using System.Linq;

namespace TrackerApplication.Domain
{
    public static class TrackerDataFormat1Normalizer
    {
        public static IEnumerable<NormalizedTrackerData.TrackerData> NormalizeTrackerData(TrackerDataFormat1.TrackerData data)
        {
            var companyId = data.PartnerId.ToString();
            var companyName = data.PartnerName;

            return data.Trackers.Select(tracker => CreateTrakerData(companyId, companyName, tracker));
        }

        private static NormalizedTrackerData.TrackerData CreateTrakerData(string companyId, string companyName, TrackerDataFormat1.Tracker tracker)
        {
            var aggregatedTemperature = AggregateCrumbData("Temperature", tracker);
            var aggregatedHumidty = AggregateCrumbData("Humidty", tracker);

            DateTime? firstCrumbDtm = null;
            if (aggregatedTemperature.FirstCrumbDtm.HasValue || aggregatedHumidty.LastCrumbDtm.HasValue)
            {
                if (!aggregatedTemperature.FirstCrumbDtm.HasValue)
                {
                    aggregatedTemperature.FirstCrumbDtm = DateTime.MaxValue;
                }

                if (!aggregatedHumidty.LastCrumbDtm.HasValue)
                {
                    aggregatedHumidty.LastCrumbDtm = DateTime.MinValue;
                }

                firstCrumbDtm = aggregatedTemperature.FirstCrumbDtm <= aggregatedHumidty.LastCrumbDtm ? aggregatedTemperature.FirstCrumbDtm : aggregatedHumidty.FirstCrumbDtm;
            }

            DateTime? lastCrumbDtm = null;
            if (aggregatedTemperature.LastCrumbDtm.HasValue || aggregatedHumidty.LastCrumbDtm.HasValue)
            {
                if (!aggregatedTemperature.LastCrumbDtm.HasValue)
                {
                    aggregatedTemperature.LastCrumbDtm = DateTime.MinValue;
                }

                if (!aggregatedHumidty.LastCrumbDtm.HasValue)
                {
                    aggregatedHumidty.LastCrumbDtm = DateTime.MaxValue;
                }

                lastCrumbDtm = aggregatedTemperature.LastCrumbDtm >= aggregatedHumidty.LastCrumbDtm ? aggregatedTemperature.LastCrumbDtm : aggregatedHumidty.LastCrumbDtm;
            }

            return new NormalizedTrackerData.TrackerData
            {
                CompanyId = companyId,
                CompanyName = companyName,
                TrackerId = tracker.Id,
                TrackerName = tracker.Model,
                FirstCrumbDtm = firstCrumbDtm,
                LastCrumbDtm = lastCrumbDtm,
                TempCount = aggregatedTemperature.CrumbCount,
                AvgTemp = aggregatedTemperature.AvgValue,
                HumidityCount = aggregatedHumidty.CrumbCount,
                AvgHumdity = aggregatedHumidty.AvgValue
            };
        }

        private static AggregatedCrumData AggregateCrumbData(string sensorName, TrackerDataFormat1.Tracker tracker)
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
