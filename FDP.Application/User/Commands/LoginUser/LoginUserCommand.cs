using FDP.Shared;
using MediatR;

namespace FDP.Application.User;

public class LoginUserCommand : IRequest<ApiResponse<object>>
{
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
}
