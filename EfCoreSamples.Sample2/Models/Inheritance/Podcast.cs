namespace EfCoreSamples.Sample2.Models.Inheritance;

/// <summary>
/// Клас Podcast представляє подкаст як тип контенту.
/// Наслідує абстрактний клас Content.
/// </summary>
public class Podcast : Content
{
    /// <summary>
    /// URL аудіофайлу
    /// </summary>
    public string AudioUrl { get; set; } = null!;

    /// <summary>
    /// Тривалість подкасту в секундах
    /// </summary>
    public int DurationInSeconds { get; set; }

    /// <summary>
    /// Ведучий подкасту
    /// </summary>
    public string Host { get; set; } = null!;

    /// <summary>
    /// Гості подкасту (розділені комами)
    /// </summary>
    public string? Guests { get; set; }

    /// <summary>
    /// Транскрипція подкасту
    /// </summary>
    public string? Transcript { get; set; }
}
