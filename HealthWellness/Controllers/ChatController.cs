using HealthWellness.DTOs;
using HealthWellness.Helpers;
using HealthWellness.Interfaces;
using HealthWellness.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HealthWellness.Controllers
{
    [Route("api/chat")]
    [ApiController]
    [Authorize]
    public class ChatController : ControllerBase
    {
        private readonly IAIService _service;

        public ChatController(IAIService service)
        {
            _service = service;
        }

        [HttpPost("ask")]
        public IActionResult Ask(ChatDto dto)
        {
            int userId = int.Parse(User.FindFirst("id")!.Value);

            var response = _service.GetResponse(dto.Prompt, userId);

            return Ok(ApiResponse<string>.Ok(response, "AI response"));
        }

        [HttpGet("history")]
        public IActionResult History()
        {
            int userId = int.Parse(User.FindFirst("id")!.Value);

            return Ok(ApiResponse<IEnumerable<ChatMessage>>
                .Ok(_service.GetHistory(userId)));
        }
    }
}
