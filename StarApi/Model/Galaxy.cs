using System;
using System.Collections.Generic;

namespace StarApi.Model;

public partial class Galaxy
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public Guid? UniverseId { get; set; }

    public double Size { get; set; }

    public string? Shape { get; set; }

    public string? Composition { get; set; }

    public double? DistanceFromEarth { get; set; }

    public virtual ICollection<Constellation> Constellations { get; set; } = new List<Constellation>();

    public virtual ICollection<Star> Stars { get; set; } = new List<Star>();

    public virtual Universe? Universe { get; set; }

    public Galaxy(Guid id, string name, Guid? universeId, double size, string? shape, string? composition, double? distanceFromEarth)
    {
        Id = id;
        Name = name;
        UniverseId = universeId;
        Size = size;
        Shape = shape;
        Composition = composition;
        DistanceFromEarth = distanceFromEarth;
    }
}
