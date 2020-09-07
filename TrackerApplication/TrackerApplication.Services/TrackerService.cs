using System;
using TrackerApplication.Contracts;
using TrackerApplication.Contracts.Models;

namespace TrackerApplication.Services
{
    public class TrackerService : ITrackerService
    {
        public ServiceResponse Add<TTrackerData>(TTrackerData trackerData)
        {
            throw new NotImplementedException();
        }

        public ServiceResponse<TrackerData[]> RetrieveAll()
        {
            var data = new TrackerData[]
            {
                new TrackerData
                {
                    CompanyId = "1",
                    CompanyName = "Foo1",
                    TrackerId = 1,
                    TrackerName = "ABC-100",
                    FirstCrumbDtm = DateTime.Parse("2020-08-17 10:35:00"),
                    LastCrumbDtm = DateTime.Parse("2020-08-17 10:45:00"),
                    TempCount = 3,
                    AvgTemp = 23.15,
                    HumidityCount = 3,
                    AvgHumdity = 81.5
                },
                new TrackerData
                {
                    CompanyId = "1",
                    CompanyName = "Foo1",
                    TrackerId = 2,
                    TrackerName = "ABC-200",
                    FirstCrumbDtm = DateTime.Parse("2020-08-18 10:35:00"),
                    LastCrumbDtm = DateTime.Parse("2020-08-18 10:45:00"),
                    TempCount = 3,
                    AvgTemp = 24.15,
                    HumidityCount = 3,
                    AvgHumdity = 82.5
                }
            };

            return ServiceResponse<TrackerData[]>.CreateSucceed(data);
        }
    }
}
