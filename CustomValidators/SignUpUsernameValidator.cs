using StudentManagementSystem.Clients;
using StudentManagementSystem.Models;
using System.ComponentModel.DataAnnotations;

namespace StudentManagementSystem.CustomValidators
{
    public class SignUpUsernameValidator : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object value, ValidationContext validationContext)
        {
            try
            {
                string[] accounts = System.IO.File.ReadAllLines(@"accounts.csv");
                foreach (string account in accounts) // Checks if account already exists with entered username
                {
                    string[] accountData = account.Split(',');
                    if (accountData[2].Equals(value))
                    {
                        return new ValidationResult("Username has been taken", new[] {validationContext.MemberName});
                    }
                }
                return null;
            }
            catch (FileNotFoundException ex)
            {
                return null;
            }
        }
    }
}
