using System;
using System.Text;

namespace Task3
{
    public interface IShape
    {
        int GetArea();
    }

    public class Rectangle : IShape
    {
        public int Width { get; set; }
        public int Height { get; set; }

        public Rectangle(int width, int height)
        {
            Width = width;
            Height = height;
        }

        public int GetArea() => Width * Height;
    }

    public class Square : IShape
    {
        public int Side { get; set; }

        public Square(int side)
        {
            Side = side;
        }

        public int GetArea() => Side * Side;
    }

    internal class Program
    {
        private static void Main()
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.WriteLine("=== Завдання 3: Принцип підстановки Лісков (LSP) ===\n");

            IShape rectangle = new Rectangle(5, 10);
            IShape square = new Square(5);

            Console.WriteLine($"Площа прямокутника (5 x 10): {rectangle.GetArea()}");
            Console.WriteLine($"Площа квадрата (5 x 5): {square.GetArea()}");
        }
    }
}
