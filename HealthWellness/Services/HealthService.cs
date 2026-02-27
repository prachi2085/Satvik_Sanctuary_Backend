using HealthWellness.Data;
using HealthWellness.DTOs;
using HealthWellness.Interfaces;
using HealthWellness.Models;
using Microsoft.EntityFrameworkCore;

namespace HealthWellness.Services
{
    public class HealthService : IHealthService
    {
        private readonly WellnessDbContext _db;

        public HealthService(WellnessDbContext db)
        {
            _db = db;
        }

        public HealthForm Submit(HealthFormDto dto, int? userId)
        {
            var form = new HealthForm
            {
                Name = dto.Name,
                UserId = userId,
                Age = dto.Age,
                Email = dto.Email,
                ContactNumber = dto.ContactNumber,
                Symptoms = dto.Symptoms,
                Message = dto.Message,
                SubmittedAt = DateTime.UtcNow
            };

            _db.HealthForms.Add(form);
            _db.SaveChanges();

            return form;
        }

        public IEnumerable<HealthForm> GetByUser(int userId)
            => _db.HealthForms
                  .Where(f => f.UserId == userId)
                  .OrderByDescending(f => f.SubmittedAt)
                  .ToList();

        public IEnumerable<HealthForm> GetAll()
            => _db.HealthForms
                  .Include(f => f.User)
                  .OrderByDescending(f => f.SubmittedAt)
                  .ToList();
    }
}
