using System.Collections.Generic;

namespace TrackerApplication.Contracts.Models.TrackerDataFormat1
{
    public class Sensor1
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Crumb1> Crumbs { get; set; }
    }
}
