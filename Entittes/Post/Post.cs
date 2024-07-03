using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Entittes;

public class Post : BaseEntity<Guid>
{
    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public int CategoryId { get; set; }

    public int AuthorId { get; set; }

    public Category? Category { get; set; }

    public User? Author { get; set; }
}

public class PostConfiguration : IEntityTypeConfiguration<Post>
{
    public void Configure(EntityTypeBuilder<Post> builder)
    {
        builder.Property(x => x.Title)
               .IsRequired()
               .HasMaxLength(200);

        builder.Property(x => x.Description)
               .IsRequired();

        builder.HasOne(x => x.Category)
               .WithMany(x => x.Posts)
               .HasForeignKey(x=>x.CategoryId);

        builder.HasOne(x => x.Author)
               .WithMany(x => x.Posts)
               .HasForeignKey(x => x.AuthorId);
              
    }
}
