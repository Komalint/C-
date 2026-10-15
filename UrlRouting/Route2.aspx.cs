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
                object routeIdObj = Page.RouteData.Values["id"];
                string rawId = routeIdObj != null ? routeIdObj.ToString() : Request.QueryString["id"];

                if (!string.IsNullOrEmpty(rawId) && int.TryParse(rawId, out int id))
                {
                    LoadArticle(id);
                }
                else
                {
                    lblRouteInfo.Text = "Direct Access / No Route Parameters";
                    lblTitle.Text = "No Article Selected";
                    lblDescription.Text = "Please access this page through the Article Search page (e.g. /Articles/1/Dropdown-Validation) or provide a valid ID.";
                    lblAuthor.Text = "System";
                }
            }
        }

        private void LoadArticle(int id)
        {
            object routeTitle = Page.RouteData.Values["title"] ?? "N/A";
            lblRouteInfo.Text = $"Route Params: [id = {id}, title = {routeTitle}]";

            // 1. Try fetching from SQL Server
            try
            {
                string strConn = "Data Source=.;Initial Catalog=AssignmentDB;User ID=sa;Password=mcc#1234";
                using (SqlConnection conn = new SqlConnection(strConn))
                {
                    string query = "SELECT * FROM dbo.Articletable WHERE ID=@ID";
                    using (SqlDataAdapter da = new SqlDataAdapter(query, conn))
                    {
                        da.SelectCommand.Parameters.AddWithValue("@ID", id);
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        if (dt.Rows.Count > 0)
                        {
                            lblTitle.Text = dt.Rows[0]["Title"].ToString();
                            lblDescription.Text = dt.Rows[0]["Description"].ToString();
                            lblAuthor.Text = dt.Rows[0]["Author"].ToString();
                            return;
                        }
                    }
                }
            }
            catch
            {
                // Graceful fallback for offline demo
            }

            // 2. Demo fallback content for predefined article IDs
            switch (id)
            {
                case 1:
                    lblTitle.Text = "Dropdown Validation in ASP.NET Web Forms";
                    lblDescription.Text = "ASP.NET validation controls provide rich client-side and server-side verification. For DropDownLists, a RequiredFieldValidator with InitialValue specified ensures that users select a non-default item before submitting forms.";
                    lblAuthor.Text = "ASP.NET Architecture Team";
                    break;
                case 2:
                    lblTitle.Text = ".NET Assemblies and Shared Libraries";
                    lblDescription.Text = "Assemblies form the fundamental unit of deployment, version control, reuse, and security permissions in .NET. They can be private to an application or shared across multiple applications via the Global Assembly Cache (GAC).";
                    lblAuthor.Text = "CLR & Framework Team";
                    break;
                case 3:
                    lblTitle.Text = "BinaryReader and BinaryWriter in C#";
                    lblDescription.Text = "System.IO.BinaryReader and BinaryWriter enable reading and writing binary data streams for primitive types (integers, strings, booleans, floating points) with compact serialization and high throughput.";
                    lblAuthor.Text = "System.IO Developers";
                    break;
                case 4:
                    lblTitle.Text = "StreamWriter and Text File Processing";
                    lblDescription.Text = "StreamWriter simplifies writing characters, formatted lines, and blocks of text to files and memory streams with configurable encoding (such as UTF-8) and buffer management.";
                    lblAuthor.Text = "Komal Gupta";
                    break;
                default:
                    lblTitle.Text = $"Article #{id}";
                    lblDescription.Text = $"Demonstration article content retrieved for routed identifier ID={id}. In production, this record is dynamically queried from SQL Server.";
                    lblAuthor.Text = "Portal Administrator";
                    break;
            }
        }
    }
}