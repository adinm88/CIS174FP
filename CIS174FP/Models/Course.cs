using System.ComponentModel.DataAnnotations;

namespace CIS174FP.Models
{
    public class Course
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Course name is required")]
        [StringLength(100)]
        public string? Name { get; set; }

        // links course to logged-in user (Identity)
        public string? UserId { get; set; }

        // relationship to Assignment model (one-to-many)
        public List<Assignment>? Assignments { get; set; }
    }
}
