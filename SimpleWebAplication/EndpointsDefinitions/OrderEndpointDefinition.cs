using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using SimpleWebAplication.Models;
using SimpleWebAplication.Services;
using SimpleWebAplication.Validators;

public class OrderEndpointDefinition : IEndpointDefinition
{
    public void Define(WebApplication web)
    {
        web.MapPost("/order", async (IValidator<OrderRequest> validator, IOrderService orderService, [FromBody] OrderRequest request, CancellationToken ct) =>
        {
            ValidationResult validationResult = validator.Validate(request);

            if (!validationResult.IsValid)
            {
                return Results.ValidationProblem(validationResult.ToDictionary());
            }

            var result = await orderService.Create(request, ct).ConfigureAwait(false);

            if (!result.IsOk)
                return Results.Extensions.ServiceResult(result.ErrorResponse);

            return Results.Ok(result.Result);
        });
    }

    public void DefineServices(IServiceCollection services)
    {
        services.AddScoped<IUserAssetRepository, UserAssetRepository>();
        services.AddScoped<IAssetService, AssetService>();
        services.AddScoped<IOrderService, OrderService>();
    }
}