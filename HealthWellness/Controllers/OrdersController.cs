using HealthWellness.DTOs;
using HealthWellness.Helpers;
using HealthWellness.Interfaces;
using HealthWellness.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HealthWellness.Controllers
{
  [Route("api/orders")]
  [ApiController]
  public class OrdersController : ControllerBase
  {
    private readonly IOrderService _service;

    public OrdersController(IOrderService service)
    {
      _service = service;
    }

    // POST api/orders
    // Called by Angular after Razorpay payment succeeds — no login required
    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> PlaceOrder([FromBody] PlaceOrderDto dto)
    {
      if (!ModelState.IsValid)
        return BadRequest(ApiResponse<string>.Fail("Invalid order data"));

      var order = await _service.PlaceOrderAsync(dto);
      return Ok(ApiResponse<Order>.Ok(order, "Order placed! Confirmation email sent."));
    }

    // GET api/orders  (admin only — you can view all orders here)
    [HttpGet]
    [Authorize(Roles = "Admin")]
    public IActionResult GetAll()
    {
      return Ok(ApiResponse<IEnumerable<Order>>.Ok(_service.GetAll()));
    }
  }
}
