using EfCoreSamples.Sample2.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EfCoreSamples.Sample2.Configurations;

/// <summary>
/// Клас конфігурації для сутності Comment.
/// Демонструє налаштування ієрархічних даних (самореферентний зв'язок).
/// </summary>
public class CommentConfiguration : IEntityTypeConfiguration<Comment>
{
    public void Configure(EntityTypeBuilder<Comment> builder)
    {
        // Налаштування таблиці
        builder.ToTable("Comments");

        // Налаштування первинного ключа
        builder.HasKey(c => c.Id);

        // Налаштування властивостей
        builder.Property(c => c.Content)
            .IsRequired()
            .HasMaxLength(1000);

        builder.Property(c => c.AuthorName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(c => c.AuthorEmail)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(c => c.CreatedAt)
            .IsRequired();

        // Налаштування індексів
        builder.HasIndex(c => c.PostId);
        builder.HasIndex(c => c.ParentCommentId);
        builder.HasIndex(c => c.CreatedAt);

        // Налаштування зв'язку з постом
        builder.HasOne(c => c.Post)
            .WithMany(p => p.Comments)
            .HasForeignKey(c => c.PostId)
            .OnDelete(DeleteBehavior.Cascade);

        // Налаштування самореферентного зв'язку для ієрархічних даних
        // Коментар може мати батьківський коментар (ParentComment)
        // і колекцію дочірніх коментарів (Replies)
        builder.HasOne(c => c.ParentComment)
            .WithMany(c => c.Replies)
            .HasForeignKey(c => c.ParentCommentId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
