using System;
using System.Collections.Generic;
using System.Linq;

namespace IEnumerarablePractice
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // ============================================================
            // Q1 - Find Even Numbers
            // ============================================================

            Console.WriteLine("==================================================");
            Console.WriteLine("Q1 - EVEN NUMBERS");
            Console.WriteLine("==================================================");

            List<int> numbers = new List<int>
            {
                1,2,3,4,5,6,7,8,9,10,11,12
            };

            IEnumerable<int> evenNumberList =
                numbers.Where(x => x % 2 == 0);

            Console.WriteLine("\nMethod Syntax:");

            foreach (int number in evenNumberList)
            {
                Console.Write(number + " ");
            }

            Console.WriteLine();

            IEnumerable<int> evenNumbers =
                from num in numbers
                where num % 2 == 0
                select num;

            Console.WriteLine("\nQuery Syntax:");

            foreach (int number in evenNumbers)
            {
                Console.Write(number + " ");
            }

            Console.WriteLine();



            // ============================================================
            // Q2 - Sort Students By Name
            // ============================================================

            Console.WriteLine("\n\n==================================================");
            Console.WriteLine("Q2 - STUDENTS SORTED BY NAME");
            Console.WriteLine("==================================================");

            IEnumerable<Student> students =
                Student.GetAllStudents()
                       .OrderBy(s => s.Name);

            foreach (Student student in students)
            {
                Console.WriteLine(
                    $"ID : {student.StudentID} | Name : {student.Name}"
                );
            }



            // ============================================================
            // Q3 - Group Students By Stream
            // ============================================================

            Console.WriteLine("\n\n==================================================");
            Console.WriteLine("Q3 - STUDENT COUNT BY STREAM");
            Console.WriteLine("==================================================");

            var streamGroups =
                from st in Student.GetAllStudents()
                group st by st.Stream;

            foreach (var group in streamGroups)
            {
                Console.WriteLine(
                    $"{group.Key} Stream => {group.Count()} Students"
                );
            }



            // ============================================================
            // Q4 - Graph Nodes Having Degree > 3
            // ============================================================

            Console.WriteLine("\n\n==================================================");
            Console.WriteLine("Q4 - GRAPH NODES WITH DEGREE GREATER THAN 3");
            Console.WriteLine("==================================================");

            var nodeDegrees = Edge.GetGraphEdges()

                .SelectMany(edge => new[]
                {
                    edge.Source,
                    edge.Destination
                })

                .GroupBy(node => node)

                .Where(group => group.Count() > 3);

            foreach (var group in nodeDegrees)
            {
                Console.WriteLine(
                    $"Node : {group.Key} | Degree : {group.Count()}"
                );
            }



            // ============================================================
            // Q5 - Unique Prerequisite Courses
            // ============================================================

            Console.WriteLine("\n\n==================================================");
            Console.WriteLine("Q5 - UNIQUE PREREQUISITE COURSES");
            Console.WriteLine("==================================================");

            var prerequisiteCourses =
                Student.GetAllStudents()
                       .SelectMany(student => student.Prer)
                       .Distinct()
                       .OrderBy(x => x);

            foreach (var prerequisite in prerequisiteCourses)
            {
                Console.WriteLine(prerequisite);
            }



            // ============================================================
            // Q6 - Shared Prerequisites
            // ============================================================

            Console.WriteLine("\n\n==================================================");
            Console.WriteLine("Q6 - SHARED PREREQUISITES");
            Console.WriteLine("==================================================");

            var sharedPrerequisites =
                Student.GetAllStudents()
                       .SelectMany(
                           s => s.Prer,
                           (student, prereq) => new
                           {
                               student.Name,
                               Prerequisite = prereq
                           })
                       .GroupBy(x => x.Prerequisite)
                       .Where(g => g.Count() > 1);

            foreach (var group in sharedPrerequisites)
            {
                Console.WriteLine(
                    $"\nPrerequisite : {group.Key}"
                );

                Console.WriteLine(
                    $"Shared By {group.Count()} Students"
                );

                Console.WriteLine("----------------------------------");

                foreach (var item in group)
                {
                    Console.WriteLine(item.Name);
                }
            }

            Console.ReadLine();
        }
    }
}