using StudentManagementSystem.Models;
using System.Linq;

namespace StudentManagementSystem.Clients
{
    public class StudentManagement(HttpClient httpClient)
    {

        public async Task AddNewStudentAsync(StudentDetails studentDetails) => await httpClient.PostAsJsonAsync("students", studentDetails);

        public async Task EditStudentAsync(StudentDetails student) => await httpClient.PutAsJsonAsync($"students/{student.Id}", student);

        public async Task<StudentDetails> GetStudentAsync(int id) => await httpClient.GetFromJsonAsync<StudentDetails>($"students/{id}") ?? throw new Exception("Could now find game!");

        public async Task<StudentDetails[]> ViewAllStudentsAsync() =>
            await httpClient.GetFromJsonAsync<StudentDetails[]>("students") ?? [];

        public async Task DeleteStudentAsync(int id) => await httpClient.DeleteAsync($"students/{id}");
        public static void AddNewStudent(StudentDetails studentDetails)
        {
            try
            {
                int lineCount = File.ReadLines("students.csv").Count();
                using (StreamWriter file = new StreamWriter(@"students.csv", true))
                {
                    file.WriteLine($"{lineCount+1},{studentDetails.Name},{studentDetails.Age},{studentDetails.Year},{studentDetails.MobileNumber},{studentDetails.DOB}");
                }
            }
            catch (FileNotFoundException ex)
            {
                throw new ApplicationException("Oopsies!");
            }
        }

        public static void DeleteStudent(StudentDetails studentDetails)
        {
            string removedStudent = "";
            try
            {
                string[] students = System.IO.File.ReadAllLines(@"students.csv");
                foreach (string student in students)
                {
                    string[] studentData = student.Split(',');
                    if (int.Parse(studentData[0]) == studentDetails.Id)
                    {
                        LoginClient.DeleteAccount(studentDetails);
                        List<CourseDetails> courseDetails = CourseManagement.ViewModules(studentDetails.Name);
                        foreach(CourseDetails module in courseDetails)
                        {
                            CourseManagement.DeleteModule(module);
                        }
                        removedStudent = student;
                        break;
                    }
                }
                students = students.Where(x => x != removedStudent).ToArray();
                using (StreamWriter file = new StreamWriter(@"students.csv", false))
                {
                    for (int i=0; i < students.Length; i++) 
                    {
                        string[] studentData = students[i].Split(',');
                        file.WriteLine($"{i+1},{studentData[1]},{studentData[2]},{studentData[3]},{studentData[4]},{studentData[5]}");
                    }
                }
            }
            catch (FileNotFoundException ex)
            {
                throw new ApplicationException("Oopsies!");
            }
        }

        public static void EditStudent(StudentDetails studentDetails)
        {
            try
            {
                string[] students = System.IO.File.ReadAllLines(@"students.csv");
                string[] editedStudent = students[studentDetails.Id].Split(",");
                students[studentDetails.Id-1] = $"{studentDetails.Id},{studentDetails.Name},{studentDetails.Age},{studentDetails.Year},{studentDetails.MobileNumber},{studentDetails.DOB}";
                using (StreamWriter file = new StreamWriter(@"students.csv", false))
                {
                    foreach (string student in students)
                    {
                        string[] studentData = student.Split(',');
                        file.WriteLine($"{studentData[0]},{studentData[1]},{studentData[2]},{studentData[3]},{studentData[4]},{studentData[5]}");
                    }
                }
            }
            catch (FileNotFoundException ex)
            {
                throw new ApplicationException("Oopsies!");
            }
        }

        public static StudentDetails GetStudent(int Id)
        {
            try 
            { 
                string[] students = System.IO.File.ReadAllLines(@"students.csv");
                foreach (string student in students)
                {
                    string[] studentData = student.Split(',');
                    if (int.Parse(studentData[0]) == Id)
                    {
                        StudentDetails details = new StudentDetails
                        {
                            Id = int.Parse(studentData[0]),
                            Name = studentData[1],
                            Age = int.Parse(studentData[2]),
                            Year = int.Parse(studentData[3]),
                            MobileNumber = studentData[4],
                            DOB = DateOnly.Parse(studentData[5])
                        };
                        return details;
                    }
                }
            }
            catch (FileNotFoundException ex)
            {
                throw new ApplicationException("Oopsies!");
            }
            return null;
        }
    }
}
