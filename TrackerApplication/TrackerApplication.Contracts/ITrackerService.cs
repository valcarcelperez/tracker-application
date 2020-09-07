using TrackerApplication.Contracts.Models;

namespace TrackerApplication.Contracts
{
    public interface ITrackerService
    {
        ServiceResponse<TrackerData[]> RetrieveAll();
        ServiceResponse Add(Models.TrackerDataFormat1.TrackerData trackerData1);
        ServiceResponse Add(Models.TrackerDataFormat2.TrackerData trackerData2);
        ServiceResponse Add(Models.TrackerDataFormat3.TrackerData trackerData3);
    }
}
