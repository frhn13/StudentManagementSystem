using StudentManagementSystem.CustomValidators;
using System.ComponentModel.DataAnnotations;

namespace StudentManagementSystem.Models
{
    public class LoginDetails
    {
        public string? Name { get; set; }
        [Required]
        [StringLength(50)]
        [LoginUsernameValidator]
        public required string Username { get; set; }
        [Required]
        [StringLength(50)]
        public required string Password { get; set; }
        public string? Role { get; set; }
    }
}
