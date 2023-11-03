using Microsoft.AspNetCore.Identity;
using StoreProducts.Core.User.Enum;

namespace StoreProducts.Core.User.Entity;

public class User:IdentityUser<int>
{
    public string FullName { get; set; }
    public UserType UserType { get; set; }
}