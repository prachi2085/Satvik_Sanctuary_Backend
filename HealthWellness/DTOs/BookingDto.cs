namespace HealthWellness.DTOs
{
    public class BookingDto
    {
        public DateTime AppointmentDate { get; set; }
        public string PreferredTime { get; set; } = string.Empty;
    }
}
