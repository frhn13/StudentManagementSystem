using StudentManagementSystem.Models;
using System.Linq;

namespace StudentManagementSystem.Clients
{
    public class DataManagement
    {
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

        }
        public static void editStudent(StudentDetails studentDetails)
        {

        }
        public static List<StudentDetails> ViewAllStudents() 
        {
            List<StudentDetails> studentDetailsList = [];
            try
            {
                string[] students = System.IO.File.ReadAllLines(@"students.csv");
                foreach (string student in students)
                {
                    string[] studentData = student.Split(',');
                    StudentDetails details = new StudentDetails
                    {
                        Id = int.Parse(studentData[0]),
                        Name = studentData[1],
                        Age = int.Parse(studentData[2]),
                        Year = int.Parse(studentData[3]),
                        MobileNumber = studentData[4],
                        DOB = DateOnly.Parse(studentData[5])
                    };
                    studentDetailsList.Add(details);
                }
            }
            catch
            {
                throw new ApplicationException("Oopsies!");
            }
            return studentDetailsList;
        }
    }
}
