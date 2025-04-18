namespace EfCoreSamples.Sample2.Models;

/// <summary>
/// Клас Author представляє автора блогу.
/// Демонструє використання ValueObject (Address) як властивості сутності.
/// </summary>
public class Author
{
    /// <summary>
    /// Унікальний ідентифікатор автора
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Ім'я автора
    /// </summary>
    public string FirstName { get; set; } = null!;

    /// <summary>
    /// Прізвище автора
    /// </summary>
    public string LastName { get; set; } = null!;

    /// <summary>
    /// Повне ім'я автора
    /// </summary>
    public string FullName => $"{FirstName} {LastName}";

    /// <summary>
    /// Email автора
    /// </summary>
    public string Email { get; set; } = null!;

    /// <summary>
    /// Біографія автора
    /// </summary>
    public string? Bio { get; set; }

    /// <summary>
    /// URL аватара автора
    /// </summary>
    public string? AvatarUrl { get; set; }

    /// <summary>
    /// Адреса автора (ValueObject)
    /// </summary>
    public Address? Address { get; set; }

    /// <summary>
    /// Дата реєстрації автора
    /// </summary>
    public DateTime RegisteredAt { get; set; } = DateTime.Now;

    /// <summary>
    /// Колекція блогів, які належать автору (навігаційна властивість)
    /// </summary>
    public virtual ICollection<Blog> Blogs { get; set; } = new List<Blog>();

    /// <summary>
    /// Колекція постів, які написав автор (навігаційна властивість)
    /// </summary>
    public virtual ICollection<Post> Posts { get; set; } = new List<Post>();
}
