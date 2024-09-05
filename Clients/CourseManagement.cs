using StudentManagementSystem.Models;

namespace StudentManagementSystem.Clients
{
    public class CourseManagement
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

        public static Course[] GetCourses() => courses;
        public static string[][] GetModules() => modules;

        public static void AddNewModule(CourseDetails courseDetails)
        {

        }
    }
}

