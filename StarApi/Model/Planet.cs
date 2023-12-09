using System;
using System.Collections.Generic;

namespace StarApi.Model;

public partial class Planet
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public double Mass { get; set; }

    public double Diameter { get; set; }

    public double DistanceFromStar { get; set; }

    public double? SurfaceTemperature { get; set; }

    public Guid? StarId { get; set; }

    public virtual Star? Star { get; set; }

    public Planet(Guid id, string name, double mass, double diameter, double distanceFromStar, double? surfaceTemperature, Guid? starId)
    {
        Id = id;
        Name = name;
        Mass = mass;
        Diameter = diameter;
        DistanceFromStar = distanceFromStar;
        SurfaceTemperature = surfaceTemperature;
        StarId = starId;
    }
}
