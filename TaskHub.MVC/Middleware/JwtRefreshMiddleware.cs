using TaskHub.Application.Interfaces;
using TaskHub.MVC.HttpCookieService;

namespace TaskHub.MVC.Middleware
{
    public class JwtRefreshMiddleware
    {
        private readonly RequestDelegate _next;

        public JwtRefreshMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, IJwtService jwtService, IRefreshTokenService refreshTokenService, CookieService cookieService)
        {
            var ct = context.RequestAborted;

            var accessToken = cookieService.GetAccessToken();
            var refreshToken = cookieService.GetRefreshToken();

            if (!string.IsNullOrEmpty(accessToken) && !jwtService.ValidateAccessToken(accessToken))
            {
                var newTokens = await refreshTokenService.RefreshAsync(refreshToken, ct);

                if (newTokens == null)
                {
                    cookieService.ClearAuthCookie();
                    context.Response.Redirect("/Auth/SignIn");
                    return;
                }

                cookieService.SetAccessCookie(newTokens.AccessToken);
                cookieService.SetRefreshCookie(newTokens.RefreshToken);
            }

            await _next(context);
        }
    }
}
