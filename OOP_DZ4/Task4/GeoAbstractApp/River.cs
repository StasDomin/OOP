namespace GeoAbstractApp
{
    public class River : GeographicalObject
    {
        private double _flowSpeed;
        private double _totalLength;

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
            : base(coordinateX, coordinateY, name, description)
        {
            _flowSpeed = flowSpeed;
            _totalLength = totalLength;
        }

        public override string GetInfo()
        {
            return $"{base.GetInfo()}\n" +
                   $"Швидкість течії: {_flowSpeed} см/с\n" +
                   $"Загальна довжина: {_totalLength} км";
        }
    }
}