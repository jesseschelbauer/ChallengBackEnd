public static class EndpointDefinitionExtensions {
    public static void AddEndpointDefinitions (this IServiceCollection services, params Type[] scanMarkers) {

        var definitions = scanMarkers.SelectMany (m => m.Assembly.ExportedTypes.Where (x => typeof (IEndpointDefinition).IsAssignableFrom (x) && !x.IsInterface).
        Select (Activator.CreateInstance)).Cast<IEndpointDefinition>().ToList();
        definitions.ForEach(d => d?.DefineServices(services));
        services.AddSingleton (definitions as IReadOnlyCollection<IEndpointDefinition>);
    }

    public static void UseEndpointDefinitions(this WebApplication app){
        var definitions = app.Services.GetRequiredService<IReadOnlyCollection<IEndpointDefinition>>().OrderBy(d => (int)d.RegisterPrior).ToList();
        
        foreach (var definition in definitions)
        {
            definition.Define(app);
        }
    }
}