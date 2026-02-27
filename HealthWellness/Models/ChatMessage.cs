namespace HealthWellness.Models
{
    public class ChatMessage
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public string Prompt { get; set; }

        public string Response { get; set; }

        public DateTime Timestamp { get; set; } = DateTime.UtcNow;

        public User User { get; set; }
    }


}
