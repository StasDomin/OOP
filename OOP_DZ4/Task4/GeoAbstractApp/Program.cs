using System;
using System.Collections.Generic;
using System.Text;

namespace GeoAbstractApp
{
    internal class Program
    {
        private static void Main()
        {
            Console.OutputEncoding = Encoding.UTF8;

            List<GeographicalObject> geoObjects = new List<GeographicalObject>
            {
                new River(50.4501, 30.5234, "Дніпро", "Найбільша річка України", 80.5, 2201),
                new Mountain(48.1603, 24.5000, "Говерла", "Найвища вершина Українських Карпат", 2061)
            };

            Console.WriteLine("=== Географічні об'єкти (Абстрактний клас) ===\n");
            foreach (GeographicalObject item in geoObjects)
            {
                Console.WriteLine(item.GetInfo());
                Console.WriteLine(new string('-', 40));
            }
        }
    }
}