using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

public interface IEndpointDefinition
{
    void Define(WebApplication web);
    void DefineServices(IServiceCollection services);
    EndpointDefinitionRegistrationPriority RegisterPrior { get { return EndpointDefinitionRegistrationPriority.AfterAuth; }}
}