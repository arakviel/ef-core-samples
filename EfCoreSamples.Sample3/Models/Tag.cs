namespace EfCoreSamples.Sample3.Models;

/// <summary>
/// Модель тегу для постів
/// </summary>
public class Tag
{
    /// <summary>
    /// Ідентифікатор тегу
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Назва тегу
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// URL-дружня назва тегу (slug)
    /// </summary>
    public string Slug { get; set; } = null!;

    /// <summary>
    /// Колекція постів з цим тегом (навігаційна властивість)
    /// </summary>
    public virtual ICollection<Post> Posts { get; set; } = new List<Post>();
}
