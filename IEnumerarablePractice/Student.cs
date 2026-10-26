using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IEnumerarablePractice
{
    internal class Student
    {
        public int StudentID { get; set; }
        public string Name { get; set; }
        public string Stream { get; set; }
        public int TotalMarks { get; set; }
        public List<string> courseE {  get; set; }
        public List<string> Prer { get; set; }


        public static List<Student> GetAllStudents()
        {
            List<Student> listStudents = new List<Student>{
                new Student
                {
                    StudentID = 101,
                    Name = "Tom",
                    Stream = "Computer Science",
                    TotalMarks = 800,
                    courseE = new List<string>
                    {
                        "Data Structures",
                        "Database Systems"
                    },
                    Prer = new List<string>
                    {
                        "Programming Fundamentals",
                        "Discrete Mathematics"
                    }
                },

                new Student
                {
                    StudentID = 102,
                    Name = "Mary",
                    Stream = "Computer Science",
                    TotalMarks = 900,
                    courseE = new List<string>
                    {
                        "Operating Systems",
                        "Database Systems",
                        "Computer Networks"
                    },
                    Prer = new List<string>
                    {
                        "Discrete Mathematics",
                        "Data Structures",
                        "Programming Fundamentals"
                    }
                },

                new Student
                {
                    StudentID = 103,
                    Name = "Valarie",
                    Stream = "Information Technology",
                    TotalMarks = 800,
                    courseE = new List<string>
                    {
                        "Web Development",
                        "Cloud Computing"
                    },
                    Prer = new List<string>
                    {
                        "Programming Fundamentals",
                        "Data Structures",
                        "Computer Organization"
                    }
                },

                new Student
                {
                    StudentID = 104,
                    Name = "John",
                    Stream = "Computer Science",
                    TotalMarks = 800,
                    courseE = new List<string>
                    {
                        "Artificial Intelligence",
                        "Machine Learning",
                        "Data Mining"
                    },
                    Prer = new List<string>
                    {
                        "Probability",
                        "Discrete Mathematics",
                        "Computer Organization"
                    }
                }
            };
            return listStudents;
        }

    }
}
