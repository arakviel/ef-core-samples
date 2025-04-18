using EfCoreSamples.Sample2.Models.Inheritance;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EfCoreSamples.Sample2.Configurations.Inheritance;

/// <summary>
/// Клас конфігурації для ієрархії Content з використанням підходу TPH (Table Per Hierarchy).
/// При цьому підході всі класи ієрархії зберігаються в одній таблиці.
/// </summary>
public class ContentTPHConfiguration : IEntityTypeConfiguration<Content>
{
    public void Configure(EntityTypeBuilder<Content> builder)
    {
        // Налаштування таблиці для всієї ієрархії
        builder.ToTable("ContentTPH");

        // Налаштування первинного ключа
        builder.HasKey(c => c.Id);

        // Налаштування властивостей базового класу
        builder.Property(c => c.Title)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(c => c.Description)
            .HasMaxLength(500);

        builder.Property(c => c.CreatedAt)
            .IsRequired();

        // Налаштування дискримінатора для розрізнення типів
        // За замовчуванням EF Core використовує TPH з дискримінатором "Discriminator"
        builder.HasDiscriminator<string>("ContentType")
            .HasValue<Article>("Article")
            .HasValue<Video>("Video")
            .HasValue<Podcast>("Podcast");

        // Налаштування індексу для дискримінатора
        builder.HasIndex("ContentType");

        // Налаштування властивостей для Article
        builder.HasDiscriminator()
            .HasValue<Article>("Article");

        // Налаштування властивостей для Video
        builder.HasDiscriminator()
            .HasValue<Video>("Video");

        // Налаштування властивостей для Podcast
        builder.HasDiscriminator()
            .HasValue<Podcast>("Podcast");
    }
}
