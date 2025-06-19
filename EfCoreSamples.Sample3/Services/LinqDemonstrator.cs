using EfCoreSamples.Sample3.Data;
using EfCoreSamples.Sample3.Models;
using Microsoft.EntityFrameworkCore;

namespace EfCoreSamples.Sample3.Services;

/// <summary>
/// Клас для демонстрації різних LINQ запитів з Entity Framework Core
/// </summary>
public class LinqDemonstrator
{
    private readonly BlogDbContext _context;

    /// <summary>
    /// Конструктор класу
    /// </summary>
    /// <param name="context">Контекст бази даних</param>
    public LinqDemonstrator(BlogDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Демонструє базові LINQ запити
    /// </summary>
    public void DemonstrateBasicQueries()
    {
        Console.WriteLine("\n=== Базові LINQ запити ===\n");

        // 1. Простий запит з Where
        Console.WriteLine("1. Пости з кількістю переглядів більше 1000:");
        var popularPosts = _context.Posts
            .Where(p => p.ViewCount > 1000)
            .ToList();

        foreach (var post in popularPosts)
        {
            Console.WriteLine($"- {post.Title} (Переглядів: {post.ViewCount})");
        }

        // 2. Запит з OrderBy
        Console.WriteLine("\n2. Пости, відсортовані за кількістю переглядів (спадання):");
        var orderedPosts = _context.Posts
            .OrderByDescending(p => p.ViewCount)
            .Take(3)
            .ToList();

        foreach (var post in orderedPosts)
        {
            Console.WriteLine($"- {post.Title} (Переглядів: {post.ViewCount})");
        }

        // 3. Запит з Select (проекція)
        Console.WriteLine("\n3. Проекція: заголовки та дати публікації постів:");
        var postTitles = _context.Posts
            .Where(p => p.IsPublished)
            .Select(p => new { p.Title, p.PublishedAt })
            .ToList();

        foreach (var post in postTitles)
        {
            Console.WriteLine($"- {post.Title} (Опубліковано: {post.PublishedAt?.ToString("dd.MM.yyyy") ?? "Не опубліковано"})");
        }

        // 4. Запит з FirstOrDefault
        Console.WriteLine("\n4. Пошук конкретного поста:");
        var specificPost = _context.Posts
            .IgnoreQueryFilters() // Ігноруємо фільтри
            .FirstOrDefault(p => p.Slug == "intro-to-ef-core");

        if (specificPost != null)
        {
            Console.WriteLine($"Знайдено пост: {specificPost.Title}");
        }
        else
        {
            Console.WriteLine("Пост не знайдено");
        }

        // 5. Запит з Any
        Console.WriteLine("\n5. Перевірка наявності неопублікованих постів:");
        var hasUnpublishedPosts = _context.Posts.Any(p => !p.IsPublished);
        Console.WriteLine($"Є неопубліковані пости: {hasUnpublishedPosts}");
    }

    /// <summary>
    /// Демонструє агрегатні LINQ запити
    /// </summary>
    public void DemonstrateAggregateQueries()
    {
        Console.WriteLine("\n=== Агрегатні LINQ запити ===\n");

        // 1. Count
        Console.WriteLine("1. Кількість постів у кожному блозі:");
        var blogPostCounts = _context.Blogs
            .Select(b => new
            {
                b.Title,
                PostCount = b.Posts.Count
            })
            .ToList();

        foreach (var blog in blogPostCounts)
        {
            Console.WriteLine($"- {blog.Title}: {blog.PostCount} постів");
        }

        // 2. Sum
        Console.WriteLine("\n2. Загальна кількість переглядів постів для кожного автора:");
        var authorViewCounts = _context.Authors
            .Select(a => new
            {
                FullName = a.FirstName + " " + a.LastName,
                TotalViews = a.Posts.Sum(p => p.ViewCount)
            })
            .ToList();

        foreach (var author in authorViewCounts)
        {
            Console.WriteLine($"- {author.FullName}: {author.TotalViews} переглядів");
        }

        // 3. Average
        Console.WriteLine("\n3. Середня кількість переглядів постів:");
        var averageViews = _context.Posts
            .Where(p => p.IsPublished)
            .Average(p => p.ViewCount);

        Console.WriteLine($"Середня кількість переглядів: {averageViews:F2}");

        // 4. Min/Max
        Console.WriteLine("\n4. Найпопулярніший та найменш популярний пости:");
        var maxViews = _context.Posts
            .Where(p => p.IsPublished)
            .Max(p => p.ViewCount);

        var minViews = _context.Posts
            .Where(p => p.IsPublished)
            .Min(p => p.ViewCount);

        var mostPopularPost = _context.Posts
            .Where(p => p.ViewCount == maxViews)
            .FirstOrDefault();

        var leastPopularPost = _context.Posts
            .Where(p => p.ViewCount == minViews)
            .FirstOrDefault();

        Console.WriteLine($"Найпопулярніший пост: {mostPopularPost?.Title} ({maxViews} переглядів)");
        Console.WriteLine($"Найменш популярний пост: {leastPopularPost?.Title} ({minViews} переглядів)");
    }

    /// <summary>
    /// Демонструє LINQ запити з групуванням
    /// </summary>
    public void DemonstrateGroupingQueries()
    {
        Console.WriteLine("\n=== LINQ запити з групуванням ===\n");

        // 1. Групування постів за блогами
        Console.WriteLine("1. Групування постів за блогами:");
        var postsByBlog = _context.Posts
            .Include(p => p.Blog)
            .GroupBy(p => p.Blog.Title)
            .Select(g => new
            {
                BlogTitle = g.Key,
                Posts = g.ToList()
            })
            .ToList();

        foreach (var group in postsByBlog)
        {
            Console.WriteLine($"Блог: {group.BlogTitle}");
            foreach (var post in group.Posts)
            {
                Console.WriteLine($"  - {post.Title}");
            }
        }

        // 2. Групування постів за роком публікації
        // TODO: не працює.
        Console.WriteLine("\n2. Групування постів за роком публікації:");
        var postsByYear = _context.Posts
            .Where(p => p.PublishedAt != null)
            .AsEnumerable()
            .GroupBy(p => p.PublishedAt!.Value.Year)
            .OrderByDescending(g => g.Key)
            .ToList();

        foreach (var group in postsByYear)
        {
            Console.WriteLine($"Рік: {group.Key}");
            foreach (var post in group.ToList())
            {
                Console.WriteLine($"  - {post.Title} ({post.PublishedAt:dd.MM.yyyy})");
            }
        }

        // 3. Групування з агрегацією
        Console.WriteLine("\n3. Кількість постів та сумарні перегляди за категоріями:");
        var statsByCategory = _context.Posts
            .Include(p => p.Blog)
            .ThenInclude(b => b.Category)
            .AsEnumerable()
            .GroupBy(p => p.Blog.Category.Name)
            .Select(g => new
            {
                CategoryName = g.Key,
                PostCount = g.Count(),
                TotalViews = g.Sum(p => p.ViewCount)
            })
            .OrderByDescending(x => x.TotalViews)
            .ToList();

        foreach (var stat in statsByCategory)
        {
            Console.WriteLine($"Категорія: {stat.CategoryName}");
            Console.WriteLine($"  Кількість постів: {stat.PostCount}");
            Console.WriteLine($"  Загальна кількість переглядів: {stat.TotalViews}");
        }
    }

    /// <summary>
    /// Демонструє LINQ запити з з'єднаннями
    /// </summary>
    public void DemonstrateJoinQueries()
    {
        Console.WriteLine("\n=== LINQ запити з з'єднаннями ===\n");

        // 1. Join (внутрішнє з'єднання)
        Console.WriteLine("1. З'єднання авторів і постів:");
        var authorsWithPosts = _context.Authors
            .Join(
                _context.Posts,
                author => author.Id,
                post => post.AuthorId,
                (author, post) => new
                {
                    AuthorName = author.FirstName + " " + author.LastName,
                    PostTitle = post.Title,
                    PostViews = post.ViewCount
                })
            .OrderBy(x => x.AuthorName)
            .ToList();

        string currentAuthor = "";
        foreach (var item in authorsWithPosts)
        {
            if (currentAuthor != item.AuthorName)
            {
                currentAuthor = item.AuthorName;
                Console.WriteLine($"Автор: {currentAuthor}");
            }
            Console.WriteLine($"  - {item.PostTitle} ({item.PostViews} переглядів)");
        }

        // 2. GroupJoin (ліве зовнішнє з'єднання з групуванням)
        Console.WriteLine("\n2. GroupJoin авторів і блогів:");
        var authorsWithBlogs = _context.Authors
            .GroupJoin(
                _context.Blogs,
                author => author.Id,
                blog => blog.AuthorId,
                (author, blogs) => new
                {
                    AuthorName = author.FirstName + " " + author.LastName,
                    Blogs = blogs.ToList()
                })
            .ToList();

        foreach (var author in authorsWithBlogs)
        {
            Console.WriteLine($"Автор: {author.AuthorName}");
            if (author.Blogs.Any())
            {
                foreach (var blog in author.Blogs)
                {
                    Console.WriteLine($"  - {blog.Title}");
                }
            }
            else
            {
                Console.WriteLine("  Немає блогів");
            }
        }

        // 3. Складне з'єднання з фільтрацією
        Console.WriteLine("\n3. Пости з тегом 'Entity Framework':");
        var efTag = _context.Tags.FirstOrDefault(t => t.Name == "Entity Framework");
        var postsWithEfTag = new List<Post>();

        if (efTag != null)
        {
            postsWithEfTag = _context.Posts
                .Where(p => p.Tags.Any(t => t.Id == efTag.Id))
                .ToList();
        }

        foreach (var post in postsWithEfTag)
        {
            Console.WriteLine($"- {post.Title}");
        }
    }

    /// <summary>
    /// Демонструє LINQ запити з Include для завантаження пов'язаних даних
    /// </summary>
    public void DemonstrateIncludeQueries()
    {
        Console.WriteLine("\n=== LINQ запити з Include ===\n");

        // 1. Простий Include
        Console.WriteLine("1. Пости з їх авторами:");
        var postsWithAuthors = _context.Posts
            .Include(p => p.Author)
            .Take(3)
            .ToList();

        foreach (var post in postsWithAuthors)
        {
            Console.WriteLine($"Пост: {post.Title}");
            Console.WriteLine($"Автор: {post.Author.FirstName} {post.Author.LastName}");
            Console.WriteLine();
        }

        // 2. Багаторівневий Include
        Console.WriteLine("2. Блоги з постами та їх коментарями:");
        var blogsWithPostsAndComments = _context.Blogs
            .Include(b => b.Posts)
                .ThenInclude(p => p.Comments)
            .Take(2)
            .ToList();

        foreach (var blog in blogsWithPostsAndComments)
        {
            Console.WriteLine($"Блог: {blog.Title}");
            foreach (var post in blog.Posts)
            {
                Console.WriteLine($"  Пост: {post.Title}");
                Console.WriteLine($"  Коментарі ({post.Comments.Count}):");
                foreach (var comment in post.Comments.Take(2))
                {
                    Console.WriteLine($"    - {comment.AuthorName}: {comment.Content.Substring(0, Math.Min(30, comment.Content.Length))}...");
                }
                Console.WriteLine();
            }
        }

        // 3. Фільтрація після Include
        Console.WriteLine("3. Блоги з опублікованими постами:");
        // TODO: ЦЕ не працює ^_^
        var blogsWithPublishedPosts = _context.Blogs
            .Include(b => b.Posts.Where(p => p.IsPublished))
            .ToList();

        foreach (var blog in blogsWithPublishedPosts)
        {
            Console.WriteLine($"Блог: {blog.Title}");
            Console.WriteLine($"  Опубліковані пости ({blog.Posts.Count}):");
            foreach (var post in blog.Posts)
            {
                Console.WriteLine($"  - {post.Title}");
            }
            Console.WriteLine();
        }
    }

    /// <summary>
    /// Демонструє складні LINQ запити
    /// </summary>
    public void DemonstrateComplexQueries()
    {
        Console.WriteLine("\n=== Складні LINQ запити ===\n");

        // 1. Пагінація з сортуванням
        Console.WriteLine("1. Пагінація постів (сторінка 1, розмір 2):");
        int pageNumber = 1;
        int pageSize = 2;
        var pagedPosts = _context.Posts
            .OrderByDescending(p => p.CreatedAt)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        foreach (var post in pagedPosts)
        {
            Console.WriteLine($"- {post.Title} (Створено: {post.CreatedAt:dd.MM.yyyy})");
        }

        // 2. Складний запит з підзапитами
        Console.WriteLine("\n2. Автори з кількістю постів та коментарів:");
        var authorStats = _context.Authors
            .Select(a => new
            {
                AuthorName = a.FirstName + " " + a.LastName,
                PostCount = a.Posts.Count,
                CommentCount = a.Posts.SelectMany(p => p.Comments).Count(),
                MostPopularPost = a.Posts.OrderByDescending(p => p.ViewCount).FirstOrDefault()
            })
            .ToList();

        foreach (var author in authorStats)
        {
            Console.WriteLine($"Автор: {author.AuthorName}");
            Console.WriteLine($"  Кількість постів: {author.PostCount}");
            Console.WriteLine($"  Кількість коментарів: {author.CommentCount}");
            Console.WriteLine($"  Найпопулярніший пост: {author.MostPopularPost?.Title ?? "Немає постів"}");
            Console.WriteLine();
        }

        // 3. Запит з умовним групуванням та агрегацією
        Console.WriteLine("3. Статистика постів за періодами:");
        var now = DateTime.Now;
        var postStats = _context.Posts
            .AsEnumerable()
            .GroupBy(p =>
            {
                var monthsAgo = (now - p.CreatedAt).TotalDays / 30;
                if (monthsAgo <= 1) return "Останній місяць";
                if (monthsAgo <= 3) return "Останні 3 місяці";
                if (monthsAgo <= 6) return "Останні 6 місяців";
                return "Більше 6 місяців";
            })
            .Select(g => new
            {
                Period = g.Key,
                Count = g.Count(),
                AverageViews = g.Average(p => p.ViewCount)
            })
            .OrderBy(x => x.Period)
            .ToList();

        foreach (var stat in postStats)
        {
            Console.WriteLine($"Період: {stat.Period}");
            Console.WriteLine($"  Кількість постів: {stat.Count}");
            Console.WriteLine($"  Середня кількість переглядів: {stat.AverageViews:F2}");
        }

        // 4. Запит з використанням Union
        Console.WriteLine("\n4. Об'єднання популярних та нових постів:");
        var popularPosts = _context.Posts
            .Where(p => p.ViewCount > 1500)
            .Take(2);

        var recentPosts = _context.Posts
            .OrderByDescending(p => p.CreatedAt)
            .Take(2);

        var combinedPosts = popularPosts.Union(recentPosts).ToList();

        Console.WriteLine("Популярні та нові пости:");
        foreach (var post in combinedPosts)
        {
            Console.WriteLine($"- {post.Title} (Переглядів: {post.ViewCount}, Створено: {post.CreatedAt:dd.MM.yyyy})");
        }
    }

    /// <summary>
    /// Демонструє всі типи LINQ запитів
    /// </summary>
    public void DemonstrateAllQueries()
    {
        DemonstrateBasicQueries();
        DemonstrateAggregateQueries();
        DemonstrateGroupingQueries();
        DemonstrateJoinQueries();
        DemonstrateIncludeQueries();
        DemonstrateComplexQueries();
    }
}
