using System.Linq;
using TrackerApplication.Contracts;
using TrackerApplication.Contracts.Models;
using TrackerApplication.Domain;

namespace TrackerApplication.Services
{
    public class TrackerService : ITrackerService
    {
        private readonly ITrakerRepository _trakerRepository;

        public TrackerService(ITrakerRepository trakerRepository)
        {
            _trakerRepository = trakerRepository;
        }

        public ServiceResponse Add(Contracts.Models.TrackerDataFormat1.TrackerData trackerData1)
        {
            var normalizedTrackerData = TrackerDataFormat1Normalizer.NormalizeTrackerData(trackerData1);
            _trakerRepository.Add(normalizedTrackerData);
            return ServiceResponse.CreateSucceed();
        }

        public ServiceResponse Add(Contracts.Models.TrackerDataFormat2.TrackerData trackerData2)
        {
            var normalizedTrackerData = TrackerDataFormat2Normalizer.NormalizeTrackerData(trackerData2);
            _trakerRepository.Add(normalizedTrackerData);
            return ServiceResponse.CreateSucceed();
        }

        public ServiceResponse Add(Contracts.Models.TrackerDataFormat3.TrackerData trackerData3)
        {
            var normalizedTrackerData = TrackerDataFormat3Normalizer.NormalizeTrackerData(trackerData3);
            _trakerRepository.Add(normalizedTrackerData);
            return ServiceResponse.CreateSucceed();
        }

        public ServiceResponse<TrackerData[]> RetrieveAll()
        {
            var data = _trakerRepository.Retrieve().ToArray();

            //var data = new TrackerData[]
            //{
            //    new TrackerData
            //    {
            //        CompanyId = "1",
            //        CompanyName = "Foo1",
            //        TrackerId = 1,
            //        TrackerName = "ABC-100",
            //        FirstCrumbDtm = DateTime.Parse("2020-08-17 10:35:00"),
            //        LastCrumbDtm = DateTime.Parse("2020-08-17 10:45:00"),
            //        TempCount = 3,
            //        AvgTemp = 23.15,
            //        HumidityCount = 3,
            //        AvgHumdity = 81.5
            //    },
            //    new TrackerData
            //    {
            //        CompanyId = "1",
            //        CompanyName = "Foo1",
            //        TrackerId = 2,
            //        TrackerName = "ABC-200",
            //        FirstCrumbDtm = DateTime.Parse("2020-08-18 10:35:00"),
            //        LastCrumbDtm = DateTime.Parse("2020-08-18 10:45:00"),
            //        TempCount = 3,
            //        AvgTemp = 24.15,
            //        HumidityCount = 3,
            //        AvgHumdity = 82.5
            //    }
            //};

            return ServiceResponse<TrackerData[]>.CreateSucceed(data);
        }
    }
}
