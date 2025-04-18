namespace EfCoreSamples.Sample2.Models;

/// <summary>
/// Клас Blog представляє блог у системі.
/// Наслідує абстрактний клас Meta для отримання SEO-властивостей.
/// </summary>
public class Blog : Meta
{
    /// <summary>
    /// Назва блогу
    /// </summary>
    public string Title { get; set; } = null!;

    /// <summary>
    /// Опис блогу
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Ідентифікатор автора блогу
    /// </summary>
    public int AuthorId { get; set; }

    /// <summary>
    /// Автор блогу (навігаційна властивість)
    /// </summary>
    public virtual Author Author { get; set; } = null!;

    /// <summary>
    /// Колекція постів у блозі (навігаційна властивість)
    /// Демонструє зв'язок один-до-багатьох (One-to-Many)
    /// </summary>
    public virtual ICollection<Post> Posts { get; set; } = new List<Post>();
}
