using FDP.Lib;
using FDP.Shared;
using MediatR;
using System.Net;

namespace FDP.Application.User;

public class CreateUserCommandHandler(IUserService userService) : IRequestHandler<CreateUserCommand, ApiResponse<object>>
{
    private readonly IUserService _iUserService = userService;

    public async Task<ApiResponse<object>> Handle(CreateUserCommand command, CancellationToken ct)
    {
        var userCreateRequest = new UserCreateRequestModel
        {
            FirstName = command.FirstName,
            LastName = command.LastName,
            Email = command.Email,
            PhoneNumber = command.PhoneNumber,
            Role = command.Role,
            Password = command.Password,
            Status = (int)TaskStatusEnum.UserStatus.Active
        };

        var results = await _iUserService.CreateUser(userCreateRequest);
        var result = new ApiResponse<object>
        {
            StatusCode = (int)HttpStatusCode.OK,
            Message = "Fatched successfully",
            Data = results
        };
        return result;
    }
}
