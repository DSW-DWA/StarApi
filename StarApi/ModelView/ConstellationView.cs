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
    }
}
