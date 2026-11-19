namespace GeoAbstractApp
{
    public class Mountain : GeographicalObject
    {
        private double _highestPoint;

        public double HighestPoint
        {
            get { return _highestPoint; }
            set { _highestPoint = value; }
        }

        public Mountain(double coordinateX, double coordinateY, string name, string description, double highestPoint)
            : base(coordinateX, coordinateY, name, description)
        {
            _highestPoint = highestPoint;
        }

        public override string GetInfo()
        {
            return $"{base.GetInfo()}\n" +
                   $"Найвища точка: {_highestPoint} м";
        }
    }
}