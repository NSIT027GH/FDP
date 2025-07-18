using FDP.Application.Address;
using FDP.Lib;
using FDP.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace FDP.API.Controllers;

//[Authorize]
[Route("api/[controller]")]
[ApiController]
public class AddressController(IAddressService iAddressService, GetAddressQueryHandler getAddressQueryHandler, GetAddressByIdQueryHandler getAddressByIdHandler, GetAddressByUserIdQueryHandler getAddressByUserIdHandler, CreateAddressCommandValidator createAddressCommandValidations, GetAddressByUserIdQueryValidator getAddressByUserIdQueryValidations, GetAddressByIdQueryValidator getAddressByIdQueryValidations, CreateAddressCommandHandler createAddressHandler, UpdateAddressCommandValidator updateAddressCommandValidations, UpdateAddressCommandHandler updateAddressHandler) : ControllerBase
{
    public readonly IAddressService _iAddressService = iAddressService;
    private readonly GetAddressQueryHandler _getAddressHandler = getAddressQueryHandler;
    private readonly GetAddressByIdQueryHandler _getAddressByIdHandler = getAddressByIdHandler;
    private readonly GetAddressByUserIdQueryHandler _getAddressByUserIdHandler = getAddressByUserIdHandler;
    private readonly GetAddressByIdQueryValidator _getAddressByIdQueryValidations = getAddressByIdQueryValidations;
    private readonly GetAddressByUserIdQueryValidator _getAddressByUserIdQueryValidations = getAddressByUserIdQueryValidations;
    private readonly CreateAddressCommandHandler _createAddressHandler = createAddressHandler;
    private readonly CreateAddressCommandValidator _createAddressCommandValidations = createAddressCommandValidations;
    private readonly UpdateAddressCommandHandler _updateAddressHandler = updateAddressHandler;
    private readonly UpdateAddressCommandValidator _updateAddressCommandValidations = updateAddressCommandValidations;

   
    [HttpGet]
    [Route("GetAddress")]
    public async Task<IActionResult> GetAddress()
    {
        var result = await _getAddressHandler.Handle(new CancellationToken());
        return result.Success ? Ok(result): BadRequest(result);
    }

    [HttpGet]
    [Route("GetAddressById")]
    public async Task<IActionResult> GetAddressById(int id)
    {
        var ct = new CancellationToken();
        var query = new GetAddressByIdQuery(id);
        var validation = await _getAddressByIdQueryValidations.ValidateAsync(query, ct);
        if (!validation.IsValid)
        {
            var response = new ApiResponse<object>
            {
                Data = null,
                Message = "Validation Error : " + validation.Errors[0].ToString(),
                StatusCode = (int)HttpStatusCode.BadRequest
            };
            return BadRequest(response);
        }
        var result = await _getAddressByIdHandler.Handle(query, new CancellationToken());
        return result.Success ? Ok(result) : BadRequest(result);
    }

    [HttpGet]
    [Route("GetAddressByUserId")]
    public async Task<IActionResult> GetAddressByUserId(int id)
    {
        var ct = new CancellationToken();
        var query = new GetAddressByUserIdQuery(id);
        var validation = await _getAddressByUserIdQueryValidations.ValidateAsync(query, ct);
        if (!validation.IsValid)
        {
            var response = new ApiResponse<object>
            {
                Data = null,
                Message = "Validation Error : " + validation.Errors[0].ToString(),
                StatusCode = (int)HttpStatusCode.BadRequest
            };
            return BadRequest(response);
        }
        var result = await _getAddressByUserIdHandler.Handle(query, new CancellationToken());
        return result.Success ? Ok(result) : BadRequest(result);
    }

    [HttpPost]
    [Route("AddNewAddress")]
    public async Task<IActionResult> AddNewAddress(CreateAddressCommand command)
    {
        var ct = new CancellationToken();
        var validation = await _createAddressCommandValidations.ValidateAsync(command, ct);
        if (!validation.IsValid)
        {
            var response = new ApiResponse<object>
            {
                Data = null,
                Message = "Validation Error : " + validation.Errors.ToString(),
                StatusCode = (int)HttpStatusCode.BadRequest
            };
            return BadRequest(response);
        }

        var result = await _createAddressHandler.Handle(command, ct);
        return result.Success ? Ok(result) : BadRequest(result);
    }


    [HttpPut]
    [Route("UpdateAddressDetails")]
    public async Task<IActionResult> UpdateAddressData(UpdateAddressCommand command)
    {
        var ct = new CancellationToken();
        var validation = await _updateAddressCommandValidations.ValidateAsync(command, ct);
        if (!validation.IsValid)
        {
            var response = new ApiResponse<object>
            {
                Data = null,
                Message = "Validation Error : " + validation.Errors.ToString(),
                StatusCode = (int)HttpStatusCode.BadRequest
            };
            return BadRequest(response);
        }

        var result = await _updateAddressHandler.Handle(command, ct);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    [HttpDelete]
    [Route("AddressDelete")]
    public async Task<IActionResult> DeleteAddressData(int id)
    {
        var resData = await _iAddressService.DeleteAddress(id);
        if (resData == (int)TaskStatusEnum.TaskResultStatus.Success)
            return Ok("Deleted successfully");
        return BadRequest(resData);
    }

    [HttpPut]
    [Route("SetDefaultAddress")]
    public async Task<IActionResult> SetDefaultAddress(int id)
    {
        var resData = await _iAddressService.SetDefaultAddress(id);
       
            return Ok(resData);
        //return BadRequest(resData);
    }

    [AllowAnonymous]
    [HttpGet]
    [Route("GetCountry")]
    public async Task<IActionResult> GetCountry()
    {
        return Ok(await _iAddressService.GetCountry());
    }

    [AllowAnonymous]
    [HttpGet]
    [Route("GetStates")]
    public async Task<IActionResult> GetState()
    {
        return Ok(await _iAddressService.GetState());
    }
}
