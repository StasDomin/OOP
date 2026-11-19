namespace GeoAbstractApp
{
    public abstract class GeographicalObject
    {
        private double _coordinateX;
        private double _coordinateY;
        private string _name;
        private string _description;

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

        protected GeographicalObject(double coordinateX, double coordinateY, string name, string description)
        {
            _coordinateX = coordinateX;
            _coordinateY = coordinateY;
            _name = name;
            _description = description;
        }

        public virtual string GetInfo()
        {
            return $"Назва: {_name}\n" +
                   $"Координати: X = {_coordinateX}, Y = {_coordinateY}\n" +
                   $"Опис: {_description}";
        }
    }
}