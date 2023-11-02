using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace StoreProducts.Infrastructure.Database;

public class DatabaseContext : IdentityDbContext<Core.User.Entity.User,IdentityRole<int>,int>
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options):base(options)
    {
        
    }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        //builder.SoftDeleteGlobalFilter();
        builder.Entity<Core.Product.Entity.Product>().HasQueryFilter(p => !p.IsDeleted);

        builder.Entity<Core.Product.Entity.Product>().ToTable("Products");

        foreach (var relationship in builder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            relationship.DeleteBehavior = DeleteBehavior.Restrict;

        builder.Entity<IdentityUserLogin<int>>().HasKey(p => new { p.ProviderKey, p.LoginProvider });
        builder.Entity<IdentityUserRole<int>>().HasKey(p => new { p.UserId, p.RoleId });
        builder.Entity<IdentityUserToken<int>>().HasKey(p => new { p.UserId, p.LoginProvider });
    }
}