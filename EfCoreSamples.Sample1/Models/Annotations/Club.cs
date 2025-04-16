using System.ComponentModel.DataAnnotations;

namespace EfCoreSamples.Sample1.Models.Annotations;

// Модель для демонстрації зв'язку Many-to-Many за допомогою анотацій
public class Club
{
    [Key]
    public int ClubId { get; set; }

    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = null!;

    // Навігаційна властивість для зв'язку багато-до-багатьох
    // [ForeignKey] не потрібен для багато-до-багатьох, оскільки EF Core створить таблицю зв'язків
    public List<Student> Students { get; set; } = new();
}
