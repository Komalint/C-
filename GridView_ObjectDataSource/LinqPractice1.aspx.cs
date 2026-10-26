using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Linq;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GridView_ObjectDataSource
{
    public partial class LinqPractice1 : System.Web.UI.Page
    {
        protected global::System.Web.UI.WebControls.GridView GridView1;

        private readonly string cs = ConfigurationManager.ConnectionStrings["DbConn"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGridData();
            }
        }

        // Procedure to fetch and bind employee data to GridView
        private void BindGridData()
        {
            using (SampleEmployeeContextDataContext dataContext = new SampleEmployeeContextDataContext(cs))
            {
                GridView1.DataSource = dataContext.udp_GetallUsers().ToList();
                GridView1.DataBind();
            }
        }

        // Paging event method
        protected void gv_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            BindGridData();
        }

        // Row editing event method
        protected void gv_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView1.EditIndex = e.NewEditIndex;
            BindGridData();
        }

        // Row edit cancel event method
        protected void gv_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView1.EditIndex = -1;
            BindGridData();
        }

        // Row update event method
        protected void gv_inRowupdate(object sender, GridViewUpdateEventArgs e)
        {
            int empId = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value);
            GridViewRow row = GridView1.Rows[e.RowIndex];

            string firstName = ((TextBox)row.FindControl("txtFirstName")).Text.Trim();
            string lastName = ((TextBox)row.FindControl("txtLastName")).Text.Trim();
            DateTime dob = Convert.ToDateTime(((TextBox)row.FindControl("txtDOB")).Text.Trim());
            decimal salary = Convert.ToDecimal(((TextBox)row.FindControl("txtSalary")).Text.Trim());
            string gender = ((DropDownList)row.FindControl("ddlGender")).SelectedValue;
            string department = ((TextBox)row.FindControl("txtDepartment")).Text.Trim();
            string country = ((TextBox)row.FindControl("txtCountry")).Text.Trim();

            // Calling procedure to perform update
            UpdateEmployee(empId, firstName, lastName, dob, salary, gender, department, country);

            GridView1.EditIndex = -1;
            BindGridData();
        }

        // Row delete event method
        protected void gv_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int empId = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value);

            // Calling procedure to perform delete
            DeleteEmployee(empId);

            BindGridData();
        }

        // Insert new employee event method from GridView footer
        protected void btnInsert_Click(object sender, EventArgs e)
        {
            GridViewRow footerRow = GridView1.FooterRow;

            string firstName = ((TextBox)footerRow.FindControl("txtNewFirstName")).Text.Trim();
            string lastName = ((TextBox)footerRow.FindControl("txtNewLastName")).Text.Trim();
            DateTime dob = Convert.ToDateTime(((TextBox)footerRow.FindControl("txtNewDOB")).Text.Trim());
            int salary = Convert.ToInt32(((TextBox)footerRow.FindControl("txtNewSalary")).Text.Trim());
            string gender = ((DropDownList)footerRow.FindControl("ddlNewGender")).SelectedValue;
            string department = ((TextBox)footerRow.FindControl("txtNewDepartment")).Text.Trim();
            string country = ((TextBox)footerRow.FindControl("txtNewCountry")).Text.Trim();

            // Calling procedure to perform insert
            InsertEmployee(firstName, lastName, dob, salary, gender, department, country);

            BindGridData();
        }

        #region Procedures / Methods to perform database operations

        // Procedure to insert employee details
        private void InsertEmployee(string firstName, string lastName, DateTime dob, int salary, string gender, string department, string country)
        {
            using (SampleEmployeeContextDataContext dataContext = new SampleEmployeeContextDataContext(cs))
            {
                dataContext.udp_InsertEmployeeDetails(firstName, lastName, dob, salary, gender, department, country);
            }
        }

        // Procedure to update employee details
        private void UpdateEmployee(int empId, string firstName, string lastName, DateTime dob, decimal salary, string gender, string department, string country)
        {
            using (SampleEmployeeContextDataContext dataContext = new SampleEmployeeContextDataContext(cs))
            {
                dataContext.udp_UpdateEmployeeDetails(empId, firstName, lastName, dob, salary, gender, department, country);
            }
        }

        // Procedure to delete employee details
        private void DeleteEmployee(int empId)
        {
            using (SampleEmployeeContextDataContext dataContext = new SampleEmployeeContextDataContext(cs))
            {
                dataContext.ExecuteCommand("EXEC udp_DeleteUserById @id = {0}", empId);
            }
        }

        #endregion
    }
}
