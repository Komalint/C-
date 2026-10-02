using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;

namespace FirstADOConsoleApp
{
    internal class Program
    {
        //static void Main(string[] args)
        //{

        //    // normal connection
        ////    string connectionString = "Data Source=.;Initial Catalog=AssignmentDB;Persist Security Info=True;User ID=sa;Password=mcc#1234 ";



        ////    string connectString = ConfigurationManager.ConnectionStrings["DBConn"].ConnectionString;



        ////    using (SqlConnection conn = new SqlConnection(connectString))
        ////    {
        ////        conn.Open();
        ////        string query = "select * from employees where substring(employee_name,1,1) ='E'";
        ////        SqlCommand command = new SqlCommand(query, conn);
        ////        using (SqlDataReader reader = command.ExecuteReader())
        ////        {
        ////            while (reader.Read())
        ////            {
        ////                Console.WriteLine($" Employee ID : {reader["employee_id"]}, Employee Name : {reader["employee_name"]}  ");
        ////            }
        ////        }
        ////    }

        ////    Console.WriteLine("Press any key to exit");
        ////    Console.ReadKey();



        //}



        public static void Main(string[] args)
        {
            BikeshopRepo repo = new BikeshopRepo();

            while (true)
            {
                Console.WriteLine("\n=== Add New Bike ===");

                Console.Write("Enter Bike Name: ");
                string name = Console.ReadLine();

                int price;
                while (true)
                {
                    Console.Write("Enter Bike Price: ");
                    if (int.TryParse(Console.ReadLine(), out price) && price >= 0)
                    {
                        break;
                    }
                    Console.WriteLine("Invalid price! Please enter a valid number.");
                }

                Bikeshop bike = new Bikeshop
                {
                    Name = name,
                    Price = price
                };

                try
                {
                    repo.AddBikeShop(bike);
                    Console.WriteLine("Bike successfully saved to database!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Database Error: {ex.Message}");
                }

                Console.Write("\nDo you want to add another bike? (y/n): ");
                string choice = Console.ReadLine()?.Trim().ToLower();
                if (choice != "y" && choice != "yes")
                {
                    Console.WriteLine("\n=== All Bike Shops in Database ===");
                    List<Bikeshop> allBikes = repo.GetAllBikeShop();
                    if (allBikes.Count == 0)
                    {
                        Console.WriteLine("No records found.");
                    }
                    else
                    {
                        foreach (var b in allBikes)
                        {
                            Console.WriteLine($"Name: {b.Name} | Price: {b.Price}");
                        }
                    }
                    break;
                }

            }

            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }

    }
}
    }
}
