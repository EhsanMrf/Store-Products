using Common.Jwt.Authorization;
using FluentValidation;
using MediatR;
using StoreProducts.CoreService.Product.CommandHandler;
using StoreProducts.Infrastructure.Mapper;
using StoreProducts.Infrastructure.Validation.Product.Create;
using StoreProducts.WebApi.ProviderExtensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.ConfigurationSwaggerGe(builder.Configuration);

builder.Services.DatabaseContext(builder.Configuration.GetSection("Connection").Value);
builder.Services.InternalizeDataBase();
builder.Services.InjectScope();
builder.Services.ConfigureJwt(builder.Configuration);
builder.Services.SingletonCrudManager();
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));
builder.Services.AddMediatR(typeof(ProductCommandHandler));

builder.Services.AddSingleton<IAuthorizationJwt, AuthorizationJwt>();

builder.Services.AddValidatorsFromAssemblyContaining<CreateProductCommandValidator>();
builder.Services.BeforeRequestInPipeLine();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
