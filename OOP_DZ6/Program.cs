using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;

namespace JsonTask
{
    internal class Program
    {
        private static void Main()
        {
            Console.OutputEncoding = Encoding.UTF8;

            List<Book> books = new List<Book>
            {
                new Book
                {
                    PublishingHouseId = 2,
                    Title = "Підручник. Алгебра 8",
                    PublishingHouse = new PublishingHouse { Id = 2, Name = "ГІМНАЗІЯ", Adress = "Адреса 2" }
                },
                new Book
                {
                    PublishingHouseId = 1,
                    Title = "Щоденник нейрохірурга",
                    PublishingHouse = new PublishingHouse { Id = 1, Name = "Видавництво старого лева", Adress = "Адреса 1" }
                },
                new Book
                {
                    PublishingHouseId = 2,
                    Title = "Посібник. Алгебра 9",
                    PublishingHouse = new PublishingHouse { Id = 2, Name = "ГІМНАЗІЯ", Adress = "Адреса 2" }
                }
            };

            JsonSerializerOptions options = new JsonSerializerOptions
            {
                WriteIndented = true,
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            };

            string serializedJson = JsonSerializer.Serialize(books, options);
            Console.WriteLine("=== Пункти 2 і 3: Серіалізація без PublishingHouseId та з Name замість Title ===\n");
            Console.WriteLine(serializedJson);
        }
    }
}