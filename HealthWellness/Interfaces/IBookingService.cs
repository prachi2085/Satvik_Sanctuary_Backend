using HealthWellness.DTOs;
using HealthWellness.Models;
using HealthWellness.Interfaces;

namespace HealthWellness.Interfaces
{
    public interface IBookingService
    {
        Booking Book(BookingDto dto, int userId);
        IEnumerable<Booking> GetUserBookings(int userId);
        IEnumerable<Booking> GetAll();
        IEnumerable<DateTime> GetAvailableSlots();
    }
}
