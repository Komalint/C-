using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Xml;

namespace WebServicesPractice
{
    /// <summary>
    /// Summary description for WebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]

    public class WebService : System.Web.Services.WebService
    {
        string cnStr = ConfigurationManager.ConnectionStrings["DBConn"].ConnectionString;
        [WebMethod]
        public XmlElement GetUserDetails(string userName)
        {
            SqlConnection con = new SqlConnection(cnStr);
            con.Open();
            SqlCommand cmd = new SqlCommand("select * from Users where UserName like @userName+'%'", con);
            cmd.Parameters.AddWithValue("@userName", userName);
            cmd.ExecuteNonQuery();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            // Create an instance of DataSet.
            DataSet ds = new DataSet();
            da.Fill(ds);
            con.Close();
            // Return the DataSet as an XmlElement.
            XmlDataDocument xmldata = new XmlDataDocument(ds);
            XmlElement xmlElement = xmldata.DocumentElement;
            return xmlElement;
        }

        [WebMethod]

        public XmlElement GetAllUsers()
        {
            SqlConnection con = new SqlConnection(cnStr);
            con.Open();
            string query = "select * from Users";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.ExecuteNonQuery();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            con.Close();
            XmlDataDocument xmldata = new XmlDataDocument(ds);
            XmlElement xmlElement = xmldata.DocumentElement;
            return xmlElement;
        }

        [WebMethod]
        public XmlElement GetMillionData()
        {
            SqlConnection con = new SqlConnection(cnStr);
            con.Open();
            string query = "select * from [AssignmentDB].[dbo].[tblOrder]";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.ExecuteNonQuery();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            con.Close();
            XmlDataDocument xmldata = new XmlDataDocument(ds);
            XmlElement xmlElement = xmldata.DocumentElement;
            return xmlElement;
        }

        [WebMethod]
        public DataSet GetEmployees()
        {
            SqlConnection con = new SqlConnection(cnStr);

            SqlCommand cmd = new SqlCommand(
                "SELECT Employeeld,FirstName,Gender,Salary,Country FROM tblEmployee",
                con);

            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataSet ds = new DataSet();

            da.Fill(ds);

            return ds;
        }

        [WebMethod]

        //Insert Employeee
        public int InsertEmployee
            (
                string FirstName,
                string Gender,
                int Salary,
                string Country
            )
                    {
                        SqlConnection con = new SqlConnection(cnStr);

                        string query =
                        @"INSERT INTO tblEmployee
                  (FirstName,Gender,Salary,Country)
                  VALUES
                  (@FirstName,@Gender,@Salary,@Country)";

                        SqlCommand cmd = new SqlCommand(query, con);

                        cmd.Parameters.AddWithValue("@FirstName", FirstName);
                        cmd.Parameters.AddWithValue("@Gender", Gender);
                        cmd.Parameters.AddWithValue("@Salary", Salary);
                        cmd.Parameters.AddWithValue("@Country", Country);

                        con.Open();

                        int rows = cmd.ExecuteNonQuery();

                        con.Close();

                        return rows;
        }

        [WebMethod]
        //updating employees
        public int UpdateEmployee(
            int EmployeeId,
            string FirstName,
            string Gender,
            int Salary,
            string Country
        )
                {
                    SqlConnection con = new SqlConnection(cnStr);

                    string query =
                    @"UPDATE tblEmployee
              SET FirstName=@FirstName,
                  Gender=@Gender,
                  Salary=@Salary,
                  Country=@Country
              WHERE Employeeld=@EmployeeId";

                    SqlCommand cmd = new SqlCommand(query, con);

                    cmd.Parameters.AddWithValue("@EmployeeId", EmployeeId);
                    cmd.Parameters.AddWithValue("@FirstName", FirstName);
                    cmd.Parameters.AddWithValue("@Gender", Gender);
                    cmd.Parameters.AddWithValue("@Salary", Salary);
                    cmd.Parameters.AddWithValue("@Country", Country);

                    con.Open();

                    int rows = cmd.ExecuteNonQuery();

                    con.Close();

                    return rows;
                }

        [WebMethod]
        // Deleteing employees
        public int DeleteEmployee(int EmployeeId)
                {
                    SqlConnection con = new SqlConnection(cnStr);

                    SqlCommand cmd =
                        new SqlCommand(
                            "DELETE FROM tblEmployee WHERE Employeeld=@EmployeeId",
                            con);

                    cmd.Parameters.AddWithValue("@EmployeeId", EmployeeId);

                    con.Open();

                    int rows = cmd.ExecuteNonQuery();

                    con.Close();

                    return rows;
        }

    }
}
