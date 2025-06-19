namespace EfCoreSamples.Sample3.Models;

/// <summary>
/// Модель для зв'язку багато-до-багатьох між постами та тегами
/// </summary>
public class PostTag
{
    /// <summary>
    /// Ідентифікатор поста
    /// </summary>
    public int PostId { get; set; }

    /// <summary>
    /// Ідентифікатор тегу
    /// </summary>
    public int TagId { get; set; }

    /// <summary>
    /// Пост (навігаційна властивість)
    /// </summary>
    public virtual Post Post { get; set; } = null!;

    /// <summary>
    /// Тег (навігаційна властивість)
    /// </summary>
    public virtual Tag Tag { get; set; } = null!;
}
