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
        public List<BikeShap> BikeShopList = new List<BikeShap>();


        //insert
        public void AddBikeshop(BikeShap bikeshop)
        {

            using (SqlConnection con = new SqlConnection(connectString))
            {
                con.Open();
                string query = "insert into Bikeshop(Id, Name,Price) values(@Id, @Name,@price)";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Id", bikeshop.Id);
                    cmd.Parameters.AddWithValue("@Name", bikeshop.Name);
                    cmd.Parameters.AddWithValue("@price", bikeshop.Price);
                    cmd.ExecuteNonQuery();

                }
            }
            BikeShopList.Add(bikeshop);
        }


        // read
        public List<BikeShap> GetAllBikeShop()
        {
            List<BikeShap> bikelist = new List<BikeShap>();
            using (SqlConnection con = new SqlConnection(connectString))
            {
                con.Open();
                string query = "select * from Bikeshop";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            BikeShap bikeshop = new BikeShap()
                            {
                                Id = int.Parse(reader["Id"].ToString()),
                                Name = reader["Name"].ToString(),
                                Price = double.Parse(reader["Price"].ToString())
                            };
                            bikelist.Add(bikeshop);
                        }

                    }
                }
            }
            return bikelist;
        }

        // get by id

        public BikeShap getBikebyID(int id)
        {
            BikeShap b = null;
            using (SqlConnection con = new SqlConnection(connectString))
            {
                con.Open();
                string query = "select * from Bikeshop where Id =@Id";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {

                        if (reader.Read())
                        {
                            b = new BikeShap
                            {
                                Id = int.Parse(reader["Id"].ToString()),
                                Name = reader["Name"].ToString(),
                                Price = double.Parse(reader["Price"].ToString())
                            };
                            return b;


                        }
                        else
                        {
                            Console.WriteLine("Bike Shop not found");
                            return null;
                        }

                    }
                }
            }
        }


        // delete
        public void delBikeshop(int id)
        {
            using (SqlConnection con = new SqlConnection(connectString))
            {
                con.Open();
                string query = "delete from Bikeshop where Id =@Id";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.ExecuteNonQuery();
                    if (BikeShopList.Exists(b => b.Id == id))
                    {
                        BikeShopList.RemoveAll(b => b.Id == id);
                    }
                }
            }
        }


        // update

        public void UpdateUserDetails(int id, string name ="", double price=0.0) 
        {
            using (SqlConnection con = new SqlConnection(connectString))
            {
                con.Open();
                string query = "update Bikeshop set Name =@Name , price=@Price where Id = @Id";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.Parameters.AddWithValue("@Name",name);
                    cmd.Parameters.AddWithValue("@Price",price);
                    var rowAffected = cmd.ExecuteNonQuery();
                    if(rowAffected > 0)
                    {
                        Console.WriteLine("User details updated successfully");
                    }
                    else { Console.WriteLine("User not Found"); }

                }
            }
        }


        // storing in data table
        
        public void storeInDataTable()
        {
            try 
            {
                using (SqlConnection con = new SqlConnection(connectString))
                {
                    string query = "select * from  Bikeshop";
                    SqlDataAdapter da = new SqlDataAdapter(query, con);
                    da.SelectCommand.CommandType = CommandType.Text;
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    foreach (DataRow row in dt.Rows)
                    {
                        Console.WriteLine($"ID : {row["Id"]} , Bike Name : {row["Name"]}, Pricing : {row["Price"]} ");
                    }
                }
            }
            catch(Exception e) { 
                Console.WriteLine($"OOPs something went wrong{e.ToString()}");
            }
            
        }


        // storing in data set
        public void storeInDataSet()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectString))
                {
                    string query = "select * from  Bikeshop";
                    SqlDataAdapter da = new SqlDataAdapter(query, con);
                    da.SelectCommand.CommandType = CommandType.Text;
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    ds.Tables[0].TableName = "BikePrice";
                    foreach (DataRow row in ds.Tables["BikePrice"].Rows)
                    {
                        Console.WriteLine($"ID : {row["Id"]} , Bike Name : {row["Name"]}, Pricing : {row["Price"]} ");
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"OOPs something went wrong{e.ToString()}");
            }
            
        }


        // use procedure *simple procedure to get by id*
        public void getByProcedure(int id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectString))
                {
                    //Create the Command Object
                    SqlCommand cmd = new SqlCommand()
                    {
                        CommandText = "udp_getBike_data",
                        Connection = connection,
                        CommandType = CommandType.StoredProcedure,
                    };
                    SqlParameter param1 = new SqlParameter()
                    {
                        ParameterName = "@id", //parameter name in actual sql query
                        SqlDbType = SqlDbType.Int,
                        Value = id,
                        Direction = ParameterDirection.Input,
                    };

                    cmd.Parameters.Add(param1);
                    connection.Open();
                    cmd.Parameters[0].Value = id;
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        Console.WriteLine(
                            $"ID: {reader["Id"]}, " +
                            $"Name: {reader["Name"]}, " +
                            $"Price: {reader["Price"]}"
                        );
                    }
                    else
                    {
                        Console.WriteLine("Bike not found.");
                    }
                }
            }
            catch(Exception ex) 
            {
                Console.WriteLine($"Exception Occurred: {ex.Message}");
            }
        }



    }
}
