using PaparaHomeworkAPI.Services;

namespace PaparaHomeworkAPI.Middlewares;

public class LoginMiddleware
{
    private readonly RequestDelegate _next;

    public LoginMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context, ILoginService loginService)
    {
        var username = context.Request.Headers["Username"].FirstOrDefault();
        var password = context.Request.Headers["Password"].FirstOrDefault();

        if (username != null && password != null)
        {
            var user = loginService.Authenticate(username, password);
            if (user != null)
            {
                context.Items["User"] = user;
            }
        }
        await _next(context);
    }
}