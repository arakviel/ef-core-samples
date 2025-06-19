namespace EfCoreSamples.Sample3.Models;

/// <summary>
/// Модель коментаря до поста
/// </summary>
public class Comment
{
    /// <summary>
    /// Ідентифікатор коментаря
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Вміст коментаря
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
    /// Чи схвалений коментар модератором
    /// </summary>
    public bool IsApproved { get; set; }

    /// <summary>
    /// Ідентифікатор поста, до якого належить коментар
    /// </summary>
    public int PostId { get; set; }

    /// <summary>
    /// Ідентифікатор батьківського коментаря (для відповідей на коментарі)
    /// </summary>
    public int? ParentCommentId { get; set; }

    /// <summary>
    /// Пост, до якого належить коментар (навігаційна властивість)
    /// </summary>
    public virtual Post Post { get; set; } = null!;

    /// <summary>
    /// Батьківський коментар (навігаційна властивість)
    /// </summary>
    public virtual Comment? ParentComment { get; set; }

    /// <summary>
    /// Дочірні коментарі (відповіді) (навігаційна властивість)
    /// </summary>
    public virtual ICollection<Comment> Replies { get; set; } = new List<Comment>();
}
