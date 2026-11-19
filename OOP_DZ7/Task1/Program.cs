using System;
using System.Text;

namespace Task1
{
    public interface ICar
    {
        string GetModel();
    }

    public interface IEngine
    {
        string GetSpecs();
    }

    public interface ICarFactory
    {
        ICar CreateCar();
        IEngine CreateEngine();
    }

    public class Toyota : ICar
    {
        public string GetModel() => "Toyota Camry";
    }

    public class ToyotaEngine : IEngine
    {
        public string GetSpecs() => "Двигун Toyota 2.5L Hybrid";
    }

    public class ToyotaFactory : ICarFactory
    {
        public ICar CreateCar() => new Toyota();
        public IEngine CreateEngine() => new ToyotaEngine();
    }

    public class Ford : ICar
    {
        public string GetModel() => "Ford Mustang";
    }

    public class FordEngine : IEngine
    {
        public string GetSpecs() => "Двигун Ford 5.0L V8";
    }

    public class FordFactory : ICarFactory
    {
        public ICar CreateCar() => new Ford();
        public IEngine CreateEngine() => new FordEngine();
    }

    public class Mercedes : ICar
    {
        public string GetModel() => "Mercedes-Benz E-Class";
    }

    public class MercedesEngine : IEngine
    {
        public string GetSpecs() => "Двигун Mercedes 3.0L Turbo";
    }

    public class MercedesFactory : ICarFactory
    {
        public ICar CreateCar() => new Mercedes();
        public IEngine CreateEngine() => new MercedesEngine();
    }

    public class CarAssembler
    {
        public void Assemble(ICarFactory factory)
        {
            ICar car = factory.CreateCar();
            IEngine engine = factory.CreateEngine();
            Console.WriteLine($"Зібрано автомобіль: {car.GetModel()} | Комплектація: {engine.GetSpecs()}");
        }
    }

    internal class Program
    {
        private static void Main()
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.WriteLine("=== Завдання 1: Абстрактна фабрика (Abstract Factory) ===\n");

            CarAssembler assembler = new CarAssembler();
            assembler.Assemble(new ToyotaFactory());
            assembler.Assemble(new FordFactory());
            assembler.Assemble(new MercedesFactory());
        }
    }
}
