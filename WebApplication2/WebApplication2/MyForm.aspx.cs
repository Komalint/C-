using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication2
{
    public partial class MyForm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            lblMsg.Text = "Hello my frist Web Application in .NET";
        }

        protected void BtnUpdate_Click(object sender, EventArgs e)
        {

        }

        protected void DrpListCategory_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}