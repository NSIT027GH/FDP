using FDP.Application.User;
using FDP.Lib;
using FDP.Shared;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FDP.API.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class UserController(IUserService userClass, IMediator mediator, CreateUserCommandHandler createUserHandler) : ControllerBase
{
    private readonly IUserService _userService = userClass;
    private readonly IMediator _iMediator = mediator;
    private readonly CreateUserCommandHandler _createUserHandler = createUserHandler;


    [HttpGet]
    [Route("GetUserDetails")]
    public IActionResult GetUser()
    {
        var result =  _userService.GetUserDetails();
        return Ok(result);
    }

    [HttpGet]
    [Route("GetUserDetailsById")]
    public async Task<IActionResult> GetUserById(int id)
    {
            var query = new GetUserByIdQuery(id);

            UserDetailsResponseModel? result;
            try
            {
                result = await _iMediator.Send(query);
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = "Internal server error" });
            }

        return result is null ? NotFound(new { message = $"User with ID {id} not found." }) : Ok(result);
    }

    [AllowAnonymous]
    [HttpPost]
    [Route("UserRegistration")]
    public async Task<IActionResult> AddUser(CreateUserCommand command)
    {

        var result = await _iMediator.Send(command);
        return Ok(result);
    }

    [HttpPut]
    [Route("UpdateUserDetails")]
    public async Task<IActionResult> UpdateUserData(UpdateUserCommand command)
    {
        var result = await _iMediator.Send(command);
        return Ok(result);
    }

    [HttpPut]
    [Route("DeleteUserDetails")]
    public async Task<IActionResult> DeleteUserData(DeleteUserCommand command)
    {
        var result = await _iMediator.Send(command);
        return Ok(result);
    }

    [AllowAnonymous]
    [HttpPost]
    [Route("UserLogin")]
    public async Task<IActionResult> UserLogin(UserLoginRequestModel userLoginRequestClass)
    {
        return Ok(await _userService.LoginUser(userLoginRequestClass));
    }
}

