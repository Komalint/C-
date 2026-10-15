using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UrlRouting
{
    public partial class ProductEntry : System.Web.UI.Page
    {
        private DataTable ProductsTable
        {
            get
            {
                if (ViewState["ProductsTable"] == null)
                {
                    DataTable dt = new DataTable();
                    dt.Columns.Add("Name", typeof(string));
                    dt.Columns.Add("Price", typeof(decimal));
                    dt.Columns.Add("Stock", typeof(int));
                    dt.Columns.Add("AddedAt", typeof(string));
                    ViewState["ProductsTable"] = dt;
                }
                return (DataTable)ViewState["ProductsTable"];
            }
            set
            {
                ViewState["ProductsTable"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindProductGrid();
            }
        }

        private void BindProductGrid()
        {
            gvProducts.DataSource = ProductsTable;
            gvProducts.DataBind();
        }

        protected void btnAddProductDetails_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid)
            {
                return;
            }

            string name = txtPname.Text.Trim();
            decimal price = Convert.ToDecimal(txtPprice.Text.Trim());
            int stock = Convert.ToInt32(txtPstocks.Text.Trim());

            bool dbSuccess = false;
            string dbMessage = string.Empty;

            var connSettings = ConfigurationManager.ConnectionStrings["DBConn"];
            if (connSettings != null && !string.IsNullOrWhiteSpace(connSettings.ConnectionString))
            {
                try
                {
                    string _conStr = connSettings.ConnectionString;
                    string query = "INSERT INTO hra.Products (Name, Price, Stock) VALUES (@name, @price, @stock)";

                    using (SqlConnection conn = new SqlConnection(_conStr))
                    {
                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@name", name);
                            cmd.Parameters.AddWithValue("@price", price);
                            cmd.Parameters.AddWithValue("@stock", stock);

                            conn.Open();
                            int rows = cmd.ExecuteNonQuery();
                            if (rows > 0)
                            {
                                dbSuccess = true;
                                dbMessage = "Record inserted into SQL Server table hra.Products.";
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    dbMessage = $"Database Notice: {ex.Message}";
                }
            }
            else
            {
                dbMessage = "Connection string DBConn not found.";
            }

            DataTable dt = ProductsTable;
            DataRow row = dt.NewRow();
            row["Name"] = name;
            row["Price"] = price;
            row["Stock"] = stock;
            row["AddedAt"] = DateTime.Now.ToString("T");
            dt.Rows.InsertAt(row, 0);
            ProductsTable = dt;

            BindProductGrid();

            if (dbSuccess)
            {
                lbldisplay.ForeColor = System.Drawing.Color.Green;
                lbldisplay.Text = $"Product '{name}' added successfully to database.";
            }
            else
            {
                lbldisplay.ForeColor = System.Drawing.Color.DarkBlue;
                lbldisplay.Text = $"Product '{name}' added. ({dbMessage})";
            }

            txtPname.Text = string.Empty;
            txtPprice.Text = string.Empty;
            txtPstocks.Text = string.Empty;
        }
    }
}
