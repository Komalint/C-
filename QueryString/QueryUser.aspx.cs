using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace QueryString
{
    public partial class QueryUser : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnClickRedrctDash_Click(object sender, EventArgs e)
        {
            Response.Redirect($"WebDasboard.aspx?username={Server.UrlEncode(txtName.Text.Trim())}&useremail={ Server.UrlEncode(txtEmail.Text.Trim())}");
        }

        protected void btnClickSendData_Click(object sender, EventArgs e)
        {
            HttpCookie cookie = new HttpCookie("UserDetails");
            cookie["Name"] = txtName.Text;
            cookie["Email"] = txtEmail.Text;

            cookie.Expires = DateTime.Now.AddMinutes(1);
            Response.Cookies.Add(cookie);
            Response.Redirect("WebDasboard.aspx");

        }
    }
}