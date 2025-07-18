using FDP.Lib;
using FDP.Shared;
using MediatR;
using System.Net;

namespace FDP.Application.User;

public record GetUsersQuery : IRequest<ApiResponse<object>>;
public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, ApiResponse<object>>    
{
    private readonly IUserService _iUserService;
    public GetUsersQueryHandler(IUserService userService)
    {
        _iUserService = userService;
    }

    public async Task<ApiResponse<object>> Handle(GetUsersQuery query, CancellationToken ctn)
    {
        var userDetails = await _iUserService.GetUserDetails();
        try
        {
            if (userDetails.Count == 0)
            {
                throw new Exception($"Users not found.");
            }

            return new ApiResponse<object>
            {
                Data = userDetails,
                Message = "User details retrieved successfully",
                StatusCode = (int)HttpStatusCode.OK
            };
        }
        catch (Exception ex)
        {
            return new ApiResponse<object>
            {
                Data = null,
                Message = "Exception occurred!" + ex.Message,
                StatusCode = (int)HttpStatusCode.InternalServerError
            };
        }
    }
}
