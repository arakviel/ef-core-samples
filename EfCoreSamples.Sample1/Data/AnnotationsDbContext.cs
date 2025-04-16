using EfCoreSamples.Sample1.Models.Annotations;
using Microsoft.EntityFrameworkCore;

namespace EfCoreSamples.Sample1.Data;

// Контекст бази даних для демонстрації зв'язків за допомогою анотацій (annotations)
public class AnnotationsDbContext : DbContext
{
    // Конструктор з параметрами для конфігурації
    public AnnotationsDbContext(DbContextOptions<AnnotationsDbContext> options)
        : base(options)
    {
    }

    // DbSet для кожної моделі
    public DbSet<Student> Students { get; set; } = null!;
    public DbSet<StudentProfile> StudentProfiles { get; set; } = null!;
    public DbSet<Course> Courses { get; set; } = null!;
    public DbSet<Club> Clubs { get; set; } = null!;

    // Метод OnModelCreating не потрібен для анотацій,
    // оскільки EF Core автоматично налаштує зв'язки на основі анотацій в класах
}
