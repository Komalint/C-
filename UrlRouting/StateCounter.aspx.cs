using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UrlRouting
{
    public partial class StateCounter : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (ViewState["Clicks"] == null)
                {
                    ViewState["Clicks"] = 0;
                }
                txtViewStateCount.Text = ViewState["Clicks"].ToString();

                if (Session["GlobalClicks"] == null)
                {
                    Session["GlobalClicks"] = 0;
                }
                txtSessionCount.Text = Session["GlobalClicks"].ToString();
            }
        }

        protected void btnIncrementViewState_Click(object sender, EventArgs e)
        {
            int clicks = ViewState["Clicks"] != null ? (int)ViewState["Clicks"] : 0;
            clicks++;
            ViewState["Clicks"] = clicks;
            txtViewStateCount.Text = clicks.ToString();
        }

        protected void btnResetViewState_Click(object sender, EventArgs e)
        {
            ViewState["Clicks"] = 0;
            txtViewStateCount.Text = "0";
        }

        protected void btnIncrementSession_Click(object sender, EventArgs e)
        {
            int globalClicks = Session["GlobalClicks"] != null ? (int)Session["GlobalClicks"] : 0;
            globalClicks++;
            Session["GlobalClicks"] = globalClicks;
            txtSessionCount.Text = globalClicks.ToString();
        }

        protected void btnResetSession_Click(object sender, EventArgs e)
        {
            Session["GlobalClicks"] = 0;
            txtSessionCount.Text = "0";
        }

        protected void DrpListCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(DrpListCategory.SelectedValue))
            {
                lblSelectionResult.Text = $"Selected: {DrpListCategory.SelectedItem.Text}";
            }
            else
            {
                lblSelectionResult.Text = "No location selected";
            }
        }

        protected void btnGreeting_Click(object sender, EventArgs e)
        {
            lblGreetingMsg.Text = "Hello! Welcome to the ASP.NET WebForms Application.";
        }
    }
}
