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

        [HttpGet]
        public IActionResult Info()
        {
            return Ok("TrackerController");
        }

        [HttpPost("retrieve")]
        public IActionResult Retrieve()
        {
            return Execute(() => _trackerService.RetrieveAll());
        }
    }
}
