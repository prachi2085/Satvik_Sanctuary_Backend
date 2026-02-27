namespace HealthWellness.Models
{
    public class Booking
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public DateTime AppointmentDate { get; set; }

        public string PreferredTime { get; set; }

        public string Status { get; set; } = "Pending";
        // Pending, Approved, Rejected

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public User User { get; set; }
    }


}
