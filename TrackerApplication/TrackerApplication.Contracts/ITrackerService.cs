﻿using TrackerApplication.Contracts.Models;

namespace TrackerApplication.Contracts
{
    public interface ITrackerService
    {
        ServiceResponse<TrackerData[]> RetrieveAll();
        ServiceResponse Add<TTrackerData>(TTrackerData trackerData);
    }
}
