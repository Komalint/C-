using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UrlRouting
{
    public partial class StateUser : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblStateMessage.Text = string.Empty;
            }
        }

        protected void btnQueryString_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid)
            {
                return;
            }

            string username = Server.UrlEncode(txtName.Text.Trim());
            string email = Server.UrlEncode(txtEmail.Text.Trim());

            string targetUrl = $"StateDashboard.aspx?username={username}&useremail={email}";
            Response.Redirect(targetUrl);
        }

        protected void btnCookie_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid)
            {
                return;
            }

            HttpCookie cookie = new HttpCookie("UserDetails");
            cookie["Name"] = txtName.Text.Trim();
            cookie["Email"] = txtEmail.Text.Trim();
            cookie["CreatedAt"] = DateTime.Now.ToString("g");
            cookie.Expires = DateTime.Now.AddMinutes(10); // 10 minute lifespan

            Response.Cookies.Add(cookie);

            Response.Redirect(GetRouteUrl("RouteForStateDashboard", null) ?? "StateDashboard.aspx");
        }
    }
}
