using AutoMapper;
using FDP.Infrastructure.Models;

namespace FDP.Shared;

public class AutoMapperConfigClass : Profile
{
    public AutoMapperConfigClass()
    {
        CreateMap<User, UserDetailsResponseModel>().ReverseMap();;
        CreateMap<UserUpdateRequestModel, User>().ReverseMap();
        CreateMap<AddressRequestModel, Address>().ReverseMap();
        CreateMap<UserCreateRequestModel, User>().ReverseMap();
        CreateMap<AddressResponseModel, Address>().ReverseMap();
        CreateMap<Country, CountryRequestModel>().ReverseMap();
        CreateMap<State, StateRequestModel>().ReverseMap();
    }
}