using EfCoreSamples.Sample2.Data;
using Microsoft.EntityFrameworkCore;

namespace EfCoreSamples.Sample2.Services;

/// <summary>
/// Сервіс для демонстрації різних способів завантаження пов'язаних даних в EF Core.
/// </summary>
public class LoadingDemonstrator
{
    private readonly BlogDbContext _context;

    public LoadingDemonstrator(BlogDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Демонструє Eager Loading (жадібне завантаження) пов'язаних даних.
    /// При Eager Loading пов'язані дані завантажуються разом з основними даними в одному запиті.
    /// </summary>
    public void DemonstrateEagerLoading()
    {
        Console.WriteLine("\n--- Демонстрація Eager Loading ---");

        // Завантаження блогу разом з постами та автором
        Console.WriteLine("Завантаження блогу разом з постами та автором:");
        var blog = _context.Blogs
            .Include(b => b.Posts) // Включення постів
            .Include(b => b.Author) // Включення автора
                  .ThenInclude(a => a.Address) // Включення адреси автора
            .FirstOrDefault();

        if (blog != null)
        {
            Console.WriteLine($"Блог: {blog.Title}");
            Console.WriteLine($"Автор: {blog.Author.FullName}");
            Console.WriteLine($"Адреса автора: {blog.Author.Address?.FullAddress ?? "Не вказана"}");
            Console.WriteLine($"Кількість постів: {blog.Posts.Count}");

            // Виведення інформації про пости
            foreach (var _post in blog.Posts)
            {
                Console.WriteLine($"  - Пост: {_post.Title}");
            }
        }

        // Завантаження поста разом з коментарями та їх відповідями
        Console.WriteLine("\nЗавантаження поста разом з коментарями та їх відповідями:");
        var post = _context.Posts
            .Include(p => p.Comments) // Включення коментарів
                .ThenInclude(c => c.Replies) // Включення відповідей на коментарі
                    .ThenInclude(r => r.Replies) // Включення відповідей на відповіді (третій рівень)
            .Include(p => p.Author) // Включення автора поста
            .FirstOrDefault();

        if (post != null)
        {
            Console.WriteLine($"Пост: {post.Title}");
            Console.WriteLine($"Автор: {post.Author.FullName}");
            Console.WriteLine($"Кількість коментарів верхнього рівня: {post.Comments.Count}");

            // Виведення інформації про коментарі та їх відповіді
            foreach (var comment in post.Comments)
            {
                Console.WriteLine($"  - Коментар: {comment.Content}");
                Console.WriteLine($"    Автор: {comment.AuthorName}");
                Console.WriteLine($"    Кількість відповідей: {comment.Replies.Count}");

                foreach (var reply in comment.Replies)
                {
                    Console.WriteLine($"      - Відповідь: {reply.Content}");
                    Console.WriteLine($"        Автор: {reply.AuthorName}");
                    Console.WriteLine($"        Кількість відповідей: {reply.Replies.Count}");

                    foreach (var nestedReply in reply.Replies)
                    {
                        Console.WriteLine($"          - Вкладена відповідь: {nestedReply.Content}");
                        Console.WriteLine($"            Автор: {nestedReply.AuthorName}");
                    }
                }
            }
        }
    }

    /// <summary>
    /// Демонструє Lazy Loading (ліниве завантаження) пов'язаних даних.
    /// При Lazy Loading пов'язані дані завантажуються з бази даних лише тоді,
    /// коли до них звертаються через навігаційну властивість.
    /// </summary>
    public void DemonstrateLazyLoading()
    {
        Console.WriteLine("\n--- Демонстрація Lazy Loading ---");

        // Для демонстрації Lazy Loading потрібно:
        // 1. Додати пакет Microsoft.EntityFrameworkCore.Proxies
        // 2. Викликати UseLazyLoadingProxies() при налаштуванні контексту
        // 3. Зробити навігаційні властивості віртуальними (virtual)

        // Завантаження блогу без явного включення пов'язаних даних
        var blog = _context.Blogs.FirstOrDefault();

        if (blog != null)
        {
            Console.WriteLine($"Блог: {blog.Title}");

            // При зверненні до навігаційної властивості Author
            // EF Core автоматично завантажить дані автора з бази даних
            Console.WriteLine($"Автор: {blog.Author.FullName}");

            Console.WriteLine($"Адреса автора: {blog.Author.Address?.FullAddress ?? "Не вказана"}");

            // При зверненні до навігаційної властивості Posts
            // EF Core автоматично завантажить дані постів з бази даних
            Console.WriteLine($"Кількість постів: {blog.Posts.Count}");

            // Виведення інформації про пости
            foreach (var post in blog.Posts)
            {
                Console.WriteLine($"  - Пост: {post.Title}");

                // При зверненні до навігаційної властивості Comments
                // EF Core автоматично завантажить дані коментарів з бази даних
                var topLevelComments = post.Comments.Where(c => c.ParentCommentId == null).ToList();
                Console.WriteLine($"    Кількість коментарів верхнього рівня: {topLevelComments.Count}");

                foreach (var comment in topLevelComments)
                {
                    Console.WriteLine($"    - Коментар: {comment.Content}");
                    Console.WriteLine($"      Автор: {comment.AuthorName}");

                    // При зверненні до навігаційної властивості Replies
                    // EF Core автоматично завантажить дані відповідей з бази даних
                    Console.WriteLine($"      Кількість відповідей: {comment.Replies.Count}");
                }
            }
        }

        // Демонстрація проблеми N+1 запитів при використанні Lazy Loading
        Console.WriteLine("\nДемонстрація проблеми N+1 запитів при використанні Lazy Loading:");
        Console.WriteLine("При використанні Lazy Loading для колекції об'єктів може виникнути проблема N+1 запитів,");
        Console.WriteLine("коли для кожного об'єкта в колекції виконується окремий запит до бази даних.");
        Console.WriteLine("Наприклад, при завантаженні всіх блогів та доступі до їх авторів:");

        var blogs = _context.Blogs.ToList();
        Console.WriteLine($"Завантажено {blogs.Count} блогів.");

        foreach (var b in blogs)
        {
            // Для кожного блогу буде виконано окремий запит для завантаження автора
            Console.WriteLine($"Блог: {b.Title}, Автор: {b.Author.FullName}");
        }

        Console.WriteLine("\nЩоб уникнути проблеми N+1 запитів, краще використовувати Eager Loading для колекцій.");
    }

    /// <summary>
    /// Демонструє Explicit Loading (явне завантаження) пов'язаних даних.
    /// При Explicit Loading пов'язані дані завантажуються явно за допомогою методів
    /// Entry().Reference().Load() для одиничних зв'язків та Entry().Collection().Load() для колекцій.
    /// </summary>
    public void DemonstrateExplicitLoading()
    {
        Console.WriteLine("\n--- Демонстрація Explicit Loading ---");

        // Завантаження блогу без пов'язаних даних
        var blog = _context.Blogs.FirstOrDefault();

        if (blog != null)
        {
            Console.WriteLine($"Блог: {blog.Title}");

            // Явне завантаження автора
            _context.Entry(blog).Reference(b => b.Author).Load();
            Console.WriteLine($"Автор: {blog.Author.FullName}");

            Console.WriteLine($"Адреса автора: {blog.Author.Address?.FullAddress ?? "Не вказана"}");

            // Явне завантаження постів
            _context.Entry(blog).Collection(b => b.Posts).Load();
            Console.WriteLine($"Кількість постів: {blog.Posts.Count}");

            // Явне завантаження коментарів для першого поста
            if (blog.Posts.Any())
            {
                var post = blog.Posts.First();
                Console.WriteLine($"  - Пост: {post.Title}");

                _context.Entry(post).Collection(p => p.Comments).Load();
                var topLevelComments = post.Comments.Where(c => c.ParentCommentId == null).ToList();
                Console.WriteLine($"    Кількість коментарів верхнього рівня: {topLevelComments.Count}");

                // Явне завантаження відповідей для кожного коментаря
                foreach (var comment in topLevelComments)
                {
                    _context.Entry(comment).Collection(c => c.Replies).Load();
                    Console.WriteLine($"    - Коментар: {comment.Content}");
                    Console.WriteLine($"      Автор: {comment.AuthorName}");
                    Console.WriteLine($"      Кількість відповідей: {comment.Replies.Count}");
                }
            }
        }

        // Явне завантаження з фільтрацією
        Console.WriteLine("\nЯвне завантаження з фільтрацією:");
        var author = _context.Authors.FirstOrDefault();

        if (author != null)
        {
            Console.WriteLine($"Автор: {author.FullName}");

            // Явне завантаження опублікованих постів
            _context.Entry(author)
                .Collection(a => a.Posts)
                .Query()
                .Where(p => p.IsPublished)
                .Load();

            Console.WriteLine($"Кількість опублікованих постів: {author.Posts.Count}");

            foreach (var post in author.Posts)
            {
                Console.WriteLine($"  - Пост: {post.Title}");
                Console.WriteLine($"    Опубліковано: {post.PublishedAt}");
            }
        }
    }

    /// <summary>
    /// Демонструє порівняння різних способів завантаження пов'язаних даних.
    /// </summary>
    public void CompareLoadingStrategies()
    {
        Console.WriteLine("\n--- Порівняння стратегій завантаження даних ---");

        Console.WriteLine("1. Eager Loading (жадібне завантаження):");
        Console.WriteLine("   - Переваги: Один запит до бази даних, уникнення проблеми N+1 запитів.");
        Console.WriteLine("   - Недоліки: Може завантажувати непотрібні дані, складні запити для глибоких ієрархій.");
        Console.WriteLine("   - Коли використовувати: Коли ви точно знаєте, які пов'язані дані потрібні.");

        Console.WriteLine("\n2. Lazy Loading (ліниве завантаження):");
        Console.WriteLine("   - Переваги: Простота використання, завантаження даних лише за потреби.");
        Console.WriteLine("   - Недоліки: Проблема N+1 запитів, складно відстежувати запити до бази даних.");
        Console.WriteLine("   - Коли використовувати: Для прототипування, коли продуктивність не критична.");

        Console.WriteLine("\n3. Explicit Loading (явне завантаження):");
        Console.WriteLine("   - Переваги: Повний контроль над завантаженням даних, можливість фільтрації.");
        Console.WriteLine("   - Недоліки: Більше коду, все ще можлива проблема N+1 запитів.");
        Console.WriteLine("   - Коли використовувати: Коли потрібен точний контроль над завантаженням даних.");

        Console.WriteLine("\nРекомендації:");
        Console.WriteLine("- Для колекцій об'єктів краще використовувати Eager Loading.");
        Console.WriteLine("- Для одиничних зв'язків можна використовувати Lazy Loading.");
        Console.WriteLine("- Для складних сценаріїв з фільтрацією використовуйте Explicit Loading.");
        Console.WriteLine("- Уникайте Lazy Loading у високонавантажених додатках.");
    }
}
