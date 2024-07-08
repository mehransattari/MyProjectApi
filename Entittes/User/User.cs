
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;

namespace Entittes;


public class User :IdentityUser<int>, IEntity
{
    public string FullName { get; set; }

    public int Age { get; set; }

    public GenderType Gender { get; set; }

    public bool IsActive { get; set; } = true;

    public DateTimeOffset? LastLoginDate { get; set; }

    public ICollection<Post>? Posts { get; set; }
}

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(x => x.UserName) //for identity
               .IsRequired()
               .HasMaxLength(100);

        builder.Property(x => x.FullName)
               .IsRequired()
               .HasMaxLength(100);

        builder.Property(x=>x.IsActive)
               .HasDefaultValue(true);
    }
}
public enum GenderType
{
    [Display(Name = "مرد")]
    Male = 1,

    [Display(Name = "زن")]
    Female = 2
}