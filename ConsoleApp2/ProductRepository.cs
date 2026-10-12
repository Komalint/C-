using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace ConsoleApp2
{
    public class ProductRepository
    {
        private readonly string _conStr;

        public ProductRepository(string conStr)
        {
            _conStr = conStr;
        }

        public void AddProduct(Product product)
        {
            string query = "insert into hra.Products (Name,Price, Stock) values(@name , @price, @stock)";

            using(SqlConnection conn = new SqlConnection(_conStr))
            {
                using(SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    cmd.Parameters.AddWithValue("@name", product.Name);
                    cmd.Parameters.AddWithValue("@price", product.Price);
                    cmd.Parameters.AddWithValue("@stock", product.Stock);

                    

                    int row = Convert.ToInt32(cmd.ExecuteNonQuery());
                    if (row > 0)
                    {
                        Console.WriteLine($"Successfully inserted {row} rows!!");
                    }
                    else { Console.WriteLine($"failed to insert rows!!"); }
                    
                }
            }
        }

        public List<Product> GetAllProducts()
        {
            var products = new List<Product>();
            string query = "select  * from hra.Products";
            using (SqlConnection con = new SqlConnection(_conStr))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            products.Add(new Product
                            {
                                Id = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                Price = reader.GetDecimal(2),
                                Stock = reader.GetInt32(3)
                            });
                        }
                    }
                }
            }
            return products;
        }

        public Product GetProductById(int id)
        {
            var product = new Product();
            string query = "select  * from hra.Products where Id =@id";
            using (SqlConnection con = new SqlConnection(_conStr))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    con.Open();
                    cmd.Parameters.AddWithValue("@id", id);
                    
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            product.Id = reader.GetInt32(0);
                            product.Name = reader.GetString(1);
                            product.Price = reader.GetDecimal(2);
                            product.Stock = reader.GetInt32(3);
                            
                        }
                    }
                }
            }
            return product;
        }

       

    public void UpdateProduct(Product product)
    {
        using (SqlConnection con = new SqlConnection(_conStr))
        {
            StringBuilder query = new StringBuilder("UPDATE hra.Products SET ");

            List<SqlParameter> parameters = new List<SqlParameter>();

            if (!string.IsNullOrWhiteSpace(product.Name))
            {
                query.Append("Name = @Name,");
                parameters.Add(new SqlParameter("@Name", product.Name));
            }

            if (product.Price != 0)
            {
                query.Append("Price = @Price,");
                parameters.Add(new SqlParameter("@Price", product.Price));
            }

            if (product.Stock != 0)
            {
                query.Append("Stock = @Stock,");
                parameters.Add(new SqlParameter("@Stock", product.Stock));
            }

            
            query.Length--;

            query.Append(" WHERE Id = @Id");

            parameters.Add(new SqlParameter("@Id", product.Id));

            using (SqlCommand cmd = new SqlCommand(query.ToString(), con))
            {
                    con.Open();
                    cmd.Parameters.AddRange(parameters.ToArray());

               

                int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        Console.WriteLine("Updated Successfully!!");
                    }
                    else
                    {
                        Console.WriteLine("Updateion Failed!!");
                    }
                
            }
        }
    }


        public void DeleteproductById(int id)
        {
            string query = "delete from hra.Products where Id = @id";
            using (SqlConnection con = new SqlConnection(_conStr))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    con.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        Console.WriteLine($"Deleted {rowsAffected} rows");
                    }
                }
            }
        }
 }
}
