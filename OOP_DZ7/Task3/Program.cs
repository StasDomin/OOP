using System;
using System.Collections.Generic;
using System.Text;

namespace Task3
{
    public interface IMediator
    {
        void Send(string message, Colleague sender);
    }

    public abstract class Colleague
    {
        protected IMediator _mediator;
        public string Name { get; }

        protected Colleague(IMediator mediator, string name)
        {
            _mediator = mediator;
            Name = name;
        }

        public void Send(string message)
        {
            Console.WriteLine($"[{Name}] надсилає: \"{message}\"");
            _mediator.Send(message, this);
        }

        public abstract void Notify(string message);
    }

    public class ConcreteMediator : IMediator
    {
        private readonly List<Colleague> _colleagues = new List<Colleague>();

        public void AddColleague(Colleague colleague)
        {
            _colleagues.Add(colleague);
        }

        public void Send(string message, Colleague sender)
        {
            foreach (Colleague colleague in _colleagues)
            {
                if (colleague != sender)
                {
                    colleague.Notify(message);
                }
            }
            Console.WriteLine();
        }
    }

    public class ConcreteColleague1 : Colleague
    {
        public ConcreteColleague1(IMediator mediator) : base(mediator, "Колега 1") { }
        public override void Notify(string message) => Console.WriteLine($"   -> {Name} отримав повідомлення: \"{message}\"");
    }

    public class ConcreteColleague2 : Colleague
    {
        public ConcreteColleague2(IMediator mediator) : base(mediator, "Колега 2") { }
        public override void Notify(string message) => Console.WriteLine($"   -> {Name} отримав повідомлення: \"{message}\"");
    }

    public class ConcreteColleague3 : Colleague
    {
        public ConcreteColleague3(IMediator mediator) : base(mediator, "Колега 3") { }
        public override void Notify(string message) => Console.WriteLine($"   -> {Name} отримав повідомлення: \"{message}\"");
    }

    internal class Program
    {
        private static void Main()
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.WriteLine("=== Завдання 3: Посередник (Mediator) ===\n");

            ConcreteMediator mediator = new ConcreteMediator();

            Colleague c1 = new ConcreteColleague1(mediator);
            Colleague c2 = new ConcreteColleague2(mediator);
            Colleague c3 = new ConcreteColleague3(mediator);

            mediator.AddColleague(c1);
            mediator.AddColleague(c2);
            mediator.AddColleague(c3);

            c2.Send("Fine, thanks!");
            c1.Send("How are you?");
            c3.Send("Hello!");
        }
    }
}
