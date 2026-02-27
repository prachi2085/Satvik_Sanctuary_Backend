using HealthWellness.Models;

namespace HealthWellness.Interfaces
{
    public interface IUserService
    {
        IEnumerable<User> GetAll();
        User? GetById(int id);
        bool Delete(int id);
    }
}
