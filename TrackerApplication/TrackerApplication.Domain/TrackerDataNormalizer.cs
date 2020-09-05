namespace TrackerApplication.Domain
{
    public static class TrackerDataNormalizer
    {
        public static NormalizedTrackerData.TrackerData NormalizeTrackerDate(TrackerDataFormat1.TrackerData data)
        {
            return new NormalizedTrackerData.TrackerData
            {
                CompanyId = data.PartnerId.ToString(),
                CompanyName = data.PartnerName
            };
        }        
    }
}
