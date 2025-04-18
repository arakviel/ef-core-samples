using EfCoreSamples.Sample2.Data;
using EfCoreSamples.Sample2.Services;
using Microsoft.EntityFrameworkCore;

namespace EfCoreSamples.Sample2;

internal class Program
{
    static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.WriteLine("Навчальний проект-приклад Entity Framework Core з SQLite");
        Console.WriteLine("=========================================================\n");

        // Створення опцій для контексту бази даних
        var options = new DbContextOptionsBuilder<BlogDbContext>()
            .UseSqlite("Data Source=blog.db")
            .UseLazyLoadingProxies() // Включення lazy loading
            .Options;

        // Створення та налаштування бази даних
        using (var context = new BlogDbContext(options))
        {
            // Видалення та створення бази даних
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            Console.WriteLine("База даних створена.\n");

            // Наповнення бази даних тестовими даними
            var dataSeeder = new DataSeeder(context);
            dataSeeder.SeedData();
        }

        // Демонстрація різних можливостей EF Core
        using (var context = new BlogDbContext(options))
        {
            // Демонстрація Eager Loading та Lazy Loading
            DemonstrateLoading(context);

            // Демонстрація роботи з ValueObjects
            DemonstrateValueObjects(context);

            // Демонстрація різних підходів до наслідування
            DemonstrateInheritance(context);
        }

        Console.WriteLine("\nНатисніть будь-яку клавішу для завершення...");
        Console.ReadKey();
    }

    /// <summary>
    /// Демонстрація різних способів завантаження пов'язаних даних
    /// </summary>
    static void DemonstrateLoading(BlogDbContext context)
    {
        Console.WriteLine("\n=== Демонстрація різних способів завантаження пов'язаних даних ===");
        var loadingDemonstrator = new LoadingDemonstrator(context);

        // Демонстрація Eager Loading
        loadingDemonstrator.DemonstrateEagerLoading();

        // Демонстрація Lazy Loading
        loadingDemonstrator.DemonstrateLazyLoading();

        // Демонстрація Explicit Loading
        loadingDemonstrator.DemonstrateExplicitLoading();

        // Порівняння різних способів завантаження даних
        loadingDemonstrator.CompareLoadingStrategies();
    }

    /// <summary>
    /// Демонстрація роботи з ValueObjects
    /// </summary>
    static void DemonstrateValueObjects(BlogDbContext context)
    {
        Console.WriteLine("\n=== Демонстрація роботи з ValueObjects ===");
        var valueObjectDemonstrator = new ValueObjectDemonstrator(context);

        // Демонстрація роботи з ValueObject (Address)
        valueObjectDemonstrator.DemonstrateValueObjects();

        // Пояснення переваг використання ValueObjects
        valueObjectDemonstrator.ExplainValueObjectsBenefits();
    }

    /// <summary>
    /// Демонстрація різних підходів до наслідування
    /// </summary>
    static void DemonstrateInheritance(BlogDbContext context)
    {
        Console.WriteLine("\n=== Демонстрація різних підходів до наслідування ===");
        var inheritanceDemonstrator = new InheritanceDemonstrator(context);

        // Демонстрація підходу TPH (Table Per Hierarchy)
        inheritanceDemonstrator.DemonstrateTPH();

        // Демонстрація підходу TPT (Table Per Type)
        //inheritanceDemonstrator.DemonstrateTPT();

        // Демонстрація підходу TPC (Table Per Concrete class)
        //inheritanceDemonstrator.DemonstrateTPC();

        // Порівняння різних підходів до наслідування
        //inheritanceDemonstrator.CompareInheritanceApproaches();
    }
}
