using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GridView_ObjectDataSource
{
    public partial class LINQpractice : System.Web.UI.Page
    {
        private readonly string cs = ConfigurationManager.ConnectionStrings["DbConn"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGridData();
            }
        }

        private void BindGridData()
        {
            using (SampleEmployeeContextDataContext dataContext = new SampleEmployeeContextDataContext(cs))
            {
                GridView1.DataSource = from tEmployee in dataContext.tblEmployees
                                       
                                       select tEmployee;
                GridView1.DataBind();
            }
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            BindGridData();
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            // Command handling for Addnew, EditRow, DeleteRow, etc.
        }
    }
}