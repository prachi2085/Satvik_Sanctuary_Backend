using HealthWellness.DTOs;
using HealthWellness.Helpers;
using HealthWellness.Interfaces;
using HealthWellness.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HealthWellness.Controllers
{
    [Route("api/bookings")]
    [ApiController]
    [Authorize]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService _service;

        public BookingController(IBookingService service)
        {
            _service = service;
        }

        [HttpGet("slots")]
        public IActionResult GetAvailableSlots()
        {
            return Ok(ApiResponse<IEnumerable<DateTime>>
                .Ok(_service.GetAvailableSlots()));
        }

        [HttpPost("book")]
        public IActionResult Book(BookingDto dto)
        {
            int userId = int.Parse(User.FindFirst("id")!.Value);

            var booking = _service.Book(dto, userId);

            return Ok(ApiResponse<Booking>
                .Ok(booking, "Booking created. Waiting for approval"));
        }

        [HttpGet("mine")]
        public IActionResult MyBookings()
        {
            int userId = int.Parse(User.FindFirst("id")!.Value);

            return Ok(ApiResponse<IEnumerable<Booking>>
                .Ok(_service.GetUserBookings(userId)));
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("all")]
        public IActionResult GetAllBookings()
        {
            return Ok(ApiResponse<IEnumerable<Booking>>
                .Ok(_service.GetAll()));
        }
    }
}
