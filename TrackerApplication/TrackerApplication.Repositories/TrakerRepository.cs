using System.Collections.Generic;
using TrackerApplication.Contracts;
using TrackerApplication.Contracts.Models;

namespace TrackerApplication.Repositories
{
    public class TrakerRepository : ITrakerRepository
    {
        private readonly object lockObj = new object();
        private readonly List<TrackerData> _trackerDatas = new List<TrackerData>();

        public void Add(IEnumerable<TrackerData> trackerDatas)
        {
            lock(lockObj)
            {
                _trackerDatas.AddRange(trackerDatas);
            }
        }

        public IEnumerable<TrackerData> Retrieve()
        {
            lock (lockObj)
            {
                return _trackerDatas.ToArray();
            }
        }
    }
}
