namespace EfCoreSamples.Sample3.Models;

/// <summary>
/// Модель поста в блозі
/// </summary>
public class Post
{
    /// <summary>
    /// Ідентифікатор поста
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Заголовок поста
    /// </summary>
    public string Title { get; set; } = null!;

    /// <summary>
    /// Короткий опис поста
    /// </summary>
    public string? Summary { get; set; }

    /// <summary>
    /// Вміст поста
    /// </summary>
    public string Content { get; set; } = null!;

    /// <summary>
    /// URL-дружня назва поста (slug)
    /// </summary>
    public string Slug { get; set; } = null!;

    /// <summary>
    /// URL зображення для поста
    /// </summary>
    public string? ImageUrl { get; set; }

    /// <summary>
    /// Кількість переглядів поста
    /// </summary>
    public int ViewCount { get; set; }

    /// <summary>
    /// Чи опублікований пост
    /// </summary>
    public bool IsPublished { get; set; }

    /// <summary>
    /// Дата публікації поста
    /// </summary>
    public DateTime? PublishedAt { get; set; }

    /// <summary>
    /// Дата створення поста
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    /// <summary>
    /// Дата останнього оновлення поста
    /// </summary>
    public DateTime? UpdatedAt { get; set; }

    /// <summary>
    /// Ідентифікатор блогу, до якого належить пост
    /// </summary>
    public int BlogId { get; set; }

    /// <summary>
    /// Ідентифікатор автора поста
    /// </summary>
    public int AuthorId { get; set; }

    /// <summary>
    /// Блог, до якого належить пост (навігаційна властивість)
    /// </summary>
    public virtual Blog Blog { get; set; } = null!;

    /// <summary>
    /// Автор поста (навігаційна властивість)
    /// </summary>
    public virtual Author Author { get; set; } = null!;

    /// <summary>
    /// Колекція коментарів до поста (навігаційна властивість)
    /// </summary>
    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    /// <summary>
    /// Колекція тегів поста (навігаційна властивість)
    /// </summary>
    public virtual ICollection<Tag> Tags { get; set; } = new List<Tag>();
}
