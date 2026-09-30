using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFirstConsoleApp
{
    
    
        // Base Class
        public class Vehicle
        {
            protected string vehicleNumber;
            protected string gpsLocation;

            public Vehicle(string vehicleNumber, string gpsLocation)
            {
                this.vehicleNumber = vehicleNumber;
                this.gpsLocation = gpsLocation;
            }

            public void LogGPS()
            {
                Console.WriteLine($"Vehicle No : {vehicleNumber}");
                Console.WriteLine($"GPS Location : {gpsLocation}");
            }
        }

        // Derived Class (Level 2)
        public class MotorizedVehicle : Vehicle
        {
            protected double fuelCapacity;

            public MotorizedVehicle(string vehicleNumber,
                                    string gpsLocation,
                                    double fuelCapacity)
                : base(vehicleNumber, gpsLocation)
            {
                this.fuelCapacity = fuelCapacity;
            }

            public void StartEngine()
            {
                Console.WriteLine("Engine Started Successfully.");
            }
        }

        // Derived Class (Level 3)
        public class ElectricTruck : MotorizedVehicle
        {
            private double batteryPercentage;

            public ElectricTruck(string vehicleNumber,
                                 string gpsLocation,
                                 double fuelCapacity,
                                 double batteryPercentage)
                : base(vehicleNumber, gpsLocation, fuelCapacity)
            {
                this.batteryPercentage = batteryPercentage;
            }

            public double CalculateRange()
            {
                return batteryPercentage * 5;
            }

            public void DisplayDetails()
            {
                Console.WriteLine($"Battery Percentage : {batteryPercentage}%");
                Console.WriteLine($"Estimated Range : {CalculateRange()} KM");
            }
        }

       
    }
