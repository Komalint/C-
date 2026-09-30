using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public partial class StudentHelper
    {

       public string RollNo  { get; set;}
        public string Name { get; set;}
        
        public string Email {  get; set;}

        public int Semester { get; set;}

        public double Cgpa { get; set;}


        public static List<StudentHelper> listItems = new List<StudentHelper>();
        public StudentHelper(string rollno, string name, string emial, int semester, double cgpa)
        {

            RollNo = rollno;
            Name = name;
            Email = emial;
            Semester = semester;
            Cgpa = cgpa;
        }

    }
}
