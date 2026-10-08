using FleetManagementSystem.Helpers;
using FleetManagementSystem.Models;
using FleetManagementSystem.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Configuration;

namespace FleetManagementSystem
{
    class Program
    {
        static async Task Main(string[] args)
        {
            string connectionString =    ConfigurationManager.ConnectionStrings["DBCon"].ConnectionString;

            SqlHelper<Vehicle> sqlHelper =
    new SqlHelper<Vehicle>(connectionString);

            VehicleRepo vehicleRepo =
                new VehicleRepo(sqlHelper);

            

            bool exit = false;

            while (!exit)
            {
                Console.Clear();

                Console.WriteLine("===================================");
                Console.WriteLine(" FLEET MANAGEMENT SYSTEM ");
                Console.WriteLine("===================================");
                Console.WriteLine("1. Add Vehicle");
                Console.WriteLine("2. View Vehicle By Id");
                Console.WriteLine("3. View All Vehicles");
                Console.WriteLine("4. Update Vehicle");
                Console.WriteLine("5. Delete Vehicle");
                Console.WriteLine("6. Exit");
                Console.WriteLine("===================================");

                Console.Write("Enter Choice : ");

                if (!int.TryParse(Console.ReadLine(), out int choice))
                {
                    Console.WriteLine("Invalid Choice");
                    Console.ReadKey();
                    continue;
                }

                try
                {
                    switch (choice)
                    {
                        case 1:
                            await AddVehicle(vehicleRepo);
                            break;

                        case 2:
                            await GetVehicleById(vehicleRepo);
                            break;

                        case 3:
                            await GetAllVehicles(vehicleRepo);
                            break;

                        case 4:
                            await UpdateVehicle(vehicleRepo);
                            break;

                        case 5:
                            await DeleteVehicle(vehicleRepo);
                            break;

                        case 6:
                            exit = true;
                            break;

                        default:
                            Console.WriteLine("Invalid Choice");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error : {ex.Message}");
                }

                if (!exit)
                {
                    Console.WriteLine();
                    Console.WriteLine("Press Any Key To Continue...");
                    Console.ReadKey();
                }
            }
        }

        private static async Task AddVehicle(VehicleRepo vehicleRepo)
        {
            Vehicle vehicle = new Vehicle();

            Console.Write("VIN : ");
            vehicle.VIN = Console.ReadLine();

            Console.Write("Manufacturer : ");
            vehicle.Manufacturer = Console.ReadLine();

            Console.Write("Model : ");
            vehicle.Model = Console.ReadLine();

            Console.Write("Odometer Reading : ");
            vehicle.OdometerReading =
                Convert.ToDecimal(Console.ReadLine());

            Console.Write("Is Active : ");
            vehicle.IsActive = Console.ReadLine();

            vehicle.CreatedDate = DateTime.Now;

            await vehicleRepo.CreateAsync(vehicle);

            Console.WriteLine("Vehicle Added Successfully.");
        }

        private static async Task GetVehicleById(VehicleRepo vehicleRepo)
        {
            Console.Write("Enter Vehicle Id : ");

            int id = Convert.ToInt32(Console.ReadLine());

            Vehicle vehicle =
                await vehicleRepo.GetByIdAsync(id);

            if (vehicle == null)
            {
                Console.WriteLine("Vehicle Not Found.");
                return;
            }

            Console.WriteLine("--------------------------------");
            Console.WriteLine($"Id : {vehicle.Id}");
            Console.WriteLine($"VIN : {vehicle.VIN}");
            Console.WriteLine($"Manufacturer : {vehicle.Manufacturer}");
            Console.WriteLine($"Model : {vehicle.Model}");
            Console.WriteLine($"Odometer : {vehicle.OdometerReading}");
            Console.WriteLine($"Active : {vehicle.IsActive}");
            Console.WriteLine($"Created : {vehicle.CreatedDate}");
        }

        private static async Task GetAllVehicles(VehicleRepo vehicleRepo)
        {
            IEnumerable<Vehicle> vehicles =
                await vehicleRepo.GetAllAsync();

            Console.WriteLine();

            foreach (Vehicle vehicle in vehicles)
            {
                Console.WriteLine(
                    $"{vehicle.Id} | {vehicle.VIN} | {vehicle.Manufacturer} | {vehicle.Model}");
            }
        }

        private static async Task UpdateVehicle(VehicleRepo vehicleRepo)
        {
            Console.Write("Enter Vehicle Id : ");

            int id = Convert.ToInt32(Console.ReadLine());

            Vehicle vehicle =
                await vehicleRepo.GetByIdAsync(id);

            if (vehicle == null)
            {
                Console.WriteLine("Vehicle Not Found.");
                return;
            }

            Console.Write("New VIN : ");
            vehicle.VIN = Console.ReadLine();

            Console.Write("New Manufacturer : ");
            vehicle.Manufacturer = Console.ReadLine();

            Console.Write("New Model : ");
            vehicle.Model = Console.ReadLine();

            Console.Write("New Odometer Reading : ");
            vehicle.OdometerReading =
                Convert.ToDecimal(Console.ReadLine());

            Console.Write("Is Active : ");
            vehicle.IsActive = Console.ReadLine();

            await vehicleRepo.UpdateAsync(vehicle);

            Console.WriteLine("Vehicle Updated Successfully.");
        }

        private static async Task DeleteVehicle(VehicleRepo vehicleRepo)
        {
            Console.Write("Enter Vehicle Id : ");

            int id = Convert.ToInt32(Console.ReadLine());

            await vehicleRepo.DeleteAsync(id);

            Console.WriteLine("Vehicle Deleted Successfully.");
        }
    }
}