namespace FDP.WebServer.Components.Models;

public class AddUserRequest
{
    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Email { get; set; } = null!;

    public long PhoneNumber { get; set; }

    public int Role { get; set; }
}
