using System;
using System.Collections.Generic;

namespace FDP.Infrastructure.Models;

public partial class State
{
    public int StateId { get; set; }

    public int CountryId { get; set; }

    public string Name { get; set; } = null!;

    public virtual Country Country { get; set; } = null!;
}
