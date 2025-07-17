using FDP.Lib;
using FDP.Shared;
using MediatR;

namespace FDP.Application.User;

public class GetUserByIdQueryHandler(IUserService userService) : IRequestHandler<GetUserByIdQuery, UserDetailsResponseModel>
{
    private readonly IUserService _iUserService = userService;

    public async Task<UserDetailsResponseModel?> Handle(GetUserByIdQuery query, CancellationToken ctn)
    {
        var userDetails =  await _iUserService.GetUserById(query.Id);
        try
        {
            if (userDetails == null)
            {
                throw new Exception($"User with ID {query.Id} not found.");
            }

            return new UserDetailsResponseModel
            {
                FirstName = userDetails.FirstName,
                LastName = userDetails.LastName,
                Email = userDetails.Email
            };
        }
        catch(Exception ex)
        {
            return null;
        }
    }
}
