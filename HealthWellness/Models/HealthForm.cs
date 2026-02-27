namespace HealthWellness.Models
{
    public class HealthForm
    {
        public int Id { get; set; }

        public int? UserId { get; set; }

        public int Age { get; set; }

        public string Email { get; set; }

        public string ContactNumber { get; set; }

        public string Symptoms { get; set; }

        public string Message { get; set; }

        public string Name { get; set; }
        public DateTime SubmittedAt { get; set; } = DateTime.UtcNow;

        public User User { get; set; }
    }


}
