using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

namespace GridView_ObjectDataSource
{
    public partial class CRUD_User : System.Web.UI.Page
    {
       
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Addnew")
            {
                EmployeeQuery emp = new EmployeeQuery();

                TextBox txtFirstName =
                    (TextBox)GridView1.FooterRow.FindControl("txtNewFirstName");

                TextBox txtLastName =
                    (TextBox)GridView1.FooterRow.FindControl("txtNewLastName");

                TextBox txtDOB =
                    (TextBox)GridView1.FooterRow.FindControl("txtNewDOB");

                TextBox txtSalary =
                    (TextBox)GridView1.FooterRow.FindControl("txtNewSalary");

                DropDownList ddlGender =
                    (DropDownList)GridView1.FooterRow.FindControl("ddlNewGender");

                TextBox txtDepartment =
                    (TextBox)GridView1.FooterRow.FindControl("txtNewDepartment");

                TextBox txtCountry =
                    (TextBox)GridView1.FooterRow.FindControl("txtNewCountry");

                emp.FirstName = txtFirstName.Text;
                emp.LastName = txtLastName.Text;
                emp.DateOfBirth = Convert.ToDateTime(txtDOB.Text);
                emp.Salary = Convert.ToInt32(txtSalary.Text);
                emp.Gender = ddlGender.SelectedValue;
                emp.DepartmentName = txtDepartment.Text;
                emp.Country = txtCountry.Text;

                UserDataAccessLayer.InsertEmployee(emp);

                GridView1.DataBind();
            }
        }

    }
}