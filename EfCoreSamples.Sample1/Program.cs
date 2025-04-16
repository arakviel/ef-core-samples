using EfCoreSamples.Sample1.Data;
using EfCoreSamples.Sample1.Services;
using Microsoft.EntityFrameworkCore;

namespace EfCoreSamples.Sample1;

internal class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Приклади роботи зі зв'язками в Entity Framework Core");
        Console.WriteLine("==================================================");

        // Демонстрація роботи зі зв'язками за допомогою угод (conventions)
        DemonstrateConventions();

        // Демонстрація роботи зі зв'язками за допомогою анотацій (annotations)
        DemonstrateAnnotations();

        // Демонстрація роботи зі зв'язками за допомогою Fluent API
        DemonstrateFluentAPI();

        Console.WriteLine("\nНатисніть будь-яку клавішу для завершення...");
        Console.ReadKey();
    }

    static void DemonstrateConventions()
    {
        Console.WriteLine("\nДемонстрація роботи зі зв'язками за допомогою угод (conventions)");
        Console.WriteLine("------------------------------------------------------------------");

        // Створення опцій для контексту бази даних
        var options = new DbContextOptionsBuilder<ConventionsDbContext>()
            .UseSqlite("Data Source=conventions.db")
            .Options;

        // Створення та налаштування бази даних
        using var context = new ConventionsDbContext(options);

        // Видалення та створення бази даних
        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();

        Console.WriteLine("База даних створена за допомогою угод (conventions).");

        // Наповнення бази даних тестовими даними
        var dataSeeder = new ConventionsDataSeeder(context);
        dataSeeder.SeedData();

        Console.WriteLine("База даних наповнена тестовими даними.");

        // Демонстрація роботи зі зв'язками
        var relationshipDemo = new RelationshipDemonstrator(context);
        relationshipDemo.DemonstrateOneToOne();
        relationshipDemo.DemonstrateOneToMany();
        relationshipDemo.DemonstrateMany();

    }

    static void DemonstrateAnnotations()
    {
        Console.WriteLine("\nДемонстрація роботи зі зв'язками за допомогою анотацій (annotations)");
        Console.WriteLine("--------------------------------------------------------------------");

        // Створення опцій для контексту бази даних
        var options = new DbContextOptionsBuilder<AnnotationsDbContext>()
            .UseSqlite("Data Source=annotations.db")
            .Options;

        // Створення та налаштування бази даних
        using (var context = new AnnotationsDbContext(options))
        {
            // Видалення та створення бази даних
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            Console.WriteLine("База даних створена за допомогою анотацій (annotations).");

            // Наповнення бази даних тестовими даними
            var dataSeeder = new AnnotationsDataSeeder(context);
            dataSeeder.SeedData();

            Console.WriteLine("База даних наповнена тестовими даними.");

            // Демонстрація роботи зі зв'язками
            var relationshipDemo = new RelationshipDemonstrator(context);
            relationshipDemo.DemonstrateOneToOne();
            relationshipDemo.DemonstrateOneToMany();
            relationshipDemo.DemonstrateMany();
        }
    }

    static void DemonstrateFluentAPI()
    {
        Console.WriteLine("\nДемонстрація роботи зі зв'язками за допомогою Fluent API");
        Console.WriteLine("--------------------------------------------------------");

        // Створення опцій для контексту бази даних
        var options = new DbContextOptionsBuilder<FluentAPIDbContext>()
            .UseSqlite("Data Source=fluentapi.db")
            .Options;

        // Створення та налаштування бази даних
        using (var context = new FluentAPIDbContext(options))
        {
            // Видалення та створення бази даних
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            Console.WriteLine("База даних створена за допомогою Fluent API.");

            // Наповнення бази даних тестовими даними
            var dataSeeder = new FluentAPIDataSeeder(context);
            dataSeeder.SeedData();

            Console.WriteLine("База даних наповнена тестовими даними.");

            // Демонстрація роботи зі зв'язками
            var relationshipDemo = new RelationshipDemonstrator(context);
            relationshipDemo.DemonstrateOneToOne();
            relationshipDemo.DemonstrateOneToMany();
            relationshipDemo.DemonstrateMany();
        }
    }
}
