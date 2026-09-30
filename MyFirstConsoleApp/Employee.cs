using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFirstConsoleApp
{
    public class Employee
    {
        public int id;
        public string name;
        public float baseSalary;

       
        public Employee(int id, string n, float b)
        {
            this.id = id;   
            this.name = n;
            this.baseSalary = b;
        }
        public virtual float GetSalary()
        {
            return baseSalary;
        }
        public  void display()
        {
            Console.WriteLine($"ID : {id}");
            Console.WriteLine($"Name : {name}");
            Console.WriteLine($"BaseSalary : {baseSalary}");
            Console.WriteLine("Total Salary : " + GetSalary());
        }
        
    }
    public class FullTime : Employee
    {
        public float hr { get; set; }

        public FullTime(int id, string nam, float b, float hr) : base( id,  nam,  b)
        {
            this.hr = hr;
        }

        public override float GetSalary()
        {
            return hr + baseSalary;
        }
    }

    public class ContarctEmployee : Employee
    {
        public float bonus { get; set; }

        public ContarctEmployee(int id, string nam, float b, float bonus) : base(id, nam, b)
        {
            this.bonus = bonus;
        }

        public override float GetSalary()
        {
            return bonus + baseSalary;
        }
    }

    public class Freelancer : Employee
    {
        public float rate { get; set; }
            public int workHr { get; set; }

        public Freelancer(int id, string nam, float b, float rate, int workHr) : base(id, nam, b)
        {
            this.rate = rate;
            this.workHr = workHr;
        }

        public override float GetSalary()
        {
            return rate*workHr + baseSalary;
        }
    }
}
