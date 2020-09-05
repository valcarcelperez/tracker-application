using System.Collections.Generic;

namespace TrackerApplication.Domain.TrackerDataFormat1
{
    public class Sensor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Crumb> Crumbs { get; set; }
    }
}
