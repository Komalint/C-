using System;

namespace JqueryPractice.Models
{
    public class Employee
    {
        public int EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DateOfBirth { get; set; }
        public int? Salary { get; set; }
        public string Gender { get; set; }
        public string DepartmentName { get; set; }
        public string Country { get; set; }
    }
}
