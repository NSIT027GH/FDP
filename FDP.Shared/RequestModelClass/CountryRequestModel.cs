using AutoMapper;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using FDP.Infrastructure.Models;

namespace FDP.Shared;

[AutoMap(typeof(Country))]
public class CountryRequestModel
{
    [Key]
    [DefaultValue(0)]
    public int CountryId { get; set; }

    [Required]
    public string Code { get; set; } = null!;

    [Required]
    public string Name { get; set; } = null!;

    [Required]
    public int Phonecode { get; set; }
}