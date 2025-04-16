namespace EfCoreSamples.Sample1.Models.Conventions;

// Модель для демонстрації зв'язків за допомогою угод (conventions)
public class Student
{
    // Первинний ключ за угодою - властивість з назвою Id
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
}
