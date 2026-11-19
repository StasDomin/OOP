using System;
using System.Text;

namespace Task4
{
    public interface IPrice
    {
        void SetPrice(double price);
    }

    public interface IDiscount
    {
        void ApplyDiscount(string discount);
        void ApplyPromocode(string promocode);
    }

    public interface IColor
    {
        void SetColor(byte color);
    }

    public interface ISize
    {
        void SetSize(byte size);
    }

    public class Book : IPrice
    {
        public string Title { get; set; }
        public double Price { get; private set; }

        public Book(string title)
        {
            Title = title;
        }

        public void SetPrice(double price)
        {
            Price = price;
            Console.WriteLine($"[Книга «{Title}»] Встановлено ціну: {Price} грн");
        }
    }

    public class Outerwear : IPrice, IDiscount, IColor, ISize
    {
        public string Name { get; set; }
        public double Price { get; private set; }
        public byte Color { get; private set; }
        public byte Size { get; private set; }

        public Outerwear(string name)
        {
            Name = name;
        }

        public void SetPrice(double price)
        {
            Price = price;
            Console.WriteLine($"[Одяг «{Name}»] Встановлено ціну: {Price} грн");
        }

        public void ApplyDiscount(string discount)
        {
            Console.WriteLine($"[Одяг «{Name}»] Застосовано знижку: {discount}");
        }

        public void ApplyPromocode(string promocode)
        {
            Console.WriteLine($"[Одяг «{Name}»] Застосовано промокод: {promocode}");
        }

        public void SetColor(byte color)
        {
            Color = color;
            Console.WriteLine($"[Одяг «{Name}»] Встановлено код кольору: {Color}");
        }

        public void SetSize(byte size)
        {
            Size = size;
            Console.WriteLine($"[Одяг «{Name}»] Встановлено розмір: {Size}");
        }
    }

    internal class Program
    {
        private static void Main()
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.WriteLine("=== Завдання 4: Принцип розділення інтерфейсу (ISP) ===\n");

            Book book = new Book("Чистий код");
            book.SetPrice(650);

            Console.WriteLine();

            Outerwear jacket = new Outerwear("Зимова куртка");
            jacket.SetPrice(4200);
            jacket.SetColor(12);
            jacket.SetSize(48);
            jacket.ApplyDiscount("15%");
            jacket.ApplyPromocode("WINTER2026");
        }
    }
}
