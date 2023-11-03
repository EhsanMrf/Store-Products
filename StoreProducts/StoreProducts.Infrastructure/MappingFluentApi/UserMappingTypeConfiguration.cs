using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace StoreProducts.Infrastructure.MappingFluentApi;

public class UserMappingTypeConfiguration : IEntityTypeConfiguration<Core.User.Entity.User>
{
    public void Configure(EntityTypeBuilder<Core.User.Entity.User> builder)
    {
        builder.Property(x => x.UserName).HasMaxLength(150);
        builder.Property(x => x.Email).HasMaxLength(40);
        builder.Property(x => x.FullName).HasMaxLength(150);
        builder.Property(x => x.PhoneNumber).HasMaxLength(25);

        //sparse for data null => performance page and data size -- 
        builder.Property(x => x.PhoneNumber).IsSparse();
    }
}