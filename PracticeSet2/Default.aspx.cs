using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PracticeSet2
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DisplayUserActive();
        }

        private void DisplayUserActive()
        {
            if (Application["user"] != null)
            {
                lblActiveUser.Text = Application["user"].ToString();
            }
            else
            {
                lblActiveUser.Text = "0";
            }
        }
    }
}