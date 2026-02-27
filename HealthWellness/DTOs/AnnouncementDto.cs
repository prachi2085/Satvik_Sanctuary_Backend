namespace HealthWellness.DTOs
{
    public class AnnouncementDto
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime EventDate { get; set; }
    }
}
