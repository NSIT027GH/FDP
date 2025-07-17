namespace FDP.Infrastructure.Models;

public partial class Country
{
    public int CountryId { get; set; }

    public string Code { get; set; } = null!;

    public string Name { get; set; } = null!;

    public int Phonecode { get; set; }

    public virtual ICollection<State> States { get; set; } = new List<State>();
}
