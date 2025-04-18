using EfCoreSamples.Sample2.Data;
using EfCoreSamples.Sample2.Models;
using Microsoft.EntityFrameworkCore;

namespace EfCoreSamples.Sample2.Services;

/// <summary>
/// Сервіс для демонстрації роботи з ValueObjects в EF Core.
/// </summary>
public class ValueObjectDemonstrator
{
    private readonly BlogDbContext _context;

    public ValueObjectDemonstrator(BlogDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Демонструє роботу з ValueObject (Address) в EF Core.
    /// </summary>
    public void DemonstrateValueObjects()
    {
        Console.WriteLine("\n--- Демонстрація роботи з ValueObjects ---");

        // Отримання автора з адресою
        var author = _context.Authors
            .Include(a => a.Address)
            .FirstOrDefault(a => a.Address != null);

        if (author != null)
        {
            Console.WriteLine("Автор з адресою:");
            Console.WriteLine($"Ім'я: {author.FullName}");
            Console.WriteLine($"Email: {author.Email}");
            Console.WriteLine("Адреса (ValueObject):");
            Console.WriteLine($"  Вулиця: {author.Address!.Street}");
            Console.WriteLine($"  Номер будинку: {author.Address.HouseNumber}");
            Console.WriteLine($"  Місто: {author.Address.City}");
            Console.WriteLine($"  Поштовий індекс: {author.Address.PostalCode}");
            Console.WriteLine($"  Країна: {author.Address.Country}");
            Console.WriteLine($"  Повна адреса: {author.Address.FullAddress}");

            // Оновлення адреси
            Console.WriteLine("\nОновлення адреси автора...");
            author.Address = new Address
            {
                Street = "Нова вулиця",
                HouseNumber = "42",
                City = "Нове місто",
                PostalCode = "12345",
                Country = "Україна"
            };

            _context.SaveChanges();

            Console.WriteLine("Адреса оновлена:");
            Console.WriteLine($"  Вулиця: {author.Address.Street}");
            Console.WriteLine($"  Номер будинку: {author.Address.HouseNumber}");
            Console.WriteLine($"  Місто: {author.Address.City}");
            Console.WriteLine($"  Поштовий індекс: {author.Address.PostalCode}");
            Console.WriteLine($"  Країна: {author.Address.Country}");
            Console.WriteLine($"  Повна адреса: {author.Address.FullAddress}");
        }

        // Створення нового автора з адресою
        Console.WriteLine("\nСтворення нового автора з адресою...");
        var newAuthor = new Author
        {
            FirstName = "Тарас",
            LastName = "Шевченко",
            Email = "taras.shevchenko@example.com",
            Bio = "Український поет та письменник.",
            Address = new Address
            {
                Street = "Вулиця Шевченка",
                HouseNumber = "1",
                City = "Канів",
                PostalCode = "19000",
                Country = "Україна"
            }
        };

        _context.Authors.Add(newAuthor);
        _context.SaveChanges();

        Console.WriteLine("Новий автор створений:");
        Console.WriteLine($"Ім'я: {newAuthor.FullName}");
        Console.WriteLine($"Email: {newAuthor.Email}");
        Console.WriteLine("Адреса (ValueObject):");
        Console.WriteLine($"  Вулиця: {newAuthor.Address.Street}");
        Console.WriteLine($"  Номер будинку: {newAuthor.Address.HouseNumber}");
        Console.WriteLine($"  Місто: {newAuthor.Address.City}");
        Console.WriteLine($"  Поштовий індекс: {newAuthor.Address.PostalCode}");
        Console.WriteLine($"  Країна: {newAuthor.Address.Country}");
        Console.WriteLine($"  Повна адреса: {newAuthor.Address.FullAddress}");

        // Пошук авторів за містом (частина ValueObject)
        Console.WriteLine("\nПошук авторів за містом (частина ValueObject)...");
        var authorsInCity = _context.Authors
            .Where(a => a.Address != null && a.Address.City == "Київ")
            .ToList();

        Console.WriteLine($"Знайдено {authorsInCity.Count} авторів у місті Київ:");
        foreach (var a in authorsInCity)
        {
            Console.WriteLine($"- {a.FullName}, Адреса: {a.Address?.FullAddress}");
        }

        // Видалення адреси (встановлення ValueObject в null)
        if (author != null)
        {
            Console.WriteLine("\nВидалення адреси автора...");
            author.Address = null;
            _context.SaveChanges();

            // Перевірка, що адреса видалена
            var authorWithoutAddress = _context.Authors
                .Include(a => a.Address)
                .FirstOrDefault(a => a.Id == author.Id);

            Console.WriteLine($"Автор: {authorWithoutAddress?.FullName}");
            Console.WriteLine($"Адреса: {(authorWithoutAddress?.Address == null ? "Не вказана" : authorWithoutAddress.Address.FullAddress)}");
        }
    }

    /// <summary>
    /// Пояснює переваги використання ValueObjects в EF Core.
    /// </summary>
    public void ExplainValueObjectsBenefits()
    {
        Console.WriteLine("\n--- Переваги використання ValueObjects ---");

        Console.WriteLine("1. Інкапсуляція бізнес-логіки:");
        Console.WriteLine("   - ValueObject може містити методи та властивості, які працюють з його даними.");
        Console.WriteLine("   - Наприклад, властивість FullAddress в Address об'єднує всі поля адреси.");

        Console.WriteLine("\n2. Покращення дизайну домену:");
        Console.WriteLine("   - ValueObjects допомагають створювати більш виразну модель домену.");
        Console.WriteLine("   - Вони представляють концепції, які не мають власної ідентичності.");

        Console.WriteLine("\n3. Валідація даних:");
        Console.WriteLine("   - ValueObjects можуть містити логіку валідації своїх даних.");
        Console.WriteLine("   - Наприклад, можна додати перевірку формату поштового індексу.");

        Console.WriteLine("\n4. Повторне використання:");
        Console.WriteLine("   - ValueObjects можна використовувати в різних сутностях.");
        Console.WriteLine("   - Наприклад, Address можна використовувати не лише для Author, але й для інших сутностей.");

        Console.WriteLine("\n5. Спрощення запитів:");
        Console.WriteLine("   - EF Core дозволяє фільтрувати за властивостями ValueObject.");
        Console.WriteLine("   - Наприклад, можна шукати авторів за містом або країною.");

        Console.WriteLine("\n6. Зменшення дублювання коду:");
        Console.WriteLine("   - Без ValueObjects довелося б дублювати поля адреси в кожній сутності.");
        Console.WriteLine("   - З ValueObjects код стає більш DRY (Don't Repeat Yourself).");

        Console.WriteLine("\n7. Покращення читабельності коду:");
        Console.WriteLine("   - ValueObjects роблять код більш зрозумілим та самодокументованим.");
        Console.WriteLine("   - Наприклад, author.Address.City більш зрозуміло, ніж author.City.");
    }
}
