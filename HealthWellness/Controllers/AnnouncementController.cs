using HealthWellness.DTOs;
using HealthWellness.Helpers;
using HealthWellness.Interfaces;
using HealthWellness.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HealthWellness.Controllers
{
    [Route("api/announcements")]
    [ApiController]
    public class AnnouncementController : ControllerBase
    {
        private readonly IAnnouncementService _service;

        public AnnouncementController(IAnnouncementService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(ApiResponse<IEnumerable<Announcement>>
                .Ok(_service.GetAll()));
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Create(AnnouncementDto dto)
        {
            return Ok(ApiResponse<Announcement>
                .Ok(_service.Create(dto), "Announcement created"));
        }
    }
}
