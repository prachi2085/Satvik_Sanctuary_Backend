using HealthWellness.Data;
using HealthWellness.DTOs;
using HealthWellness.Interfaces;
using HealthWellness.Models;

namespace HealthWellness.Services
{
    public class AnnouncementService : IAnnouncementService
    {
        private readonly WellnessDbContext _db;

        public AnnouncementService(WellnessDbContext db)
        {
            _db = db;
        }

        public IEnumerable<Announcement> GetAll()
            => _db.Announcements
                  .Where(a => a.IsActive)
                  .OrderByDescending(a => a.EventDate)
                  .ToList();

        public Announcement Create(AnnouncementDto dto)
        {
            var ann = new Announcement
            {
                Title = dto.Title,
                Description = dto.Description,
                EventDate = dto.EventDate,
                CreatedAt = DateTime.UtcNow,
                IsActive = true
            };

            _db.Announcements.Add(ann);
            _db.SaveChanges();
            return ann;
        }

        public Announcement? Update(int id, AnnouncementDto dto)
        {
            var ann = _db.Announcements.Find(id);
            if (ann == null) return null;

            ann.Title = dto.Title;
            ann.Description = dto.Description;
            ann.EventDate = dto.EventDate;

            _db.SaveChanges();
            return ann;
        }

        public bool Delete(int id)
        {
            var ann = _db.Announcements.Find(id);
            if (ann == null) return false;

            ann.IsActive = false;
            _db.SaveChanges();
            return true;
        }
    }
}
