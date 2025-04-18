using EfCoreSamples.Sample2.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EfCoreSamples.Sample2.Configurations;

/// <summary>
/// Клас конфігурації для сутності Author.
/// Демонструє налаштування сутності за допомогою Fluent API.
/// </summary>
public class AuthorConfiguration : IEntityTypeConfiguration<Author>
{
    public void Configure(EntityTypeBuilder<Author> builder)
    {
        // Налаштування таблиці
        builder.ToTable("Authors");

        // Налаштування первинного ключа
        builder.HasKey(a => a.Id);

        // Налаштування властивостей
        builder.Property(a => a.FirstName)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(a => a.LastName)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(a => a.Email)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(a => a.Bio)
            .HasMaxLength(1000);

        builder.Property(a => a.AvatarUrl)
            .HasMaxLength(255);

        builder.Property(a => a.RegisteredAt)
            .IsRequired();

        // Налаштування ValueObject (Address)
        // EF Core автоматично налаштує ValueObject як вбудований тип
        // завдяки атрибуту [Owned] в класі Address
        builder.OwnsOne(a => a.Address, addressBuilder =>
        {
            addressBuilder.Property(a => a.Street)
                .HasColumnName("Street")
                .HasMaxLength(100);

            addressBuilder.Property(a => a.HouseNumber)
                .HasColumnName("HouseNumber")
                .HasMaxLength(20);

            addressBuilder.Property(a => a.City)
                .HasColumnName("City")
                .HasMaxLength(50);

            addressBuilder.Property(a => a.PostalCode)
                .HasColumnName("PostalCode")
                .HasMaxLength(20);

            addressBuilder.Property(a => a.Country)
                .HasColumnName("Country")
                .HasMaxLength(50);
        });

        // Налаштування зв'язків
        builder.HasMany(a => a.Blogs)
            .WithOne(b => b.Author)
            .HasForeignKey(b => b.AuthorId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(a => a.Posts)
            .WithOne(p => p.Author)
            .HasForeignKey(p => p.AuthorId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
