namespace StarApi.ModelView
{
    public class UniverseView
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;

        public double Size { get; set; }

        public string? Composition { get; set; }

        public UniverseView(Guid id, string name, double size, string? composition)
        {
            Id = id;
            Name = name;
            Size = size;
            Composition = composition;
        }
    }
}
