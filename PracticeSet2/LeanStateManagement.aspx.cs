using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PracticeSet2
{
    public partial class LeanStateManagement : System.Web.UI.Page
    {
        int count = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtCount.Text = "0";
            }
        }

        protected void btnOnSession_Click(object sender, EventArgs e)
        {

            //count++;
            //txtCount.Text = count.ToString();

            int count = Convert.ToInt32(txtCount.Text) + 1;
            txtCount.Text = count.ToString();
            //if (ViewState["Clicks"] != null)
            //{
            //    count = (int)ViewState["Clicks"] + 1;
            //}
            //txtCount.Text = count.ToString();
            //ViewState["Clicks"] = count;
        }
    }
}