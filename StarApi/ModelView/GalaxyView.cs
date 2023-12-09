namespace StarApi.ModelView
{
    public class UniverseViewView
    {
        public Guid? Id { get; set; }
        public string? Name { get; set; }

        public UniverseViewView(Guid? id, string? name)
        {
            Id = id;
            Name = name;
        }
    }
    public class GalaxyView
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public double Size { get; set; }

        public string? Shape { get; set; }

        public string? Composition { get; set; }

        public double? DistanceFromEarth { get; set; }
        public UniverseViewView? Universe { get; set; }

        public GalaxyView(Guid id, string name, double size, string? shape, string? composition, double? distanceFromEarth, Guid? universeId, string universeName)
        {
            Id = id;
            Name = name;
            Size = size;
            Shape = shape;
            Composition = composition;
            DistanceFromEarth = distanceFromEarth;
            Universe = new UniverseViewView(universeId, universeName);
        }
    }
}
