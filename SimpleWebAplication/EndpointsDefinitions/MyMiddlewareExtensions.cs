namespace SimpleWebAplication.EndpointsDefinitions;

public static class MyMiddlewareExtensions
{
    public static IApplicationBuilder UseUserInfoMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<UserInfoMiddleware>();
    }
}