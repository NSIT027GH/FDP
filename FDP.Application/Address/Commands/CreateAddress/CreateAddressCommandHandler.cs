using FDP.Lib;
using FDP.Shared;
using System.Net;
namespace FDP.Application.Address;

public class CreateAddressCommandHandler(IAddressService addressService)
{
    private readonly IAddressService _iAddressService = addressService;

    public async Task<ApiResponse<object>> Handle(CreateAddressCommand command, CancellationToken cancellationToken)
    {
        var address = new AddressRequestModel
        {
            UserId = command.UserId,
            Location = command.Location,
            Area = command.Area,
            City = command.City,
            Pincode = command.Pincode,
            StateId = command.StateId,
            CountryId = command.CountryId,
            Status = (int)TaskStatusEnum.AddressStatus.NormalAddress
        };
        try
        {
            var addressDetails = await _iAddressService.AddAddress(address);
            return new ApiResponse<object>
            {
                Data = address ?? null,
                Message = "Address Created Successfully!",
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
