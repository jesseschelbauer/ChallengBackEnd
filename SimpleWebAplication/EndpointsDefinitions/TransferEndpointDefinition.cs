using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using SimpleWebAplication.Services;
using SimpleWebAplication.Validators;

public class TransferEndpointDefinition : IEndpointDefinition
{
    public void Define(WebApplication web)
    {
        web.MapPost("/spb/events", async (IValidator<TransferRequest> validator, ITransferService transferService, [FromBody] TransferRequest request, CancellationToken ct) =>
        {
            ValidationResult validationResult = validator.Validate(request);

            if (!validationResult.IsValid)
            {
                return Results.ValidationProblem(validationResult.ToDictionary());
            }

            var result = await transferService.Execute(request, ct).ConfigureAwait(false);

            if (!result.IsOk)
                return Results.Extensions.ServiceResult(result.ErrorResponse);

            return Results.Ok(result.Result);
        }).AllowAnonymous();
    }

    public void DefineServices(IServiceCollection services)
    {
        services.AddScoped<IUserAssetRepository, UserAssetRepository>();
        services.AddScoped<IAssetService, AssetService>();
        services.AddScoped<IOrderService, OrderService>();
        services.AddScoped<ITransferService, TransferService>();
    }
}
