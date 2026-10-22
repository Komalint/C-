using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GridView_ObjectDataSource
{
    public partial class CRUD_User_wo_ObjDataSource : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGridViewData();
            }
        }
        private void BindGridViewData()
        {
            GridView1.DataSource = UserDataAccessLayer.GetAllUsers();
            GridView1.DataBind();

        }
        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if(e.CommandName == "EditRow")
            {
                int rowindex = ((GridViewRow)((LinkButton)e.CommandSource).NamingContainer).RowIndex;
                GridView1.EditIndex = rowindex;
                BindGridViewData();        
            }
            else if (e.CommandName == "DeleteRow")
            {
                //int id=Convert.ToInt32(e.CommandArgument);
                //UserDataAccessLayer.DeleteUserByIdn(id);
                //BindGridViewData();

                int rowIndex =
                    ((GridViewRow)((LinkButton)e.CommandSource).NamingContainer).RowIndex;

                EmployeeQuery emp = new EmployeeQuery();

                emp.EmployeeId =
                    Convert.ToInt32(e.CommandArgument);
                UserDataAccessLayer.DeleteUserById(emp);
                BindGridViewData();

            }
            else if (e.CommandName == "CancelUpdate")
            {
                GridView1.EditIndex = -1;
                BindGridViewData();
            }
            else if (e.CommandName == "UpdateRow")
            {
                int rowIndex =
                    ((GridViewRow)((LinkButton)e.CommandSource).NamingContainer).RowIndex;

                EmployeeQuery emp = new EmployeeQuery();

                emp.EmployeeId =
                    Convert.ToInt32(e.CommandArgument);

                emp.FirstName =
                    ((TextBox)GridView1.Rows[rowIndex]
                    .FindControl("txtFirstName")).Text;

                emp.LastName =
                    ((TextBox)GridView1.Rows[rowIndex]
                    .FindControl("txtLastName")).Text;

                emp.DateOfBirth =
                    Convert.ToDateTime(
                    ((TextBox)GridView1.Rows[rowIndex]
                    .FindControl("txtDOB")).Text);

                emp.Salary =
                    Convert.ToInt32(
                    ((TextBox)GridView1.Rows[rowIndex]
                    .FindControl("txtSalary")).Text);

                emp.Gender =
                    ((DropDownList)GridView1.Rows[rowIndex]
                    .FindControl("ddlGender")).SelectedValue;

                emp.DepartmentName =
                    ((TextBox)GridView1.Rows[rowIndex]
                    .FindControl("txtDepartment")).Text;

                emp.Country =
                    ((TextBox)GridView1.Rows[rowIndex]
                    .FindControl("txtCountry")).Text;

                UserDataAccessLayer.UpdateEmployee(emp);
                GridView1.EditIndex = -1;

                BindGridViewData();
            }
            else if (e.CommandName == "Addnew")
            {
                EmployeeQuery emp = new EmployeeQuery();

                emp.FirstName =
                    ((TextBox)GridView1.FooterRow
                    .FindControl("txtNewFirstName")).Text;

                emp.LastName =
                    ((TextBox)GridView1.FooterRow
                    .FindControl("txtNewLastName")).Text;

                emp.DateOfBirth =
                    Convert.ToDateTime(
                    ((TextBox)GridView1.FooterRow
                    .FindControl("txtNewDOB")).Text);

                emp.Salary =
                    Convert.ToInt32(
                    ((TextBox)GridView1.FooterRow
                    .FindControl("txtNewSalary")).Text);

                emp.Gender =
                    ((DropDownList)GridView1.FooterRow
                    .FindControl("ddlNewGender")).SelectedValue;

                emp.DepartmentName =
                    ((TextBox)GridView1.FooterRow
                    .FindControl("txtNewDepartment")).Text;

                emp.Country =
                    ((TextBox)GridView1.FooterRow
                    .FindControl("txtNewCountry")).Text;

                UserDataAccessLayer.InsertEmployee(emp);

                BindGridViewData();
            }

        }


        protected void gvUsers_pageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex= e.NewPageIndex;
            BindGridViewData();
        }

        //protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        //{
        //    if (e.CommandName == "Addnew")
        //    {
        //        EmployeeQuery emp = new EmployeeQuery();

        //        TextBox txtFirstName =
        //            (TextBox)GridView1.FooterRow.FindControl("txtNewFirstName");

        //        TextBox txtLastName =
        //            (TextBox)GridView1.FooterRow.FindControl("txtNewLastName");

        //        TextBox txtDOB =
        //            (TextBox)GridView1.FooterRow.FindControl("txtNewDOB");

        //        TextBox txtSalary =
        //            (TextBox)GridView1.FooterRow.FindControl("txtNewSalary");

        //        DropDownList ddlGender =
        //            (DropDownList)GridView1.FooterRow.FindControl("ddlNewGender");

        //        TextBox txtDepartment =
        //            (TextBox)GridView1.FooterRow.FindControl("txtNewDepartment");

        //        TextBox txtCountry =
        //            (TextBox)GridView1.FooterRow.FindControl("txtNewCountry");

        //        emp.FirstName = txtFirstName.Text;
        //        emp.LastName = txtLastName.Text;
        //        emp.DateOfBirth = Convert.ToDateTime(txtDOB.Text);
        //        emp.Salary = Convert.ToInt32(txtSalary.Text);
        //        emp.Gender = ddlGender.SelectedValue;
        //        emp.DepartmentName = txtDepartment.Text;
        //        emp.Country = txtCountry.Text;

        //        UserDataAccessLayer.InsertEmployee(emp);

        //        GridView1.DataBind();
        //    }
        //}
    }
}