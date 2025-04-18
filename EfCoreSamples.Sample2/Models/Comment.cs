namespace EfCoreSamples.Sample2.Models;

/// <summary>
/// Клас Comment представляє коментар до посту.
/// Демонструє ієрархічні дані (коментарі можуть мати відповіді).
/// </summary>
public class Comment
{
    /// <summary>
    /// Унікальний ідентифікатор коментаря
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Текст коментаря
    /// </summary>
    public string Content { get; set; } = null!;

    /// <summary>
    /// Ім'я автора коментаря
    /// </summary>
    public string AuthorName { get; set; } = null!;

    /// <summary>
    /// Email автора коментаря
    /// </summary>
    public string AuthorEmail { get; set; } = null!;

    /// <summary>
    /// Дата створення коментаря
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    /// <summary>
    /// Ідентифікатор посту, до якого належить коментар
    /// </summary>
    public int PostId { get; set; }

    /// <summary>
    /// Пост, до якого належить коментар (навігаційна властивість)
    /// </summary>
    public virtual Post Post { get; set; } = null!;

    /// <summary>
    /// Ідентифікатор батьківського коментаря (якщо це відповідь на інший коментар)
    /// Null, якщо це коментар верхнього рівня
    /// </summary>
    public int? ParentCommentId { get; set; }

    /// <summary>
    /// Батьківський коментар (навігаційна властивість)
    /// Демонструє самореферентний зв'язок для ієрархічних даних
    /// </summary>
    public virtual Comment? ParentComment { get; set; }

    /// <summary>
    /// Колекція відповідей на цей коментар (навігаційна властивість)
    /// Демонструє самореферентний зв'язок для ієрархічних даних
    /// </summary>
    public virtual ICollection<Comment> Replies { get; set; } = new List<Comment>();

    /// <summary>
    /// Чи схвалений коментар модератором
    /// </summary>
    public bool IsApproved { get; set; }
}
