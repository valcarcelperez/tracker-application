using TrackerApplication.Contracts.Models;

namespace TrackerApplication.Contracts
{
    public interface ITrackerService
    {
        ServiceResponse<TrackerData[]> RetrieveAll();
        ServiceResponse Add(Models.TrackerDataFormat1.TrackerData1 trackerData1);
        ServiceResponse Add(Models.TrackerDataFormat2.TrackerData2 trackerData2);
        ServiceResponse Add(Models.TrackerDataFormat3.TrackerData3 trackerData3);
    }
}
