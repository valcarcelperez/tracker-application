using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using TrackerApplication.Contracts;

namespace TrackerApplication.WebApi.Controllers
{
    public class TrackerApplicationController<TController> : ControllerBase
    {
        protected ILogger<TController> _logger;

        public TrackerApplicationController(ILogger<TController> logger)
        {
            _logger = logger;
        }

        protected IActionResult Execute<TResult>(Func<TResult> func)
            where TResult : ServiceResponse
        {
            try
            {
                var result = func();
                return Ok(result);
            }
            catch (BadRequestException ex)
            {
                _logger.LogError($"Exception: {ex}");
                return StatusCode((int)HttpStatusCode.BadRequest, ServiceResponse.CreateFailed(ex.Message));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception: {ex}");
                return StatusCode((int)HttpStatusCode.OK, ServiceResponse.CreateFailed("Internal Server Error"));
            }
        }
    }
}
