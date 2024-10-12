using StudentManagementSystem.Models;

namespace StudentManagementSystem.Clients
{
    public class CourseManagement(HttpClient httpClient)
    {
        private static readonly Course[] courses =
            [
                new() {
                    Id = 0,
                    Name = "Maths"
                },
                new() {
                    Id = 1,
                    Name = "English"
                },
                new() {
                    Id = 2,
                    Name = "Computing"
                },
                new() {
                    Id = 3,
                    Name = "Biology"
                },
                new() {
                    Id = 4,
                    Name = "Physics"
                },
                new() {
                    Id = 5,
                    Name = "Chemistry"
                },
                new() {
                    Id = 6,
                    Name = "History"
                },
                new() {
                    Id = 7,
                    Name = "Geography"
                }
            ];

        private static string[][] modules =
            [
            ["Calculus", "Algebra", "Probability and Statistics", "Mathematical Physics", "Geometry"],
            ["Renaissance Literature", "Shakespeare", "Romantic Poetry", "Modernist Fiction", "Creative Writing"],
            ["Data Management", "Human Computer Interaction", "Python Programming Portfolios", "Algorithm Analysis", "Security in Programming"],
            ["Biochemistry", "Genetics", "Plant Science", "Evolution and Biodiversity", "Environmental Issues"],
            ["Plasma and Fluids", "Quantum Physics", "Newtonian Principles", "Special and General Relativity", "Mathematical Techniques"],
            ["States of Matter", "Organic and Biological Chemistry", "Molecular Pharmacology", "Chemistry of Materials", "Properties of Molecules"],
            ["Rise and Fall of the British Empire", "The Renaissance", "Classical Civilisation", "Meiji Japan", "World War I and II"],
            ["Physical Environments", "Geopolitical Challenges", "Urban Development", "Global Climate Systems", "Environment and Society"]
            ];

        public static Course[] GetCourseChoices() => courses;
        public static string[][] GetModuleChoices() => modules;

        public async Task AddNewModuleAsync(CourseDetails courseDetails) => await httpClient.PostAsJsonAsync("courses", courseDetails);
        public async Task<CourseDetails[]> ViewStudentModulesAsync(string studentName) => await httpClient.GetFromJsonAsync<CourseDetails[]>($"courses/userCourses/{studentName}") ?? [];
        public async Task<CourseDetails> ViewModuleAsync(int id) => await httpClient.GetFromJsonAsync<CourseDetails>($"courses/{id}") ?? throw new Exception("Could not find course!");
        public async Task DeleteModuleAsync(int id) => await httpClient.DeleteAsync($"courses/{id}");
        public async Task DeleteStudentModulesAsync(string name) => await httpClient.DeleteAsync($"courses/userCourses/{name}");

        public async Task<bool> CheckNewModule(CourseDetails courseDetails)
        {
            int semesterOneModules = 0;
            int semesterTwoModules = 0;
            int semesterThreeModules = 0;
            CourseDetails[] previousCourses = await ViewStudentModulesAsync(courseDetails.Name!);
            foreach (CourseDetails module in previousCourses)
            {
                if (courseDetails.Name!.Equals(module.Name) && courseDetails.Module.Equals(module.Module)) { return false; }
                if (module.Semester == 1) { semesterOneModules++; }
                else if (module.Semester == 2) { semesterTwoModules++; }
                else { semesterThreeModules++; }
            }
            if (courseDetails.Semester == 1 && semesterOneModules >= 5 || courseDetails.Semester == 2 && semesterTwoModules >= 5 || courseDetails.Semester == 3 && semesterThreeModules >= 5)
                return false;
            return true;
        }
    }
}

