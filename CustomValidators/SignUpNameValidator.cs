using StudentManagementSystem.Clients;
using StudentManagementSystem.Models;
using System.ComponentModel.DataAnnotations;

namespace StudentManagementSystem.CustomValidators
{
    public class SignUpNameValidator : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object value, ValidationContext validationContext)
        {
            try
            {
                string[] accounts = System.IO.File.ReadAllLines(@"accounts.csv");
                List<StudentDetails> students = StudentManagement.ViewAllStudents();

                foreach (string account in accounts) // Checks if account already exists with entered name
                {
                    string[] accountData = account.Split(',');
                    if (accountData[0].Equals(value))
                    {
                        return new ValidationResult("Account for this student already exists", new[] {validationContext.MemberName});
                    }
                }
                foreach (StudentDetails student in students)
                {
                    if (student.Name.Equals(value))  // Checks if student name already exists in list of students
                    {
                        using (StreamWriter file = new StreamWriter(@"accounts.csv", true))
                        {
                            return null;
                        }
                    }
                }
                return new ValidationResult("This student isn't registered on the system", new[] { validationContext.MemberName });
            }
            catch (FileNotFoundException ex)
            {
                return new ValidationResult("This student isn't registered on the system", new[] { validationContext.MemberName });
            }
        }
    }
}
