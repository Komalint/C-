using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using MVConFramework.ViewModels;

namespace MVConFramework.Models
{
    public class UserRepo
    {
        private readonly string _cnStr = ConfigurationManager.ConnectionStrings["DBConn"].ConnectionString;

        // 1. Authenticate / Login User
        public User ValidateUser(string emailOrUsername, string password)
        {
            User user = null;

            using (SqlConnection con = new SqlConnection(_cnStr))
            {
                using (SqlCommand cmd = new SqlCommand("dbo.usp_LoginUser", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@EmailOrUsername", emailOrUsername.Trim());
                    cmd.Parameters.AddWithValue("@Password", password);

                    con.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            user = new User
                            {
                                UserId = Convert.ToInt32(dr["UserId"]),
                                UserName = dr["UserName"].ToString(),
                                Email = dr["Email"].ToString(),
                                PhoneNumber = dr["PhoneNumber"] != DBNull.Value ? dr["PhoneNumber"].ToString() : null,
                                Gender = dr["Gender"] != DBNull.Value ? dr["Gender"].ToString() : null,
                                City = dr["City"] != DBNull.Value ? dr["City"].ToString() : null,
                                State = dr["State"] != DBNull.Value ? dr["State"].ToString() : null,
                                Role = dr["Role"] != DBNull.Value ? dr["Role"].ToString() : "User",
                                IsActive = Convert.ToBoolean(dr["IsActive"]),
                                CreatedDate = Convert.ToDateTime(dr["CreatedDate"])
                            };
                        }
                    }
                }
            }

            return user;
        }

