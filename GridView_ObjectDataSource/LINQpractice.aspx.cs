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
            if (e.CommandName == "EditRow")
            {
                int rowindex = ((GridViewRow)((LinkButton)e.CommandSource).NamingContainer).RowIndex;
                GridView1.EditIndex = rowindex;
                BindGridData();
            }
            else if (e.CommandName == "DeleteRow")
            {
                int empId = Convert.ToInt32(e.CommandArgument);
                using (SampleEmployeeContextDataContext dataContext = new SampleEmployeeContextDataContext(cs))
                {
                    tblEmployee emp = (from em in dataContext.tblEmployees
                                       where em.EmployeeId == empId
                                       select em).FirstOrDefault();
                    if (emp != null)
                    {
                        dataContext.tblEmployees.DeleteOnSubmit(emp);
                        dataContext.SubmitChanges();
                    }
                }
                BindGridData();
            }
            else if (e.CommandName == "Addnew")
            {
                GridViewRow footerRow = GridView1.FooterRow;

                using (SampleEmployeeContextDataContext dataContext =
                       new SampleEmployeeContextDataContext(cs))
                {
                    tblEmployee emp = new tblEmployee();

                    emp.FirstName = ((TextBox)footerRow.FindControl("txtNewFirstName")).Text;
                    emp.LastName = ((TextBox)footerRow.FindControl("txtNewLastName")).Text;
                    emp.DateOfBirth = Convert.ToDateTime(((TextBox)footerRow.FindControl("txtNewDOB")).Text);
                    emp.Salary = Convert.ToInt32(((TextBox)footerRow.FindControl("txtNewSalary")).Text);
                    emp.Gender = ((DropDownList)footerRow.FindControl("ddlNewGender")).SelectedValue;
                    emp.DepartmentName = ((TextBox)footerRow.FindControl("txtNewDepartment")).Text;
                    emp.Country = ((TextBox)footerRow.FindControl("txtNewCountry")).Text;

                    dataContext.tblEmployees.InsertOnSubmit(emp);
                    dataContext.SubmitChanges();
                }

                BindGridData();
            }
            else if (e.CommandName == "UpdateRow")
            {
                int empId = Convert.ToInt32(e.CommandArgument);

                GridViewRow row = ((LinkButton)e.CommandSource).NamingContainer as GridViewRow;

                using (SampleEmployeeContextDataContext dataContext =
                       new SampleEmployeeContextDataContext(cs))
                {
                    tblEmployee emp = (from em in dataContext.tblEmployees
                                       where em.EmployeeId == empId
                                       select em).FirstOrDefault();

                    if (emp != null)
                    {
                        emp.FirstName = ((TextBox)row.FindControl("txtFirstName")).Text;
                        emp.LastName = ((TextBox)row.FindControl("txtLastName")).Text;
                        emp.DateOfBirth = Convert.ToDateTime(((TextBox)row.FindControl("txtDOB")).Text);
                        emp.Salary = Convert.ToInt32(((TextBox)row.FindControl("txtSalary")).Text);
                        emp.Gender = ((DropDownList)row.FindControl("ddlGender")).SelectedValue;
                        emp.DepartmentName = ((TextBox)row.FindControl("txtDepartment")).Text;
                        emp.Country = ((TextBox)row.FindControl("txtCountry")).Text;

                        dataContext.SubmitChanges();
                    }
                }
                GridView1.EditIndex = -1;
                BindGridData();
            }
            else if (e.CommandName == "CancelUpdate")
            {
                GridView1.EditIndex = -1;
                BindGridData();
            }



        }
    }
}