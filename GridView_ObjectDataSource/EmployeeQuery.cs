using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GridView_ObjectDataSource
{
    public class EmployeeQuery
    {
        public int EmployeeId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }

        public decimal Salary { get; set; }

        public string Gender { get; set; }

        public string DepartmentName { get; set; }

        public string Country { get; set; }
    }
}