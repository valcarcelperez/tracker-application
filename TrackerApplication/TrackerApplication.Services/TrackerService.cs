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
            return ServiceResponse<TrackerData[]>.CreateSucceed(data);
        }
    }
}
