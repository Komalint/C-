using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UrlRouting
{
    public partial class CheckUnameByUpdatePanel : System.Web.UI.Page
    {
        string _cnStr = ConfigurationManager.ConnectionStrings["DBConn"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void CheckUserNameAvailability(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtun.Text))
            {
                System.Threading.Thread.Sleep(3000);

                checkun.Visible = true;

                SqlConnection con = new SqlConnection(_cnStr);

                string query =
                    "SELECT UserName FROM Users WHERE UserName=@uname";

                SqlCommand cmd = new SqlCommand(query, con);

                cmd.Parameters.AddWithValue("@uname", txtun.Text);

                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    lblmsg.ForeColor = System.Drawing.Color.Red;
                    lblmsg.Text = "❌ UserName Already Exists..!!";
                }
                else
                {
                    lblmsg.ForeColor = System.Drawing.Color.Green;
                    lblmsg.Text = "✅ Congrats Username is Available..!!";
                }

                reader.Close();
                con.Close();
            }
        }
    }
}