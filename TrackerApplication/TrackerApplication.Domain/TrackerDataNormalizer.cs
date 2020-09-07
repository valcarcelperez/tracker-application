using System;

namespace TrackerApplication.Domain
{
    public class AggregatedCrumData
    {
        public DateTime? FirstCrumbDtm { get; set; }
        public DateTime? LastCrumbDtm { get; set; }
        public int? CrumbCount { get; set; }
        public double? AvgValue { get; set; }
    }

    public class Company
    {
        public string CompanyId { get; set; }
        public string CompanyName { get; set; }
    }

    public class NormilizedTracker
    {
        public int? TrackerId { get; set; }
        public string TrackerName { get; set; }
    }

    public static class TrackerDataNormalizer
    {
        public static Contracts.Models.TrackerData CreateTrakerData(
            Company company,
            NormilizedTracker tracker,
            AggregatedCrumData aggregatedTemperature,
            AggregatedCrumData aggregatedHumidty)
        {
            DateTime? firstCrumbDtm = FindfirstCrumbDtm(aggregatedTemperature, aggregatedHumidty);
            DateTime? lastCrumbDtm = FindLastCrumbDtm(aggregatedTemperature, aggregatedHumidty);

            return new Contracts.Models.TrackerData
            {
                CompanyId = company.CompanyId,
                CompanyName = company.CompanyName,
                TrackerId = tracker.TrackerId,
                TrackerName = tracker.TrackerName,
                FirstCrumbDtm = firstCrumbDtm,
                LastCrumbDtm = lastCrumbDtm,
                TempCount = aggregatedTemperature.CrumbCount,
                AvgTemp = aggregatedTemperature.AvgValue,
                HumidityCount = aggregatedHumidty.CrumbCount,
                AvgHumdity = aggregatedHumidty.AvgValue
            };
        }

        private static DateTime? FindLastCrumbDtm(AggregatedCrumData aggregatedTemperature, AggregatedCrumData aggregatedHumidty)
        {
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

            return lastCrumbDtm;
        }

        private static DateTime? FindfirstCrumbDtm(AggregatedCrumData aggregatedTemperature, AggregatedCrumData aggregatedHumidty)
        {
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

            return firstCrumbDtm;
        }
    }
}
