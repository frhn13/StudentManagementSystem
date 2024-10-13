using StudentManagementSystem.Models;
using System.Linq;

namespace StudentManagementSystem.Clients
{
    public class StudentManagement(HttpClient httpClient)
    {

        public async Task AddNewStudentAsync(StudentDetails studentDetails) => await httpClient.PostAsJsonAsync("students", studentDetails);
        public async Task EditStudentAsync(StudentDetails student) => await httpClient.PutAsJsonAsync($"students/{student.Id}", student);
        public async Task<StudentDetails> GetStudentAsync(int id) => await httpClient.GetFromJsonAsync<StudentDetails>($"students/{id}") 
            ?? throw new Exception("Could not find student!");
        public async Task<StudentDetails[]> ViewAllStudentsAsync() =>
            await httpClient.GetFromJsonAsync<StudentDetails[]>("students") ?? [];
        public async Task DeleteStudentAsync(int id) => await httpClient.DeleteAsync($"students/{id}");
        public async Task StoreAllStudentsAsync()
        {
            StudentDetails[] allStudentDetails = await ViewAllStudentsAsync();
            try
            {
                using (StreamWriter file = new StreamWriter(@"students.csv", false))
                {
                    foreach (StudentDetails studentDetails in allStudentDetails)
                    {
                        file.WriteLine($"{studentDetails.Id},{studentDetails.Name},{studentDetails.Age},{studentDetails.Year},{studentDetails.MobileNumber},{studentDetails.DOB}");
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
