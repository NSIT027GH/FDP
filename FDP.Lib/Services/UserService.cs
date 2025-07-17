using AutoMapper;
using FDP.Infrastructure;
using FDP.Infrastructure.Models;
using FDP.Shared;
using FDP.Shared.ResponseModelClass;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FDP.Lib;

public class UserService : IUserService
{
    private readonly IGenericRepository<User> _iUserRepo;
    private readonly IGenericRepository<Address> _iAddressRepo;
    private readonly IMapper _mapper;
    private readonly FdpContext _dbContext;
    private readonly DbSet<User> _entitySet;
    private readonly ILoginUserService _loginService;
    public readonly IConfiguration _configuration;
    public UserService(IGenericRepository<User> iGenericRepository, IGenericRepository<Address> iGenericAddressRepository, IMapper mapper, FdpContext dbContext, IConfiguration configuration, ILoginUserService loginService)
    {
        _dbContext = dbContext;
        _iUserRepo = iGenericRepository;
        _iAddressRepo = iGenericAddressRepository;
        _mapper = mapper;
        _entitySet = _dbContext.Set<User>();
        _configuration = configuration;
        _loginService = loginService;
    }

    public async Task<UserDetailsResponseModel?> GetUserById(int id)
    {
        var data = await _iUserRepo.GetById(id);
        if (data != null)
        {
            data.Password = Encoding.UTF8.GetString(Convert.FromBase64String(data.Password));

            return _mapper.Map<User, UserDetailsResponseModel>(data);
        }
        else
            return null;
    }

    public async Task<List<UserDetailsResponseModel>> GetUser()
    {
        var data = await _iUserRepo.GetAll();

        for (int i = 0; i < data.Count; i++)
        {
            data[i].Password = Encoding.UTF8.GetString(Convert.FromBase64String(data[i].Password));
        }

        return _mapper.Map<List<User>, List<UserDetailsResponseModel>>(data);
    }
    public List<UserDetailsWithAddressResponseModel> GetUserDetails()
    {

        var result = from u in _dbContext.Users
                     join a in _dbContext.Addresses on u.UserId equals a.UserId into addrGrp
                     from a in addrGrp.DefaultIfEmpty()
                     join s in _dbContext.States on a.StateId equals s.StateId into stateGrp
                     from s in stateGrp.DefaultIfEmpty()
                     join c in _dbContext.Countries on s.CountryId equals c.CountryId into countryGrp
                     from c in countryGrp.DefaultIfEmpty()
                     select new
                     {
                         u.FirstName,
                         u.LastName,
                         Location = a != null && a.Location != null ? a.Location : "Not Available",
                         City = a != null && a.City != null ? a.City : "Not Available",
                         State = s != null && s.Name != null ? s.Name : "Not Available",
                         Country = c != null && c.Name != null ? c.Name : "Not Available"
                     };

        List<UserDetailsWithAddressResponseModel> ans = [];
        foreach (var item in result)
        {
            var obj = new UserDetailsWithAddressResponseModel()
            {
                FirstName = item.FirstName,
                LastName = item.LastName,
                Location = item.Location,
                City = item.City,
                State = item.State,
                Country = item.Country
            };
            ans.Add(obj);
        }

        return ans;
    }

    public async Task<int> UpdateUser(UserUpdateRequestModel userUpdateRequest)
    {

        try
        {
            var otherData = await _iUserRepo.GetAll();
            var odata = await _iUserRepo.GetById(userUpdateRequest.UserId);
            var data = otherData.Where(where => where.UserId == userUpdateRequest.UserId).FirstOrDefault();
            
            if (data != null)
            {
                data.FirstName = userUpdateRequest.FirstName;
                data.LastName = userUpdateRequest.LastName;
                data.Email = userUpdateRequest.Email;
                data.PhoneNumber = userUpdateRequest.PhoneNumber;
                data.UpdationDatetime = DateTime.Now;
                data.Password = Convert.ToBase64String(Encoding.UTF8.GetBytes(userUpdateRequest.Password));
                data.UpdationBy = _loginService.GetUserId() ?? 0;
                return await _iUserRepo.Update(data);
            }
            return 0;
        }
        catch
        {
            throw;
        }
    }

