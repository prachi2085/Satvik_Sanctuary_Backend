using HealthWellness.Data;
using HealthWellness.DTOs;
using HealthWellness.Interfaces;
using HealthWellness.Models;
using Microsoft.EntityFrameworkCore;

namespace HealthWellness.Services
{
    public class BookingService : IBookingService
    {
        private readonly WellnessDbContext _db;

        public BookingService(WellnessDbContext db)
        {
            _db = db;
        }

        public Booking Book(BookingDto dto, int userId)
        {
            var booking = new Booking
            {
                UserId = userId,
                AppointmentDate = dto.AppointmentDate,
                PreferredTime = dto.PreferredTime,
                Status = "Pending",
                CreatedAt = DateTime.UtcNow
            };

            _db.Bookings.Add(booking);
            _db.SaveChanges();

            return booking;
        }

        public IEnumerable<Booking> GetUserBookings(int userId)
            => _db.Bookings
                  .Where(b => b.UserId == userId)
                  .OrderByDescending(b => b.CreatedAt)
                  .ToList();

        public IEnumerable<Booking> GetAll()
            => _db.Bookings
                  .Include(b => b.User)
                  .OrderByDescending(b => b.CreatedAt)
                  .ToList();

        public IEnumerable<DateTime> GetAvailableSlots()
        {
            return Enumerable.Range(1, 7)
                .Select(i => DateTime.UtcNow.Date.AddDays(i).AddHours(10));
        }
    }
}
