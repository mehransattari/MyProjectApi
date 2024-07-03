using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Entittes;


public class Category : BaseEntity
{
    public string Name { get; set; } = string.Empty;

    public int? ParentCategoryId { get; set; }

    public Category? ParentCategory { get; set; }

    public ICollection<Category>? ChildCategories { get; set; }

    public ICollection<Post>? Posts { get; set; }
}

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.Property(x => x.Name)
               .IsRequired()
               .HasMaxLength(50);

        builder.HasOne(x => x.ParentCategory)
               .WithMany(x=>x.ChildCategories)
               .HasForeignKey(x=>x.ParentCategoryId);

        builder.HasMany(x => x.ChildCategories)
               .WithOne(x => x.ParentCategory)
               .HasForeignKey(x=>x.ParentCategoryId);
            
        builder.HasMany(x => x.Posts)
               .WithOne(x=>x.Category)
               .HasForeignKey(x=>x.CategoryId);
    }
}