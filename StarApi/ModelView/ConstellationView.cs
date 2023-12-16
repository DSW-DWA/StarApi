namespace StarApi.ModelView
{
    public class GalaxyViewView
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public GalaxyViewView(Guid id, string name)
        {
            this.Id = id;
            this.Name = name;
        }
    }
    public class ConstellationView
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;

        public string? Shape { get; set; }

        public string? Abbreviation { get; set; }

        public string? History { get; set; }

        public GalaxyViewView Galaxy { get; set; }

        public ConstellationView(Guid id, string name, string shape, string abbreviation, string history, Guid galaxyId, string galaxyName)
        {
            this.Id = id;
            this.Name = name;
            this.Shape = shape;
            this.Abbreviation = abbreviation;
            this.History = history;
            this.Galaxy = new GalaxyViewView(galaxyId, galaxyName);
        }
    }
}
