using System.Collections.Generic;
using TrackerApplication.Contracts.Models;

namespace TrackerApplication.Contracts
{
    public interface ITrakerRepository
    {
        void Add(IEnumerable<TrackerData> trackerDatas);
        IEnumerable<TrackerData> Retrieve();
    }
}
