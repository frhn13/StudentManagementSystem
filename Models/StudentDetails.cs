using System.ComponentModel.DataAnnotations;

namespace StudentManagementSystem.Models
{
    public class StudentDetails
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public required string Name { get; set; }
        [Required]
        [Range(5, 18)]
        public int Age { get; set; }
        [Required]
        [Range(1, 13)]
        public int Year { get; set; }
        [Required]
        [StringLength(15)]
        public required string MobileNumber { get; set; }
        [Required]
        public DateOnly DOB { get; set; }
    }
}
