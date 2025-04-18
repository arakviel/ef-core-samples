using EfCoreSamples.Sample2.Models.Inheritance;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EfCoreSamples.Sample2.Configurations.Inheritance;

/// <summary>
/// Клас конфігурації для ієрархії Content з використанням підходу TPC (Table Per Concrete class).
/// При цьому підході для кожного конкретного класу створюється окрема таблиця,
/// яка містить всі властивості, включаючи успадковані від базового класу.
/// </summary>
public class ContentTPCConfiguration : IEntityTypeConfiguration<Content>
{
    public void Configure(EntityTypeBuilder<Content> builder)
    {
        // Налаштування первинного ключа для базового класу
        builder.HasKey(c => c.Id);

        // Налаштування властивостей базового класу
        builder.Property(c => c.Title)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(c => c.Description)
            .HasMaxLength(500);

        builder.Property(c => c.CreatedAt)
            .IsRequired();

        // Налаштування стратегії відображення TPC
        builder.UseTpcMappingStrategy();
    }
}

/// <summary>
/// Клас конфігурації для Article з використанням підходу TPC.
/// </summary>
public class ArticleTPCConfiguration : IEntityTypeConfiguration<Article>
{
    public void Configure(EntityTypeBuilder<Article> builder)
    {
        // Налаштування властивостей базового класу
        builder.Property(a => a.Title)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(a => a.Description)
            .HasMaxLength(500);

        builder.Property(a => a.CreatedAt)
            .IsRequired();

        // Налаштування властивостей Article
        builder.Property(a => a.Body)
            .IsRequired();

        builder.Property(a => a.Keywords)
            .HasMaxLength(200);
    }
}

/// <summary>
/// Клас конфігурації для Video з використанням підходу TPC.
/// </summary>
public class VideoTPCConfiguration : IEntityTypeConfiguration<Video>
{
    public void Configure(EntityTypeBuilder<Video> builder)
    {
        // Налаштування властивостей базового класу
        builder.Property(v => v.Title)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(v => v.Description)
            .HasMaxLength(500);

        builder.Property(v => v.CreatedAt)
            .IsRequired();

        // Налаштування властивостей Video
        builder.Property(v => v.Url)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(v => v.Format)
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(v => v.Resolution)
            .IsRequired()
            .HasMaxLength(20);
    }
}

/// <summary>
/// Клас конфігурації для Podcast з використанням підходу TPC.
/// </summary>
public class PodcastTPCConfiguration : IEntityTypeConfiguration<Podcast>
{
    public void Configure(EntityTypeBuilder<Podcast> builder)
    {
        // Налаштування властивостей базового класу
        builder.Property(p => p.Title)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(p => p.Description)
            .HasMaxLength(500);

        builder.Property(p => p.CreatedAt)
            .IsRequired();

        // Налаштування властивостей Podcast
        builder.Property(p => p.AudioUrl)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(p => p.Host)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(p => p.Guests)
            .HasMaxLength(500);

        builder.Property(p => p.Transcript)
            .HasMaxLength(10000);
    }
}
