namespace SimpleWebAplication.EndpointsDefinitions;

public class UserInfoMiddleware 
{
    private readonly RequestDelegate _next;

    public UserInfoMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext httpContext, IUserRepository repository)
    {
        var userName = httpContext.User?.Identity?.Name;
        
        if (userName != null)
        {
            var user = await repository.GetByEmail(userName, CancellationToken.None);
            httpContext.Items.Add("user", user);
        }

        await _next(httpContext);
    }
}
