namespace GeoInterfaceApp
{
    public class Mountain : IGeographicalObject
    {
        private double _coordinateX;
        private double _coordinateY;
        private string _name;
        private string _description;
        private double _highestPoint;

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

        public double HighestPoint
        {
            get { return _highestPoint; }
            set { _highestPoint = value; }
        }

        public Mountain(double coordinateX, double coordinateY, string name, string description, double highestPoint)
        {
            _coordinateX = coordinateX;
            _coordinateY = coordinateY;
            _name = name;
            _description = description;
            _highestPoint = highestPoint;
        }

        public string GetInfo()
        {
            return $"Назва: {_name}\n" +
                   $"Координати: X = {_coordinateX}, Y = {_coordinateY}\n" +
                   $"Опис: {_description}\n" +
                   $"Найвища точка: {_highestPoint} м";
        }
    }
}