using HealthWellness.DTOs;
using HealthWellness.Models;

namespace HealthWellness.Interfaces
{
  public interface IOrderService
  {
    Task<Order> PlaceOrderAsync(PlaceOrderDto dto);
    IEnumerable<Order> GetAll();
  }
}
