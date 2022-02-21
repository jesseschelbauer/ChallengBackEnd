using SimpleWebAplication.Services;

public class AccountInfoEndpointDefinition : IEndpointDefinition
{
    public void Define(WebApplication web)
    {
        web.MapGet("/account-info", async (IAccountInfoService accountInfoService, CancellationToken ct) => {

            var result = await accountInfoService.Get(ct).ConfigureAwait(false);

            if (!result.IsOk)
                return Results.Extensions.ServiceResult(result.ErrorResponse);

            return Results.Ok(result.Result);
        });
    }

    public void DefineServices(IServiceCollection services)
    {
        services.AddScoped<IAccountInfoService, AccountInfoService>();
    }
}
