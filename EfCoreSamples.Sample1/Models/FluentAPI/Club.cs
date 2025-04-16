namespace EfCoreSamples.Sample1.Models.FluentAPI;

// Модель для демонстрації зв'язку Many-to-Many за допомогою Fluent API
public class Club
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;

    // Навігаційна властивість для зв'язку багато-до-багатьох
    // Конфігурація буде виконана у DbContext за допомогою Fluent API
    public List<Student> Students { get; set; } = new();

    public List<StudentClub> StudentClubs { get; set; } = new();
}
