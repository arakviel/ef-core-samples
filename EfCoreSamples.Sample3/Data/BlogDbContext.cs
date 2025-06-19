using EfCoreSamples.Sample3.Models;
using Microsoft.EntityFrameworkCore;

namespace EfCoreSamples.Sample3.Data;

/// <summary>
/// Контекст бази даних для блогу
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

    /// <summary>
    /// Категорії
    /// </summary>
    public DbSet<Category> Categories { get; set; } = null!;

    /// <summary>
    /// Теги
    /// </summary>
    public DbSet<Tag> Tags { get; set; } = null!;

    /// <summary>
    /// Конфігурація моделі бази даних
    /// </summary>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Конфігурація зв'язку багато-до-багатьох між Post і Tag
        modelBuilder.Entity<Post>()
            .HasMany(p => p.Tags)
            .WithMany(t => t.Posts)
            .UsingEntity<PostTag>(
                j => j
                    .HasOne(pt => pt.Tag)
                    .WithMany()
                    .HasForeignKey(pt => pt.TagId),
                j => j
                    .HasOne(pt => pt.Post)
                    .WithMany()
                    .HasForeignKey(pt => pt.PostId),
                j =>
                {
                    j.HasKey(t => new { t.PostId, t.TagId });
                });

        // Конфігурація зв'язку один-до-багатьох між Blog і Post
        modelBuilder.Entity<Blog>()
            .HasMany(b => b.Posts)
            .WithOne(p => p.Blog)
            .HasForeignKey(p => p.BlogId)
            .OnDelete(DeleteBehavior.Cascade);

        // Конфігурація зв'язку один-до-багатьох між Author і Blog
        modelBuilder.Entity<Author>()
            .HasMany(a => a.Blogs)
            .WithOne(b => b.Author)
            .HasForeignKey(b => b.AuthorId)
            .OnDelete(DeleteBehavior.Cascade);

        // Конфігурація зв'язку один-до-багатьох між Author і Post
        modelBuilder.Entity<Author>()
            .HasMany(a => a.Posts)
            .WithOne(p => p.Author)
            .HasForeignKey(p => p.AuthorId)
            .OnDelete(DeleteBehavior.Restrict);

        // Конфігурація зв'язку один-до-багатьох між Post і Comment
        modelBuilder.Entity<Post>()
            .HasMany(p => p.Comments)
            .WithOne(c => c.Post)
            .HasForeignKey(c => c.PostId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Post>()
            .HasQueryFilter(p => p.IsPublished);

        // Конфігурація зв'язку один-до-багатьох між Comment і Comment (самозв'язок)
        modelBuilder.Entity<Comment>()
            .HasMany(c => c.Replies)
            .WithOne(c => c.ParentComment)
            .HasForeignKey(c => c.ParentCommentId)
            .OnDelete(DeleteBehavior.Restrict);

        // Конфігурація зв'язку один-до-багатьох між Category і Blog
        modelBuilder.Entity<Category>()
            .HasMany(c => c.Blogs)
            .WithOne(b => b.Category)
            .HasForeignKey(b => b.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);
    }

    /// <summary>
    /// Конфігурація підключення до бази даних
    /// </summary>
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder
                .UseSqlite("Data Source=blog.db")
                .UseLazyLoadingProxies();
        }
    }
}
