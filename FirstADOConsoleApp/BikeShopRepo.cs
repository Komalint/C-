using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;

namespace FirstADOConsoleApp
{
    public class BikeShopRepo
    {
            string connectString = ConfigurationManager.ConnectionStrings["DBConn"].ConnectionString;
        public  List<BikeShap> BikeShopList = new List<BikeShap>();

        public  void AddBikeshop(BikeShap bikeshop)
        {


            using (SqlConnection con = new SqlConnection(connectString))
            {
                con.Open();
                string query = "insert into Bikeshop(Name,price) values(@Name,@price)";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Name", bikeshop.Name);
                    cmd.Parameters.AddWithValue("@price", bikeshop.price);
                    cmd.ExecuteNonQuery();

                }
            }
            BikeShopList.Add(bikeshop);
        }
    }
}
