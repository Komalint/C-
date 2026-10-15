using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UrlRouting
{
    public partial class SearchHistory : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["History"] != null)
                {
                    gvSearchHistory.DataSource = (List<SearchItem>)Session["History"];
                    gvSearchHistory.DataBind();
                }
            }
        }
    }
}