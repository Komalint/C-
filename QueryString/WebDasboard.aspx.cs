using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace QueryString
{
    public partial class WebDasboard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (!Page.IsPostBack)
            //{
            //    txtname.Text = this.Request.QueryString["username"];
            //    txtmail.Text = this.Request.QueryString["useremail"];
            //}
            //   lblinitial.Text = txtname.Text.Substring(0, 1).ToUpper();


            HttpCookie userCookie = Request.Cookies["UserDetails"];
            if (userCookie != null)
            {
                txtname.Text = userCookie["Name"];
                txtmail.Text  = userCookie["Email"];

                lblinitial.Text = txtname.Text.Substring(0, 1).ToUpper();
            }
            else
            {
                Response.Redirect("QueryUser.aspx");
            }
                
        }
    }
}