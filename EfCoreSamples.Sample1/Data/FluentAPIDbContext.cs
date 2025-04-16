using EfCoreSamples.Sample1.Models.FluentAPI;
using Microsoft.EntityFrameworkCore;

namespace EfCoreSamples.Sample1.Data;

// Контекст бази даних для демонстрації зв'язків за допомогою Fluent API
public class FluentAPIDbContext : DbContext
{
    // Конструктор з параметрами для конфігурації
    public FluentAPIDbContext(DbContextOptions<FluentAPIDbContext> options)
        : base(options)
    {
    }

    // DbSet для кожної моделі
    public DbSet<Student> Students { get; set; } = null!;
    public DbSet<StudentProfile> StudentProfiles { get; set; } = null!;
    public DbSet<Course> Courses { get; set; } = null!;
    public DbSet<Club> Clubs { get; set; } = null!;
    public DbSet<StudentClub> StudentClubs { get; set; } = null!;

    // Метод OnModelCreating для конфігурації зв'язків за допомогою Fluent API
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Конфігурація для Student
        modelBuilder.Entity<Student>(entity =>
        {
            // Встановлення первинного ключа
            entity.HasKey(e => e.Id);

            // Конфігурація властивості Name
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100);
        });

        // Конфігурація для StudentProfile (One-to-One)
        modelBuilder.Entity<StudentProfile>(entity =>
        {
            // Встановлення первинного ключа
            entity.HasKey(e => e.Id);

            // Конфігурація властивостей
            entity.Property(e => e.Address)
                .IsRequired()
                .HasMaxLength(200);

            entity.Property(e => e.PhoneNumber)
                .IsRequired()
                .HasMaxLength(20);

            // Конфігурація зв'язку One-to-One
            entity.HasOne(sp => sp.Student)
                .WithOne(s => s.Profile)
                .HasForeignKey<StudentProfile>(d => d.StudentId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        // Конфігурація для Course (One-to-Many)
        modelBuilder.Entity<Course>(entity =>
        {
            // Встановлення первинного ключа
            entity.HasKey(e => e.Id);

            // Конфігурація властивості Name
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100);

            // Конфігурація зв'язку One-to-Many
            entity.HasOne(c => c.Student)
                .WithMany(s => s.Courses)
                .HasForeignKey(d => d.StudentId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        // Конфігурація для Club
        modelBuilder.Entity<Club>(entity =>
        {
            // Встановлення первинного ключа
            entity.HasKey(e => e.Id);

            // Конфігурація властивості Name
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100);
        });

        // Конфігурація для StudentClub (Many-to-Many з додатковими властивостями)
        modelBuilder.Entity<StudentClub>(entity =>
        {
            // Встановлення складеного ключа
            entity.HasKey(e => new { e.StudentId, e.ClubId });

            // Конфігурація зв'язку Many-to-Many
            entity.HasOne(sc => sc.Student)
                .WithMany(s => s.StudentClubs)
                .HasForeignKey(d => d.StudentId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(sc => sc.Club)
                .WithMany(c => c.StudentClubs)
                .HasForeignKey(d => d.ClubId)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }
}
