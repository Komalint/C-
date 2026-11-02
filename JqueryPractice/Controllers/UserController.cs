using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.Mvc;
using JqueryPractice.Models;

namespace JqueryPractice.Controllers
{
    public class UserController : Controller
    {
        string cnStr = ConfigurationManager.ConnectionStrings["DBConn"]?.ConnectionString
            ?? "Data Source=.;Initial Catalog=AssignmentDB;Persist Security Info=True;User ID=sa;Password=mcc#1234;TrustServerCertificate=True";

        // GET: User/InsertUserDetails
        public ActionResult InsertUserDetails()
        {
            UserDetails objuser = new UserDetails();
            DataSet ds = new DataSet();

            using (SqlConnection con = new SqlConnection(cnStr))
            {
                using (SqlCommand cmd = new SqlCommand("usercrudoperations", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@status", "GET");
                    con.Open();

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds);

                    List<UserDetails> userlist = new List<UserDetails>();
                    if (ds.Tables.Count > 0)
                    {
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            UserDetails uobj = new UserDetails();
                            uobj.UserId = Convert.ToInt32(ds.Tables[0].Rows[i]["userid"].ToString());
                            uobj.UserName = ds.Tables[0].Rows[i]["username"]?.ToString();
                            uobj.Education = ds.Tables[0].Rows[i]["education"]?.ToString();
                            uobj.Location = ds.Tables[0].Rows[i]["location"]?.ToString();
                            userlist.Add(uobj);
                        }
                    }
                    objuser.usersinfo = userlist;
                    con.Close();
                }
            }

            return View(objuser);
        }

        // POST: User/InsertUserDetails
        [HttpPost]
        public ActionResult InsertUserDetails(UserDetails user)
        {
            using (SqlConnection con = new SqlConnection(cnStr))
            {
                using (SqlCommand cmd = new SqlCommand("usercrudoperations", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@name", (object)user.UserName ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@education", (object)user.Education ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@location", (object)user.Location ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@status", "INSERT");

                    con.Open();
                    ViewData["result"] = cmd.ExecuteNonQuery();
                    con.Close();
                }
            }

            return View();
        }
    }
}
