using Microsoft.AspNetCore.Identity;

namespace StoreProducts.Core.User.Entity;

public class User:IdentityUser<int>
{
    public string FullName { get; set; }
}