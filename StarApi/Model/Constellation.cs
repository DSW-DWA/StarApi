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
}
