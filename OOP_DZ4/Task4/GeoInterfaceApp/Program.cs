using System;
using System.Collections.Generic;
using System.Text;

namespace GeoInterfaceApp
{
    internal class Program
    {
        private static void Main()
        {
            Console.OutputEncoding = Encoding.UTF8;

            List<IGeographicalObject> geoObjects = new List<IGeographicalObject>
            {
                new River(48.9472, 24.9836, "Дністер", "Річка на південному заході України", 95.0, 1362),
                new Mountain(48.1722, 24.4214, "Петрос", "Вершина масиву Чорногора в Карпатах", 2020)
            };

            Console.WriteLine("=== Географічні об'єкти (Інтерфейс) ===\n");
            foreach (IGeographicalObject item in geoObjects)
            {
                Console.WriteLine(item.GetInfo());
                Console.WriteLine(new string('-', 40));
            }
        }
    }
}