using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace FirstADOConsoleApp
{
    internal class BikeshopProcRepo
    {
        string conStr = ConfigurationManager.ConnectionStrings["DBConn"].ConnectionString;
        public void getAllBikesByProcedure()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(conStr))
                {
                    //Create the Command Object
                    SqlCommand cmd = new SqlCommand()
                    {
                        CommandText = "udp_getAllBike_data",
                        Connection = connection,
                        CommandType = CommandType.StoredProcedure,
                    };
                    
                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    
                    while (reader.Read())
                    {
                        Console.WriteLine(
                            $"ID: {reader["Id"]}, " +
                            $"Name: {reader["Name"]}, " +
                            $"Price: {reader["Price"]}"
                        );
                    }

                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception Occurred: {ex.Message}");
            }
        }

        public void insertBikeDataByProc(string bikeName , double bikePrice)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(conStr))
                {
                    //Create the Command Object
                    SqlCommand cmd = new SqlCommand()
                    {
                        CommandText = "udp_insertBike_data",
                        Connection = connection,
                        CommandType = CommandType.StoredProcedure,
                    };
                   

                    cmd.Parameters.AddWithValue("@bName", bikeName);
                    cmd.Parameters.AddWithValue("@bPrice", bikePrice);


                    SqlParameter outParameter = new SqlParameter("@msg",SqlDbType.VarChar,100);
                    outParameter.Direction = ParameterDirection.Output;

                    cmd.Parameters.Add(outParameter);
                    connection.Open();

                    cmd.ExecuteNonQuery();

                    Console.WriteLine(outParameter.Value.ToString());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception Occurred: {ex.Message}");
            }
        }


        public void updateBikeDataByProc(int id,string bikeName, double bikePrice)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(conStr))
                {
                    //Create the Command Object
                    SqlCommand cmd = new SqlCommand()
                    {
                        CommandText = "udp_updatetBike_data",
                        Connection = connection,
                        CommandType = CommandType.StoredProcedure,
                    };
                    //SqlParameter param1 = new SqlParameter()
                    //{
                    //    ParameterName = "@id", //parameter name in actual sql query
                    //    SqlDbType = SqlDbType.Int,
                    //    Value = id,
                    //    Direction = ParameterDirection.Input,
                    //};
                    //cmd.Parameters.Add(param1);
                    //cmd.Parameters[0].Value = id;

                    cmd.Parameters.AddWithValue ("@id", id);
                    cmd.Parameters.AddWithValue("@bName", bikeName);
                    cmd.Parameters.AddWithValue("@bPrice", bikePrice);


                    SqlParameter outParameter = new SqlParameter("@msg", SqlDbType.VarChar, 100);
                    outParameter.Direction = ParameterDirection.Output;

                    cmd.Parameters.Add(outParameter);
                    connection.Open();

                    cmd.ExecuteNonQuery();
                    Console.WriteLine(outParameter.Value.ToString());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception Occurred: {ex.Message}");
            }
        }


        public void deleteBikeDataByProc(int id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(conStr))
                {
                    //Create the Command Object
                    SqlCommand cmd = new SqlCommand()
                    {
                        CommandText = "udp_deletedBikeDataById",
                        Connection = connection,
                        CommandType = CommandType.StoredProcedure,
                    };

                    cmd.Parameters.AddWithValue("@id", id);


                    SqlParameter outParameter = new SqlParameter("@msg", SqlDbType.VarChar, 100);
                    outParameter.Direction = ParameterDirection.Output;


                    cmd.Parameters.Add(outParameter);
                    connection.Open();

                    cmd.ExecuteNonQuery();
                    Console.WriteLine(outParameter.Value.ToString());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception Occurred: {ex.Message}");
            }
        }




    }
}
