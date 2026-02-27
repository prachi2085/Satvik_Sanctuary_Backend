using HealthWellness.Data;
using HealthWellness.Interfaces;
using HealthWellness.Models;

namespace HealthWellness.Services
{
    public class AIService : IAIService
    {
        private readonly WellnessDbContext _db;

        public AIService(WellnessDbContext db)
        {
            _db = db;
        }

        public string GetResponse(string prompt, int userId)
        {
            string response =
                $"Based on your concern: \"{prompt}\", here are some suggestions: Yoga, Meditation, Healthy Diet.";

            var chat = new ChatMessage
            {
                UserId = userId,
                Prompt = prompt,
                Response = response,
                Timestamp = DateTime.UtcNow
            };

            _db.ChatMessages.Add(chat);
            _db.SaveChanges();

            return response;
        }

        public IEnumerable<ChatMessage> GetHistory(int userId)
            => _db.ChatMessages
                  .Where(c => c.UserId == userId)
                  .OrderByDescending(c => c.Timestamp)
                  .ToList();
    }
}
