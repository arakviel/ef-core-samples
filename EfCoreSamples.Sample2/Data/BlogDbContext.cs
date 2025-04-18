using EfCoreSamples.Sample2.Configurations;
using EfCoreSamples.Sample2.Configurations.Inheritance;
using EfCoreSamples.Sample2.Models;
using EfCoreSamples.Sample2.Models.Inheritance;
using Microsoft.EntityFrameworkCore;

namespace EfCoreSamples.Sample2.Data;

/// <summary>
/// Контекст бази даних для блогу.
/// Демонструє різні можливості EF Core.
/// </summary>
public class BlogDbContext : DbContext
{
    /// <summary>
    /// Конструктор за замовчуванням
    /// </summary>
    public BlogDbContext()
    {
    }

    /// <summary>
    /// Конструктор з параметрами для конфігурації
    /// </summary>
    public BlogDbContext(DbContextOptions<BlogDbContext> options)
        : base(options)
    {
    }

    /// <summary>
    /// Автори блогів
    /// </summary>
    public DbSet<Author> Authors { get; set; } = null!;

    /// <summary>
    /// Блоги
    /// </summary>
    public DbSet<Blog> Blogs { get; set; } = null!;

    /// <summary>
    /// Пости
    /// </summary>
    public DbSet<Post> Posts { get; set; } = null!;

    /// <summary>
    /// Коментарі
    /// </summary>
    public DbSet<Comment> Comments { get; set; } = null!;

    // DbSet для демонстрації наслідування TPH (Table Per Hierarchy)
    public DbSet<Content> ContentsTPH { get; set; } = null!;
    public DbSet<Article> ArticlesTPH { get; set; } = null!;
    public DbSet<Video> VideosTPH { get; set; } = null!;
    public DbSet<Podcast> PodcastsTPH { get; set; } = null!;

    // DbSet для демонстрації наслідування TPT (Table Per Type)
    public DbSet<Content> ContentsTPT { get; set; } = null!;
    public DbSet<Article> ArticlesTPT { get; set; } = null!;
    public DbSet<Video> VideosTPT { get; set; } = null!;
    public DbSet<Podcast> PodcastsTPT { get; set; } = null!;

    // DbSet для демонстрації наслідування TPC (Table Per Concrete class)
    public DbSet<Article> ArticlesTPC { get; set; } = null!;
    public DbSet<Video> VideosTPC { get; set; } = null!;
    public DbSet<Podcast> PodcastsTPC { get; set; } = null!;

    /// <summary>
    /// Налаштування підключення до бази даних
    /// </summary>
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // Якщо підключення не налаштовано, використовуємо SQLite
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder
                .UseSqlite("Data Source=blog.db")
                .UseLazyLoadingProxies(); // Включення lazy loading
        }
    }

    /// <summary>
    /// Налаштування моделі бази даних
    /// </summary>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Застосування конфігурацій для основних сутностей
        modelBuilder.ApplyConfiguration(new AuthorConfiguration());
        modelBuilder.ApplyConfiguration(new BlogConfiguration());
        modelBuilder.ApplyConfiguration(new PostConfiguration());
        modelBuilder.ApplyConfiguration(new CommentConfiguration());

        // Налаштування для демонстрації наслідування TPH (Table Per Hierarchy)
        // Використовуємо різні типи для різних підходів до наслідування
        var contentTPHType = modelBuilder.Entity<Content>().GetType();
        modelBuilder.Entity<Content>().ToTable("ContentTPH");
        modelBuilder.ApplyConfiguration(new ContentTPHConfiguration());

        // Налаштування для демонстрації наслідування TPT (Table Per Type)
        // Використовуємо різні типи для різних підходів до наслідування
        // Для демонстрації ми використовуємо ті ж самі класи, але в реальному проекті варто використовувати різні класи
        /*        modelBuilder.Entity<Content>().ToTable("ContentTPT");
                modelBuilder.Entity<Article>().ToTable("ArticlesTPT");
                modelBuilder.Entity<Video>().ToTable("VideosTPT");
                modelBuilder.Entity<Podcast>().ToTable("PodcastsTPT");
                modelBuilder.ApplyConfiguration(new ContentTPTConfiguration());
                modelBuilder.ApplyConfiguration(new ArticleTPTConfiguration());
                modelBuilder.ApplyConfiguration(new VideoTPTConfiguration());
                modelBuilder.ApplyConfiguration(new PodcastTPTConfiguration());*/

        // Налаштування для демонстрації наслідування TPC (Table Per Concrete class)
        // Стратегія відображення повинна бути налаштована тільки для кореневого типу
        // Застосування конфігурації для базового класу Content
        //modelBuilder.ApplyConfiguration(new ContentTPCConfiguration());

        // Налаштування таблиць для конкретних класів
        /*        modelBuilder.Entity<Article>().ToTable("ArticlesTPC");
                modelBuilder.Entity<Video>().ToTable("VideosTPC");
                modelBuilder.Entity<Podcast>().ToTable("PodcastsTPC");*/

        // Застосування конфігурацій для конкретних класів TPC
        /*        modelBuilder.ApplyConfiguration(new ArticleTPCConfiguration());
                modelBuilder.ApplyConfiguration(new VideoTPCConfiguration());
                modelBuilder.ApplyConfiguration(new PodcastTPCConfiguration());*/
    }
}
