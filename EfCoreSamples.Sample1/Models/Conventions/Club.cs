namespace EfCoreSamples.Sample1.Models.Conventions;

// Модель для демонстрації зв'язку Many-to-Many за допомогою угод
public class Club
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;

    // Навігаційна властивість для зв'язку багато-до-багатьох
    // За угодою, EF Core створить таблицю зв'язків ClubStudent
    // з складеним ключем (ClubId, StudentId)
    public List<Student> Students { get; set; } = new();
}
