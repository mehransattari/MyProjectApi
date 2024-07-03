
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Entittes;


public class Role : IdentityRole<int> , IEntity
{
    public string Description { get; set; } = string.Empty;
}

public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.Property(x => x.Name)
               .IsRequired()
               .HasMaxLength(50);

        builder.Property(x => x.Description)
               .HasMaxLength(200);
    }
}
