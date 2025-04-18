namespace EfCoreSamples.Sample2.Models;

/// <summary>
/// Клас Post представляє публікацію в блозі.
/// Наслідує абстрактний клас Meta для отримання SEO-властивостей.
/// </summary>
public class Post : Meta
{
    /// <summary>
    /// Заголовок посту
    /// </summary>
    public string Title { get; set; } = null!;

    /// <summary>
    /// Короткий опис посту
    /// </summary>
    public string? Summary { get; set; }

    /// <summary>
    /// Вміст посту
    /// </summary>
    public string Content { get; set; } = null!;

    /// <summary>
    /// Ідентифікатор блогу, до якого належить пост
    /// </summary>
    public int BlogId { get; set; }

    /// <summary>
    /// Блог, до якого належить пост (навігаційна властивість)
    /// Демонструє зв'язок багато-до-одного (Many-to-One)
    /// </summary>
    public virtual Blog Blog { get; set; } = null!;

    /// <summary>
    /// Ідентифікатор автора посту
    /// </summary>
    public int AuthorId { get; set; }

    /// <summary>
    /// Автор посту (навігаційна властивість)
    /// </summary>
    public virtual Author Author { get; set; } = null!;

    /// <summary>
    /// Колекція коментарів до посту (навігаційна властивість)
    /// Демонструє зв'язок один-до-багатьох (One-to-Many)
    /// </summary>
    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    /// <summary>
    /// Чи опублікований пост
    /// </summary>
    public bool IsPublished { get; set; }

    /// <summary>
    /// Дата публікації посту
    /// </summary>
    public DateTime? PublishedAt { get; set; }
}
