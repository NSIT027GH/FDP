using FDP.Shared;
using MediatR; 

namespace FDP.Application.User;

public class CreateUserCommand : IRequest<ApiResponse<object>>
{

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Email { get; set; } = null!;

    public long PhoneNumber { get; set; }

    public int Role { get; set; }
}
