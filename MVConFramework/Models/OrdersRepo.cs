using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace MVConFramework.Models
{
    public class OrdersRepo
    {
        string _cnStr = ConfigurationManager.ConnectionStrings["DBConn"].ConnectionString;

        // Get All Orders
        public List<Orders> GetAllOrders()
        {
            List<Orders> orderLi = new List<Orders>();

            using (SqlConnection con = new SqlConnection(_cnStr))
            {
                using (SqlCommand cmd = new SqlCommand("hra.usp_GetAllOrders", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    con.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Orders order = new Orders();

                            order.OrderId = Convert.ToInt32(dr["OrderId"]);
                            order.StoreId = Convert.ToInt32(dr["StoreId"]);
                            order.StoreName = dr["StoreName"].ToString();
                            order.CustomerName = dr["CustomerName"].ToString();
                            order.ProductName = dr["ProductName"].ToString();
                            order.OrderDate = Convert.ToDateTime(dr["OrderDate"]);
                            order.Quantity = Convert.ToInt32(dr["Quantity"]);
                            order.TotalAmount = Convert.ToInt32(dr["TotalAmount"]);
                            order.PaymentMethod = dr["PaymentMethod"].ToString();
                            order.DeliveryCity = dr["DeliveryCity"].ToString();

                            orderLi.Add(order);
                        }
                    }
                }
            }

            return orderLi;
        }

        // Get Order By Id
        public Orders GetOrderById(int orderId)
        {
            Orders order = null;

            using (SqlConnection con = new SqlConnection(_cnStr))
            {
                using (SqlCommand cmd = new SqlCommand("hra.usp_GetOrderById", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@OrderId", orderId);

                    con.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            order = new Orders();

                            order.OrderId = Convert.ToInt32(dr["OrderId"]);
                            order.StoreId = Convert.ToInt32(dr["StoreId"]);
                            order.StoreName = dr["StoreName"].ToString();
                            order.CustomerName = dr["CustomerName"].ToString();
                            order.ProductName = dr["ProductName"].ToString();
                            order.OrderDate = Convert.ToDateTime(dr["OrderDate"]);
                            order.Quantity = Convert.ToInt32(dr["Quantity"]);
                            order.TotalAmount = Convert.ToInt32(dr["TotalAmount"]);
                            order.PaymentMethod = dr["PaymentMethod"].ToString();
                            order.DeliveryCity = dr["DeliveryCity"].ToString();
                        }
                    }
                }
            }

            return order;
        }

        // Get Orders By Store Id
        public List<Orders> GetOrdersByStoreId(int storeId)
        {
            List<Orders> orderLi = new List<Orders>();

            using (SqlConnection con = new SqlConnection(_cnStr))
            {
                using (SqlCommand cmd = new SqlCommand("hra.usp_GetOrdersByStoreId", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@StoreId", storeId);

                    con.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Orders order = new Orders();

                            order.OrderId = Convert.ToInt32(dr["OrderId"]);
                            order.StoreId = Convert.ToInt32(dr["StoreId"]);
                            order.StoreName = dr["StoreName"].ToString();
                            order.CustomerName = dr["CustomerName"].ToString();
                            order.ProductName = dr["ProductName"].ToString();
                            order.OrderDate = Convert.ToDateTime(dr["OrderDate"]);
                            order.Quantity = Convert.ToInt32(dr["Quantity"]);
                            order.TotalAmount = Convert.ToInt32(dr["TotalAmount"]);
                            order.PaymentMethod = dr["PaymentMethod"].ToString();
                            order.DeliveryCity = dr["DeliveryCity"].ToString();

                            orderLi.Add(order);
                        }
                    }
                }
            }

            return orderLi;
        }

        // Add Order
        public string AddOrder(Orders order)
        {
            using (SqlConnection con = new SqlConnection(_cnStr))
            {
                using (SqlCommand cmd = new SqlCommand("hra.usp_AddOrder", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@StoreId", order.StoreId);
                    cmd.Parameters.AddWithValue("@StoreName", order.StoreName);
                    cmd.Parameters.AddWithValue("@CustomerName", order.CustomerName);
                    cmd.Parameters.AddWithValue("@ProductName", order.ProductName);
                    cmd.Parameters.AddWithValue("@OrderDate", order.OrderDate);
                    cmd.Parameters.AddWithValue("@Quantity", order.Quantity);
                    cmd.Parameters.AddWithValue("@TotalAmount", order.TotalAmount);
                    cmd.Parameters.AddWithValue("@PaymentMethod", order.PaymentMethod);
                    cmd.Parameters.AddWithValue("@DeliveryCity", order.DeliveryCity);

                    con.Open();
                    cmd.ExecuteNonQuery();

                    return "Inserted order details successfully";
                }
            }
        }

        // Update Order
        public string UpdateOrder(Orders order)
        {
            using (SqlConnection con = new SqlConnection(_cnStr))
            {
                using (SqlCommand cmd = new SqlCommand("hra.usp_UpdateOrder", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@OrderId", order.OrderId);
                    cmd.Parameters.AddWithValue("@StoreId", order.StoreId);
                    cmd.Parameters.AddWithValue("@StoreName", order.StoreName);
                    cmd.Parameters.AddWithValue("@CustomerName", order.CustomerName);
                    cmd.Parameters.AddWithValue("@ProductName", order.ProductName);
                    cmd.Parameters.AddWithValue("@OrderDate", order.OrderDate);
                    cmd.Parameters.AddWithValue("@Quantity", order.Quantity);
                    cmd.Parameters.AddWithValue("@TotalAmount", order.TotalAmount);
                    cmd.Parameters.AddWithValue("@PaymentMethod", order.PaymentMethod);
                    cmd.Parameters.AddWithValue("@DeliveryCity", order.DeliveryCity);

                    con.Open();
                    cmd.ExecuteNonQuery();

                    return "Updated order details successfully";
                }
            }
        }

        // Delete Order
        public string DeleteOrder(int orderId)
        {
            using (SqlConnection con = new SqlConnection(_cnStr))
            {
                using (SqlCommand cmd = new SqlCommand("hra.usp_DeleteOrder", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@OrderId", orderId);

                    con.Open();
                    cmd.ExecuteNonQuery();

                    return "Deleted order successfully";
                }
            }
        }
    }
}