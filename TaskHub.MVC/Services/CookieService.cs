namespace TaskHub.MVC.HttpCookieService
{
    public class CookieService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CookieService(IHttpContextAccessor httpContextAccessor) 
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public void SetAccessCookie(string token)
        {
            var httpContext = _httpContextAccessor.HttpContext;
            if (httpContext == null)
                throw new InvalidOperationException("No active HttpContext.");

            httpContext.Response.Cookies.Append(
                "jwt",
                token,
                new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Strict,
                    Path = "/",
                    Expires = DateTimeOffset.UtcNow.AddDays(7)
                });
        }

        public void SetRefreshCookie(string token)
        {
            var httpContext = _httpContextAccessor.HttpContext;

            if (httpContext == null)
                throw new InvalidOperationException("No active HttpContext.");


            httpContext.Response.Cookies.Append(
                "refresh",
                token,
                new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Strict,
                    Path = "/",
                    Expires = DateTimeOffset.UtcNow.AddDays(7)
                });
        }

        public void ClearAuthCookie()
        {
            var httpContext = _httpContextAccessor.HttpContext;
            if (httpContext == null)
                throw new InvalidOperationException("No active HttpContext.");

            httpContext.Response.Cookies.Delete("jwt", new CookieOptions
            {
                Path = "/",
                Secure = true,
                SameSite = SameSiteMode.Strict
            });

            httpContext.Response.Cookies.Delete("refresh", new CookieOptions
            {
                Path = "/",
                Secure = true,
                SameSite = SameSiteMode.Strict
            });
        }

        public void ClearVerifyCookie()
        {
            var httpContext = _httpContextAccessor.HttpContext;
            if (httpContext == null)
                throw new InvalidOperationException("No active HttpContext.");

            httpContext.Response.Cookies.Delete("verify", new CookieOptions
            {
                Path = "/",
                Secure = true,
                SameSite = SameSiteMode.Strict
            });
        }

        public string? GetAccessToken()
        {
            var httpContext = _httpContextAccessor.HttpContext;
            return httpContext?.Request.Cookies["jwt"];
        }

        public string? GetRefreshToken()
        {
            var httpContext = _httpContextAccessor.HttpContext;
            return httpContext?.Request.Cookies["refresh"];
        }

        public string? GetVerifyToken()
        {
            var httpContext = _httpContextAccessor.HttpContext;
            return httpContext?.Request.Cookies["verify"];
        }


        public void SetVerifyCookie(Guid userId)
        {
            var httpContext = _httpContextAccessor.HttpContext;

            if (httpContext == null)
                throw new InvalidOperationException("No active HttpContext.");


            httpContext.Response.Cookies.Append(
                "verify",
                userId.ToString(),
                new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Strict,
                    Path = "/",
                    Expires = DateTimeOffset.UtcNow.AddDays(7)
                });
        }
    }
}
