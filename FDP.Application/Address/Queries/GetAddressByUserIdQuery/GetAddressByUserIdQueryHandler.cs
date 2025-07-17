using FDP.Lib;
using FDP.Shared;
using System.Net;

namespace FDP.Application.Address
{
    public record GetAddressByUserIdQuery(int Id);
    public class GetAddressByUserIdQueryHandler
    {
        private readonly IAddressService _iAddressService;
        public GetAddressByUserIdQueryHandler(IAddressService addressService)
        {
            _iAddressService = addressService;
        }
        public async Task<ApiResponse<object>> Handle(GetAddressByUserIdQuery query, CancellationToken ctn)
        {
            try
            {
                var addressDetails = await _iAddressService.GetAddressByUserId(query.Id);
                return new ApiResponse<object>
                {
                    Data = addressDetails ?? null,
                    Message = "Address Details",
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
}
