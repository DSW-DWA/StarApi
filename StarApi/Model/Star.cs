using System;
using System.Collections.Generic;

namespace StarApi.Model;

public partial class Star
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public Guid? GalaxyId { get; set; }

    public string SpectralType { get; set; } = null!;

    public double Luminosity { get; set; }

    public double DistanceFromEarth { get; set; }

    public double Temperature { get; set; }

    public virtual Galaxy? Galaxy { get; set; }

    public virtual ICollection<Planet> Planets { get; set; } = new List<Planet>();

    public virtual ICollection<Constellation> Constellations { get; set; } = new List<Constellation>();

    public Star(Guid id, string name, Guid? galaxyId, string spectralType, double luminosity, double distanceFromEarth, double temperature)
    {
        Id = id;
        Name = name;
        GalaxyId = galaxyId;
        SpectralType = spectralType;
        Luminosity = luminosity;
        DistanceFromEarth = distanceFromEarth;
        Temperature = temperature;
    }
}
