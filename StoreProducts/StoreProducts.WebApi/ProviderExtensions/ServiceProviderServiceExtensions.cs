using Common.TransientService;

namespace StoreProducts.WebApi.ProviderExtensions;

public static class ServiceProviderServiceExtensions
{
    public static void GetService<T>(this IServiceCollection services)
    {
        services.Scan(scan => scan.FromAssembliesOf(typeof(RegisterRepository))
            .AddClasses(classes => classes.AssignableTo<ITransientService>())
            .AsImplementedInterfaces()
            .WithScopedLifetime());
    }
}