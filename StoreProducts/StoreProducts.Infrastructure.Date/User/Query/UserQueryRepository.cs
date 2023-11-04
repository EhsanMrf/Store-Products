using Common.BaseService;
using Common.Jwt;
using Common.Jwt.Authorization;
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
        private readonly IAuthorizationJwt _authorizationJwt;
        public UserQueryRepository(SignInManager<Core.User.Entity.User> signInManager, UserManager<Core.User.Entity.User> userManager, IAuthorizationJwt authorizationJwt)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _authorizationJwt = authorizationJwt;
        }

        public async Task<ServiceResponse<string>> Login(UserLoginQuery query)
        {
            var user = await _userManager.FindByEmailAsync(query.Email);
            var logIn = await _signInManager.PasswordSignInAsync(user, query.Password, false, true);
            var token = string.Empty;
            if (logIn.Succeeded)
                token = _authorizationJwt.CreateToken(null, new UserTransfer(user.Email,user.Identity.ToString()));
            return token;
        }
    }
}
