using Common.OperationCrud;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using StoreProducts.Core.User.Entity;
using StoreProducts.Infrastructure.Database;
using StoreProducts.Infrastructure.Mapper;

namespace StoreProducts.Test
{
    public static class ServiceCollectionExtensions 
    {
        public static IServiceCollection AddServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.Add(new ServiceDescriptor(typeof(ICrudManager<Core.Product.Entity.Product, int, DatabaseContext>),
                typeof(CrudManager<Core.Product.Entity.Product, int, DatabaseContext>), ServiceLifetime.Scoped));
            serviceCollection.AddAutoMapper(typeof(AutoMapperProfile));
            
            serviceCollection
                .AddIdentity<User, IdentityRole<int>>()
                .AddEntityFrameworkStores<DatabaseContext>()
                .AddDefaultTokenProviders();

            serviceCollection.AddDbContext<DatabaseContext>(x => x.UseSqlServer("Data Source=SARDAR\\SQL2019;Initial Catalog=ProductStr;Persist Security Info=True;TrustServerCertificate=True;User ID=sa;Password=Ehsan..1374"));

            return serviceCollection;
        }
    }
}