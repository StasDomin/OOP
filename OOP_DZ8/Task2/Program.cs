using System;
using System.Text;

namespace Task2
{
    public class Email
    {
        public string Theme { get; set; }
        public string From { get; set; }
        public string To { get; set; }

        public Email(string from, string to, string theme)
        {
            From = from;
            To = to;
            Theme = theme;
        }
    }

    public class Logger
    {
        public void Log(string message)
        {
            Console.WriteLine($"[LOG {DateTime.Now:HH:mm:ss}]: {message}");
        }
    }

    public class EmailSender
    {
        private readonly Logger _logger;

        public EmailSender(Logger logger)
        {
            _logger = logger;
        }

        public void Send(Email email)
        {
            _logger.Log($"Лист '{email.Theme}' від '{email.From}' успішно надіслано до '{email.To}'.");
        }
    }

    internal class Program
    {
        private static void Main()
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.WriteLine("=== Завдання 2: Принцип єдиної відповідальності (EmailSender + Logger) ===\n");

            Logger logger = new Logger();
            EmailSender sender = new EmailSender(logger);

            Email e1 = new Email("student@univ.edu.ua", "teacher@univ.edu.ua", "Здача ДЗ №8");
            Email e2 = new Email("admin@club.ua", "client@gmail.com", "Підтвердження запису");

            sender.Send(e1);
            sender.Send(e2);
        }
    }
}
