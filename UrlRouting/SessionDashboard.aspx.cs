using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UrlRouting
{
    public partial class SessionDashboard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["username"] != null && !string.IsNullOrWhiteSpace(Session["username"].ToString()))
                {
                    pnlLoggedIn.Visible = true;
                    pnlNotLoggedIn.Visible = false;

                    lblUserN.Text = Session["username"].ToString();
                    lblLoginTime.Text = Session["loginTime"] != null ? Session["loginTime"].ToString() : DateTime.Now.ToString("g");
                    lblSessionId.Text = Session.SessionID;
                }
                else
                {
                    pnlLoggedIn.Visible = false;
                    pnlNotLoggedIn.Visible = true;
                }
            }
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();
            Response.Redirect(GetRouteUrl("RouteForSessionLogin", null) ?? "SessionLogin.aspx");
        }
    }
}
