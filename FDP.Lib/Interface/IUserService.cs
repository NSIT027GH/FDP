using FDP.Shared;
using FDP.Shared.ResponseModelClass;

namespace FDP.Lib;

public interface IUserService
{
    Task<UserDetailsResponseModel?> GetUserById(int id);
    Task<List<UserDetailsResponseModel>> GetUser();
    List<UserDetailsWithAddressResponseModel> GetUserDetails();
    Task<int> UpdateUser(UserUpdateRequestModel usreUpdateRequest);
    Task<int> CreateUser(UserCreateRequestModel userCreateRequestClass);
    Task<int> DeleteUserAsync(int id);
    Task<UserLoginResponseModel> LoginUser(UserLoginRequestModel userLoginRequestClass);

    Task<bool> CheckMailExist(string email);

    Task<bool> CheckPhoneNumberExist(long phoneNumber);
    Task<bool> CheckMailUpdateExist(string email, int id);

    Task<bool> CheckPhoneNumberUpdateExist(long phoneNumber, int id);
}
