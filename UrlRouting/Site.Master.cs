using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UrlRouting
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            List<SearchItem> history;

            if (Session["History"] == null)
            {
                history = new List<SearchItem>();
            }
            else
            {
                history = (List<SearchItem>)Session["History"];
            }

            history.Add(new SearchItem
            {
                Id = history.Count + 1,
                SearchTerm = txtSearch.Text,
                SearchTime = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt")
            });

            Session["History"] = history;
        }
    }
}