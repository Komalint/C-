using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GridView_ObjectDataSource
{
    public partial class EmployeeListFormerDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            BindGridViewData();
        }

        protected void BindGridViewData()
        {
            int pageIndex = 1;

            if (Request.QueryString["PageIndex"] != null)
            {
                pageIndex =
                    Convert.ToInt32(Request.QueryString["PageIndex"]);
            }

            int totalRows;
            int pageSize = 4;

            gvEmployees.DataSource =
                UserDataAccessLayer.GetAllEmployees(
                    pageIndex,
                    pageSize,
                    out totalRows);

            gvEmployees.DataBind();

            int totalPages =
                (int)Math.Ceiling((double)totalRows / pageSize);

            lblPageInfo.Text =
                "Page " + pageIndex + " of " + totalPages;

            // Previous Button Logic
            if (pageIndex > 1)
            {
                lnkPrevious.NavigateUrl =
                    "EmployeeListFormerDetails.aspx?PageIndex="
                    + (pageIndex - 1);

                lnkPrevious.Visible = true;
            }
            else
            {
                lnkPrevious.Visible = false;
            }

            // Next Button Logic
            if (pageIndex < totalPages)
            {
                lnkNext.NavigateUrl =
                    "EmployeeListFormerDetails.aspx?PageIndex="
                    + (pageIndex + 1);

                lnkNext.Visible = true;
            }
            else
            {
                lnkNext.Visible = false;
            }
        }

        protected void btnClick_getEmployeeDetails(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;

            int employeeId =
                Convert.ToInt32(btn.CommandArgument);

            Response.Redirect(
                "EmployeeFullDrtails.aspx?EmployeeId=" + employeeId);
        }
    }
}