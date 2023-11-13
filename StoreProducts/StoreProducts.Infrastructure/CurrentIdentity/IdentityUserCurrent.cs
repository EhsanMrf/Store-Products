using System.Linq.Expressions;
using Common.BaseService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace StoreProducts.Infrastructure.CurrentIdentity;

public abstract class IdentityUserCurrent :BaseService
{
    private readonly IHttpContextAccessor _accessor;
    private readonly UserManager<Core.User.Entity.User> _userManager;
    protected IdentityUserCurrent(IHttpContextAccessor accessor, UserManager<Core.User.Entity.User> userManager)
    {
        _accessor = accessor;
        _userManager = userManager;
    }

    public async Task<Core.User.Entity.User> CurrentUser()
    {
        var identityUser = _accessor.HttpContext!.User.Claims.FirstOrDefault(x => x.Type.Equals("IdentityUser")).Value;
        var userBySelect = GetUserBySelect();
        var firstOrDefaultAsync = await _userManager.Users
            .Where(x=>x.Identity.Equals(identityUser)).Select(userBySelect).FirstOrDefaultAsync();
        return firstOrDefaultAsync;
    }

    public abstract Expression<Func<Core.User.Entity.User, Core.User.Entity.User>> GetUserBySelect();
}