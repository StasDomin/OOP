using System;
using System.Collections.Generic;
using System.Text;

namespace Task2_2
{
    public class Team
    {
        private string _teamName;
        private List<Worker> _workers;

        public string TeamName
        {
            get { return _teamName; }
            set { _teamName = value; }
        }

        public Team(string teamName)
        {
            _teamName = teamName;
            _workers = new List<Worker>();
        }

        public void AddWorker(Worker worker)
        {
            _workers.Add(worker);
        }

        public string GetShortInfo()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Команда: {_teamName}");

            if (_workers.Count == 0)
            {
                sb.AppendLine("У команді поки немає співробітників.");
                return sb.ToString();
            }

            foreach (Worker worker in _workers)
            {
                sb.AppendLine($"- {worker.Name}");
            }

            return sb.ToString();
        }

        public string GetDetailedInfo()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Детальна інформація про команду: {_teamName}");

            if (_workers.Count == 0)
            {
                sb.AppendLine("У команді поки немає співробітників.");
                return sb.ToString();
            }

            foreach (Worker worker in _workers)
            {
                worker.ResetWorkDay();
                worker.FillWorkDay();
                sb.AppendLine($"- {worker.Name} - {worker.Position} - {worker.WorkDay}");
            }

            return sb.ToString();
        }
    }
}