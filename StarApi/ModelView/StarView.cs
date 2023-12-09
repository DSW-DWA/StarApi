namespace StarApi.ModelView
{
    public class StarView
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string SpectralType { get; set; }

        public double Luminosity { get; set; }

        public double DistanceFromEarth { get; set; }

        public double Temperature { get; set; }

        public GalaxyViewView Galaxy { get; set; }

        public StarView(Guid id, string name, string spectralType, double luminosity, double distanceFromEarth, double temperature, Guid galaxyId, string galaxyName)
        {
            Id = id;
            Name = name;
            SpectralType = spectralType;
            Luminosity = luminosity;
            DistanceFromEarth = distanceFromEarth;
            Temperature = temperature;
            Galaxy = new GalaxyViewView(galaxyId, galaxyName);
        }
    }
}
