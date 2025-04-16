using System.ComponentModel.DataAnnotations;

namespace EfCoreSamples.Sample1.Models.Conventions;

// Модель для демонстрації зв'язку One-to-One за допомогою угод
public class StudentProfile
{
    // Первинний ключ, який також є зовнішнім ключем до Student
    // За угодою, якщо властивість називається {NavigationPropertyName}Id, 
    // вона буде використана як зовнішній ключ
    [Key]
    public int StudentId { get; set; }

    public string Address { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;

    // Навігаційна властивість для зв'язку один-до-одного
    // За угодою, EF Core розпізнає це як зв'язок один-до-одного,
    // оскільки StudentId є і первинним, і зовнішнім ключем
    public Student Student { get; set; } = null!;
}
