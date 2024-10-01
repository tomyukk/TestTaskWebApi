using SimpleWebApi.Interfaces;

namespace SimpleWebApi.Middlewares
{
    public class AuthenticationMiddleware
    {
        private readonly RequestDelegate _next;

        public AuthenticationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, IUserRepository userService, IJwtService jwtService)
        {
            var token = context.Request.Headers["Authorization"];
            var userId = jwtService.ValidateToken(token);
            if (!string.IsNullOrEmpty(userId))
            {
                context.Items["User"] = await userService.GetByIdAsync(userId);
            }

            await _next(context);
        }
    }
}
