using EfCoreSamples.Sample3.Data;
using EfCoreSamples.Sample3.Services;
using Microsoft.EntityFrameworkCore;

namespace EfCoreSamples.Sample3;

/// <summary>
/// Головний клас програми
/// </summary>
internal class Program
{
    /// <summary>
    /// Головний метод програми
    /// </summary>
    static async Task Main(string[] args)
    {
        try
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("=== Entity Framework Core - Демонстрація LINQ запитів ===\n");
            Console.WriteLine("Програма запущена успішно!");

            // Налаштування контексту бази даних
            var options = new DbContextOptionsBuilder<BlogDbContext>()
                .UseSqlite("Data Source=blog.db")
                .UseLazyLoadingProxies()
                .LogTo(Console.WriteLine, Microsoft.Extensions.Logging.LogLevel.Information)
                .EnableSensitiveDataLogging()
                .Options;

            // Створення та налаштування бази даних
            using (var context = new BlogDbContext(options))
            {
                // Видалення та створення бази даних
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                Console.WriteLine("База даних створена.\n");

                // Наповнення бази даних тестовими даними
                var dataSeeder = new BlogDataSeeder(context);
                dataSeeder.SeedData();

                // Наповнення фейковими даними 
                //FakeSeeder fakeSeeder = new FakeSeeder(context);
                //await fakeSeeder.SeedAsync(10);
            }

            // Демонстрація LINQ запитів
            Console.WriteLine("\nДемонстрація LINQ запитів з Entity Framework Core");
            Console.WriteLine("Натисніть будь-яку клавішу для продовження...");
            Console.ReadKey();

            using (var context = new BlogDbContext(options))
            {
                var linqDemonstrator = new LinqDemonstrator(context);

                // Демонстрація базових запитів
                linqDemonstrator.DemonstrateBasicQueries();
                Console.WriteLine("\nНатисніть будь-яку клавішу для продовження...");
                Console.ReadKey();

                // Демонстрація агрегатних запитів
                linqDemonstrator.DemonstrateAggregateQueries();
                Console.WriteLine("\nНатисніть будь-яку клавішу для продовження...");
                Console.ReadKey();

                // Демонстрація запитів з групуванням
                linqDemonstrator.DemonstrateGroupingQueries();
                Console.WriteLine("\nНатисніть будь-яку клавішу для продовження...");
                Console.ReadKey();

                // Демонстрація запитів з з'єднаннями
                linqDemonstrator.DemonstrateJoinQueries();
                Console.WriteLine("\nНатисніть будь-яку клавішу для продовження...");
                Console.ReadKey();

                // Демонстрація запитів з Include
                linqDemonstrator.DemonstrateIncludeQueries();
                Console.WriteLine("\nНатисніть будь-яку клавішу для продовження...");
                Console.ReadKey();

                // Демонстрація складних запитів
                linqDemonstrator.DemonstrateComplexQueries();
            }

            Console.WriteLine("\nДемонстрація завершена. Натисніть будь-яку клавішу для виходу...");
            Console.ReadKey();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Помилка: {ex.Message}");
            Console.WriteLine($"StackTrace: {ex.StackTrace}");

            if (ex.InnerException != null)
            {
                Console.WriteLine($"InnerException: {ex.InnerException.Message}");
                Console.WriteLine($"InnerException StackTrace: {ex.InnerException.StackTrace}");
            }

            Console.WriteLine("Натисніть будь-яку клавішу для виходу...");
            Console.ReadKey();
        }
    }
}
