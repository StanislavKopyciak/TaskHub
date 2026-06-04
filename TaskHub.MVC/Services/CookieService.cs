namespace TaskHub.MVC.HttpCookieService
{
    public class CookieService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CookieService(IHttpContextAccessor httpContextAccessor) 
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public void SetCookie(string token)
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
                    Expires = DateTimeOffset.Now.AddDays(1)
                });
        }

        public void SignOut()
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
        }
    }
}
