using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace FDP.Lib;

public class LoginUserService : ILoginUserService
{
    private readonly ClaimsPrincipal _user;

    public LoginUserService(IHttpContextAccessor accessor)
    {
        //_user = accessor.HttpContext?.User ?? throw new InvalidOperationException("No user context");
        _user = accessor.HttpContext.User ;
    }

    public int? GetUserId()
    {
        var idVal = _user.FindFirstValue("UserId");
        return int.TryParse(idVal, out var id) ? id : (int?)null;
    }
}
