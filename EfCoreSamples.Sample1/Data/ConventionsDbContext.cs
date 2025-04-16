using EfCoreSamples.Sample1.Models.Conventions;
using Microsoft.EntityFrameworkCore;

namespace EfCoreSamples.Sample1.Data;

// Контекст бази даних для демонстрації зв'язків за допомогою угод (conventions)
public class ConventionsDbContext : DbContext
{
    public ConventionsDbContext() { }

    // Конструктор з параметрами для конфігурації
    public ConventionsDbContext(DbContextOptions<ConventionsDbContext> options)
        : base(options)
    {
    }

    // DbSet для кожної моделі
    public DbSet<Student> Students { get; set; } = null!;
    public DbSet<StudentProfile> StudentProfiles { get; set; } = null!;
    public DbSet<Course> Courses { get; set; } = null!;
    public DbSet<Club> Clubs { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);

        optionsBuilder.UseSqlite("Data Source=conventions.db");
    }

    // Метод OnModelCreating не потрібен для угод,
    // оскільки EF Core автоматично налаштує зв'язки на основі структури класів
}
