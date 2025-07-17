using FDP.Infrastructure;
using FDP.Shared;

namespace FDP.Lib;

public interface IAddressService
{
    Task<int> AddAddress(AddressRequestModel addressRequestC);
    Task<List<AddressRequestModel>> GetAddress();
    Task<AddressResponseModel> GetAddressById(int id);
    Task<List<AddressResponseModel>?> GetAddressByUserId(int id);
    Task<int> UpdateAddress(AddressRequestModel addressRequestClass);
    Task<List<AddressDto>> SetDefaultAddress(int id);
    Task<int> DeleteAddress(int id);
    Task<List<CountryRequestModel>> GetCountry();
    Task<List<StateRequestModel>> GetState();
}
