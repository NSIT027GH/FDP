

using AutoMapper;
using FDP.Infrastructure.Models;
using System.ComponentModel.DataAnnotations;

namespace FDP.Shared;

[AutoMap(typeof(State))]
public class StateRequestModel
{
    [Key]
    public int StateId { get; set; }

    [Required]
    public int CountryId { get; set; }

    [Required]
    public string Name { get; set; } = null!;
}