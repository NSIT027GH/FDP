namespace FDP.Shared;

public class UserLoginResponseModel
{
    public string? AccessToken { get; set; } = null!;
    public DateTime? ExpireTime { get; set; }
    public int? Role { get; set; }
    public int? UserId { get; set; }
    public int? RestaurantId { get; set; }
    public int? RestaurantStatus { get; set; }
    public int? UserStatus { get; set; }

    public string? City { get; set; }
}