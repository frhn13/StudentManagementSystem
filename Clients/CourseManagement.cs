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

        public static Course[] GetCourseChoices() => courses;
        public static string[][] GetModuleChoices() => modules;

        public static bool AddNewModule(CourseDetails courseDetails)
        {
            try
            {
                List<CourseDetails> previousCourses = ViewModules(courseDetails.Name!);
                foreach (CourseDetails module in previousCourses)
                {
                    if (courseDetails.Name!.Equals(module.Name) && courseDetails.Module.Equals(module.Module))
                    {
                        return false;
                    }
                }
                int lineCount = File.ReadLines("modules.csv").Count();
                using (StreamWriter file = new StreamWriter(@"modules.csv", true))
                {
                    file.WriteLine($"{lineCount+1},{courseDetails.Name},{courseDetails.Course},{courseDetails.Module},{courseDetails.Semester}");
                }
                return true;
            }
            catch (FileNotFoundException ex)
            {
                throw new ApplicationException("Oopsies!");
            }
        }

        public static List<CourseDetails> ViewModules(string studentName)
        {
            List<CourseDetails> modulesList = [];
            try
            {
                string[] modules = File.ReadAllLines(@"modules.csv");
                foreach (string module in modules)
                {
                    string[] moduleData = module.Split(',');
                    
                    if (moduleData[1].Equals(studentName))
                    {
                        CourseDetails courseDetails = new()
                        {
                            Id = int.Parse(moduleData[0]),
                            Name = moduleData[1],
                            Course = moduleData[2],
                            Module = moduleData[3],
                            Semester = int.Parse(moduleData[4])
                        };
                        modulesList.Add(courseDetails);
                    }
                }
            }
            catch (FileNotFoundException ex)
            {
                throw new ApplicationException("Oopsies!");
            }
            return modulesList;
        }

        public static void DeleteModule(CourseDetails courseDetails)
        {
            string removedModule = "";
            try
            {
                string[] modules = File.ReadAllLines(@"modules.csv");
                foreach (string module in modules)
                {
                    string[] moduleData = module.Split(',');
                    if (int.Parse(moduleData[0]) == courseDetails.Id)
                    {
                        removedModule = module;
                        break;
                    }
                }
                modules = modules.Where(x => x != removedModule).ToArray();
                using (StreamWriter file = new StreamWriter(@"modules.csv", false))
                {
                    for (int i = 0; i < modules.Length; i++)
                    {
                        string[] moduleData = modules[i].Split(',');
                        file.WriteLine($"{moduleData[0]},{moduleData[1]},{moduleData[2]},{moduleData[3]},{moduleData[4]}");
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

