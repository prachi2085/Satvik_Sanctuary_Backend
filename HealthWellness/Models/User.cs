namespace HealthWellness.Models
{
    public class User
    {
        public int Id { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public string PasswordHash { get; set; }

        public string Role { get; set; } = "User";

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<HealthForm> HealthForms { get; set; }

        public ICollection<Booking> Bookings { get; set; }

        public ICollection<ChatMessage> ChatMessages { get; set; }
    }


}
