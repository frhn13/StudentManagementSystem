using System.ComponentModel.DataAnnotations;

namespace StudentManagementSystem.Models
{
    public class CourseDetails
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        [Required]
        public required string Course { get; set; }
        [Required]
        public required string Module { get; set; }
        [Required]
        [Range (1, 3, ErrorMessage = "Must choose between 1st, 2nd or 3rd semester")]
        public int Semester { get; set; }

    }
}
