using FDP.Lib;
using FDP.Shared;
using System.Net;

namespace FDP.Application.Address
{
    public class UpdateAddressCommandHandler(IAddressService addressService)
    {
        private readonly IAddressService _iAddressService = addressService;
        public async Task<ApiResponse<object>> Handle(UpdateAddressCommand command, CancellationToken cancellationToken)
        {
            var address = new AddressRequestModel
            {
                AddressId = command.AddressId,
                UserId = command.UserId,
                Location = command.Location,
                Area = command.Area,
                City = command.City,
                Pincode = command.Pincode,
                StateId = command.StateId,
                CountryId = command.CountryId,
                Status = command.Status,
            };
            try
            {
                var addressDetails = await _iAddressService.UpdateAddress(address);
                return new ApiResponse<object>
                {
                    Data = address ?? null,
                    Message = "Address Updated Successfully!",
                    StatusCode = (int)HttpStatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<object>
                {
                    Data = null,
                    Message = "Exception occurred! : " + ex.Message,
                    StatusCode = (int)HttpStatusCode.InternalServerError
                };
            }
        }
    }
}
