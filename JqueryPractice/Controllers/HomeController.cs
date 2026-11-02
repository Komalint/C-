using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.Mvc;
using JqueryPractice.Models;

namespace JqueryPractice.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public JsonResult AjaxMethod()
        {
            List<Employee> employees = new List<Employee>();
            string constr = ConfigurationManager.ConnectionStrings["DBConn"]?.ConnectionString
                ?? "Data Source=.;Initial Catalog=AssignmentDB;Persist Security Info=True;User ID=sa;Password=mcc#1234;TrustServerCertificate=True";

            using (SqlConnection con = new SqlConnection(constr))
            {
                string query = "SELECT EmployeeId, FirstName, LastName, DateOfBirth, Salary, Gender, DepartmentName, Country FROM tblEmployee";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            employees.Add(new Employee
                            {
                                EmployeeId = Convert.ToInt32(sdr["EmployeeId"]),
                                FirstName = sdr["FirstName"] != DBNull.Value ? sdr["FirstName"].ToString() : string.Empty,
                                LastName = sdr["LastName"] != DBNull.Value ? sdr["LastName"].ToString() : string.Empty,
                                DateOfBirth = sdr["DateOfBirth"] != DBNull.Value ? Convert.ToDateTime(sdr["DateOfBirth"]).ToString("yyyy-MM-dd") : string.Empty,
                                Salary = sdr["Salary"] != DBNull.Value ? Convert.ToInt32(sdr["Salary"]) : (int?)null,
                                Gender = sdr["Gender"] != DBNull.Value ? sdr["Gender"].ToString() : string.Empty,
                                DepartmentName = sdr["DepartmentName"] != DBNull.Value ? sdr["DepartmentName"].ToString() : string.Empty,
                                Country = sdr["Country"] != DBNull.Value ? sdr["Country"].ToString() : string.Empty
                            });
                        }
                    }
                    con.Close();
                }
            }

            return Json(employees, JsonRequestBehavior.AllowGet);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
