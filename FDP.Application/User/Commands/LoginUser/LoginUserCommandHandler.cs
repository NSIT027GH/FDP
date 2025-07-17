using FDP.Lib;
using FDP.Shared;
using MediatR;
using System.Net;

namespace FDP.Application.User;

public class LoginUserCommandHandler(IUserService userService) : IRequestHandler<LoginUserCommand, ApiResponse<object>>
{
    private readonly IUserService _iUserService = userService;
    public async Task<ApiResponse<object>> Handle(LoginUserCommand command, CancellationToken ct)
    {
        var loginRequest = new UserLoginRequestModel
        {
            Email = command.Email,
            Password = command.Password,
            Status = (int)TaskStatusEnum.UserStatus.Active
        };

        var results = await _iUserService.LoginUser(loginRequest);
        var result = new ApiResponse<object>
        {
            StatusCode = (int)HttpStatusCode.OK,
            Message = "Login successfully",
            Data = results
        };
        return result;
    }
}
