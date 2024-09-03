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
        public static void EditStudent(StudentDetails studentDetails)
        {
            try
            {
                string[] students = System.IO.File.ReadAllLines(@"students.csv");
                string[] editedStudent = students[studentDetails.Id].Split(",");
                editedStudent[0] = studentDetails.Id.ToString();
                editedStudent[1] = studentDetails.Name;
                editedStudent[2] = studentDetails.Age.ToString();
                editedStudent[3] = studentDetails.Year.ToString();
                editedStudent[4] = studentDetails.MobileNumber;
                editedStudent[5] = studentDetails.DOB.ToString();
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
            catch (FileNotFoundException ex)
            {
                throw new ApplicationException("Oopsies!");
            }
            return studentDetailsList;
        }
    }
}
