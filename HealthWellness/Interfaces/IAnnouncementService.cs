using HealthWellness.DTOs;
using HealthWellness.Models;

namespace HealthWellness.Interfaces
{
    
    public interface IAnnouncementService
    {
        IEnumerable<Announcement> GetAll();
        Announcement Create(AnnouncementDto dto);
        Announcement? Update(int id, AnnouncementDto dto);
        bool Delete(int id);
    }
}
