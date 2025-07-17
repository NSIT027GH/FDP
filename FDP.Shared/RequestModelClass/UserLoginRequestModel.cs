using System.ComponentModel.DataAnnotations;

namespace FDP.Shared;

public class UserLoginRequestModel
{
    [Required, EmailAddress]
    public string Email { get; set; } = null!;

    [Required, MinLength(8), MaxLength(16)]
    public string Password { get; set; } = null!;

    [Required]
    public int Status { get; set; }
}