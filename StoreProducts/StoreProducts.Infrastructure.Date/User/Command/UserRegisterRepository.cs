using Microsoft.AspNetCore.Identity;
using StoreProducts.Core.User.Command;
using StoreProducts.Core.User.RepositoryCommand;
using StoreProducts.Infrastructure.User.Builder;

namespace StoreProducts.Infrastructure.Date.User.Command;

public class UserRegisterRepository :IUserRegisterRepository
{
    private readonly UserManager<Core.User.Entity.User> _userManager;
    private readonly IUserBuilder _userBuilder;

    public UserRegisterRepository(UserManager<Core.User.Entity.User> userManager, IUserBuilder userBuilder)
    {
        _userManager = userManager;
        _userBuilder = userBuilder;
    }

    public async Task<bool> UserRegister(UserRegisterCommand command)
    {
        var user = _userBuilder.WithEmail(command.Email).WithFullName(command.FullName).Build();
        var identityResult = await _userManager.CreateAsync(user, command.Password);
        return identityResult.Succeeded;
    }
}