


namespace HealthWellness.Interfaces
{
    using HealthWellness.Models;
    public interface IAIService
    {
        string GetResponse(string prompt, int userId);
        IEnumerable<ChatMessage> GetHistory(int userId);
    }
}
