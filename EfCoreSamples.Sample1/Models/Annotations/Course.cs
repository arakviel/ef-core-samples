using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EfCoreSamples.Sample1.Models.Annotations
{
    // Модель для демонстрації зв'язку One-to-Many за допомогою анотацій
    public class Course
    {
        [Key]
        public int CourseId { get; set; }
        
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = null!;
        
        // Зовнішній ключ для зв'язку один-до-багатьох
        [ForeignKey("Student")] // Явно вказуємо, що це зовнішній ключ до Student
        public int StudentId { get; set; }
        
        // Навігаційна властивість для зв'язку один-до-багатьох
        // [InverseProperty] вказує на властивість в іншому класі, яка є зворотною навігаційною властивістю
        [InverseProperty("Courses")]
        public Student Student { get; set; } = null!;
    }
}
