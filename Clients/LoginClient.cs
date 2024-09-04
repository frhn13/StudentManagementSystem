using StudentManagementSystem.Models;

namespace StudentManagementSystem.Clients
{
    public class LoginClient
    {
        private LoginDetails loggedInUser = new() { Username=string.Empty, Password=string.Empty};

        public LoginDetails GetLoggedInUser() => loggedInUser;

        public void ResetLogin()
        {
            loggedInUser.Name = null;
            loggedInUser.Username = string.Empty;
            loggedInUser.Password = string.Empty;
            loggedInUser.Role = null;
        }

        public static void CreateAccount(SignUpDetails signUpDetails)
        {
            try
            {
                using (StreamWriter file = new StreamWriter(@"accounts.csv", true))
                {
                    file.WriteLine($"{signUpDetails.Name},{signUpDetails.Username},{signUpDetails.Password},user");
                }
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
        public static void ViewAllAccounts()
        {

        }
    }
}
