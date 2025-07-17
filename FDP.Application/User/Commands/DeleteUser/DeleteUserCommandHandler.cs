using FDP.Lib;
using FDP.Shared;
using MediatR;

namespace FDP.Application.User;

public record DeleteUserCommand(int Id) : IRequest<ApiResponse<object>>;
public class DeleteUserCommandHandler(IUserService userService) : IRequestHandler<DeleteUserCommand, ApiResponse<object>>
{
    private readonly IUserService _userService = userService;

    public async Task<ApiResponse<object>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var result = await _userService.DeleteUserAsync(request.Id);
        return new ApiResponse<object> { Data = result, Message = "User deleted" };
    }
}
