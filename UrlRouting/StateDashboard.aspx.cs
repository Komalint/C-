using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UrlRouting
{
    public partial class StateDashboard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadUserState();
            }
        }

        private void LoadUserState()
        {
            string userName = Request.QueryString["username"];
            string userEmail = Request.QueryString["useremail"];

            // 1. Check if passed via QueryString
            if (!string.IsNullOrEmpty(userName) || !string.IsNullOrEmpty(userEmail))
            {
                pnlContent.Visible = true;
                pnlNoState.Visible = false;

                txtname.Text = userName ?? "Anonymous";
                txtmail.Text = userEmail ?? "Not provided";
                lblDisplayName.Text = txtname.Text;
                lblSourceBadge.Text = "URL QueryString";
                lblTransportDetails.Text = "Passed as HTTP GET Query Parameters in the browser URL.";

                if (!string.IsNullOrEmpty(txtname.Text))
                {
                    lblInitial.Text = txtname.Text.Trim().Substring(0, 1).ToUpper();
                }

                btnClearCookie.Visible = false;
                return;
            }

            // 2. Check if stored in HttpCookie
            HttpCookie userCookie = Request.Cookies["UserDetails"];
            if (userCookie != null && !string.IsNullOrEmpty(userCookie["Name"]))
            {
                pnlContent.Visible = true;
                pnlNoState.Visible = false;

                txtname.Text = userCookie["Name"];
                txtmail.Text = userCookie["Email"] ?? "Not provided";
                lblDisplayName.Text = txtname.Text;
                lblSourceBadge.Text = "Browser HttpCookie";
                lblTransportDetails.Text = $"Retrieved from client HttpCookie 'UserDetails' (Created: {userCookie["CreatedAt"] ?? "Recent"}).";

                if (!string.IsNullOrEmpty(txtname.Text))
                {
                    lblInitial.Text = txtname.Text.Trim().Substring(0, 1).ToUpper();
                }

                btnClearCookie.Visible = true;
                return;
            }

            // 3. Neither present
            pnlContent.Visible = false;
            pnlNoState.Visible = true;
        }

        protected void btnClearCookie_Click(object sender, EventArgs e)
        {
            if (Request.Cookies["UserDetails"] != null)
            {
                HttpCookie expiredCookie = new HttpCookie("UserDetails")
                {
                    Expires = DateTime.Now.AddDays(-1)
                };
                Response.Cookies.Add(expiredCookie);
            }

            Response.Redirect(GetRouteUrl("RouteForStateUser", null) ?? "StateUser.aspx");
        }
    }
}
