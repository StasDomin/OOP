using System;
using System.Text;

namespace Task6
{
    public interface ITransport
    {
        void Deliver();
    }

    public class Truck : ITransport
    {
        public void Deliver() => Console.WriteLine("Доставка вантажівкою по дорозі.");
    }

    public class Ship : ITransport
    {
        public void Deliver() => Console.WriteLine("Доставка кораблем по морю.");
    }

    public class Drone : ITransport
    {
        public void Deliver() => Console.WriteLine("Доставка дроном по повітрю.");
    }

    public abstract class Logistics
    {
        public abstract ITransport CreateTransport();

        public void PlanDelivery()
        {
            ITransport transport = CreateTransport();
            transport.Deliver();
        }
    }

    public class RoadLogistics : Logistics
    {
        public override ITransport CreateTransport() => new Truck();
    }

    public class SeaLogistics : Logistics
    {
        public override ITransport CreateTransport() => new Ship();
    }

    public class AirLogistics : Logistics
    {
        public override ITransport CreateTransport() => new Drone();
    }

    public interface ICelsiusSensor
    {
        double GetTemperatureCelsius();
    }

    public class LocalCelsiusSensor : ICelsiusSensor
    {
        public double GetTemperatureCelsius() => 22.0;
    }

    public class AmericanFahrenheitSensor
    {
        public double ReadFahrenheit() => 77.0;
    }

    public class FahrenheitSensorAdapter : ICelsiusSensor
    {
        private readonly AmericanFahrenheitSensor _fahrenheitSensor;

        public FahrenheitSensorAdapter(AmericanFahrenheitSensor fahrenheitSensor)
        {
            _fahrenheitSensor = fahrenheitSensor;
        }

        public double GetTemperatureCelsius()
        {
            double f = _fahrenheitSensor.ReadFahrenheit();
            return (f - 32.0) * 5.0 / 9.0;
        }
    }

    public class WeatherStation
    {
        public void DisplayTemperature(string sensorName, ICelsiusSensor sensor)
        {
            Console.WriteLine($"{sensorName}: {sensor.GetTemperatureCelsius():F1} °C");
        }
    }

    internal class Program
    {
        private static void Main()
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.WriteLine("=== Завдання 6.1: Фабричний метод (Factory Method) ===");

            Logistics road = new RoadLogistics();
            Logistics sea = new SeaLogistics();
            Logistics air = new AirLogistics();

            road.PlanDelivery();
            sea.PlanDelivery();
            air.PlanDelivery();

            Console.WriteLine("\n=== Завдання 6.2: Адаптер (Adapter) ===");

            WeatherStation station = new WeatherStation();
            ICelsiusSensor localSensor = new LocalCelsiusSensor();
            ICelsiusSensor adaptedSensor = new FahrenheitSensorAdapter(new AmericanFahrenheitSensor());

            station.DisplayTemperature("Локальний датчик", localSensor);
            station.DisplayTemperature("Американський датчик (через адаптер)", adaptedSensor);
        }
    }
}
