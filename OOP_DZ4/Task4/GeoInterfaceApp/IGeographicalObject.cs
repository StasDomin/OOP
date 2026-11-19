namespace GeoInterfaceApp
{
    public interface IGeographicalObject
    {
        double CoordinateX { get; set; }
        double CoordinateY { get; set; }
        string Name { get; set; }
        string Description { get; set; }

        string GetInfo();
    }
}