using FDP.Lib;
using FDP.Shared;
using System.Net;

namespace FDP.Application.Address
{
    public record GetAddressByIdQuery(int Id);
    public class GetAddressByIdQueryHandler
    {
        private readonly IAddressService _iAddressService;
        public GetAddressByIdQueryHandler(IAddressService addressService)
        {
            _iAddressService = addressService;
        }
        public async Task<ApiResponse<object>> Handle(GetAddressByIdQuery query,CancellationToken ctn)
        {
            try
            {
                var addressDetails = await _iAddressService.GetAddressById(query.Id);
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
