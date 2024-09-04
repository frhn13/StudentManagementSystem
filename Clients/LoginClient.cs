using StudentManagementSystem.Models;

namespace StudentManagementSystem.Clients
{
    public class LoginClient
    {
        public LoginDetails loggedInUser = new() { Username=String.Empty, Password=String.Empty};

        public LoginDetails GetLoggedInUser() => loggedInUser;

        public static bool CreateAccount(SignUpDetails signUpDetails)
        {
            try
            {
                string[] accounts = System.IO.File.ReadAllLines(@"accounts.csv");
                List<StudentDetails> students = DataManagement.ViewAllStudents();
                foreach (StudentDetails student in students)
                {
                    foreach (string account in accounts) // Checks if account already exists with entered name or username
                    {
                        string[] accountData = account.Split(',');
                        if (accountData[0].Equals(signUpDetails.Name) || accountData[1].Equals(signUpDetails.Username))
                        {
                            return false;
                        }
                    }
                    if (student.Name.Equals(signUpDetails.Name))  // Checks if student name already exists in list of students
                    {
                        using (StreamWriter file = new StreamWriter(@"accounts.csv", true))
                        {
                            file.WriteLine($"{signUpDetails.Name},{signUpDetails.Username},{signUpDetails.Password},user");
                            return true;
                        }
                    }
                }
                return false;
            }
            catch (FileNotFoundException ex)
            {
                throw new ApplicationException("Oopsies!");
            }
        }

        public bool LoginUser(LoginDetails loginDetails)
        {
            try
            {
                string[] accounts = System.IO.File.ReadAllLines(@"accounts.csv");
                foreach (string account in accounts) // Checks if account already exists with entered name or username
                {
                    string[] accountData = account.Split(',');
                    if (accountData[1].Equals(loginDetails.Username) || accountData[2].Equals(loginDetails.Password))
                    {
                        loggedInUser.Name = accountData[0];
                        loggedInUser.Username = accountData[1];
                        loggedInUser.Password = accountData[2];
                        loggedInUser.Role = accountData[3];
                        return true;
                    }
                }
                return false;
            }
            catch (FileNotFoundException ex)
            {
                throw new ApplicationException("Oopsies!");
            }
        }

        public bool LogoutUser(LoginDetails loginDetails)
        {
            return false;
        }

        public static void ViewAllAccounts()
        {

        }
    }
}
