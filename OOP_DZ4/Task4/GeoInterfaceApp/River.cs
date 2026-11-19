namespace GeoInterfaceApp
{
    public class River : IGeographicalObject
    {
        private double _coordinateX;
        private double _coordinateY;
        private string _name;
        private string _description;
        private double _flowSpeed;
        private double _totalLength;

        public double CoordinateX
        {
            get { return _coordinateX; }
            set { _coordinateX = value; }
        }

        public double CoordinateY
        {
            get { return _coordinateY; }
            set { _coordinateY = value; }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        public double FlowSpeed
        {
            get { return _flowSpeed; }
            set { _flowSpeed = value; }
        }

        public double TotalLength
        {
            get { return _totalLength; }
            set { _totalLength = value; }
        }

        public River(double coordinateX, double coordinateY, string name, string description, double flowSpeed, double totalLength)
        {
            _coordinateX = coordinateX;
            _coordinateY = coordinateY;
            _name = name;
            _description = description;
            _flowSpeed = flowSpeed;
            _totalLength = totalLength;
        }

        public string GetInfo()
        {
            return $"Назва: {_name}\n" +
                   $"Координати: X = {_coordinateX}, Y = {_coordinateY}\n" +
                   $"Опис: {_description}\n" +
                   $"Швидкість течії: {_flowSpeed} см/с\n" +
                   $"Загальна довжина: {_totalLength} км";
        }
    }
}