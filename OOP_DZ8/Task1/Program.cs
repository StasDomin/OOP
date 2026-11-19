using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Task1
{
    public class Item
    {
        public string Name { get; set; }
        public decimal Price { get; set; }

        public Item(string name, decimal price)
        {
            Name = name;
            Price = price;
        }
    }

    public class Order
    {
        private readonly List<Item> _items = new List<Item>();

        public IReadOnlyList<Item> GetItems() => _items.AsReadOnly();

        public int GetItemCount() => _items.Count;

        public void AddItem(Item item) => _items.Add(item);

        public void DeleteItem(Item item) => _items.Remove(item);

        public decimal CalculateTotalSum() => _items.Sum(i => i.Price);
    }

    public class OrderPrint
    {
        public void PrintOrder(Order order)
        {
            Console.WriteLine("=== Склад замовлення ===");
            foreach (var item in order.GetItems())
            {
                Console.WriteLine($"- {item.Name}: {item.Price} грн");
            }
            Console.WriteLine($"Кількість товарів: {order.GetItemCount()}");
            Console.WriteLine($"Загальна сума: {order.CalculateTotalSum()} грн\n");
        }
    }

    public class OrderRepository
    {
        public void Load(int orderId) => Console.WriteLine($"[Repository] Замовлення #{orderId} завантажено зі сховища.");
        public void Save(Order order) => Console.WriteLine($"[Repository] Замовлення на суму {order.CalculateTotalSum()} грн збережено у сховище.");
        public void Update(Order order) => Console.WriteLine("[Repository] Дані замовлення оновлено у сховищі.");
        public void Delete(int orderId) => Console.WriteLine($"[Repository] Замовлення #{orderId} видалено зі сховища.");
    }

    internal class Program
    {
        private static void Main()
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.WriteLine("=== Завдання 1: Принцип єдиної відповідальності (Order) ===\n");

            Order order = new Order();
            order.AddItem(new Item("Ноутбук", 32000));
            order.AddItem(new Item("Мишка бездротова", 1200));

            OrderPrint printer = new OrderPrint();
            printer.PrintOrder(order);

            OrderRepository repository = new OrderRepository();
            repository.Save(order);
            repository.Load(101);
        }
    }
}
