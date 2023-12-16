using System;
using System.Collections.Generic;

namespace StarApi.Model;

public partial class Constellation
{
    public Guid Id { get; set; }

    public Guid? GalaxyId { get; set; }

    public string Name { get; set; } = null!;

    public string? Shape { get; set; }

    public string? Abbreviation { get; set; }

    public string? History { get; set; }

    public virtual Galaxy? Galaxy { get; set; }

    public virtual ICollection<Star> Stars { get; set; } = new List<Star>();

    public Constellation(Guid id, Guid galaxyId, string name, string shape, string abbreviation, string history)
    {
        this.Id = id;
        this.GalaxyId = galaxyId;
        this.Name = name;
        this.Shape = shape;
        this.Abbreviation = abbreviation;
        this.History = history;
    }
}
