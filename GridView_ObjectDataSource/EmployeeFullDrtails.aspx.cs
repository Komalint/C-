using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GridView_ObjectDataSource
{
    public partial class EmployeeFullDrtails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int employeeId = Convert.ToInt32(Request.QueryString["EmployeeId"]);
                BindEmployeeDetails(employeeId);
            }
        }

        private void BindEmployeeDetails(int employeeId)
        {
            dvEmployee.DataSource = UserDataAccessLayer.GetEmployeeDetailsById(employeeId);
            dvEmployee.DataBind();
        }
    }
}