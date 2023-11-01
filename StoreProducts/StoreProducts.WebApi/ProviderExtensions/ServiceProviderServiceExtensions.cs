using Common.TransientService;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StoreProducts.Core.User.Entity;
using StoreProducts.Infrastructure.Database;
using StoreProducts.Infrastructure.Date.Product.Command;
using StoreProducts.Infrastructure.Product.Builder;

namespace StoreProducts.WebApi.ProviderExtensions;

public static class ServiceProviderServiceExtensions
{
    public static void InjectScope(this IServiceCollection services)
    {
        services.Scan(scan => scan.FromAssembliesOf(typeof(ProductRepository))
            .AddClasses(classes => classes.AssignableTo<ITransientService>())
            .AsImplementedInterfaces()
            .WithScopedLifetime());

        services.Scan(scan => scan.FromAssembliesOf(typeof(ProductBuilder))
            .AddClasses(classes => classes.AssignableTo<ITransientServiceInfrastructure>())
            .AsImplementedInterfaces()
            .WithScopedLifetime());
    }  
    
    public static void InternalizeDataBase(this IServiceCollection services)
    {
        services
            .AddIdentity<User, IdentityRole<int>>()
            .AddEntityFrameworkStores<DatabaseContext>()
            .AddDefaultTokenProviders();
    }
    
    public static void DatabaseContext(this IServiceCollection services, string connection)
    {
        services.AddDbContext<DatabaseContext>(x => x.UseSqlServer(connection));
    }
}