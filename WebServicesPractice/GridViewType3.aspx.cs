using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebServicesPractice
{
    public partial class GridViewType3 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string country = DataBinder.Eval(e.Row.DataItem, "Country").ToString();

                int salary = Convert.ToInt32(
                    DataBinder.Eval(e.Row.DataItem, "Salary"));

                string formattedSalary = "";

                if (country == "US")
                {
                    formattedSalary = string.Format(
                        new System.Globalization.CultureInfo("en-US"),
                        "{0:C}",
                        salary);
                }
                else if (country == "UK")
                {
                    formattedSalary = string.Format(
                        new System.Globalization.CultureInfo("en-GB"),
                        "{0:C}",
                        salary);
                }
                else if (country == "India")
                {
                    formattedSalary = string.Format(
                        new System.Globalization.CultureInfo("en-IN"),
                        "{0:C}",
                        salary);
                }
                else if (country == "South Africa")
                {
                    formattedSalary = string.Format(
                        new System.Globalization.CultureInfo("en-ZA"),
                        "{0:C}",
                        salary);
                }
                else if (country == "Malaysia")
                {
                    formattedSalary = string.Format(
                        new System.Globalization.CultureInfo("ms-MY"),
                        "{0:C}",
                        salary);
                }

                e.Row.Cells[4].Text = formattedSalary;
            }
        }

        protected void btnInsert_Click(object sender, EventArgs e)
        {
            TextBox txtFirstName =
                (TextBox)GridView1.FooterRow.FindControl("txtNewFirstName");

            DropDownList ddlGender =
                (DropDownList)GridView1.FooterRow.FindControl("ddlNewGender");

            TextBox txtSalary =
                (TextBox)GridView1.FooterRow.FindControl("txtNewSalary");

            TextBox txtCountry =
                (TextBox)GridView1.FooterRow.FindControl("txtNewCountry");

            SqlDataSource1.InsertParameters.Clear();

            SqlDataSource1.InsertParameters.Add("FirstName", txtFirstName.Text);
            SqlDataSource1.InsertParameters.Add("Gender", ddlGender.SelectedValue);
            SqlDataSource1.InsertParameters.Add("Salary", txtSalary.Text);
            SqlDataSource1.InsertParameters.Add("Country", txtCountry.Text);

            SqlDataSource1.Insert();

            GridView1.DataBind();
        }
    }
}