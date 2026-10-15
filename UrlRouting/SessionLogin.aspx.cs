using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UrlRouting
{
    public partial class SessionLogin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblLoginMessage.Text = string.Empty;
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid)
            {
                return;
            }

            // Save credentials and session context
            Session["username"] = txtUsernameInput.Text.Trim();
            Session["password"] = txtPassword.Text;
            Session["loginTime"] = DateTime.Now.ToString("F");

            // Redirect to protected Session Dashboard via Friendly Route or Page URL
            Response.Redirect(GetRouteUrl("RouteForSessionDashboard", null) ?? "SessionDashboard.aspx");
        }
    }
}
