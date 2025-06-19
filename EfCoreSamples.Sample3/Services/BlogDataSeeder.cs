using EfCoreSamples.Sample3.Data;
using EfCoreSamples.Sample3.Models;

namespace EfCoreSamples.Sample3.Services;

/// <summary>
/// Сервіс для наповнення бази даних тестовими даними
/// </summary>
public class BlogDataSeeder
{
    private readonly BlogDbContext _context;

    /// <summary>
    /// Конструктор сервісу
    /// </summary>
    /// <param name="context">Контекст бази даних</param>
    public BlogDataSeeder(BlogDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Наповнює базу даних тестовими даними
    /// </summary>
    public void SeedData()
    {
        Console.WriteLine("Наповнення бази даних тестовими даними...");

        // Створення авторів
        var authors = CreateAuthors();
        _context.SaveChanges();

        // Створення категорій
        var categories = CreateCategories();
        _context.SaveChanges();

        // Створення блогів
        var blogs = CreateBlogs(authors, categories);
        _context.SaveChanges();

        // Створення тегів
        var tags = CreateTags();
        _context.SaveChanges();

        // Створення постів
        var posts = CreatePosts(blogs, authors, tags);
        _context.SaveChanges();

        // Створення коментарів
        CreateComments(posts);
        _context.SaveChanges();

        Console.WriteLine("База даних успішно наповнена тестовими даними.");
    }

    /// <summary>
    /// Створює тестових авторів
    /// </summary>
    private List<Author> CreateAuthors()
    {
        Console.WriteLine("Створення авторів...");

        var authors = new List<Author>
        {
            new Author
            {
                FirstName = "Іван",
                LastName = "Петренко",
                Email = "ivan.petrenko@example.com",
                Bio = "Технічний письменник та розробник програмного забезпечення з 10-річним досвідом.",
                AvatarUrl = "https://example.com/avatars/ivan.jpg",
                RegisteredAt = DateTime.Now.AddYears(-3)
            },
            new Author
            {
                FirstName = "Марія",
                LastName = "Коваленко",
                Email = "maria.kovalenko@example.com",
                Bio = "Фахівець з цифрового маркетингу та контент-менеджер.",
                AvatarUrl = "https://example.com/avatars/maria.jpg",
                RegisteredAt = DateTime.Now.AddYears(-2)
            },
            new Author
            {
                FirstName = "Олександр",
                LastName = "Сидоренко",
                Email = "oleksandr.sydorenko@example.com",
                Bio = "Експерт з кібербезпеки та аналітик даних.",
                AvatarUrl = "https://example.com/avatars/oleksandr.jpg",
                RegisteredAt = DateTime.Now.AddYears(-1)
            }
        };

        _context.Authors.AddRange(authors);
        return authors;
    }

    /// <summary>
    /// Створює тестові категорії
    /// </summary>
    private List<Category> CreateCategories()
    {
        Console.WriteLine("Створення категорій...");

        var categories = new List<Category>
        {
            new Category
            {
                Name = "Технології",
                Description = "Статті про сучасні технології та інновації",
                Slug = "technology"
            },
            new Category
            {
                Name = "Програмування",
                Description = "Все про програмування та розробку програмного забезпечення",
                Slug = "programming"
            },
            new Category
            {
                Name = "Маркетинг",
                Description = "Статті про цифровий маркетинг та стратегії просування",
                Slug = "marketing"
            },
            new Category
            {
                Name = "Кібербезпека",
                Description = "Інформація про кібербезпеку та захист даних",
                Slug = "cybersecurity"
            }
        };

        _context.Categories.AddRange(categories);
        return categories;
    }

    /// <summary>
    /// Створює тестові блоги
    /// </summary>
    private List<Blog> CreateBlogs(List<Author> authors, List<Category> categories)
    {
        Console.WriteLine("Створення блогів...");

        var blogs = new List<Blog>
        {
            new Blog
            {
                Title = "Технології та програмування",
                Description = "Блог про сучасні технології та програмування.",
                Slug = "tech-and-programming",
                AuthorId = authors[0].Id,
                CategoryId = categories[0].Id,
                CreatedAt = DateTime.Now.AddYears(-2),
                UpdatedAt = DateTime.Now.AddMonths(-1)
            },
            new Blog
            {
                Title = "Цифровий маркетинг",
                Description = "Блог про цифровий маркетинг та стратегії просування.",
                Slug = "digital-marketing",
                AuthorId = authors[1].Id,
                CategoryId = categories[2].Id,
                CreatedAt = DateTime.Now.AddYears(-1),
                UpdatedAt = DateTime.Now.AddMonths(-2)
            },
            new Blog
            {
                Title = "Кібербезпека сьогодні",
                Description = "Актуальні питання кібербезпеки та захисту інформації.",
                Slug = "cybersecurity-today",
                AuthorId = authors[2].Id,
                CategoryId = categories[3].Id,
                CreatedAt = DateTime.Now.AddMonths(-10),
                UpdatedAt = DateTime.Now.AddDays(-15)
            },
            new Blog
            {
                Title = "Розробка на .NET",
                Description = "Все про розробку на платформі .NET та C#.",
                Slug = "dotnet-development",
                AuthorId = authors[0].Id,
                CategoryId = categories[1].Id,
                CreatedAt = DateTime.Now.AddMonths(-8),
                UpdatedAt = DateTime.Now.AddDays(-5)
            }
        };

        _context.Blogs.AddRange(blogs);
        return blogs;
    }

    /// <summary>
    /// Створює тестові теги
    /// </summary>
    private List<Tag> CreateTags()
    {
        Console.WriteLine("Створення тегів...");

        var tags = new List<Tag>
        {
            new Tag { Name = "C#", Slug = "csharp" },
            new Tag { Name = "Entity Framework", Slug = "entity-framework" },
            new Tag { Name = ".NET", Slug = "dotnet" },
            new Tag { Name = "ASP.NET", Slug = "aspnet" },
            new Tag { Name = "SQL", Slug = "sql" },
            new Tag { Name = "LINQ", Slug = "linq" },
            new Tag { Name = "Безпека", Slug = "security" },
            new Tag { Name = "Маркетинг", Slug = "marketing" },
            new Tag { Name = "SEO", Slug = "seo" },
            new Tag { Name = "Технології", Slug = "technology" }
        };

        _context.Tags.AddRange(tags);
        return tags;
    }

    /// <summary>
    /// Створює тестові пости
    /// </summary>
    private List<Post> CreatePosts(List<Blog> blogs, List<Author> authors, List<Tag> tags)
    {
        Console.WriteLine("Створення постів...");

        var posts = new List<Post>();

        // Пости для блогу "Технології та програмування"
        var techBlog = blogs[0];
        var techAuthor = authors[0];

        var post1 = new Post
        {
            Title = "Вступ до Entity Framework Core",
            Summary = "Огляд основних можливостей Entity Framework Core",
            Content = "Entity Framework Core - це легкий, розширюваний та кросплатформений ORM-фреймворк від Microsoft...",
            Slug = "intro-to-ef-core",
            BlogId = techBlog.Id,
            AuthorId = techAuthor.Id,
            CreatedAt = DateTime.Now.AddMonths(-6),
            PublishedAt = DateTime.Now.AddMonths(-6),
            ViewCount = 1250,
            IsPublished = true
        };
        post1.Tags.Add(tags[1]); // Entity Framework
        post1.Tags.Add(tags[2]); // .NET
        post1.Tags.Add(tags[4]); // SQL
        posts.Add(post1);

        var post2 = new Post
        {
            Title = "LINQ запити в C#",
            Summary = "Детальний огляд LINQ запитів у C#",
            Content = "LINQ (Language Integrated Query) - це набір функцій у C#, які додають можливості запитів до мови...",
            Slug = "linq-queries-in-csharp",
            BlogId = techBlog.Id,
            AuthorId = techAuthor.Id,
            CreatedAt = DateTime.Now.AddMonths(-5),
            PublishedAt = DateTime.Now.AddMonths(-5),
            ViewCount = 980,
            IsPublished = true
        };
        post2.Tags.Add(tags[0]); // C#
        post2.Tags.Add(tags[5]); // LINQ
        posts.Add(post2);

        // Пости для блогу "Цифровий маркетинг"
        var marketingBlog = blogs[1];
        var marketingAuthor = authors[1];

        var post3 = new Post
        {
            Title = "Стратегії SEO у 2023 році",
            Summary = "Найефективніші стратегії SEO для просування сайтів",
            Content = "У цій статті ми розглянемо найефективніші стратегії SEO, які допоможуть вам підвищити рейтинг вашого сайту...",
            Slug = "seo-strategies-2023",
            BlogId = marketingBlog.Id,
            AuthorId = marketingAuthor.Id,
            CreatedAt = DateTime.Now.AddMonths(-4),
            PublishedAt = DateTime.Now.AddMonths(-4),
            ViewCount = 2100,
            IsPublished = true
        };
        post3.Tags.Add(tags[7]); // Маркетинг
        post3.Tags.Add(tags[8]); // SEO
        posts.Add(post3);

        // Пости для блогу "Кібербезпека сьогодні"
        var securityBlog = blogs[2];
        var securityAuthor = authors[2];

        var post4 = new Post
        {
            Title = "Захист даних у веб-додатках",
            Summary = "Основні принципи захисту даних у сучасних веб-додатках",
            Content = "Безпека даних у веб-додатках є критично важливою. У цій статті ми розглянемо основні принципи...",
            Slug = "data-protection-in-web-apps",
            BlogId = securityBlog.Id,
            AuthorId = securityAuthor.Id,
            CreatedAt = DateTime.Now.AddMonths(-3),
            PublishedAt = DateTime.Now.AddMonths(-3),
            ViewCount = 1560,
            IsPublished = true
        };
        post4.Tags.Add(tags[6]); // Безпека
        post4.Tags.Add(tags[3]); // ASP.NET
        posts.Add(post4);

        // Пости для блогу "Розробка на .NET"
        var dotnetBlog = blogs[3];
        var dotnetAuthor = authors[0];

        var post5 = new Post
        {
            Title = "Новинки в C# 12",
            Summary = "Огляд нових можливостей C# 12",
            Content = "C# продовжує розвиватися, і версія 12 приносить багато цікавих нововведень...",
            Slug = "csharp-12-new-features",
            BlogId = dotnetBlog.Id,
            AuthorId = dotnetAuthor.Id,
            CreatedAt = DateTime.Now.AddMonths(-2),
            PublishedAt = DateTime.Now.AddMonths(-2),
            ViewCount = 1820,
            IsPublished = true
        };
        post5.Tags.Add(tags[0]); // C#
        post5.Tags.Add(tags[2]); // .NET
        posts.Add(post5);

        var post6 = new Post
        {
            Title = "Робота з SQLite в Entity Framework Core",
            Summary = "Практичний посібник з використання SQLite в EF Core",
            Content = "SQLite - це легка вбудована база даних, яка ідеально підходить для розробки та тестування...",
            Slug = "sqlite-in-ef-core",
            BlogId = dotnetBlog.Id,
            AuthorId = dotnetAuthor.Id,
            CreatedAt = DateTime.Now.AddMonths(-1),
            PublishedAt = null,
            ViewCount = 0,
            IsPublished = false
        };
        post6.Tags.Add(tags[1]); // Entity Framework
        post6.Tags.Add(tags[4]); // SQL
        posts.Add(post6);

        _context.Posts.AddRange(posts);
        return posts;
    }

    /// <summary>
    /// Створює тестові коментарі
    /// </summary>
    private void CreateComments(List<Post> posts)
    {
        Console.WriteLine("Створення коментарів...");

        var comments = new List<Comment>();

        // Коментарі до посту "Вступ до Entity Framework Core"
        var post1 = posts[0];

        var comment1 = new Comment
        {
            Content = "Дуже корисна стаття! Дякую за детальний огляд.",
            AuthorName = "Петро Іваненко",
            AuthorEmail = "petro@example.com",
            CreatedAt = post1.PublishedAt!.Value.AddDays(2),
            IsApproved = true,
            PostId = post1.Id
        };
        comments.Add(comment1);

        var comment2 = new Comment
        {
            Content = "Чи можна використовувати EF Core з NoSQL базами даних?",
            AuthorName = "Олена Сергієнко",
            AuthorEmail = "olena@example.com",
            CreatedAt = post1.PublishedAt!.Value.AddDays(3),
            IsApproved = true,
            PostId = post1.Id
        };
        comments.Add(comment2);

        var reply1 = new Comment
        {
            Content = "Так, EF Core підтримує деякі NoSQL бази даних через провайдери, наприклад, Cosmos DB.",
            AuthorName = "Іван Петренко",
            AuthorEmail = "ivan.petrenko@example.com",
            CreatedAt = post1.PublishedAt!.Value.AddDays(3).AddHours(2),
            IsApproved = true,
            PostId = post1.Id,
            ParentCommentId = 2 // Буде встановлено після збереження comment2
        };
        comments.Add(reply1);

        // Коментарі до посту "LINQ запити в C#"
        var post2 = posts[1];

        var comment3 = new Comment
        {
            Content = "LINQ дійсно спрощує роботу з колекціями даних!",
            AuthorName = "Микола Дмитренко",
            AuthorEmail = "mykola@example.com",
            CreatedAt = post2.PublishedAt!.Value.AddDays(1),
            IsApproved = true,
            PostId = post2.Id
        };
        comments.Add(comment3);

        // Коментарі до посту "Стратегії SEO у 2023 році"
        var post3 = posts[2];

        var comment4 = new Comment
        {
            Content = "Які інструменти ви рекомендуєте для аналізу SEO?",
            AuthorName = "Анна Ковальчук",
            AuthorEmail = "anna@example.com",
            CreatedAt = post3.PublishedAt!.Value.AddDays(2),
            IsApproved = true,
            PostId = post3.Id
        };
        comments.Add(comment4);

        var reply2 = new Comment
        {
            Content = "Я рекомендую Google Search Console та Ahrefs для початку.",
            AuthorName = "Марія Коваленко",
            AuthorEmail = "maria.kovalenko@example.com",
            CreatedAt = post3.PublishedAt!.Value.AddDays(2).AddHours(3),
            IsApproved = true,
            PostId = post3.Id,
            ParentCommentId = 5 // Буде встановлено після збереження comment4
        };
        comments.Add(reply2);

        var reply3 = new Comment
        {
            Content = "Також варто спробувати SEMrush для комплексного аналізу.",
            AuthorName = "Сергій Мельник",
            AuthorEmail = "sergiy@example.com",
            CreatedAt = post3.PublishedAt!.Value.AddDays(3),
            IsApproved = true,
            PostId = post3.Id,
            ParentCommentId = 5 // Буде встановлено після збереження comment4
        };
        comments.Add(reply3);

        // Коментарі до посту "Захист даних у веб-додатках"
        var post4 = posts[3];

        var comment5 = new Comment
        {
            Content = "Чи можете ви розповісти більше про OWASP Top 10?",
            AuthorName = "Віктор Лисенко",
            AuthorEmail = "viktor@example.com",
            CreatedAt = post4.PublishedAt!.Value.AddDays(4),
            IsApproved = true,
            PostId = post4.Id
        };
        comments.Add(comment5);

        // Коментарі до посту "Новинки в C# 12"
        var post5 = posts[4];

        var comment6 = new Comment
        {
            Content = "Які нові функції вам найбільше подобаються?",
            AuthorName = "Дмитро Шевченко",
            AuthorEmail = "dmytro@example.com",
            CreatedAt = post5.PublishedAt!.Value.AddDays(1),
            IsApproved = true,
            PostId = post5.Id
        };
        comments.Add(comment6);

        _context.Comments.AddRange(comments);

        // Встановлення ParentCommentId після збереження коментарів
        _context.SaveChanges();

        // Оновлення ParentCommentId для відповідей
        var savedComment2 = _context.Comments.Find(2);
        var savedReply1 = _context.Comments.Find(3);
        if (savedComment2 != null && savedReply1 != null)
        {
            savedReply1.ParentCommentId = savedComment2.Id;
        }

        var savedComment4 = _context.Comments.Find(5);
        var savedReply2 = _context.Comments.Find(6);
        var savedReply3 = _context.Comments.Find(7);
        if (savedComment4 != null && savedReply2 != null && savedReply3 != null)
        {
            savedReply2.ParentCommentId = savedComment4.Id;
            savedReply3.ParentCommentId = savedComment4.Id;
        }

        _context.SaveChanges();
    }
}
