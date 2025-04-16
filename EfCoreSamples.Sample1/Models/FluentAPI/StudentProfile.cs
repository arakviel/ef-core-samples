namespace EfCoreSamples.Sample1.Models.FluentAPI;

// Модель для демонстрації зв'язку One-to-One за допомогою Fluent API
public class StudentProfile
{
    public int Id { get; set; }
    public string Address { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;

    // Зовнішній ключ для зв'язку один-до-одного
    // Конфігурація буде виконана у DbContext за допомогою Fluent API
    public int StudentId { get; set; }

    // Навігаційна властивість для зв'язку один-до-одного
    public Student Student { get; set; } = null!;
}
