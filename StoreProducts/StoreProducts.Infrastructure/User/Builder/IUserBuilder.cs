using Common.TransientService;

namespace StoreProducts.Infrastructure.User.Builder;

public interface IUserBuilder : ITransientServiceInfrastructure
{
    IUserBuilder WithFullName(string fullName);
    IUserBuilder WithUserName(string userName);
    IUserBuilder WithEmail(string email);

    Core.User.Entity.User Build();
}