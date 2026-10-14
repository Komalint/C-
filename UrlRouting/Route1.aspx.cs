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

        }
        

        protected void btnView_Click(object sender, EventArgs e)
        {
            string constr =
                "Data Source=.;Initial Catalog=AssignmentDB;User ID=sa;Password=mcc#1234";

            SqlConnection con = new SqlConnection(constr);

            string query =
                "SELECT ID,Title FROM dbo.Articletable WHERE ID=@ID";

            SqlCommand cmd = new SqlCommand(query, con);

            cmd.Parameters.AddWithValue("@ID", txtArticleId.Text);

            con.Open();

            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                string id = dr["ID"].ToString();

                string title = dr["Title"].ToString().Replace(" ", "-");

                Response.Redirect(GetRouteUrl("RouteForArticle", new
                {
                    id,
                    title
                }));
            }

            con.Close();
        }




    }
}