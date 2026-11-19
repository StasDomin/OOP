using System;
using System.Text;

namespace Task2
{
    public class Pizza
    {
        public string Name { get; set; } = string.Empty;
        public string Dough { get; set; } = string.Empty;
        public string Sauce { get; set; } = string.Empty;
        public string Topping { get; set; } = string.Empty;

        public void ShowInfo()
        {
            Console.WriteLine($"Піца «{Name}»: тісто — {Dough}, соус — {Sauce}, начинка — {Topping}.");
        }
    }

    public interface IPizzaBuilder
    {
        void Reset();
        void BuildDough();
        void BuildSauce();
        void BuildTopping();
        Pizza GetPizza();
    }

    public class HawaiianPizzaBuilder : IPizzaBuilder
    {
        private Pizza _pizza = new Pizza();
        public void Reset() => _pizza = new Pizza { Name = "Гавайська" };
        public void BuildDough() => _pizza.Dough = "пухке дріжджове";
        public void BuildSauce() => _pizza.Sauce = "вершковий";
        public void BuildTopping() => _pizza.Topping = "курка та ананаси";
        public Pizza GetPizza() => _pizza;
    }

    public class SpicyPizzaBuilder : IPizzaBuilder
    {
        private Pizza _pizza = new Pizza();
        public void Reset() => _pizza = new Pizza { Name = "Пепероні" };
        public void BuildDough() => _pizza.Dough = "класичне";
        public void BuildSauce() => _pizza.Sauce = "гострий чилі";
        public void BuildTopping() => _pizza.Topping = "пепероні та халапеньйо";
        public Pizza GetPizza() => _pizza;
    }

    public class MargaritaPizzaBuilder : IPizzaBuilder
    {
        private Pizza _pizza = new Pizza();
        public void Reset() => _pizza = new Pizza { Name = "Маргарита" };
        public void BuildDough() => _pizza.Dough = "тонке тісто";
        public void BuildSauce() => _pizza.Sauce = "томатний соус";
        public void BuildTopping() => _pizza.Topping = "моцарела з базиліком";
        public Pizza GetPizza() => _pizza;
    }

    public class Waiter
    {
        public void ConstructPizza(IPizzaBuilder builder)
        {
            builder.Reset();
            builder.BuildDough();
            builder.BuildSauce();
            builder.BuildTopping();
        }
    }

    internal class Program
    {
        private static void Main()
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.WriteLine("=== Завдання 2: Будівельник (Builder) ===\n");

            Waiter waiter = new Waiter();

            IPizzaBuilder hawaiian = new HawaiianPizzaBuilder();
            waiter.ConstructPizza(hawaiian);
            hawaiian.GetPizza().ShowInfo();

            IPizzaBuilder spicy = new SpicyPizzaBuilder();
            waiter.ConstructPizza(spicy);
            spicy.GetPizza().ShowInfo();

            IPizzaBuilder margarita = new MargaritaPizzaBuilder();
            waiter.ConstructPizza(margarita);
            margarita.GetPizza().ShowInfo();
        }
    }
}
