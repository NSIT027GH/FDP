namespace FDP.Infrastructure.Models;

public partial class Address
{
    public int AddressId { get; set; }

    public int UserId { get; set; }

    public string Location { get; set; } = null!;

    public string? Area { get; set; }

    public string City { get; set; } = null!;

    public int StateId { get; set; }

    public int CountryId { get; set; }

    public int Pincode { get; set; }

    public int Status { get; set; }

    public virtual User User { get; set; } = null!;
}