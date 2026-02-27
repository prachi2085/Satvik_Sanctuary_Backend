using HealthWellness.Data;
using HealthWellness.Interfaces;
using HealthWellness.Models;

namespace HealthWellness.Services
{
    public class UserService : IUserService
    {
        private readonly WellnessDbContext _db;

        public UserService(WellnessDbContext db)
        {
            _db = db;
        }

        public IEnumerable<User> GetAll()
            => _db.Users.OrderByDescending(u => u.CreatedAt).ToList();

        public User GetById(int id)
            => _db.Users.Find(id);

        public bool Delete(int id)
        {
            var user = _db.Users.Find(id);
            if (user == null) return false;

            _db.Users.Remove(user);
            _db.SaveChanges();
            return true;
        }
    }
}
