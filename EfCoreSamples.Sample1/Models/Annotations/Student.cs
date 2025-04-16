using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EfCoreSamples.Sample1.Models.Annotations;

// Модель для демонстрації зв'язків за допомогою анотацій (annotations)
public class Student
{
    [Key] // Явно вказуємо, що це первинний ключ
    public int StudentId { get; set; }

    [Required] // Вказуємо, що поле обов'язкове
    [MaxLength(100)] // Обмежуємо довжину рядка
    public string Name { get; set; } = null!;

    // One-to-One: Один студент має один профіль
    // Навігаційна властивість для зв'язку один-до-одного
    public StudentProfile? Profile { get; set; }

    // One-to-Many: Один студент має багато курсів
    // Навігаційна властивість для зв'язку один-до-багатьох
    // [InverseProperty] вказує на властивість в іншому класі, яка є зворотною навігаційною властивістю
    [InverseProperty("Student")]
    public List<Course> Courses { get; set; } = new();

    // Many-to-Many: Багато студентів можуть бути записані на багато гуртків
    // Навігаційна властивість для зв'язку багато-до-багатьох
    // [ForeignKey] не потрібен для багато-до-багатьох, оскільки EF Core створить таблицю зв'язків
    public List<Club> Clubs { get; set; } = new();
}
