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
    }
}
