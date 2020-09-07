using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TrackerApplication.Contracts;

namespace TrackerApplication.WebApi.Controllers
{
    [Route("api/tracker")]
    [ApiController]
    public class TrackerController : TrackerApplicationController<TrackerController>
    {
        private readonly ITrackerService _trackerService;

        public TrackerController(ILogger<TrackerController> logger, ITrackerService trackerService)
            : base(logger)
        {
            _trackerService = trackerService;
        }

        [HttpPost("retrieve")]
        public IActionResult Retrieve()
        {
            return Execute(() => _trackerService.RetrieveAll());
        }

        [HttpPost("saveTrackerData1")]
        public IActionResult SaveTrackerDataWithFormat1([FromBody] Contracts.Models.TrackerDataFormat1.TrackerData1 trackerData1)
        {
            return Execute(() => _trackerService.Add(trackerData1));
        }

        [HttpPost("saveTrackerData2")]
        public IActionResult SaveTrackerDataWithFormat2([FromBody] Contracts.Models.TrackerDataFormat2.TrackerData2 trackerData2)
        {
            return Execute(() => _trackerService.Add(trackerData2));
        }

        [HttpPost("saveTrackerData3")]
        public IActionResult SaveTrackerDataWithFormat3([FromBody] Contracts.Models.TrackerDataFormat3.TrackerData3 trackerData3)
        {
            return Execute(() => _trackerService.Add(trackerData3));
        }
    }
}
