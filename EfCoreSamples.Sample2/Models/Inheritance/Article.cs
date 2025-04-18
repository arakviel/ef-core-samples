namespace EfCoreSamples.Sample2.Models.Inheritance;

/// <summary>
/// Клас Article представляє статтю як тип контенту.
/// Наслідує абстрактний клас Content.
/// </summary>
public class Article : Content
{
    /// <summary>
    /// Текст статті
    /// </summary>
    public string Body { get; set; } = null!;

    /// <summary>
    /// Кількість слів у статті
    /// </summary>
    public int WordCount { get; set; }

    /// <summary>
    /// Приблизний час читання в хвилинах
    /// </summary>
    public int ReadingTimeMinutes { get; set; }

    /// <summary>
    /// Ключові слова для SEO
    /// </summary>
    public string? Keywords { get; set; }
}
