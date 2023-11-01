using Common.Database;
using Common.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StoreProducts.Core.User.Entity;
using System.Reflection.Emit;

namespace StoreProducts.Infrastructure.Database;

public class DatabaseContext : IdentityDbContext<User,IdentityRole<int>,int>
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options):base(options)
    {
        
    }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.SoftDeleteGlobalFilter();

        builder.Entity<Core.Product.Entity.Product>().ToTable("Products");

        foreach (var relationship in builder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            relationship.DeleteBehavior = DeleteBehavior.Restrict;

        builder.Entity<IdentityUserLogin<int>>().HasKey(p => new { p.ProviderKey, p.LoginProvider });
        builder.Entity<IdentityUserRole<int>>().HasKey(p => new { p.UserId, p.RoleId });
        builder.Entity<IdentityUserToken<int>>().HasKey(p => new { p.UserId, p.LoginProvider });
    }
}