using Common.BaseService;
using Common.Response;
using Microsoft.AspNetCore.Identity;
using StoreProducts.Core.User.Query;
using StoreProducts.Core.User.RepositoryQuery;

namespace StoreProducts.Infrastructure.Date.User.Query
{
    public class UserQueryRepository : BaseService,IUserQueryRepository
    {
        private readonly SignInManager<Core.User.Entity.User> _signInManager;
        private readonly UserManager<Core.User.Entity.User>  _userManager;

        public UserQueryRepository(SignInManager<Core.User.Entity.User> signInManager, UserManager<Core.User.Entity.User> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public async Task<ServiceResponse<bool>> Login(UserLoginQuery query)
        {
            var user = await _userManager.FindByEmailAsync(query.Email);
            var logIn = await _signInManager.PasswordSignInAsync(user, query.Password, false, true);
            return logIn.IsLockedOut ? Invalid(false) : logIn.Succeeded;
        }
    }
}
