using HealthWellness.DTOs;
using HealthWellness.Helpers;
using HealthWellness.Interfaces;
using HealthWellness.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HealthWellness.Controllers
{
    [Route("api/healthform")]
    [ApiController]
   
    public class HealthFormController : ControllerBase
    {
        private readonly IHealthService _service;

        public HealthFormController(IHealthService service)
        {
            _service = service;
        }
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Running");
        }

        [HttpPost("submit")]
        public IActionResult Submit([FromBody] HealthFormDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            int? userId = null;

            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                var userIdClaim = User.FindFirst("id");

                if (userIdClaim != null)
                {
                    userId = int.Parse(userIdClaim.Value);
                }
            }

            var form = _service.Submit(dto, userId);

            return Ok(ApiResponse<HealthForm>.Ok(form, "Form submitted successfully"));
        }

        [HttpGet("mine")]
        public IActionResult MyForms()
        {
            int userId = int.Parse(User.FindFirst("id")!.Value);

            var forms = _service.GetByUser(userId);

            return Ok(ApiResponse<IEnumerable<HealthForm>>.Ok(forms));
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("all")]
        public IActionResult GetAll()
        {
            return Ok(ApiResponse<IEnumerable<HealthForm>>.Ok(_service.GetAll()));
        }
    }
}
