using System.ComponentModel.DataAnnotations;

namespace StudentManagementSystem.Models
{
    public class SignUpDetails
    {
        [Required]
        [StringLength(50)]
        public required string Name { get; set; }
        [Required]
        [StringLength(50)]
        public required string Username { get; set; }
        [Required]
        [MaxLength(15, ErrorMessage = "Password can't be more than 15 characters")]
        [MinLength(6, ErrorMessage = "Password can't be less than 6 characters")]
        public required string Password { get; set; }
        [Required(ErrorMessage = "The Confirm Password field is required")]
        [Compare("Password", ErrorMessage = "Passwords must be the same")]
        public required string ConfirmPassword { get; set; }
        public int UserId { get; set; }
        public string? Role {  get; set; }
    }
}
