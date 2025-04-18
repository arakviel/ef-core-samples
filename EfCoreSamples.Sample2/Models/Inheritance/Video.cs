namespace EfCoreSamples.Sample2.Models.Inheritance;

/// <summary>
/// Клас Video представляє відео як тип контенту.
/// Наслідує абстрактний клас Content.
/// </summary>
public class Video : Content
{
    /// <summary>
    /// URL відео
    /// </summary>
    public string Url { get; set; } = null!;

    /// <summary>
    /// Тривалість відео в секундах
    /// </summary>
    public int DurationInSeconds { get; set; }

    /// <summary>
    /// Формат відео (наприклад, MP4, AVI)
    /// </summary>
    public string Format { get; set; } = null!;

    /// <summary>
    /// Роздільна здатність відео
    /// </summary>
    public string Resolution { get; set; } = null!;
}
