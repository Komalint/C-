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
    public partial class GettingDataFromWebService : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }    

        public void LoadUsers(string st)
        {
            localhost.WebService objUserDetails = new localhost.WebService();
            DataSet dsresult = new DataSet();
            XmlElement exelement = objUserDetails.GetUserDetails(st);
            if (exelement != null)
            {
                XmlNodeReader nodereader = new XmlNodeReader(exelement);
                dsresult.ReadXml(nodereader, XmlReadMode.Auto);
                gvUserDetails.DataSource = dsresult;
                gvUserDetails.DataBind();
            }
            else
            {
                gvUserDetails.DataSource = null;
                gvUserDetails.DataBind();
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            LoadUsers(txtUserName.Text);
        }
    }

    
}