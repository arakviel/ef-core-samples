using EfCoreSamples.Sample2.Models.Inheritance;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EfCoreSamples.Sample2.Configurations.Inheritance;

/// <summary>
/// Клас конфігурації для ієрархії Content з використанням підходу TPT (Table Per Type).
/// При цьому підході для кожного типу створюється окрема таблиця, яка містить
/// лише властивості, специфічні для цього типу, а спільні властивості зберігаються
/// в таблиці базового класу.
/// </summary>
public class ContentTPTConfiguration : IEntityTypeConfiguration<Content>
{
    public void Configure(EntityTypeBuilder<Content> builder)
    {
        // Налаштування таблиці для базового класу
        builder.ToTable("ContentTPT");

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

        // Налаштування TPT для Article
        builder.UseTptMappingStrategy();
    }
}

/// <summary>
/// Клас конфігурації для Article з використанням підходу TPT.
/// </summary>
public class ArticleTPTConfiguration : IEntityTypeConfiguration<Article>
{
    public void Configure(EntityTypeBuilder<Article> builder)
    {
        // Налаштування таблиці для Article
        builder.ToTable("ArticlesTPT");

        // Налаштування властивостей
        builder.Property(a => a.Body)
            .IsRequired();

        builder.Property(a => a.Keywords)
            .HasMaxLength(200);
    }
}

/// <summary>
/// Клас конфігурації для Video з використанням підходу TPT.
/// </summary>
public class VideoTPTConfiguration : IEntityTypeConfiguration<Video>
{
    public void Configure(EntityTypeBuilder<Video> builder)
    {
        // Налаштування таблиці для Video
        builder.ToTable("VideosTPT");

        // Налаштування властивостей
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
/// Клас конфігурації для Podcast з використанням підходу TPT.
/// </summary>
public class PodcastTPTConfiguration : IEntityTypeConfiguration<Podcast>
{
    public void Configure(EntityTypeBuilder<Podcast> builder)
    {
        // Налаштування таблиці для Podcast
        builder.ToTable("PodcastsTPT");

        // Налаштування властивостей
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
