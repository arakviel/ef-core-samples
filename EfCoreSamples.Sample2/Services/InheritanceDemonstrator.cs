using EfCoreSamples.Sample2.Data;
using EfCoreSamples.Sample2.Models.Inheritance;
using Microsoft.EntityFrameworkCore;

namespace EfCoreSamples.Sample2.Services;

/// <summary>
/// Сервіс для демонстрації різних підходів до наслідування в EF Core.
/// </summary>
public class InheritanceDemonstrator
{
    private readonly BlogDbContext _context;

    public InheritanceDemonstrator(BlogDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Демонструє підхід TPH (Table Per Hierarchy) в EF Core.
    /// </summary>
    public void DemonstrateTPH()
    {
        Console.WriteLine("\n--- Демонстрація підходу TPH (Table Per Hierarchy) ---");
        Console.WriteLine("При підході TPH всі класи ієрархії зберігаються в одній таблиці.");
        Console.WriteLine("Для розрізнення типів використовується стовпець-дискримінатор.");

        // Отримання всіх типів контенту
        var allContent = _context.ContentsTPH.ToList();
        Console.WriteLine($"Всього контенту: {allContent.Count}");

        // Отримання статей
        var articles = _context.ArticlesTPH.ToList();
        Console.WriteLine($"Статей: {articles.Count}");

        // Отримання відео
        var videos = _context.VideosTPH.ToList();
        Console.WriteLine($"Відео: {videos.Count}");

        // Отримання подкастів
        var podcasts = _context.PodcastsTPH.ToList();
        Console.WriteLine($"Подкастів: {podcasts.Count}");

        // Виведення інформації про статті
        Console.WriteLine("\nСтатті:");
        foreach (var article in articles)
        {
            Console.WriteLine($"- {article.Title}");
            Console.WriteLine($"  Опис: {article.Description}");
            Console.WriteLine($"  Кількість слів: {article.WordCount}");
            Console.WriteLine($"  Час читання: {article.ReadingTimeMinutes} хв");
            Console.WriteLine($"  Ключові слова: {article.Keywords}");
        }

        // Виведення інформації про відео
        Console.WriteLine("\nВідео:");
        foreach (var video in videos)
        {
            Console.WriteLine($"- {video.Title}");
            Console.WriteLine($"  Опис: {video.Description}");
            Console.WriteLine($"  URL: {video.Url}");
            Console.WriteLine($"  Тривалість: {video.DurationInSeconds} сек");
            Console.WriteLine($"  Формат: {video.Format}");
            Console.WriteLine($"  Роздільна здатність: {video.Resolution}");
        }

        // Виведення інформації про подкасти
        Console.WriteLine("\nПодкасти:");
        foreach (var podcast in podcasts)
        {
            Console.WriteLine($"- {podcast.Title}");
            Console.WriteLine($"  Опис: {podcast.Description}");
            Console.WriteLine($"  URL: {podcast.AudioUrl}");
            Console.WriteLine($"  Тривалість: {podcast.DurationInSeconds} сек");
            Console.WriteLine($"  Ведучий: {podcast.Host}");
            Console.WriteLine($"  Гості: {podcast.Guests}");
        }

        // Пояснення переваг та недоліків TPH
        Console.WriteLine("\nПереваги TPH:");
        Console.WriteLine("1. Проста структура бази даних (одна таблиця).");
        Console.WriteLine("2. Ефективні запити для отримання всіх типів контенту.");
        Console.WriteLine("3. Простота міграцій при зміні ієрархії класів.");

        Console.WriteLine("\nНедоліки TPH:");
        Console.WriteLine("1. Багато NULL-значень у таблиці (для властивостей, специфічних для певного типу).");
        Console.WriteLine("2. Обмеження на кількість стовпців у таблиці.");
        Console.WriteLine("3. Неможливість встановити NOT NULL обмеження для властивостей підкласів.");
    }

    /// <summary>
    /// Демонструє підхід TPT (Table Per Type) в EF Core.
    /// </summary>
    public void DemonstrateTPT()
    {
        Console.WriteLine("\n--- Демонстрація підходу TPT (Table Per Type) ---");
        Console.WriteLine("При підході TPT для кожного типу створюється окрема таблиця.");
        Console.WriteLine("Таблиця базового класу містить спільні властивості, а таблиці підкласів - специфічні.");

        // Отримання всіх типів контенту
        var allContent = _context.ContentsTPT.ToList();
        Console.WriteLine($"Всього контенту: {allContent.Count}");

        // Отримання статей
        var articles = _context.ArticlesTPT.ToList();
        Console.WriteLine($"Статей: {articles.Count}");

        // Отримання відео
        var videos = _context.VideosTPT.ToList();
        Console.WriteLine($"Відео: {videos.Count}");

        // Отримання подкастів
        var podcasts = _context.PodcastsTPT.ToList();
        Console.WriteLine($"Подкастів: {podcasts.Count}");

        // Виведення інформації про статті
        Console.WriteLine("\nСтатті:");
        foreach (var article in articles)
        {
            Console.WriteLine($"- {article.Title}");
            Console.WriteLine($"  Опис: {article.Description}");
            Console.WriteLine($"  Кількість слів: {article.WordCount}");
            Console.WriteLine($"  Час читання: {article.ReadingTimeMinutes} хв");
            Console.WriteLine($"  Ключові слова: {article.Keywords}");
        }

        // Виведення інформації про відео
        Console.WriteLine("\nВідео:");
        foreach (var video in videos)
        {
            Console.WriteLine($"- {video.Title}");
            Console.WriteLine($"  Опис: {video.Description}");
            Console.WriteLine($"  URL: {video.Url}");
            Console.WriteLine($"  Тривалість: {video.DurationInSeconds} сек");
            Console.WriteLine($"  Формат: {video.Format}");
            Console.WriteLine($"  Роздільна здатність: {video.Resolution}");
        }

        // Виведення інформації про подкасти
        Console.WriteLine("\nПодкасти:");
        foreach (var podcast in podcasts)
        {
            Console.WriteLine($"- {podcast.Title}");
            Console.WriteLine($"  Опис: {podcast.Description}");
            Console.WriteLine($"  URL: {podcast.AudioUrl}");
            Console.WriteLine($"  Тривалість: {podcast.DurationInSeconds} сек");
            Console.WriteLine($"  Ведучий: {podcast.Host}");
            Console.WriteLine($"  Гості: {podcast.Guests}");
        }

        // Пояснення переваг та недоліків TPT
        Console.WriteLine("\nПереваги TPT:");
        Console.WriteLine("1. Нормалізована структура бази даних (відсутність NULL-значень).");
        Console.WriteLine("2. Можливість встановити NOT NULL обмеження для властивостей підкласів.");
        Console.WriteLine("3. Відповідність структури бази даних об'єктній моделі.");

        Console.WriteLine("\nНедоліки TPT:");
        Console.WriteLine("1. Складні JOIN-запити для отримання всіх даних.");
        Console.WriteLine("2. Нижча продуктивність порівняно з TPH для запитів, що охоплюють всю ієрархію.");
        Console.WriteLine("3. Складніші міграції при зміні ієрархії класів.");
    }

    /// <summary>
    /// Демонструє підхід TPC (Table Per Concrete class) в EF Core.
    /// </summary>
    public void DemonstrateTPC()
    {
        Console.WriteLine("\n--- Демонстрація підходу TPC (Table Per Concrete class) ---");
        Console.WriteLine("При підході TPC для кожного конкретного класу створюється окрема таблиця.");
        Console.WriteLine("Кожна таблиця містить всі властивості, включаючи успадковані від базового класу.");

        // Отримання статей
        var articles = _context.ArticlesTPC.ToList();
        Console.WriteLine($"Статей: {articles.Count}");

        // Отримання відео
        var videos = _context.VideosTPC.ToList();
        Console.WriteLine($"Відео: {videos.Count}");

        // Отримання подкастів
        var podcasts = _context.PodcastsTPC.ToList();
        Console.WriteLine($"Подкастів: {podcasts.Count}");

        // Виведення інформації про статті
        Console.WriteLine("\nСтатті:");
        foreach (var article in articles)
        {
            Console.WriteLine($"- {article.Title}");
            Console.WriteLine($"  Опис: {article.Description}");
            Console.WriteLine($"  Кількість слів: {article.WordCount}");
            Console.WriteLine($"  Час читання: {article.ReadingTimeMinutes} хв");
            Console.WriteLine($"  Ключові слова: {article.Keywords}");
        }

        // Виведення інформації про відео
        Console.WriteLine("\nВідео:");
        foreach (var video in videos)
        {
            Console.WriteLine($"- {video.Title}");
            Console.WriteLine($"  Опис: {video.Description}");
            Console.WriteLine($"  URL: {video.Url}");
            Console.WriteLine($"  Тривалість: {video.DurationInSeconds} сек");
            Console.WriteLine($"  Формат: {video.Format}");
            Console.WriteLine($"  Роздільна здатність: {video.Resolution}");
        }

        // Виведення інформації про подкасти
        Console.WriteLine("\nПодкасти:");
        foreach (var podcast in podcasts)
        {
            Console.WriteLine($"- {podcast.Title}");
            Console.WriteLine($"  Опис: {podcast.Description}");
            Console.WriteLine($"  URL: {podcast.AudioUrl}");
            Console.WriteLine($"  Тривалість: {podcast.DurationInSeconds} сек");
            Console.WriteLine($"  Ведучий: {podcast.Host}");
            Console.WriteLine($"  Гості: {podcast.Guests}");
        }

        // Пояснення переваг та недоліків TPC
        Console.WriteLine("\nПереваги TPC:");
        Console.WriteLine("1. Повна незалежність таблиць (немає зовнішніх ключів між таблицями ієрархії).");
        Console.WriteLine("2. Можливість оптимізувати кожну таблицю окремо.");
        Console.WriteLine("3. Висока продуктивність для запитів до конкретного типу.");

        Console.WriteLine("\nНедоліки TPC:");
        Console.WriteLine("1. Дублювання стовпців для спільних властивостей у всіх таблицях.");
        Console.WriteLine("2. Складність підтримки цілісності даних при зміні базового класу.");
        Console.WriteLine("3. Неможливість використовувати UNION для отримання всіх типів контенту в одному запиті.");
    }

    /// <summary>
    /// Порівнює різні підходи до наслідування в EF Core.
    /// </summary>
    public void CompareInheritanceApproaches()
    {
        Console.WriteLine("\n--- Порівняння підходів до наслідування в EF Core ---");

        Console.WriteLine("1. TPH (Table Per Hierarchy):");
        Console.WriteLine("   - Структура: Одна таблиця для всієї ієрархії з дискримінатором.");
        Console.WriteLine("   - Продуктивність: Висока для запитів до всієї ієрархії, середня для запитів до конкретного типу.");
        Console.WriteLine("   - Використання: Для невеликих ієрархій з невеликою кількістю специфічних властивостей.");
        Console.WriteLine("   - Приклад використання: Система контенту з різними типами (статті, відео, подкасти).");

        Console.WriteLine("\n2. TPT (Table Per Type):");
        Console.WriteLine("   - Структура: Окрема таблиця для кожного типу з зовнішніми ключами до базової таблиці.");
        Console.WriteLine("   - Продуктивність: Середня для запитів до всієї ієрархії, висока для запитів до конкретного типу.");
        Console.WriteLine("   - Використання: Для ієрархій з великою кількістю специфічних властивостей.");
        Console.WriteLine("   - Приклад використання: Система платежів з різними типами (кредитна картка, банківський переказ, PayPal).");

        Console.WriteLine("\n3. TPC (Table Per Concrete class):");
        Console.WriteLine("   - Структура: Окрема таблиця для кожного конкретного класу з усіма властивостями.");
        Console.WriteLine("   - Продуктивність: Низька для запитів до всієї ієрархії, дуже висока для запитів до конкретного типу.");
        Console.WriteLine("   - Використання: Коли типи дуже різні та рідко запитуються разом.");
        Console.WriteLine("   - Приклад використання: Система користувачів з різними ролями (адміністратор, модератор, звичайний користувач).");

        Console.WriteLine("\nРекомендації щодо вибору підходу:");
        Console.WriteLine("- За замовчуванням використовуйте TPH, якщо немає особливих вимог.");
        Console.WriteLine("- Використовуйте TPT, якщо потрібна нормалізована структура бази даних.");
        Console.WriteLine("- Використовуйте TPC, якщо типи дуже різні та потрібна максимальна продуктивність для запитів до конкретного типу.");
    }
}
