using System;

namespace MyFirstConsoleApp
{
    public abstract class SmartDevice
    {
        public abstract void TurnOn();

        public abstract void TurnOff();

        public abstract double GetEnergyConsumption();
    }

    public class SmartLight : SmartDevice
    {
        public override void TurnOn()
        {
            Console.WriteLine("Smart Light Turned ON");
        }

        public override void TurnOff()
        {
            Console.WriteLine("Smart Light Turned OFF");
        }

        public override double GetEnergyConsumption()
        {
            return 15.5;
        }
    }

    public class SmartThermostat : SmartDevice
    {
        public override void TurnOn()
        {
            Console.WriteLine("Smart Thermostat Turned ON");
        }

        public override void TurnOff()
        {
            Console.WriteLine("Smart Thermostat Turned OFF");
        }

        public override double GetEnergyConsumption()
        {
            return 25.5;
        }
    }

    public class SmartAC : SmartDevice
    {
        public override void TurnOn()
        {
            Console.WriteLine("Smart AC Turned ON");
        }

        public override void TurnOff()
        {
            Console.WriteLine("Smart AC Turned OFF");
        }

        public override double GetEnergyConsumption()
        {
            return 50.0;
        }
    }
}