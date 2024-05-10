using Application.Common.Interfaces;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;



namespace ChatServer.Web.Services;

public class CurrentUser : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUser(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public  string? UserId =>  _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
    public Task<string?> Token =>  _httpContextAccessor.HttpContext?.GetTokenAsync("access_token");

}
