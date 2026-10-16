using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Xml.Linq;

namespace WebServicesPractice
{
    public partial class LargeDataSetToPaging : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLoadData_Click(object sender, EventArgs e)
        {
            LoadUsers();
        }

        public void LoadUsers()
        {
            GridViewReference.WebServiceSoapClient objUserDetails = new GridViewReference.WebServiceSoapClient();
            DataSet dsresult = new DataSet();
            XElement exelement = objUserDetails.GetMillionData();
            
            DataSet ds =new DataSet();
            using (XmlReader reader = exelement.CreateReader())
            {
                ds.ReadXml(reader);
            }
            
                gridViewLong.DataSource = ds.Tables[0];
                gridViewLong.DataBind();
            
        }
    
    
        public void gvPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gridViewLong.PageIndex = e.NewPageIndex;
            LoadUsers();
        }
    }
}