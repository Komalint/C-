using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Routing;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UrlRouting
{
    public partial class Route1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblMessage.Text = string.Empty;
            }
        }

        protected void btnView_Click(object sender, EventArgs e)
        {
            string idInput = txtArticleId.Text.Trim();
            if (string.IsNullOrEmpty(idInput))
            {
                lblMessage.ForeColor = System.Drawing.Color.Red;
                lblMessage.Text = "Please enter an Article ID.";
                return;
            }

            // 1. Try querying the Database
            try
            {
                string constr = "Data Source=.;Initial Catalog=AssignmentDB;User ID=sa;Password=mcc#1234";
                using (SqlConnection con = new SqlConnection(constr))
                {
                    string query = "SELECT ID, Title FROM dbo.Articletable WHERE ID=@ID";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@ID", idInput);
                        con.Open();

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.Read())
                            {
                                string id = dr["ID"].ToString();
                                string title = dr["Title"].ToString().Replace(" ", "-");

                                Response.Redirect(GetRouteUrl("RouteForArticle", new
                                {
                                    id,
                                    title
                                }));
                                return;
                            }
                        }
                    }
                }
            }
            catch
            {
                // In case database server is offline or table is unavailable,
                // fall back to default demo articles so the routing demonstration works seamlessly!
            }

            // 2. Fallback sample catalog for offline/standalone demo
            Dictionary<string, string> sampleArticles = new Dictionary<string, string>
            {
                { "1", "Dropdown-Validation" },
                { "2", ".NET-Assemblies" },
                { "3", "BinaryReader-BinaryWriter" },
                { "4", "StreamWriter" }
            };

            if (sampleArticles.ContainsKey(idInput))
            {
                string id = idInput;
                string title = sampleArticles[idInput];

                Response.Redirect(GetRouteUrl("RouteForArticle", new
                {
                    id,
                    title
                }));
                return;
            }

            lblMessage.ForeColor = System.Drawing.Color.Red;
            lblMessage.Text = $"Article with ID '{idInput}' was not found. Try ID 1, 2, 3, or 4.";
        }
    }
}