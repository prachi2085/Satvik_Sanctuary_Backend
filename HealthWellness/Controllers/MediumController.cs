using HealthWellness.Services;
using Microsoft.AspNetCore.Mvc;

namespace HealthWellness.Controllers
{
    [ApiController]
    [Route("api/medium")]
    public class MediumController : ControllerBase
    {
        private readonly MediumService _mediumService;

        public MediumController(MediumService mediumService)
        {
            _mediumService = mediumService;
        }

        [HttpGet]
        public async Task<IActionResult> GetMediumArticles()
        {
            var result = await _mediumService.GetArticles();

            return Ok(result);   // ✅ Correct
        }
    }
}