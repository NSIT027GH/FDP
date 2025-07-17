using FDP.Shared;
using MediatR;

namespace FDP.Application.User;

public record GetUserByIdQuery(int Id) : IRequest<UserDetailsResponseModel>;