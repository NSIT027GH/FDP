using AutoMapper;
using FDP.Infrastructure.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FDP.Shared;

[AutoMap(typeof(Address))]
public class AddressRequestModel
{
    [Key]
    [DefaultValue(0)]
    public int AddressId { get; set; }

    [Required]
    public int UserId { get; set; }

    [Required]
    public string Location { get; set; } = null!;
    
    public string? Area { get; set; }

    [Required]
    public string City { get; set; } = null!;

    [Required]
    public int StateId { get; set; }

    [Required]
    public int CountryId { get; set; }

    [Required] 
    public int Pincode { get; set; }

    [DefaultValue(0)]
    public int Status { get; set; }
}