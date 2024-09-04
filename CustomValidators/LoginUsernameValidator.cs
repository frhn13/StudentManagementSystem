using StudentManagementSystem.Models;
using System.ComponentModel.DataAnnotations;

namespace StudentManagementSystem.CustomValidators
{
    public class LoginUsernameValidator : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            try
            {
                string[] accounts = System.IO.File.ReadAllLines(@"accounts.csv");
                foreach (string account in accounts) // Checks if account already exists with entered name or username
                {
                    string[] accountData = account.Split(',');
                    if (accountData[1].Equals(value))
                    {
                        return null;
                    }
                }
                return new ValidationResult("Account doesn't exist", new[] { validationContext.MemberName });
            }
            catch (FileNotFoundException ex)
            {
                return new ValidationResult("Account doesn't exist", new[] { validationContext.MemberName });
            }
        }
    }
}
