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
        private string GetConnectionString()
        {
            var connSettings = ConfigurationManager.ConnectionStrings["DBConn"];
            return connSettings != null ? connSettings.ConnectionString : null;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                checkun.Visible = false;
                lblFormFeedback.Text = string.Empty;
            }
        }

        protected void CheckUserNameAvailability(object sender, EventArgs e)
        {
            string username = txtun.Text.Trim();
            if (string.IsNullOrEmpty(username))
            {
                checkun.Visible = false;
                return;
            }

            System.Threading.Thread.Sleep(1000);

            checkun.Visible = true;
            bool usernameExists = false;

            string cnStr = GetConnectionString();
            if (!string.IsNullOrEmpty(cnStr))
            {
                try
                {
                    using (SqlConnection con = new SqlConnection(cnStr))
                    {
                        string query = "SELECT UserName FROM Users WHERE UserName=@uname";
                        using (SqlCommand cmd = new SqlCommand(query, con))
                        {
                            cmd.Parameters.AddWithValue("@uname", username);
                            con.Open();
                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    usernameExists = true;
                                }
                            }
                        }
                    }
                }
                catch
                {
                    string[] reserved = new[] { "admin", "komal", "user1", "testuser", "manager" };
                    usernameExists = reserved.Contains(username.ToLower());
                }
            }
            else
            {
                string[] reserved = new[] { "admin", "komal", "user1", "testuser", "manager" };
                usernameExists = reserved.Contains(username.ToLower());
            }

            if (usernameExists)
            {
                lblmsg.ForeColor = System.Drawing.Color.Red;
                lblmsg.Text = "Username already exists. Please choose another.";
            }
            else
            {
                lblmsg.ForeColor = System.Drawing.Color.Green;
                lblmsg.Text = "Username is available.";
            }
        }

        protected void txtbtn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtun.Text))
            {
                lblFormFeedback.ForeColor = System.Drawing.Color.Red;
                lblFormFeedback.Text = "Please enter a valid username.";
                return;
            }

            //if (txtpwd.Text != txtconpwd.Text)
            //{
            //    lblFormFeedback.ForeColor = System.Drawing.Color.Red;
            //    lblFormFeedback.Text = "Passwords do not match!";
            //    return;
            //}

            lblFormFeedback.ForeColor = System.Drawing.Color.Green;
            lblFormFeedback.Text = $"Registration completed for user '{txtun.Text}'.";
        }
    }
}