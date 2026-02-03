using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using TaskHub.Application.DTO.User;
using Microsoft.AspNetCore.Mvc;

namespace TaskHub.Infrastructure.HttpCookieService
{
    public class CookieService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CookieService(IHttpContextAccessor httpContextAccessor) 
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task SetCookieAsync(UserDTO user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new Claim("Name", user.Name ?? string.Empty),
                new Claim("Email", user.Email ?? string.Empty)
            };

            var identity = new ClaimsIdentity(claims, "CookieAuth");
            var principal = new ClaimsPrincipal(identity);

            var authProperties = new AuthenticationProperties 
            {
                IsPersistent = true,
            };

            await _httpContextAccessor.HttpContext!.SignInAsync(
                "CookieAuth",
                new ClaimsPrincipal(identity),
                authProperties
            );
        }

        public async Task SignOutAsync()
        {
            await _httpContextAccessor.HttpContext!.SignOutAsync(
                "CookieAuth"
            );
        }
    }
}
