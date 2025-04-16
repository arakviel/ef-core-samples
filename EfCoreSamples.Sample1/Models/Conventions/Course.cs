namespace EfCoreSamples.Sample1.Models.Conventions;

// Модель для демонстрації зв'язку One-to-Many за допомогою угод
public class Course
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;

    // Зовнішній ключ для зв'язку один-до-багатьох
    // За угодою, якщо властивість називається {NavigationPropertyName}Id, 
    // вона буде використана як зовнішній ключ
    public int StudentId { get; set; }

    // Навігаційна властивість для зв'язку один-до-багатьох
    // За угодою, EF Core розпізнає це як зв'язок один-до-багатьох,
    // оскільки Course має властивість StudentId як зовнішній ключ
    public Student Student { get; set; } = null!;
}
