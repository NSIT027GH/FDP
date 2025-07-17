using AutoMapper;
using FDP.Infrastructure;
using FDP.Infrastructure.Models;
using FDP.Shared;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Text;

namespace FDP.Lib;

public class AddressService(IGenericRepository<Address> iAddressRepo, IMapper mapper, IGenericRepository<Country> iCountryRepo, IGenericRepository<State> iStateRepo, IConfiguration configuration, ILoginUserService loginService, FdpContext dbContext) : IAddressService
{
    private readonly IGenericRepository<Address> _iAddressRepo = iAddressRepo;
    private readonly IGenericRepository<Country> _iCountryRepo = iCountryRepo;
    private readonly IGenericRepository<State> _iStateRepo = iStateRepo;
    private readonly IMapper _mapper = mapper;
    public IConfiguration _configuration = configuration;
    private readonly ILoginUserService _loginService = loginService;
    private readonly FdpContext _dbContext = dbContext;

    public async Task<List<AddressRequestModel>> GetAddress()
    {
        var data = await _iAddressRepo.GetAll();
        return _mapper.Map<List<Address>, List<AddressRequestModel>>(data);
    }

    public async Task<AddressResponseModel> GetAddressById(int id)
    {
        var data = await _iAddressRepo.GetById(id);
        return _mapper.Map<Address, AddressResponseModel>(data);
    }

    public async Task<List<AddressResponseModel>?> GetAddressByUserId(int id)
    {
        var data = await _iAddressRepo.GetAll(where => where.UserId == id);
        //data = null;
        if(data?.Count > 0)
        {
            return _mapper.Map<List<Address>, List<AddressResponseModel>>(data);
        }
        else
            return null;
    }

    public async Task<int> AddAddress(AddressRequestModel addressRequest)
    {
        var addData = await _iAddressRepo.GetAll(where => where.UserId == addressRequest.UserId && where.Status > (int)TaskStatusEnum.AddressStatus.DeleteAddress);

        addressRequest.Status = addData.Count == 0 ? (int)TaskStatusEnum.AddressStatus.DefaultAddress : (int)TaskStatusEnum.AddressStatus.NormalAddress;

        var data = _mapper.Map<AddressRequestModel, Address>(addressRequest);
        return await _iAddressRepo.Create(data);
    }

    public async Task<int> UpdateAddress(AddressRequestModel addressRequest)
    {
        var data = _mapper.Map<AddressRequestModel, Address>(addressRequest);
        return await _iAddressRepo.Update(data);
    }

    public async Task<List<AddressDto>> SetDefaultAddress(int id)
    {
        var userId = _loginService.GetUserId() ?? 0;
        var connString = _configuration["SqlConnectionString"];
        using var conn = new SqlConnection(connString);
        using var cmd = new SqlCommand("UpdateAddressStatus", conn)
        {
            CommandType = CommandType.StoredProcedure
        };

        //cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = userId });
        //cmd.Parameters.Add(new SqlParameter("@SelectedAddressId", SqlDbType.Int) { Value = id });
        cmd.Parameters.AddWithValue("@UserId", userId);
        cmd.Parameters.AddWithValue("@SelectedAddressId", id);

        //await conn.OpenAsync();
        //using var reader = await cmd.ExecuteReaderAsync();
        var addresses = new List<Address>();

        int userID = _loginService.GetUserId() ?? 0;
        int selectedAddressId = id;

        var results = await _dbContext.AddressDtos
            .FromSqlInterpolated(
               $"EXEC dbo.UpdateAddressStatus @UserId={userID}, @SelectedAddressId={selectedAddressId}"
            )
            .ToListAsync();

        var results2 = await _dbContext.AddressDtos
            .FromSqlRaw("EXEC dbo.UpdateAddressStatus @UserId, @SelectedAddressId", userID,selectedAddressId)
            .ToListAsync();

        //while (await reader.ReadAsync())
        //{
        //    addresses.Add(new Address
        //    {
        //        AddressId = reader.GetInt32(reader.GetOrdinal("AddressId")),
        //        UserId = reader.GetInt32(reader.GetOrdinal("UserId")),
        //        Location = reader.GetString(reader.GetOrdinal("Location")),
        //        Pincode = reader.GetInt32(reader.GetOrdinal("Pincode")),
        //        Status = reader.GetInt32(reader.GetOrdinal("Status"))
        //    });
        //}

        return results;
    }

    public async Task<int> DeleteAddress(int id)
    {
        var data = await _iAddressRepo.GetById(id);
        data.Status = (int)TaskStatusEnum.AddressStatus.DeleteAddress;
        return await _iAddressRepo.Update(data);
    }

    public async Task<List<CountryRequestModel>> GetCountry()
    {
        var data = await _iCountryRepo.GetAll();
        return _mapper.Map<List<Country>, List<CountryRequestModel>>(data);
    }

    public async Task<List<StateRequestModel>> GetState()
    {
        var data = await _iStateRepo.GetAll();
        return _mapper.Map<List<State>, List<StateRequestModel>>(data);
    }
}