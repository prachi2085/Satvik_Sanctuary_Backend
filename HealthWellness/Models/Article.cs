namespace HealthWellness.Models
{
    public class Article
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string MediumUrl { get; set; }

        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
    }


}
