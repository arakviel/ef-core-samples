namespace EfCoreSamples.Sample3.Models;

/// <summary>
/// Модель блогу
/// </summary>
public class Blog
{
    /// <summary>
    /// Ідентифікатор блогу
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Назва блогу
    /// </summary>
    public string Title { get; set; } = null!;

    /// <summary>
    /// Опис блогу
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// URL-дружня назва блогу (slug)
    /// </summary>
    public string Slug { get; set; } = null!;

    /// <summary>
    /// Дата створення блогу
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    /// <summary>
    /// Дата останнього оновлення блогу
    /// </summary>
    public DateTime? UpdatedAt { get; set; }

    /// <summary>
    /// Ідентифікатор автора блогу
    /// </summary>
    public int AuthorId { get; set; }

    /// <summary>
    /// Ідентифікатор категорії блогу
    /// </summary>
    public int CategoryId { get; set; }

    /// <summary>
    /// Автор блогу (навігаційна властивість)
    /// </summary>
    public virtual Author Author { get; set; } = null!;

    /// <summary>
    /// Категорія блогу (навігаційна властивість)
    /// </summary>
    public virtual Category Category { get; set; } = null!;

    /// <summary>
    /// Колекція постів у блозі (навігаційна властивість)
    /// </summary>
    public virtual ICollection<Post> Posts { get; set; } = new List<Post>();
}
