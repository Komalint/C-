using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

namespace GridView_ObjectDataSource
{
    public class UserDataAccessLayer
    {
        public readonly static string _conStr = ConfigurationManager.ConnectionStrings["DbConn"].ConnectionString;


        // Get All users
        public static List<EmployeeQuery> GetAllUsers()
        {
            List<EmployeeQuery> employeeQueries = new List<EmployeeQuery>();
            using (SqlConnection con = new SqlConnection(_conStr))
            {
                using (SqlCommand cmd = new SqlCommand("udp_GetallUsers", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    con.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            EmployeeQuery emp = new EmployeeQuery();

                            emp.EmployeeId = Convert.ToInt32(dr["EmployeeId"]);
                            emp.FirstName = dr["FirstName"].ToString();
                            emp.LastName = dr["LastName"].ToString();
                            emp.DateOfBirth = Convert.ToDateTime(dr["DateOfBirth"]);
                            emp.Salary = Convert.ToInt32(dr["Salary"]);
                            emp.Gender = dr["Gender"].ToString();
                            emp.DepartmentName = dr["DepartmentName"].ToString();
                            emp.Country = dr["Country"].ToString();

                            employeeQueries.Add(emp);
                        }


                    }
                }
            }
            return employeeQueries;
        }


        //Update user details

        public static void UpdateEmployee(EmployeeQuery emp)
        {
            using (SqlConnection con = new SqlConnection(_conStr))
            {
                using (SqlCommand cmd = new SqlCommand("udp_UpdateEmployeeDetails", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@EmployeeId", emp.EmployeeId);
                    cmd.Parameters.AddWithValue("@FirstName", emp.FirstName);
                    cmd.Parameters.AddWithValue("@LastName", emp.LastName);
                    cmd.Parameters.AddWithValue("@DateOfBirth", emp.DateOfBirth);
                    cmd.Parameters.AddWithValue("@Salary", emp.Salary);
                    cmd.Parameters.AddWithValue("@Gender", emp.Gender);
                    cmd.Parameters.AddWithValue("@DepartmentName", emp.DepartmentName);
                    cmd.Parameters.AddWithValue("@Country", emp.Country);

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        //insert employees

        public static void InsertEmployee(EmployeeQuery emp)
        {
            using (SqlConnection con = new SqlConnection(_conStr))
            {
                using (SqlCommand cmd = new SqlCommand("udp_InsertEmployeeDetails", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    
                    cmd.Parameters.AddWithValue("@FirstName", emp.FirstName);
                    cmd.Parameters.AddWithValue("@LastName", emp.LastName);
                    cmd.Parameters.AddWithValue("@DateOfBirth", emp.DateOfBirth);
                    cmd.Parameters.AddWithValue("@Salary", emp.Salary);
                    cmd.Parameters.AddWithValue("@Gender", emp.Gender);
                    cmd.Parameters.AddWithValue("@DepartmentName", emp.DepartmentName);
                    cmd.Parameters.AddWithValue("@Country", emp.Country);

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }


        //Delete Employee
        public static void DeleteUserById(EmployeeQuery emp)
        {
            using (SqlConnection con = new SqlConnection(_conStr))
            {
                using (SqlCommand cmd = new SqlCommand("udp_DeleteUserById", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@id", emp.EmployeeId);

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }


        public static void DeleteUserByIdn(int id)
        {
            using (SqlConnection con = new SqlConnection(_conStr))
            {
                using (SqlCommand cmd = new SqlCommand("udp_DeleteUserById", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@id", id);

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

    }
}