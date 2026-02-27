using HealthWellness.DTOs;
using HealthWellness.Models;
using HealthWellness.Interfaces;

namespace HealthWellness.Interfaces
{
    public interface IHealthService
    {
        HealthForm Submit(HealthFormDto dto, int? userId);
        IEnumerable<HealthForm> GetByUser(int userId);
        IEnumerable<HealthForm> GetAll();
    }
}
