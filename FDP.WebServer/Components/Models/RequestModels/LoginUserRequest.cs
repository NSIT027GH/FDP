namespace FDP.WebServer.Components.Models.RequestModels
{
    public class LoginUserRequest
    {
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
