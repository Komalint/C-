using System;
using System.Data;
using System.Web.UI.WebControls;

namespace WebServicesPractice
{
    public partial class GridViewPractice4 : System.Web.UI.Page
    {
        WebService obj = new WebService();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGrid();
            }
        }

        private void BindGrid()
        {
            DataSet ds = obj.GetEmployees();

            GridView1.DataSource = ds.Tables[0];

            GridView1.DataBind();
        }

        protected void btnInsert_Click(object sender, EventArgs e)
        {
            TextBox txtName =
              (TextBox)GridView1.FooterRow.FindControl("txtNewFirstName");

            DropDownList ddlGender =
              (DropDownList)GridView1.FooterRow.FindControl("ddlNewGender");

            TextBox txtSalary =
              (TextBox)GridView1.FooterRow.FindControl("txtNewSalary");

            TextBox txtCountry =
              (TextBox)GridView1.FooterRow.FindControl("txtNewCountry");

            obj.InsertEmployee(
                txtName.Text,
                ddlGender.SelectedValue,
                Convert.ToInt32(txtSalary.Text),
                txtCountry.Text);

            BindGrid();
        }

        protected void GridView1_RowEditing
        (
            object sender,
            GridViewEditEventArgs e
        )
        {
            GridView1.EditIndex = e.NewEditIndex;

            BindGrid();
        }

        protected void GridView1_RowCancelingEdit
        (
            object sender,
            GridViewCancelEditEventArgs e
        )
        {
            GridView1.EditIndex = -1;

            BindGrid();
        }

        protected void GridView1_RowUpdating
        (
            object sender,
            GridViewUpdateEventArgs e
        )
        {
            int id =
                Convert.ToInt32(
                GridView1.Rows[e.RowIndex].Cells[1].Text);

            string name =
                ((TextBox)GridView1.Rows[e.RowIndex]
                .FindControl("txtFirstName")).Text;

            DropDownList ddlGender =
                (DropDownList)GridView1.Rows[e.RowIndex]
                .FindControl("ddlGender");

            string gender = ddlGender.SelectedValue;

            string salaryText =
                ((TextBox)GridView1.Rows[e.RowIndex]
                .FindControl("txtSalary")).Text;

            string country =
                ((TextBox)GridView1.Rows[e.RowIndex]
                .FindControl("txtCountry")).Text;

            obj.UpdateEmployee(
                id,
                name,
                gender,
                Convert.ToInt32(salaryText),
                country);

            GridView1.EditIndex = -1;

            BindGrid();
        }

        protected void GridView1_RowDeleting
        (
            object sender,
            GridViewDeleteEventArgs e
        )
        {
            int id =
                Convert.ToInt32(
                GridView1.Rows[e.RowIndex].Cells[1].Text);

            obj.DeleteEmployee(id);

            BindGrid();
        }
    }
}