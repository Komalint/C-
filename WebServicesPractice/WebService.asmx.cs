using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Xml;

namespace WebServicesPractice
{
    /// <summary>
    /// Summary description for WebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]

    public class WebService : System.Web.Services.WebService
    {
        string cnStr = ConfigurationManager.ConnectionStrings["DBConn"].ConnectionString ;
        [WebMethod]
        public XmlElement GetUserDetails(string userName)
        {
            SqlConnection con = new SqlConnection(cnStr);
            con.Open();
            SqlCommand cmd = new SqlCommand("select * from Users where UserName like @userName+'%'", con);
            cmd.Parameters.AddWithValue("@userName", userName);
            cmd.ExecuteNonQuery();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            // Create an instance of DataSet.
            DataSet ds = new DataSet();
            da.Fill(ds);
            con.Close();
            // Return the DataSet as an XmlElement.
            XmlDataDocument xmldata = new XmlDataDocument(ds);
            XmlElement xmlElement = xmldata.DocumentElement;
            return xmlElement;
        }

        [WebMethod]

        public XmlElement GetAllUsers()
        {
            SqlConnection con = new SqlConnection(cnStr);
            con.Open();
            string query = "select * from Users";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.ExecuteNonQuery();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            con.Close();
            XmlDataDocument xmldata = new XmlDataDocument(ds);
            XmlElement xmlElement = xmldata.DocumentElement;
            return xmlElement;
        }

    }
}
