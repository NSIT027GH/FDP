using AutoMapper;
using FDP.Infrastructure.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FDP.Shared;

[AutoMap(typeof(User))]
public class UserUpdateRequestModel
{
    [Key]
    [DefaultValue(0)]
    public int UserId { get; set; }
    
    public int? RestaurantId { get; set; }

    [Required, MaxLength(50)]
    public string FirstName { get; set; } = null!;

    [Required, MaxLength(75)]
    public string LastName { get; set; } = null!;

    [Required, MinLength(8), MaxLength(16)]
    public string Password { get; set; } = null!;

    [Required]
    public string Email { get; set; } = null!;

    [Required, RegularExpression("^[0-9]{10}$", ErrorMessage = "Phone number must be 10 digits")]
    public long PhoneNumber { get; set; }

    public int UpdationBy { get; set; }

    //public DateTime CreationDatetime { get; set; }

    public DateTime UpdationDatetime { get; set; }
}