using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EfCoreSamples.Sample1.Models.Annotations;

// Модель для демонстрації зв'язку One-to-One за допомогою анотацій
public class StudentProfile
{
    [Key] // Вказуємо, що це первинний ключ
    [ForeignKey("Student")] // Вказуємо, що це також зовнішній ключ до Student
    public int StudentId { get; set; }

    [Required]
    [MaxLength(200)]
    public string Address { get; set; } = null!;

    [Required]
    [MaxLength(20)]
    public string PhoneNumber { get; set; } = null!;

    // Навігаційна властивість для зв'язку один-до-одного
    // [Required] вказує, що зв'язок обов'язковий
    [Required]
    public Student Student { get; set; } = null!;
}
