using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UrlRouting
{
    public partial class Route2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int id = Convert.ToInt32(
                    Page.RouteData.Values["id"]);

                LoadArticle(id);
            }
        }


        private void LoadArticle(int id)
        {
            string strConn = "Data Source=.;Initial Catalog=AssignmentDB;User ID=sa;Password=mcc#1234";

            SqlConnection conn = new SqlConnection(strConn);

            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM dbo.Articletable WHERE ID=@ID", conn);

            da.SelectCommand.Parameters.AddWithValue("@ID", id);

            DataTable dt = new DataTable();

            da.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                lblTitle.Text = dt.Rows[0]["Title"].ToString();

                lblDescription.Text = dt.Rows[0]["Description"].ToString();

                lblAuthor.Text = dt.Rows[0]["Author"].ToString();
            }
        }


    }
}