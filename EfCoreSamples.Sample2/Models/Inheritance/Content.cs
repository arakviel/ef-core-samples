namespace EfCoreSamples.Sample2.Models.Inheritance;

/// <summary>
/// Абстрактний базовий клас для різних типів контенту.
/// Використовується для демонстрації різних підходів до наслідування в EF Core:
/// - TPH (Table Per Hierarchy) - одна таблиця для всієї ієрархії
/// - TPT (Table Per Type) - окрема таблиця для кожного типу
/// - TPC (Table Per Concrete class) - окрема таблиця для кожного конкретного класу
/// </summary>
public abstract class Content
{
    /// <summary>
    /// Унікальний ідентифікатор контенту
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Заголовок контенту
    /// </summary>
    public string Title { get; set; } = null!;

    /// <summary>
    /// Опис контенту
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Дата створення контенту
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    /// <summary>
    /// Дата публікації контенту
    /// </summary>
    public DateTime? PublishedAt { get; set; }

    /// <summary>
    /// Чи опублікований контент
    /// </summary>
    public bool IsPublished { get; set; }
}
