using System;
using System.Collections.Generic;
using System.Text;

namespace Task2_2
{
    internal class Program
    {
        private static void Main()
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;

            List<Team> teams = new List<Team>();

            while (true)
            {
                Console.WriteLine("\n=== Управління командами ===");
                Console.WriteLine("1. Створити команду");
                Console.WriteLine("2. Додати співробітника в команду");
                Console.WriteLine("3. Показати коротку інформацію про команду");
                Console.WriteLine("4. Показати детальну інформацію про команду");
                Console.WriteLine("0. Вихід");
                Console.Write("Оберіть дію: ");

                string? choice = Console.ReadLine();

                if (choice == "0")
                {
                    break;
                }

                switch (choice)
                {
                    case "1":
                        Console.Write("Введіть назву команди: ");
                        string? teamName = Console.ReadLine();
                        if (string.IsNullOrWhiteSpace(teamName))
                        {
                            Console.WriteLine("Помилка: назва команди не може бути порожньою.");
                            break;
                        }
                        teams.Add(new Team(teamName.Trim()));
                        Console.WriteLine($"Команду '{teamName.Trim()}' успішно створено.");
                        break;

                    case "2":
                        Team? teamToAdd = SelectTeam(teams);
                        if (teamToAdd == null) break;

                        Console.Write("Введіть ім'я співробітника: ");
                        string? workerName = Console.ReadLine();
                        if (string.IsNullOrWhiteSpace(workerName))
                        {
                            Console.WriteLine("Помилка: ім'я не може бути порожнім.");
                            break;
                        }

                        Console.WriteLine("Оберіть посаду (1 - Розробник, 2 - Менеджер): ");
                        string? roleChoice = Console.ReadLine();

                        if (roleChoice == "1")
                        {
                            teamToAdd.AddWorker(new Developer(workerName.Trim()));
                            Console.WriteLine($"Розробника {workerName.Trim()} додано до команди '{teamToAdd.TeamName}'.");
                        }
                        else if (roleChoice == "2")
                        {
                            teamToAdd.AddWorker(new Manager(workerName.Trim()));
                            Console.WriteLine($"Менеджера {workerName.Trim()} додано до команди '{teamToAdd.TeamName}'.");
                        }
                        else
                        {
                            Console.WriteLine("Помилка: невідома посада.");
                        }
                        break;

                    case "3":
                        Team? teamShort = SelectTeam(teams);
                        if (teamShort != null)
                        {
                            Console.WriteLine("\n" + teamShort.GetShortInfo());
                        }
                        break;

                    case "4":
                        Team? teamDetailed = SelectTeam(teams);
                        if (teamDetailed != null)
                        {
                            Console.WriteLine("\n" + teamDetailed.GetDetailedInfo());
                        }
                        break;

                    default:
                        Console.WriteLine("Невідома команда. Введіть число від 0 до 4.");
                        break;
                }
            }
        }

        private static Team? SelectTeam(List<Team> teams)
        {
            if (teams.Count == 0)
            {
                Console.WriteLine("Помилка: спочатку створіть хоча б одну команду.");
                return null;
            }

            Console.WriteLine("Доступні команди:");
            for (int i = 0; i < teams.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {teams[i].TeamName}");
            }

            Console.Write("Введіть номер команди: ");
            if (int.TryParse(Console.ReadLine(), out int index) && index >= 1 && index <= teams.Count)
            {
                return teams[index - 1];
            }

            Console.WriteLine("Помилка: некоректний номер команди.");
            return null;
        }
    }
}