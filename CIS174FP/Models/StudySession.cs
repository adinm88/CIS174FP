using System.ComponentModel.DataAnnotations;

namespace CIS174FP.Models
{
    public class StudySession
    {
        public int Id { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public int DurationMinutes { get; set; }

        public string? Notes { get; set; }

        public string? UserId { get; set; }
    }
}
