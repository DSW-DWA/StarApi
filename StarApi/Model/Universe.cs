using System;
using System.Collections.Generic;

namespace StarApi.Model;

public partial class Universe
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public double Size { get; set; }

    public string? Composition { get; set; }

    public virtual ICollection<Galaxy> Galaxies { get; set; } = new List<Galaxy>();

    public Universe(Guid id, string Name, double size, string Composition)
    {
        this.Id = id;
        this.Name = Name;
        this.Size = size;
        this.Composition = Composition;
    }
}
