using System;
using System.Linq;

namespace ConsoleApp1
{
    public partial class StudentHelper
    {
        public static void AddStudent()
        {
            Console.Write("Enter Student Roll No: ");
            string rollNo = Console.ReadLine();

            Console.Write("Enter Student Name: ");
            string name = Console.ReadLine();

            Console.Write("Enter Student Email: ");
            string email = Console.ReadLine();

            Console.Write("Enter Student Semester: ");
            int semester = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter Student CGPA: ");
            double cgpa = Convert.ToDouble(Console.ReadLine());

            listItems.Add(
                new StudentHelper(
                    rollNo,
                    name,
                    email,
                    semester,
                    cgpa));

            Console.WriteLine("Student Added Successfully.");
        }

        public static void RemoveStudent()
        {
            Console.Write("Enter Roll No: ");
            string rollNo = Console.ReadLine();

            var student =
                listItems.Find(x => x.RollNo == rollNo);

            if (student != null)
            {
                listItems.Remove(student);
                Console.WriteLine("Student Removed Successfully.");
            }
            else
            {
                Console.WriteLine("Student Not Found.");
            }
        }

        public static void SearchStudent()
        {
            Console.Write("Enter Roll No: ");
            string rollNo = Console.ReadLine();

            var student =
                listItems.Find(x => x.RollNo == rollNo);

            if (student != null)
            {
                Console.WriteLine("\nStudent Details");
                Console.WriteLine($"Roll No : {student.RollNo}");
                Console.WriteLine($"Name    : {student.Name}");
                Console.WriteLine($"Email   : {student.Email}");
                Console.WriteLine($"Semester: {student.Semester}");
                Console.WriteLine($"CGPA    : {student.Cgpa}");
            }
            else
            {
                Console.WriteLine("Student Not Found.");
            }
        }

        public static void ShowEligibleStudents()
        {
            var students =
                listItems
                .Where(x => x.Cgpa >= 8.0 &&
                            x.Semester >= 6);

            Console.WriteLine("\nEligible Students");

            foreach (var student in students)
            {
                Console.WriteLine(
                    $"{student.RollNo} | " +
                    $"{student.Name} | " +
                    $"{student.Cgpa}");
            }
        }

        public static void ShowStatistics()
        {
            Console.WriteLine($"\nTotal Students : {listItems.Count}");

            if (listItems.Count > 0)
            {
                double avgCgpa =
                    listItems.Average(x => x.Cgpa);

                Console.WriteLine(
                    $"Average CGPA : {avgCgpa:F2}");
            }
        }

        public static void DisplayAllStudents()
        {
            foreach (var student in listItems)
            {
                Console.WriteLine(
                    $"{student.RollNo} | " +
                    $"{student.Name} | " +
                    $"{student.Email} | " +
                    $"{student.Semester} | " +
                    $"{student.Cgpa}");
            }
        }
    }
}