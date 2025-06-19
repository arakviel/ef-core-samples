namespace EfCoreSamples.Sample3.Models;

/// <summary>
/// Модель категорії блогу
/// </summary>
public class Category
{
    /// <summary>
    /// Ідентифікатор категорії
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Назва категорії
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// Опис категорії
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// URL-дружня назва категорії (slug)
    /// </summary>
    public string Slug { get; set; } = null!;

    /// <summary>
    /// Колекція блогів у цій категорії (навігаційна властивість)
    /// </summary>
    public virtual ICollection<Blog> Blogs { get; set; } = new List<Blog>();
}
