using StudentManagementSystem.Models;
using System.Reflection;

namespace StudentManagementSystem.Clients
{
    public class LoginClient(HttpClient httpClient)
    {
        private LoginDetails loggedInUser = new() { Id=0, Username=string.Empty, Password=string.Empty};

        public LoginDetails GetLoggedInUser() => loggedInUser;

        public void ResetLogin()
        {
            loggedInUser.Id = 0;
            loggedInUser.Name = null;
            loggedInUser.Username = string.Empty;
            loggedInUser.Password = string.Empty;
            loggedInUser.Role = null;
        }

        public async Task CreateAccountAsync(SignUpDetails signUpDetails) =>
            await httpClient.PostAsJsonAsync("accounts", signUpDetails);

        public async Task<LoginDetails> LoginUserAsync(string username) =>
            await httpClient.GetFromJsonAsync<LoginDetails>($"accounts/userAccount/{username}") ?? null!;

        public async Task DeleteAccountAsync(string name) => await httpClient.DeleteAsync($"accounts/userAccount/{name}");

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
            if (loginDetails is not null)
            {
                loggedInUser.Id = loginDetails.Id;
                loggedInUser.Name = loginDetails.Name;
                loggedInUser.Username = loginDetails.Username;
                loggedInUser.Password = loginDetails.Password;
                loggedInUser.Role = loginDetails.Role;
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool StoreLoggedInDetails(LoginDetails loginDetails)
        {
            try
            {
                using (StreamWriter file = new StreamWriter(@"loggedInDetails.csv", false))
                {
                    file.WriteLine($"{loginDetails.Id},{loginDetails.Name},{loginDetails.Username},{loginDetails.Password},user");
                    return true;
                }
            }
            catch (FileNotFoundException ex)
            {
                return false;
            }
        }

        public LoginDetails GetLoggedInDetails()
        {
            try
            {
                string[] userDetails = File.ReadAllLines(@"loggedInDetails.csv");
                if (userDetails.Length == 0)
                {
                    return new()
                    {
                        Id = 0,
                        Name = null,
                        Username = string.Empty,
                        Password = string.Empty,
                        Role = null
                    };
                }
                else
                {
                    foreach (string user in userDetails)
                    {
                        string[] userData = user.Split(',');

                        return new()
                        {
                            Id = int.Parse(userData[0]),
                            Name = userData[1],
                            Username = userData[2],
                            Password = userData[3],
                            Role = userData[4]
                        };
                    }
                }
            }
            catch(FileNotFoundException ex)
            {
                return new()
                {
                    Id = 0,
                    Name = null,
                    Username = string.Empty,
                    Password = string.Empty,
                    Role = null
                };
            }
            return new()
            {
                Id = 0,
                Name = null,
                Username = string.Empty,
                Password = string.Empty,
                Role = null
            };
        }

        public static void DeleteAccount(StudentDetails studentDetails)
        {
            string removedAccount = string.Empty;
            try
            {
                string[] accounts = System.IO.File.ReadAllLines(@"accounts.csv");
                foreach (string account in accounts)
                {
                    string[] accountData = account.Split(',');
                    if (studentDetails.Name.Equals(accountData[0]))
                    {
                        removedAccount = account;
                        break;
                    }
                }
                accounts = accounts.Where(x => x != removedAccount).ToArray();
                using (StreamWriter file = new StreamWriter(@"accounts.csv", false))
                {
                    for (int i = 0; i < accounts.Length; i++)
                    {
                        string[] accountData = accounts[i].Split(',');
                        file.WriteLine($"{accountData[0]},{accountData[1]},{accountData[2]},{accountData[3]}");
                    }
                }
            }
            catch (FileNotFoundException ex)
            {
                throw new ApplicationException("Oopsies!");
            }
        }
    }
}
