namespace Task2_2
{
    public abstract class Worker
    {
        private string _name;
        private string _position;
        private string _workDay;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public string Position
        {
            get { return _position; }
            protected set { _position = value; }
        }

        public string WorkDay
        {
            get { return _workDay; }
            protected set { _workDay = value; }
        }

        protected Worker(string name)
        {
            _name = name;
            _position = string.Empty;
            _workDay = string.Empty;
        }

        public void Call()
        {
            AddAction("Call");
        }

        public void WriteCode()
        {
            AddAction("WriteCode");
        }

        public void Relax()
        {
            AddAction("Relax");
        }

        public abstract void FillWorkDay();

        private void AddAction(string action)
        {
            if (string.IsNullOrEmpty(_workDay))
            {
                _workDay = action;
            }
            else
            {
                _workDay += ", " + action;
            }
        }
    }
}