namespace EfCoreSamples.Sample1.Models.FluentAPI;

// Модель для демонстрації зв'язку One-to-Many за допомогою Fluent API
public class Course
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;

    // Зовнішній ключ для зв'язку один-до-багатьох
    // Конфігурація буде виконана у DbContext за допомогою Fluent API
    public int StudentId { get; set; }

    // Навігаційна властивість для зв'язку один-до-багатьох
    public Student Student { get; set; } = null!;
}
