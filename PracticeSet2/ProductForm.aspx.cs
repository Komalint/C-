using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace PracticeSet2
{
    public partial class ProductForm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           // btnAddProductDetails_Click(sender, e);
        }

        protected void btnAddProductDetails_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid)
            {
                return;
            }

            try
            {
                var connSettings = ConfigurationManager.ConnectionStrings["DBConn"];
                if (connSettings == null || string.IsNullOrWhiteSpace(connSettings.ConnectionString))
                {
                    lbldisplay.Text = "Error: Connection string 'DBConn' is missing in Web.config.";
                    lbldisplay.ForeColor = System.Drawing.Color.Red;
                    return;
                }

                string _conStr = connSettings.ConnectionString;
                string query = "insert into hra.Products (Name, Price, Stock) values(@name, @price, @stock)";

                using (SqlConnection conn = new SqlConnection(_conStr))
                {
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@name", txtPname.Text.Trim());
                        cmd.Parameters.AddWithValue("@price", Convert.ToDecimal(txtPprice.Text.Trim()));
                        cmd.Parameters.AddWithValue("@stock", Convert.ToInt32(txtPstocks.Text.Trim()));

                        conn.Open();
                        int row = cmd.ExecuteNonQuery();

                        if (row > 0)
                        {
                            lbldisplay.Text = "Data Added Successfully";
                            lbldisplay.ForeColor = System.Drawing.Color.Green;
                            txtPname.Text = string.Empty;
                            txtPprice.Text = string.Empty;
                            txtPstocks.Text = string.Empty;
                        }
                        else
                        {
                            lbldisplay.Text = "Record could not be inserted.";
                            lbldisplay.ForeColor = System.Drawing.Color.Red;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lbldisplay.Text = "Error: " + ex.Message;
                lbldisplay.ForeColor = System.Drawing.Color.Red;
            }
        }

        
    }
}