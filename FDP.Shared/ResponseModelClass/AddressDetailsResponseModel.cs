using AutoMapper;
using FDP.Infrastructure.Models;

namespace FDP.Shared;

[AutoMap(typeof(Address))]
public class AddressDetailsResponseModel
{
    public int AddressId { get; set; }

    public int UserId { get; set; }

    public string Location { get; set; } = null!;

    public string? Area { get; set; }

    public string City { get; set; } = null!;

    public string? StateName { get; set; }

    public string? CountryName { get; set; }

    public int Pincode { get; set; }

    public int Status { get; set; }
}