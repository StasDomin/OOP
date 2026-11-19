using System;
using System.Globalization;
using System.Text;

namespace Task3
{
    internal class Program
    {
        private static void Main()
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;

            try
            {
                Console.Write("Введіть курс долара (USD) до гривні: ");
                decimal usdRate = ReadDecimal();

                Console.Write("Введіть курс євро (EUR) до гривні: ");
                decimal eurRate = ReadDecimal();

                Converter converter = new Converter(usdRate, eurRate);

                while (true)
                {
                    Console.WriteLine("\n=== Конвертер валют ===");
                    Console.WriteLine("1. Гривні (UAH) -> Долари (USD)");
                    Console.WriteLine("2. Гривні (UAH) -> Євро (EUR)");
                    Console.WriteLine("3. Долари (USD) -> Гривні (UAH)");
                    Console.WriteLine("4. Євро (EUR) -> Гривні (UAH)");
                    Console.WriteLine("0. Вихід");
                    Console.Write("Оберіть операцію: ");

                    string? choice = Console.ReadLine();
                    if (choice == "0") break;

                    Console.Write("Введіть суму: ");
                    decimal amount = ReadDecimal();

                    switch (choice)
                    {
                        case "1":
                            Console.WriteLine($"Результат: {converter.ConvertUahToUsd(amount):F2} USD");
                            break;
                        case "2":
                            Console.WriteLine($"Результат: {converter.ConvertUahToEur(amount):F2} EUR");
                            break;
                        case "3":
                            Console.WriteLine($"Результат: {converter.ConvertUsdToUah(amount):F2} UAH");
                            break;
                        case "4":
                            Console.WriteLine($"Результат: {converter.ConvertEurToUah(amount):F2} UAH");
                            break;
                        default:
                            Console.WriteLine("Помилка: невідома операція.");
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка: {ex.Message}");
            }
        }

        private static decimal ReadDecimal()
        {
            string input = (Console.ReadLine() ?? string.Empty).Replace(',', '.');
            if (!decimal.TryParse(input, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal result))
            {
                throw new FormatException("введено некоректне число.");
            }
            return result;
        }
    }
}