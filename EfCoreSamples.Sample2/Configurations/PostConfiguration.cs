using EfCoreSamples.Sample2.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EfCoreSamples.Sample2.Configurations;

/// <summary>
/// Клас конфігурації для сутності Post.
/// Демонструє налаштування сутності, яка наслідує абстрактний клас.
/// </summary>
public class PostConfiguration : IEntityTypeConfiguration<Post>
{
    public void Configure(EntityTypeBuilder<Post> builder)
    {
        // Налаштування таблиці
        builder.ToTable("Posts");

        // Налаштування первинного ключа успадковується з базового класу Meta

        // Налаштування властивостей
        builder.Property(p => p.Title)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(p => p.Summary)
            .HasMaxLength(500);

        builder.Property(p => p.Content)
            .IsRequired();

        builder.Property(p => p.Slug)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(p => p.MetaTitle)
            .HasMaxLength(100);

        builder.Property(p => p.MetaDescription)
            .HasMaxLength(200);

        builder.Property(p => p.MetaImage)
            .HasMaxLength(255);

        // Налаштування індексів
        builder.HasIndex(p => p.Slug)
            .IsUnique();

        builder.HasIndex(p => p.PublishedAt);

        // Налаштування зв'язків
        builder.HasOne(p => p.Blog)
            .WithMany(b => b.Posts)
            .HasForeignKey(p => p.BlogId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(p => p.Author)
            .WithMany(a => a.Posts)
            .HasForeignKey(p => p.AuthorId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(p => p.Comments)
            .WithOne(c => c.Post)
            .HasForeignKey(c => c.PostId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
