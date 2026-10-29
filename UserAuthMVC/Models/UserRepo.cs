using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace UserAuthMVC.Models
{
    public class UserRepo
    {
        private readonly string _cnStr;

        public UserRepo()
        {
            _cnStr = ConfigurationManager.ConnectionStrings["DBConn"]?.ConnectionString;
            if (string.IsNullOrEmpty(_cnStr))
            {
                // Fallback default connection string
                _cnStr = "Data Source=.;Initial Catalog=AssignmentDB;Persist Security Info=True;User ID=sa;Password=mcc#1234;TrustServerCertificate=True";
            }
        }

        // Register User
        public int RegisterUser(RegisterViewModel model, out string message)
        {
            message = string.Empty;

            using (SqlConnection con = new SqlConnection(_cnStr))
            {
                using (SqlCommand cmd = new SqlCommand("dbo.usp_RegisterUser", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@UserName", model.UserName.Trim());
                    cmd.Parameters.AddWithValue("@Email", model.Email.Trim().ToLower());
                    cmd.Parameters.AddWithValue("@Password", model.Password);
                    cmd.Parameters.AddWithValue("@PhoneNumber", (object)model.PhoneNumber ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Gender", (object)model.Gender ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@City", (object)model.City ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@State", (object)model.State ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Role", "User");

                    SqlParameter resultParam = new SqlParameter("@Result", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    cmd.Parameters.Add(resultParam);

                    SqlParameter messageParam = new SqlParameter("@Message", SqlDbType.NVarChar, 255)
                    {
                        Direction = ParameterDirection.Output
                    };
                    cmd.Parameters.Add(messageParam);

                    con.Open();
                    cmd.ExecuteNonQuery();

                    int result = Convert.ToInt32(resultParam.Value);
                    message = messageParam.Value?.ToString();

                    return result;
                }
            }
        }

        // Validate Login User
        public UserModel ValidateUser(string emailOrUsername, string password)
        {
            UserModel user = null;

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
                            user = new UserModel
                            {
                                UserId = Convert.ToInt32(dr["UserId"]),
                                UserName = dr["UserName"].ToString(),
                                Email = dr["Email"].ToString(),
                                PhoneNumber = dr["PhoneNumber"] != DBNull.Value ? dr["PhoneNumber"].ToString() : string.Empty,
                                Gender = dr["Gender"] != DBNull.Value ? dr["Gender"].ToString() : string.Empty,
                                City = dr["City"] != DBNull.Value ? dr["City"].ToString() : string.Empty,
                                State = dr["State"] != DBNull.Value ? dr["State"].ToString() : string.Empty,
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

        // Get User Details By Id
        public UserModel GetUserById(int userId)
        {
            UserModel user = null;

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
                            user = new UserModel
                            {
                                UserId = Convert.ToInt32(dr["UserId"]),
                                UserName = dr["UserName"].ToString(),
                                Email = dr["Email"].ToString(),
                                PhoneNumber = dr["PhoneNumber"] != DBNull.Value ? dr["PhoneNumber"].ToString() : string.Empty,
                                Gender = dr["Gender"] != DBNull.Value ? dr["Gender"].ToString() : string.Empty,
                                City = dr["City"] != DBNull.Value ? dr["City"].ToString() : string.Empty,
                                State = dr["State"] != DBNull.Value ? dr["State"].ToString() : string.Empty,
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

        // Check If Email Exists
        public bool IsEmailExists(string email)
        {
            using (SqlConnection con = new SqlConnection(_cnStr))
            {
                using (SqlCommand cmd = new SqlCommand("dbo.usp_CheckEmailExists", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Email", email.Trim().ToLower());

                    SqlParameter isExistsParam = new SqlParameter("@IsExists", SqlDbType.Bit)
                    {
                        Direction = ParameterDirection.Output
                    };
                    cmd.Parameters.Add(isExistsParam);

                    con.Open();
                    cmd.ExecuteNonQuery();

                    return Convert.ToBoolean(isExistsParam.Value);
                }
            }
        }

        // Update User Profile
        public bool UpdateUserProfile(UserModel model)
        {
            using (SqlConnection con = new SqlConnection(_cnStr))
            {
                using (SqlCommand cmd = new SqlCommand("dbo.usp_UpdateUserProfile", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@UserId", model.UserId);
                    cmd.Parameters.AddWithValue("@UserName", model.UserName.Trim());
                    cmd.Parameters.AddWithValue("@PhoneNumber", (object)model.PhoneNumber ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Gender", (object)model.Gender ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@City", (object)model.City ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@State", (object)model.State ?? DBNull.Value);

                    con.Open();
                    int rows = cmd.ExecuteNonQuery();

                    return rows > 0;
                }
            }
        }
    }
}
