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

        // ------------------------------------------------------------------------------------
        //public static void Main(string[] args)
        //{
        //    BikeShopRepo repo = new BikeShopRepo();

        //    while (true)
        //    {
        //        Console.WriteLine("\n=== Add New Bike ===");

        //        Console.Write("Enter Bike Name: ");
        //        string name = Console.ReadLine();

        //        int price;
        //        while (true)
        //        {
        //            Console.Write("Enter Bike Price: ");
        //            if (int.TryParse(Console.ReadLine(), out price) && price >= 0)
        //            {
        //                break;
        //            }
        //            Console.WriteLine("Invalid price! Please enter a valid number.");
        //        }

        //        BikeShap bike = new BikeShap
        //        {
        //            Name = name,
        //            Price = price
        //        };

        //        try
        //        {
        //            repo.AddBikeshop(bike);
        //            Console.WriteLine("Bike successfully saved to database!");
        //        }
        //        catch (Exception ex)
        //        {
        //            Console.WriteLine($"Database Error: {ex.Message}");
        //        }

        //        Console.Write("\nDo you want to add another bike? (y/n): ");
        //        string choice = Console.ReadLine()?.Trim().ToLower();
        //        if (choice != "y" && choice != "yes")
        //        {
        //            Console.WriteLine("\n=== All Bike Shops in Database ===");
        //            List<BikeShap> allBikes = repo.GetAllBikeShop();
        //            if (allBikes.Count == 0)
        //            {
        //                Console.WriteLine("No records found.");
        //            }
        //            else
        //            {
        //                foreach (var b in allBikes)
        //                {
        //                    Console.WriteLine($"Name: {b.Name} | Price: {b.Price}");
        //                }
        //            }
        //            break;
        //        }

        //    }

        //    Console.WriteLine("\nPress any key to exit...");
        //    Console.ReadKey();
        //}


        public static void Main(string[] args)
        {
            BikeShopRepo repo = new BikeShopRepo();

            while (true)
            {
                Console.Clear();

                Console.WriteLine("\n*************************** Operations ************************\n");
                Console.WriteLine("1 - Add Bike");
                Console.WriteLine("2 - Get All Bikes");
                Console.WriteLine("3 - Get Bike By Id");
                Console.WriteLine("4 - Update Bike");
                Console.WriteLine("5 - Delete Bike");
                Console.WriteLine("6 - Exit");
                Console.Write("\nEnter your choice: ");

                int choice;
                if (!int.TryParse(Console.ReadLine(), out choice))
                {
                    Console.WriteLine("Invalid choice!");
                    Console.ReadKey();
                    continue;
                }

                switch (choice)
                {
                    case 1:

                        Console.Write("Enter Bike Id: ");
                        int id = Convert.ToInt32(Console.ReadLine());
                        Console.Write("Enter Bike Name: ");
                        string name = Console.ReadLine();

                        Console.Write("Enter Bike Price: ");
                        double price = Convert.ToInt32(Console.ReadLine());

                        BikeShap bike = new BikeShap
                        {
                            Id = id,
                            Name = name,
                            Price = price
                        };

                        repo.AddBikeshop(bike);
                        Console.WriteLine("Bike Added Successfully!");
                        break;

                    case 2:
                        List<BikeShap> bikes = repo.GetAllBikeShop();

                        foreach (var b in bikes)
                        {
                            Console.WriteLine(
                                $"Id: {b.Id}, Name: {b.Name}, Price: {b.Price}");
                        }
                        break;

                    case 3:
                        Console.Write("Enter Bike Id: ");
                        int getId = Convert.ToInt32(Console.ReadLine());

                        BikeShap foundBike = repo.getBikebyID(getId);

                        if (foundBike != null)
                        {
                            Console.WriteLine(
                                $"Id: {foundBike.Id}, Name: {foundBike.Name}, Price: {foundBike.Price}");
                        }
                        else
                        {
                            Console.WriteLine("Bike not found.");
                        }
                        break;

                    case 4:
                        Console.Write("Enter Bike Id: ");
                        int updateId = Convert.ToInt32(Console.ReadLine());

                        Console.Write("Enter New Name: ");
                        string newName = Console.ReadLine();

                        Console.Write("Enter New Price: ");
                        double newPrice = Convert.ToDouble(Console.ReadLine());

                        repo.UpdateUserDetails(updateId, newName, newPrice);
                        Console.WriteLine("Bike Updated Successfully!");
                        break;

                    case 5:
                        Console.Write("Enter Bike Id: ");
                        int deleteId = Convert.ToInt32(Console.ReadLine());

                        repo.delBikeshop(deleteId);
                        Console.WriteLine("Bike Deleted Successfully!");
                        break;

                    case 6:
                        return;

                    default:
                        Console.WriteLine("Invalid Choice!");
                        break;
                }

                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
            }
        }



    }
}