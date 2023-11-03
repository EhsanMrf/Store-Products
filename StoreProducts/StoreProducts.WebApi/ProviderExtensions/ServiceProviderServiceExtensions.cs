using System.Text;
using Common.Jwt;
using Common.OperationCrud;
using Common.TransientService;
using MediatR;
using MediatR.Pipeline;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using StoreProducts.Core.Product.Entity;
using StoreProducts.Core.User.Entity;
using StoreProducts.Infrastructure.Database;
using StoreProducts.Infrastructure.Date.Product.Command;
using StoreProducts.Infrastructure.MediatR;
using StoreProducts.Infrastructure.Product.Builder;

namespace StoreProducts.WebApi.ProviderExtensions;

public static class ServiceProviderServiceExtensions
{

    public static void ConfigurationSwaggerGe(this IServiceCollection services, IConfiguration configuration)
    {
        var swaggerDoc = configuration.GetSection("SwaggerDoc").Get<SwaggerDoc>();
        services.AddSwaggerGen(x =>
        {
            x.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = swaggerDoc.Version,
                Title = swaggerDoc.Title,
                Description = swaggerDoc.Description,
                Contact = new OpenApiContact { Name = swaggerDoc.Content }
            });
            x.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "JWt Authorization header using Bearer schema"
            });
            x.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },new string[]{}
                }
            });
        });
    }

    public static void ConfigureJwt(this IServiceCollection services, IConfiguration configuration)
    {
        var jwtConfig = configuration.GetSection("jwtConfig").Get<JwtConfig>();
        services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtConfig.ValidIssuer,
                    ValidAudience = jwtConfig.ValidAudience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfig.Secret))
                };
            });
    }

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

    public static void SingletonCrudManager(this IServiceCollection services)
    {
        services.Add(new ServiceDescriptor(typeof(ICrudManager<Product, int, DatabaseContext>),
            typeof(CrudManager<Product, int, DatabaseContext>), ServiceLifetime.Scoped));

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

    public static void BeforeRequestInPipeLine(this IServiceCollection services)
    {
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        services.AddTransient(typeof(IRequestExceptionHandler<,,>), typeof(ManageExceptionBehavior<,,>));
    }
}