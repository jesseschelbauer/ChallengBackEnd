using FluentValidation;
using SimpleWebAplication.EndpointsDefinitions;
using SimpleWebAplication.Validators;

var builder = WebApplication.CreateBuilder (args);
builder.Services.AddEndpointDefinitions(typeof(IEndpointDefinition));
builder.Services.AddValidatorsFromAssemblyContaining<RegisterRequestValidator>(lifetime: ServiceLifetime.Scoped);
var app = builder.Build ();

app.UseEndpointDefinitions();
app.UseUserInfoMiddleware();
app.Run ();