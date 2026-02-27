using System.ComponentModel.DataAnnotations;

namespace HealthWellness.DTOs
{
    public class HealthFormDto
    {
        [Required]
        [MinLength(3)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [Range(1, 120)]
        public int Age { get; set; }
        public int? UserId { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [RegularExpression(@"^[0-9]{10}$",
            ErrorMessage = "Phone number must be 10 digits.")]
        public string ContactNumber { get; set; } = string.Empty;

        [Required]
        [MinLength(5)]
        public string Symptoms { get; set; } = string.Empty;

        [Required]
        [MinLength(5)]
        public string Message { get; set; } = string.Empty;
    }
}
