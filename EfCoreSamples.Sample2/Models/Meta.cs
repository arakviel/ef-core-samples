namespace EfCoreSamples.Sample2.Models;

/// <summary>
/// Абстрактний базовий клас для сутностей, які містять метадані для SEO.
/// Демонструє використання абстрактних класів в EF Core.
/// </summary>
public abstract class Meta
{
    /// <summary>
    /// Унікальний ідентифікатор запису
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// URL-дружній ідентифікатор (slug) для SEO
    /// </summary>
    public string Slug { get; set; } = null!;

    /// <summary>
    /// Заголовок для метатегу title
    /// </summary>
    public string? MetaTitle { get; set; }

    /// <summary>
    /// Опис для метатегу description
    /// </summary>
    public string? MetaDescription { get; set; }

    /// <summary>
    /// URL зображення для метатегів og:image та twitter:image
    /// </summary>
    public string? MetaImage { get; set; }

    /// <summary>
    /// Дата створення запису
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    /// <summary>
    /// Дата останнього оновлення запису
    /// </summary>
    public DateTime? UpdatedAt { get; set; }
}