    public async Task<int> CreateUser(UserCreateRequestModel userCreateRequest)
    {
        userCreateRequest.Password = Convert.ToBase64String(Encoding.UTF8.GetBytes(userCreateRequest.Password));
        userCreateRequest.CreationDatetime = DateTime.Now;

        var data = _mapper.Map<UserCreateRequestModel, User>(userCreateRequest);
        return await _iUserRepo.Create(data);
    }

    public async Task<UserLoginResponseModel> LoginUser(UserLoginRequestModel userLoginRequest)
    {
        var userLoginResponseclassObj = new UserLoginResponseModel();
        byte[] strorePassword = ASCIIEncoding.ASCII.GetBytes(userLoginRequest.Password);
        userLoginRequest.Password = Convert.ToBase64String(strorePassword);
        try
        {
            if (userLoginRequest != null && userLoginRequest.Email != null && userLoginRequest.Password != null)
            {
                var user = await _entitySet.Where(val => val.Email == userLoginRequest.Email && val.Password == userLoginRequest.Password).FirstOrDefaultAsync();

                if (user != null)
                {
                    var JwtSubject = _configuration["Jwt:Subject"];
                    var JwtKey = _configuration["Jwt:Key"];
                    if (user.Status == 1 && JwtSubject != null && JwtKey != null)
                    {
                        var nowSeconds = DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString();

                        var claims = new[] {
                                            new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"] ?? ""),
                                            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                                            new Claim(JwtRegisteredClaimNames.Iat, nowSeconds, ClaimValueTypes.Integer64),
                                            new Claim("UserId" , user.UserId.ToString()),
                                            new Claim("Email", user.Email),
                                        };

                        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"] ?? ""));
                        var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                        var token = new JwtSecurityToken(
                            _configuration["Jwt:Issuer"],
                            _configuration["Jwt:Audience"],
                            claims,
                            DateTime.UtcNow,
                            DateTime.UtcNow.AddMinutes(30),
                            signIn);

                        byte[] encryptedPassword1 = Convert.FromBase64String(userLoginRequest.Password);
                        userLoginRequest.Password = ASCIIEncoding.ASCII.GetString(encryptedPassword1);

                        userLoginResponseclassObj.AccessToken = new JwtSecurityTokenHandler().WriteToken(token);

                        var userAdd = await _iAddressRepo.GetAll(where => where.UserId == user.UserId && where.Status == 1);

                        userLoginResponseclassObj.UserId = user.UserId;
                        userLoginResponseclassObj.Role = user.Role;
                        userLoginResponseclassObj.RestaurantId = user.RestaurantId;

                        if (userAdd.Count > 0)
                        {
                            userLoginResponseclassObj.City = userAdd[0].City;
                        }
                        else
                        {
                            userLoginResponseclassObj.City = null;
                        }

                        if (user.RestaurantId == null || user.RestaurantId == 0)
                        {
                            userLoginResponseclassObj.RestaurantStatus = null;
                        }

                        userLoginResponseclassObj.UserStatus = user.Status;

                        userLoginResponseclassObj.ExpireTime = DateTime.UtcNow.AddMinutes(30);

                        return userLoginResponseclassObj;
                    }
                    else
                    {
                        userLoginResponseclassObj.UserId = user.UserId;
                        userLoginResponseclassObj.UserStatus = user.Status;
                        return userLoginResponseclassObj;
                    }
                }
                else
                {
                    return userLoginResponseclassObj;
                }
            }
            else
            {
                return userLoginResponseclassObj;
            }
        }
        catch (NullReferenceException ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<bool> CheckMailExist(string email)
    {
        var oldEmail = await _entitySet.Where(val => val.Email == email).ToListAsync();
        return oldEmail.Count != 0;
    }

    public async Task<bool> CheckPhoneNumberExist(long phoneNumber)
    {
        var oldNumber = await _entitySet.Where(val => val.PhoneNumber == phoneNumber).ToListAsync();
        return oldNumber.Count != 0;
    }
    public async Task<bool> CheckMailUpdateExist(string email, int id)
    {
        var oldEmail = await _entitySet.Where(val => val.Email == email && val.UserId != id).ToListAsync();
        return oldEmail.Count == 0;
    }

    public async Task<bool> CheckPhoneNumberUpdateExist(long phoneNumber, int id)
    {
        var oldNumber = await _entitySet.Where(val => val.PhoneNumber == phoneNumber && val.UserId != id).ToListAsync();
        return oldNumber.Count == 0;
    }
}

