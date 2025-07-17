namespace FDP.WebServer.Components.Models;

public class UserDetails
{
    public int UserId { get; set; }

    public int? RestaurantId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Email { get; set; } = null!;

    public long PhoneNumber { get; set; }

    public int Role { get; set; }

    public int CreationBy { get; set; }

    public int UpdationBy { get; set; }

    public DateTime CreationDatetime { get; set; }

    public DateTime UpdationDatetime { get; set; }

    public int Status { get; set; }
}
