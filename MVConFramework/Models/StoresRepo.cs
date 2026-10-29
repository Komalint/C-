using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace MVConFramework.Models
{
    public class StoresRepo
    {
        string _cnStr = ConfigurationManager.ConnectionStrings["DBConn"].ConnectionString;

        // Get All Stores
        public List<Stores> GetAllStores()
        {
            List<Stores> storeLi = new List<Stores>();

            using (SqlConnection con = new SqlConnection(_cnStr))
            {
                using (SqlCommand cmd = new SqlCommand("hra.usp_GetAllStores", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    con.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Stores store = new Stores();

                            store.StoreId = Convert.ToInt32(dr["StoreId"]);
                            store.StoreName = dr["StoreName"].ToString();
                            store.OwnerName = dr["OwnerName"].ToString();
                            store.StoreType = dr["StoreType"].ToString();
                            store.ContactNumber = dr["ContactNumber"].ToString();
                            store.Email = dr["Email"].ToString();
                            store.City = dr["City"].ToString();
                            store.State = dr["State"].ToString();
                            store.OpeningDate = Convert.ToDateTime(dr["OpeningDate"]);
                            store.IsActive = Convert.ToBoolean(dr["IsActive"]);

                            storeLi.Add(store);
                        }
                    }
                }
            }

            return storeLi;
        }

        // Get Store By Id
        public Stores GetStoreById(int storeId)
        {
            Stores store = null;

            using (SqlConnection con = new SqlConnection(_cnStr))
            {
                using (SqlCommand cmd = new SqlCommand("hra.usp_GetStoreById", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@StoreId", storeId);

                    con.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            store = new Stores();

                            store.StoreId = Convert.ToInt32(dr["StoreId"]);
                            store.StoreName = dr["StoreName"].ToString();
                            store.OwnerName = dr["OwnerName"].ToString();
                            store.StoreType = dr["StoreType"].ToString();
                            store.ContactNumber = dr["ContactNumber"].ToString();
                            store.Email = dr["Email"].ToString();
                            store.City = dr["City"].ToString();
                            store.State = dr["State"].ToString();
                            store.OpeningDate = Convert.ToDateTime(dr["OpeningDate"]);
                            store.IsActive = Convert.ToBoolean(dr["IsActive"]);
                        }
                    }
                }
            }

            return store;
        }

        // Add Store
        public string AddStore(Stores store)
        {
            using (SqlConnection con = new SqlConnection(_cnStr))
            {
                using (SqlCommand cmd = new SqlCommand("hra.usp_AddStore", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@StoreName", store.StoreName);
                    cmd.Parameters.AddWithValue("@OwnerName", store.OwnerName);
                    cmd.Parameters.AddWithValue("@StoreType", store.StoreType);
                    cmd.Parameters.AddWithValue("@ContactNumber", store.ContactNumber);
                    cmd.Parameters.AddWithValue("@Email", store.Email);
                    cmd.Parameters.AddWithValue("@City", store.City);
                    cmd.Parameters.AddWithValue("@State", store.State);
                    cmd.Parameters.AddWithValue("@OpeningDate", store.OpeningDate);
                    cmd.Parameters.AddWithValue("@IsActive", store.IsActive);

                    con.Open();
                    cmd.ExecuteNonQuery();

                    return "Inserted store details successfully";
                }
            }
        }

        // Update Store
        public string UpdateStore(Stores store)
        {
            using (SqlConnection con = new SqlConnection(_cnStr))
            {
                using (SqlCommand cmd = new SqlCommand("hra.usp_UpdateStore", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@StoreId", store.StoreId);
                    cmd.Parameters.AddWithValue("@StoreName", store.StoreName);
                    cmd.Parameters.AddWithValue("@OwnerName", store.OwnerName);
                    cmd.Parameters.AddWithValue("@StoreType", store.StoreType);
                    cmd.Parameters.AddWithValue("@ContactNumber", store.ContactNumber);
                    cmd.Parameters.AddWithValue("@Email", store.Email);
                    cmd.Parameters.AddWithValue("@City", store.City);
                    cmd.Parameters.AddWithValue("@State", store.State);
                    cmd.Parameters.AddWithValue("@OpeningDate", store.OpeningDate);
                    cmd.Parameters.AddWithValue("@IsActive", store.IsActive);

                    con.Open();
                    cmd.ExecuteNonQuery();

                    return "Updated store details successfully";
                }
            }
        }

        // Delete Store
        public string DeleteStore(int storeId)
        {
            using (SqlConnection con = new SqlConnection(_cnStr))
            {
                using (SqlCommand cmd = new SqlCommand("hra.usp_DeleteStore", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@StoreId", storeId);

                    con.Open();
                    cmd.ExecuteNonQuery();

                    return "Deleted store successfully";
                }
            }
        }
    }
}