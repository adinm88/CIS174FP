using System.ComponentModel.DataAnnotations;

namespace CIS174FP.Models
{
    public class Assignment
    {
        public int Id { get; set; }

        [Required]
        public string? Title { get; set; }

        [Required]
        public DateTime DueDate { get; set; }

        // Related to the Course model (foreign key)
        [Required]
        public int CourseId { get; set; }

        public string? UserId { get; set; }

        // Relationship to Course model (many-to-one)
        public Course? Course { get; set; }
    }
}
