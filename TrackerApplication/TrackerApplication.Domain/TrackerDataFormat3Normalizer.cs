using System;
using System.Collections.Generic;
using System.Linq;
using TrackerApplication.Domain.TrackerDataFormat3;

namespace TrackerApplication.Domain
{
    public static class TrackerDataFormat3Normalizer
    {
        public static IEnumerable<Contracts.Models.TrackerData> NormalizeTrackerData(TrackerData data)
        {
            var company = new Company { CompanyId = data.CompanyId.ToString(), CompanyName = data.Company };
            return data.Devices.Select(device => CreateTrakerData(company, device));
        }

        private static Contracts.Models.TrackerData CreateTrakerData(Company company, Device device)
        {
            var aggregatedTemperature = AggregateCrumbData("TEMP", device);
            var aggregatedHumidty = AggregateCrumbData("HUM", device);

            var normalizedTracker = new NormilizedTracker { TrackerId = device.ID, TrackerName = device.DeviceName };
            return TrackerDataNormalizer.CreateTrakerData(company, normalizedTracker, aggregatedTemperature, aggregatedHumidty);
        }

        private static AggregatedCrumData AggregateCrumbData(string sensorName, Device device)
        {
            if (device.Data == null || device.Data.Count == 0)
            {
                return new AggregatedCrumData();
            }

            var sensor = device.Data
                .Where(data => data.SensorType == sensorName);

            if (sensor.Count() > 0)
            {
                return sensor
                .GroupBy(item => 1)
                .Select(data => new AggregatedCrumData
                {
                    FirstCrumbDtm = data.Min(a => a.DateTime),
                    LastCrumbDtm = data.Max(a => a.DateTime),
                    CrumbCount = data.Count(),
                    AvgValue = Math.Round(data.Average(a => a.Value), 2)
                })
                .First();
            }

            return new AggregatedCrumData();
        }
    }
}
