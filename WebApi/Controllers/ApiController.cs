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
        public IActionResult Enrolment([FromQuery] EnrolmentRequest enrolmentRequest)
        {
            if (ModelState.IsValid)
            {
                if (enrolmentRequest.Surname.Equals("Smith", StringComparison.CurrentCultureIgnoreCase))
                {
                    var result = new EnrolmentResult
                    {
                        EnrolledAddress = "EASTERN VALLEY WAY, WILLOUGHBY EAST NSW 2068",
                        LocalGovernmentArea = "Willoughby City Council",
                        LocalGovernmentAreaWard = "Middle Harbour Ward",
                        StateElectoralDistrict = "Willoughby",
                        ValidAsAt = new DateTime(2018, 6, 6)
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