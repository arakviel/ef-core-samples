namespace EfCoreSamples.Sample1.Models.FluentAPI;

// Модель для демонстрації зв'язків за допомогою Fluent API
public class Student
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;

    // One-to-One: Один студент має один профіль
    // Навігаційна властивість для зв'язку один-до-одного
    public StudentProfile? Profile { get; set; }

    // One-to-Many: Один студент має багато курсів
    // Навігаційна властивість для зв'язку один-до-багатьох
    public List<Course> Courses { get; set; } = new();

    // Many-to-Many: Багато студентів можуть бути записані на багато гуртків
    // Навігаційна властивість для зв'язку багато-до-багатьох
    public List<Club> Clubs { get; set; } = new();

    public List<StudentClub> StudentClubs { get; set; } = new();
}
