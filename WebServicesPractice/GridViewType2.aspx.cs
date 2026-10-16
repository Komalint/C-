using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebServicesPractice
{
    public partial class GridViewType2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int salary = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Salary"));

                if (salary > 42000)
                {
                    e.Row.BackColor = System.Drawing.Color.Green;
                }
                else if(salary < 42000 && salary > 31000)
                {
                    e.Row.BackColor = System.Drawing.Color.Yellow;
                }
                else
                {
                    e.Row.BackColor = System.Drawing.Color.LightGoldenrodYellow;
                }
            }
        }
    }
}