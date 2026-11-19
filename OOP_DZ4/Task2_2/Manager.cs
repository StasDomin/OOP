using System;

namespace Task2_2
{
    public class Manager : Worker
    {
        private readonly Random _random;

        public Manager(string name) : base(name)
        {
            Position = "Менеджер";
            _random = new Random();
        }

        public override void FillWorkDay()
        {
            int firstCallsCount = _random.Next(1, 11);
            for (int i = 0; i < firstCallsCount; i++)
            {
                Call();
            }

            Relax();

            int secondCallsCount = _random.Next(1, 6);
            for (int i = 0; i < secondCallsCount; i++)
            {
                Call();
            }
        }
    }
}