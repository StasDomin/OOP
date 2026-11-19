using System;
using System.Text;

namespace Task5
{
    public interface IFigure
    {
        IFigure Clone();
        void GetInfo();
    }

    public class Rectangle : IFigure
    {
        public int Width { get; }
        public int Height { get; }

        public Rectangle(int width, int height)
        {
            Width = width;
            Height = height;
        }

        public IFigure Clone() => new Rectangle(Width, Height);

        public void GetInfo()
        {
            Console.WriteLine($"Прямокутник: ширина = {Width}, висота = {Height}");
        }
    }

    public class Triangle : IFigure
    {
        public double A { get; }
        public double B { get; }
        public double C { get; }

        public Triangle(double a, double b, double c)
        {
            if (a <= 0 || b <= 0 || c <= 0 || a + b <= c || a + c <= b || b + c <= a)
            {
                throw new ArgumentException("Порушено нерівність трикутника: сторони задані некоректно.");
            }

            A = a;
            B = b;
            C = c;
        }

        public IFigure Clone() => new Triangle(A, B, C);

        public void GetInfo()
        {
            Console.WriteLine($"Трикутник зі сторонами: a = {A}, b = {B}, c = {C}");
        }
    }

    internal class Program
    {
        private static void Main()
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.WriteLine("=== Завдання 5: Прототип (Prototype) ===\n");

            IFigure rect = new Rectangle(10, 20);
            IFigure clonedRect = rect.Clone();
            rect.GetInfo();
            clonedRect.GetInfo();
            Console.WriteLine($"Прямокутники — це різні об'єкти в пам'яті: {!ReferenceEquals(rect, clonedRect)}\n");

            IFigure triangle = new Triangle(3, 4, 5);
            IFigure clonedTriangle = triangle.Clone();
            triangle.GetInfo();
            clonedTriangle.GetInfo();
            Console.WriteLine($"Трикутники — це різні об'єкти в пам'яті: {!ReferenceEquals(triangle, clonedTriangle)}");
        }
    }
}