        // 2. Register / Sign Up User
        public bool RegisterUser(RegisterViewModel model, out string message)
        {
            message = string.Empty;

            using (SqlConnection con = new SqlConnection(_cnStr))
            {
                using (SqlCommand cmd = new SqlCommand("dbo.usp_RegisterUser", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@UserName", model.UserName.Trim());
                    cmd.Parameters.AddWithValue("@Email", model.Email.Trim());
                    cmd.Parameters.AddWithValue("@Password", model.Password);
                    cmd.Parameters.AddWithValue("@PhoneNumber", string.IsNullOrWhiteSpace(model.PhoneNumber) ? (object)DBNull.Value : model.PhoneNumber.Trim());
                    cmd.Parameters.AddWithValue("@Gender", string.IsNullOrWhiteSpace(model.Gender) ? (object)DBNull.Value : model.Gender.Trim());
                    cmd.Parameters.AddWithValue("@City", string.IsNullOrWhiteSpace(model.City) ? (object)DBNull.Value : model.City.Trim());
                    cmd.Parameters.AddWithValue("@State", string.IsNullOrWhiteSpace(model.State) ? (object)DBNull.Value : model.State.Trim());
                    cmd.Parameters.AddWithValue("@Role", string.IsNullOrWhiteSpace(model.Role) ? "User" : model.Role.Trim());

                    SqlParameter pResult = new SqlParameter("@Result", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    cmd.Parameters.Add(pResult);

                    SqlParameter pMessage = new SqlParameter("@Message", SqlDbType.NVarChar, 255)
                    {
                        Direction = ParameterDirection.Output
                    };
                    cmd.Parameters.Add(pMessage);

                    con.Open();
                    cmd.ExecuteNonQuery();

                    int result = Convert.ToInt32(pResult.Value);
                    message = pMessage.Value != DBNull.Value ? pMessage.Value.ToString() : string.Empty;

                    return result > 0;
                }
            }
        }

        // 3. Get User By Id
        public User GetUserById(int userId)
        {
            User user = null;

            using (SqlConnection con = new SqlConnection(_cnStr))
            {
                using (SqlCommand cmd = new SqlCommand("dbo.usp_GetUserById", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserId", userId);

                    con.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            user = new User
                            {
                                UserId = Convert.ToInt32(dr["UserId"]),
                                UserName = dr["UserName"].ToString(),
                                Email = dr["Email"].ToString(),
                                PhoneNumber = dr["PhoneNumber"] != DBNull.Value ? dr["PhoneNumber"].ToString() : null,
                                Gender = dr["Gender"] != DBNull.Value ? dr["Gender"].ToString() : null,
                                City = dr["City"] != DBNull.Value ? dr["City"].ToString() : null,
                                State = dr["State"] != DBNull.Value ? dr["State"].ToString() : null,
                                Role = dr["Role"] != DBNull.Value ? dr["Role"].ToString() : "User",
                                IsActive = Convert.ToBoolean(dr["IsActive"]),
                                CreatedDate = Convert.ToDateTime(dr["CreatedDate"])
                            };
                        }
                    }
                }
            }

            return user;
        }

        // 4. Get Dashboard Statistics & Recent Orders
        public DashboardViewModel GetDashboardStats()
        {
            DashboardViewModel stats = new DashboardViewModel();

            try
            {
                using (SqlConnection con = new SqlConnection(_cnStr))
                {
                    using (SqlCommand cmd = new SqlCommand("dbo.usp_GetDashboardStats", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        con.Open();

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            // 1st Result: KPI Counts
                            if (dr.Read())
                            {
                                stats.TotalStores = dr["TotalStores"] != DBNull.Value ? Convert.ToInt32(dr["TotalStores"]) : 0;
                                stats.TotalOrders = dr["TotalOrders"] != DBNull.Value ? Convert.ToInt32(dr["TotalOrders"]) : 0;
                                stats.TotalRevenue = dr["TotalRevenue"] != DBNull.Value ? Convert.ToDecimal(dr["TotalRevenue"]) : 0;
                                stats.TotalUsers = dr["TotalUsers"] != DBNull.Value ? Convert.ToInt32(dr["TotalUsers"]) : 0;
                            }

                            // 2nd Result: Recent 5 Orders
                            if (dr.NextResult())
                            {
                                while (dr.Read())
                                {
                                    Orders ord = new Orders
                                    {
                                        OrderId = Convert.ToInt32(dr["OrderId"]),
                                        StoreId = Convert.ToInt32(dr["StoreId"]),
                                        StoreName = dr["StoreName"].ToString(),
                                        CustomerName = dr["CustomerName"].ToString(),
                                        ProductName = dr["ProductName"].ToString(),
                                        OrderDate = Convert.ToDateTime(dr["OrderDate"]),
                                        Quantity = Convert.ToInt32(dr["Quantity"]),
                                        TotalAmount = Convert.ToInt32(dr["TotalAmount"]),
                                        PaymentMethod = dr["PaymentMethod"].ToString(),
                                        DeliveryCity = dr["DeliveryCity"].ToString()
                                    };

                                    stats.RecentOrders.Add(ord);
                                }
                            }
                        }
                    }
                }
            }
            catch
            {
                // Fallback: Query directly or use existing repos if stored proc is not yet executed
                try
                {
                    using (SqlConnection con = new SqlConnection(_cnStr))
                    {
                        con.Open();

                        using (SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM hra.Stores WHERE IsActive = 1", con))
                        {
                            stats.TotalStores = Convert.ToInt32(cmd.ExecuteScalar() ?? 0);
                        }

                        using (SqlCommand cmd = new SqlCommand("SELECT COUNT(*), ISNULL(SUM(TotalAmount),0) FROM hra.Orders", con))
                        {
                            using (SqlDataReader rdr = cmd.ExecuteReader())
                            {
                                if (rdr.Read())
                                {
                                    stats.TotalOrders = Convert.ToInt32(rdr[0]);
                                    stats.TotalRevenue = Convert.ToDecimal(rdr[1]);
                                }
                            }
                        }

                        using (SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM dbo.UserTbl WHERE IsActive = 1", con))
                        {
                            stats.TotalUsers = Convert.ToInt32(cmd.ExecuteScalar() ?? 0);
                        }

                        using (SqlCommand cmd = new SqlCommand("SELECT TOP 5 OrderId, StoreId, StoreName, CustomerName, ProductName, OrderDate, Quantity, TotalAmount, PaymentMethod, DeliveryCity FROM hra.Orders ORDER BY OrderId DESC", con))
                        {
                            using (SqlDataReader dr = cmd.ExecuteReader())
                            {
                                while (dr.Read())
                                {
                                    Orders ord = new Orders
                                    {
                                        OrderId = Convert.ToInt32(dr["OrderId"]),
                                        StoreId = Convert.ToInt32(dr["StoreId"]),
                                        StoreName = dr["StoreName"].ToString(),
                                        CustomerName = dr["CustomerName"].ToString(),
                                        ProductName = dr["ProductName"].ToString(),
                                        OrderDate = Convert.ToDateTime(dr["OrderDate"]),
                                        Quantity = Convert.ToInt32(dr["Quantity"]),
                                        TotalAmount = Convert.ToInt32(dr["TotalAmount"]),
                                        PaymentMethod = dr["PaymentMethod"].ToString(),
                                        DeliveryCity = dr["DeliveryCity"].ToString()
                                    };
                                    stats.RecentOrders.Add(ord);
                                }
                            }
                        }
                    }
                }
                catch
                {
                    // If DB is unreachable, return empty stats without crashing
                }
            }

            return stats;
        }
    }
}
