using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Route("api/v1")]
    [Authorize]
    [ApiController]
    public class ApiController : ControllerBase
    {
        private readonly ILogger _logger;

        public ApiController(ILogger<ApiController> logger)
        {
            _logger = logger;
        }

        [HttpGet("enrolment")]
        [ProducesResponseType(typeof(EnrolmentResult), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public IActionResult Enrolment([FromQuery] EnrolmentRequest enrolmentRequest)
        {
            if (ModelState.IsValid)
            {
                if (enrolmentRequest.Surname.Equals("Smith", StringComparison.CurrentCultureIgnoreCase))
                {
                    var result = new EnrolmentResult
                    {
                        StreetName = "EASTERN VALLEY WAY, WILLOUGHBY EAST NSW 2068",
                        ValidAsAt = DateTime.Now
                    };

                    return Ok(result);
                }
                else
                {
                    return NotFound();
                }
            }

            _logger.LogError("Bad request");
            return BadRequest(ModelState);
        }
    }
}