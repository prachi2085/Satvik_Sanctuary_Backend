using HealthWellness.Data;
using HealthWellness.Helpers;
using HealthWellness.Models;
using Microsoft.AspNetCore.Mvc;

namespace HealthWellness.Controllers
{
  [Route("api/sessionregistrations")]
  [ApiController]
  public class SessionRegistrationController : ControllerBase
  {
    private readonly WellnessDbContext _db;

    public SessionRegistrationController(WellnessDbContext db)
    {
      _db = db;
    }

    // Called by the announcement form
    [HttpPost]
    public async Task<IActionResult> Register([FromBody] SessionRegistration reg)
    {
      reg.CreatedAt = DateTime.UtcNow;
      _db.SessionRegistrations.Add(reg);
      await _db.SaveChangesAsync();
      return Ok(ApiResponse<SessionRegistration>.Ok(reg, "Registered!"));
    }

    // Called by your admin dashboard
    [HttpGet("all")]
    public IActionResult GetAll()
    {
      var list = _db.SessionRegistrations
                    .OrderByDescending(r => r.CreatedAt)
                    .ToList();
      return Ok(ApiResponse<IEnumerable<SessionRegistration>>.Ok(list));
    }
  }
}
