using System;
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
                TrackerName = tracker.Model,
                FirstCrumbDtm = FindFirstCrumbDtm(tracker),
                LastCrumbDtm = FindLastCrumbDtm(tracker)
            };
        }

        private static DateTime? FindFirstCrumbDtm(TrackerDataFormat1.Tracker tracker)
        {
            if (tracker.Sensors == null || tracker.Sensors.Count == 0)
            {
                return null;
            }

            var firstTemperatureCrumbDtm = FindFirstCrumbDtm("Temperature", tracker);
            var firstHumidtyCrumbDtm = FindFirstCrumbDtm("Humidty", tracker);
            return firstTemperatureCrumbDtm <= firstHumidtyCrumbDtm ? firstTemperatureCrumbDtm : firstHumidtyCrumbDtm;
        }

        private static DateTime FindFirstCrumbDtm(string sensorName, TrackerDataFormat1.Tracker tracker)
        {
            var temperatureSensor = tracker.Sensors.Where(sensor => sensor.Name == sensorName).FirstOrDefault();
            if (temperatureSensor != null && temperatureSensor.Crumbs != null && temperatureSensor.Crumbs.Count > 0)
            {
                return  temperatureSensor.Crumbs.Min(crumbs => crumbs.CreatedDtm);
            }
            else
            {
                return DateTime.MaxValue;
            }
        }

        private static DateTime? FindLastCrumbDtm(TrackerDataFormat1.Tracker tracker)
        {
            if (tracker.Sensors == null || tracker.Sensors.Count == 0)
            {
                return null;
            }

            var firstTemperatureCrumbDtm = FindLastCrumbDtm("Temperature", tracker);
            var firstHumidtyCrumbDtm = FindLastCrumbDtm("Humidty", tracker);
            return firstTemperatureCrumbDtm >= firstHumidtyCrumbDtm ? firstTemperatureCrumbDtm : firstHumidtyCrumbDtm;
        }

        private static DateTime FindLastCrumbDtm(string sensorName, TrackerDataFormat1.Tracker tracker)
        {
            var temperatureSensor = tracker.Sensors.Where(sensor => sensor.Name == sensorName).FirstOrDefault();
            if (temperatureSensor != null && temperatureSensor.Crumbs != null && temperatureSensor.Crumbs.Count > 0)
            {
                return temperatureSensor.Crumbs.Max(crumbs => crumbs.CreatedDtm);
            }
            else
            {
                return DateTime.MinValue;
            }
        }
    }
}
