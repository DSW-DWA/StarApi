namespace StarApi.ModelView
{
    public class StarViewView
    {
        public Guid? Id { get; set; }
        public string? Name { get; set; }

        public StarViewView(Guid? id, string? name)
        {
            Id = id;
            Name = name;
        }
    }
    public class PlanetView
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;

        public double Mass { get; set; }

        public double Diameter { get; set; }

        public double DistanceFromStar { get; set; }

        public double? SurfaceTemperature { get; set; }
        public StarViewView? Star { get; set; }
        public PlanetView(Guid id, string name, double mass, double diameter, double distanceFromStar, double? surfaceTemperature, Guid? starId, string? starName)
        {
            Id = id;
            Name = name;
            Mass = mass;
            Diameter = diameter;
            DistanceFromStar = distanceFromStar;
            SurfaceTemperature = surfaceTemperature;
            Star = new StarViewView(starId, starName);
        }
    }
}
