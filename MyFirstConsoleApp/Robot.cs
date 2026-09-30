using System;

namespace MyFirstConsoleApp
{
    // Abstract Class
    public abstract class BaseRobot
    {
        public string RobotName { get; set; }

        public BaseRobot(string name)
        {
            RobotName = name;
        }

        public void StartRobot()
        {
            Console.WriteLine($"{RobotName} Started");
        }

        public abstract void PerformTask();
    }

    // Interface 1
    public interface INavigable
    {
        void Navigate();
    }

    // Interface 2
    public interface IChargeable
    {
        void ChargeBattery();
    }

    // Concrete Class
    public class WarehouseRobot : BaseRobot, INavigable, IChargeable
    {
        public WarehouseRobot(string name) : base(name)
        {
        }

        public override void PerformTask()
        {
            Console.WriteLine("Picking and Transporting Goods");
        }

        public void Navigate()
        {
            Console.WriteLine("Robot Navigating to Destination");
        }

        public void ChargeBattery()
        {
            Console.WriteLine("Robot Battery Charging");
        }
    }
}