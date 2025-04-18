using EfCoreSamples.Sample2.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EfCoreSamples.Sample2.Configurations;

/// <summary>
/// Клас конфігурації для сутності Blog.
/// Демонструє налаштування сутності, яка наслідує абстрактний клас.
/// </summary>
public class BlogConfiguration : IEntityTypeConfiguration<Blog>
{
    public void Configure(EntityTypeBuilder<Blog> builder)
    {
        // Налаштування таблиці
        builder.ToTable("Blogs");

        // Налаштування первинного ключа успадковується з базового класу Meta

        // Налаштування властивостей
        builder.Property(b => b.Title)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(b => b.Description)
            .HasMaxLength(500);

        builder.Property(b => b.Slug)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(b => b.MetaTitle)
            .HasMaxLength(100);

        builder.Property(b => b.MetaDescription)
            .HasMaxLength(200);

        builder.Property(b => b.MetaImage)
            .HasMaxLength(255);

        // Налаштування індексів
        builder.HasIndex(b => b.Slug)
            .IsUnique();

        // Налаштування зв'язків
        builder.HasOne(b => b.Author)
            .WithMany(a => a.Blogs)
            .HasForeignKey(b => b.AuthorId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(b => b.Posts)
            .WithOne(p => p.Blog)
            .HasForeignKey(p => p.BlogId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
