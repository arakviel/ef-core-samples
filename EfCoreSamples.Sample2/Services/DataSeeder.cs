using EfCoreSamples.Sample2.Data;
using EfCoreSamples.Sample2.Models;
using EfCoreSamples.Sample2.Models.Inheritance;

namespace EfCoreSamples.Sample2.Services;

/// <summary>
/// Сервіс для наповнення бази даних тестовими даними.
/// </summary>
public class DataSeeder
{
    private readonly BlogDbContext _context;

    public DataSeeder(BlogDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Наповнює базу даних тестовими даними.
    /// </summary>
    public void SeedData()
    {
        // Створення авторів
        var authors = CreateAuthors();

        // Створення блогів
        var blogs = CreateBlogs(authors);

        // Створення постів
        var posts = CreatePosts(blogs, authors);

        // Створення коментарів
        CreateComments(posts);

        // Створення контенту для демонстрації наслідування
        CreateInheritanceContent();

        // Збереження змін у базі даних
        _context.SaveChanges();

        Console.WriteLine("База даних успішно наповнена тестовими даними.");
    }

    /// <summary>
    /// Створює тестових авторів.
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
                Address = new Address
                {
                    Street = "Хрещатик",
                    HouseNumber = "10",
                    City = "Київ",
                    PostalCode = "01001",
                    Country = "Україна"
                }
            },
            new Author
            {
                FirstName = "Марія",
                LastName = "Коваленко",
                Email = "maria.kovalenko@example.com",
                Bio = "Фахівець з цифрового маркетингу та контент-менеджер.",
                AvatarUrl = "https://example.com/avatars/maria.jpg",
                Address = new Address
                {
                    Street = "Дерибасівська",
                    HouseNumber = "15",
                    City = "Одеса",
                    PostalCode = "65000",
                    Country = "Україна"
                }
            },
            new Author
            {
                FirstName = "Олександр",
                LastName = "Сидоренко",
                Email = "oleksandr.sydorenko@example.com",
                Bio = "Розробник баз даних та архітектор програмного забезпечення.",
                AvatarUrl = "https://example.com/avatars/oleksandr.jpg",
                Address = new Address
                {
                    Street = "Проспект Свободи",
                    HouseNumber = "20",
                    City = "Львів",
                    PostalCode = "79000",
                    Country = "Україна"
                }
            }
        };

        _context.Authors.AddRange(authors);
        _context.SaveChanges();

        return authors;
    }

    /// <summary>
    /// Створює тестові блоги.
    /// </summary>
    private List<Blog> CreateBlogs(List<Author> authors)
    {
        Console.WriteLine("Створення блогів...");

        var blogs = new List<Blog>
        {
            new Blog
            {
                Title = "Технології та програмування",
                Description = "Блог про сучасні технології та програмування.",
                Slug = "tech-and-programming",
                MetaTitle = "Технології та програмування | Блог",
                MetaDescription = "Блог про сучасні технології, програмування та розробку програмного забезпечення.",
                MetaImage = "https://example.com/images/tech-blog.jpg",
                AuthorId = authors[0].Id
            },
            new Blog
            {
                Title = "Цифровий маркетинг",
                Description = "Блог про цифровий маркетинг та стратегії просування.",
                Slug = "digital-marketing",
                MetaTitle = "Цифровий маркетинг | Блог",
                MetaDescription = "Блог про цифровий маркетинг, SEO, SMM та контент-маркетинг.",
                MetaImage = "https://example.com/images/marketing-blog.jpg",
                AuthorId = authors[1].Id
            },
            new Blog
            {
                Title = "Бази даних та архітектура",
                Description = "Блог про бази даних, архітектуру програмного забезпечення та проектування систем.",
                Slug = "databases-and-architecture",
                MetaTitle = "Бази даних та архітектура | Блог",
                MetaDescription = "Блог про бази даних, архітектуру програмного забезпечення та проектування систем.",
                MetaImage = "https://example.com/images/database-blog.jpg",
                AuthorId = authors[2].Id
            }
        };

        _context.Blogs.AddRange(blogs);
        _context.SaveChanges();

        return blogs;
    }

    /// <summary>
    /// Створює тестові пости.
    /// </summary>
    private List<Post> CreatePosts(List<Blog> blogs, List<Author> authors)
    {
        Console.WriteLine("Створення постів...");

        var posts = new List<Post>
        {
            new Post
            {
                Title = "Вступ до Entity Framework Core",
                Summary = "Огляд основних можливостей Entity Framework Core та його переваг.",
                Content = "Entity Framework Core (EF Core) - це легка, розширювана та кросплатформена версія популярної технології доступу до даних Entity Framework. EF Core може служити об'єктно-реляційним мапером (O/RM), що дозволяє розробникам .NET працювати з базою даних, використовуючи .NET об'єкти та усуваючи необхідність у більшості коду доступу до даних, який зазвичай потрібно писати...",
                Slug = "introduction-to-entity-framework-core",
                MetaTitle = "Вступ до Entity Framework Core | Технології та програмування",
                MetaDescription = "Огляд основних можливостей Entity Framework Core та його переваг для розробників .NET.",
                BlogId = blogs[0].Id,
                AuthorId = authors[0].Id,
                IsPublished = true,
                PublishedAt = DateTime.Now.AddDays(-10)
            },
            new Post
            {
                Title = "Lazy Loading в Entity Framework Core",
                Summary = "Як працює Lazy Loading в Entity Framework Core та коли його використовувати.",
                Content = "Lazy Loading - це процес, при якому пов'язані дані завантажуються з бази даних лише тоді, коли до них звертаються через навігаційну властивість. У Entity Framework Core Lazy Loading можна включити за допомогою проксі-класів або явно завантажувати пов'язані дані за допомогою методів Include та ThenInclude...",
                Slug = "lazy-loading-in-entity-framework-core",
                MetaTitle = "Lazy Loading в Entity Framework Core | Технології та програмування",
                MetaDescription = "Як працює Lazy Loading в Entity Framework Core та коли його використовувати для оптимізації запитів.",
                BlogId = blogs[0].Id,
                AuthorId = authors[0].Id,
                IsPublished = true,
                PublishedAt = DateTime.Now.AddDays(-5)
            },
            new Post
            {
                Title = "Стратегії SEO для технічних блогів",
                Summary = "Ефективні стратегії SEO для технічних блогів та сайтів.",
                Content = "Оптимізація для пошукових систем (SEO) є критично важливою для технічних блогів, оскільки допомагає залучати цільову аудиторію. У цій статті ми розглянемо ефективні стратегії SEO, включаючи дослідження ключових слів, оптимізацію контенту, технічне SEO та стратегії побудови посилань...",
                Slug = "seo-strategies-for-technical-blogs",
                MetaTitle = "Стратегії SEO для технічних блогів | Цифровий маркетинг",
                MetaDescription = "Ефективні стратегії SEO для технічних блогів та сайтів, які допоможуть залучити цільову аудиторію.",
                BlogId = blogs[1].Id,
                AuthorId = authors[1].Id,
                IsPublished = true,
                PublishedAt = DateTime.Now.AddDays(-8)
            },
            new Post
            {
                Title = "Оптимізація запитів в SQL",
                Summary = "Техніки оптимізації запитів в SQL для підвищення продуктивності бази даних.",
                Content = "Оптимізація запитів є важливою частиною розробки та підтримки баз даних. У цій статті ми розглянемо різні техніки оптимізації запитів в SQL, включаючи використання індексів, аналіз планів виконання запитів, оптимізацію JOIN операцій та інші стратегії для підвищення продуктивності...",
                Slug = "sql-query-optimization",
                MetaTitle = "Оптимізація запитів в SQL | Бази даних та архітектура",
                MetaDescription = "Техніки оптимізації запитів в SQL для підвищення продуктивності бази даних та швидкості виконання запитів.",
                BlogId = blogs[2].Id,
                AuthorId = authors[2].Id,
                IsPublished = true,
                PublishedAt = DateTime.Now.AddDays(-3)
            },
            new Post
            {
                Title = "Використання Value Objects в Entity Framework Core",
                Summary = "Як використовувати Value Objects в Entity Framework Core для покращення дизайну домену.",
                Content = "Value Objects - це об'єкти, які не мають власної ідентичності, а визначаються своїми атрибутами. У цій статті ми розглянемо, як використовувати Value Objects в Entity Framework Core для покращення дизайну домену та інкапсуляції бізнес-логіки...",
                Slug = "using-value-objects-in-entity-framework-core",
                MetaTitle = "Використання Value Objects в Entity Framework Core | Технології та програмування",
                MetaDescription = "Як використовувати Value Objects в Entity Framework Core для покращення дизайну домену та інкапсуляції бізнес-логіки.",
                BlogId = blogs[0].Id,
                AuthorId = authors[0].Id,
                IsPublished = false
            }
        };

        _context.Posts.AddRange(posts);
        _context.SaveChanges();

        return posts;
    }

    /// <summary>
    /// Створює тестові коментарі з ієрархічною структурою.
    /// </summary>
    private void CreateComments(List<Post> posts)
    {
        Console.WriteLine("Створення коментарів...");

        // Коментарі для першого поста
        var comment1 = new Comment
        {
            Content = "Дуже корисна стаття! Чи плануєте ви написати про міграції в EF Core?",
            AuthorName = "Петро Іваненко",
            AuthorEmail = "petro@example.com",
            PostId = posts[0].Id,
            IsApproved = true
        };

        _context.Comments.Add(comment1);
        _context.SaveChanges();

        var reply1 = new Comment
        {
            Content = "Дякую за відгук! Так, планую написати про міграції в наступній статті.",
            AuthorName = "Іван Петренко",
            AuthorEmail = "ivan.petrenko@example.com",
            PostId = posts[0].Id,
            ParentCommentId = comment1.Id,
            IsApproved = true
        };

        _context.Comments.Add(reply1);

        /*var reply2 = new Comment
        {
            Content = "Чекаю з нетерпінням! Міграції - це дуже важлива тема.",
            AuthorName = "Петро Іваненко",
            AuthorEmail = "petro@example.com",
            PostId = posts[0].Id,
            ParentCommentId = reply1.Id,
            IsApproved = true
        };

        _context.Comments.Add(reply2);

        var comment2 = new Comment
        {
            Content = "Чи можете ви порівняти EF Core з Dapper?",
            AuthorName = "Олена Коваль",
            AuthorEmail = "olena@example.com",
            PostId = posts[0].Id,
            IsApproved = true
        };

        _context.Comments.Add(comment2);

        // Коментарі для другого поста
        var comment3 = new Comment
        {
            Content = "Чи є якісь недоліки у використанні Lazy Loading?",
            AuthorName = "Максим Шевченко",
            AuthorEmail = "maksym@example.com",
            PostId = posts[1].Id,
            IsApproved = true
        };

        _context.Comments.Add(comment3);
        var reply3 = new Comment
        {
            Content = "Так, основний недолік - це можливість виникнення проблеми N+1 запитів, коли для кожного об'єкта в колекції виконується окремий запит до бази даних.",
            AuthorName = "Іван Петренко",
            AuthorEmail = "ivan.petrenko@example.com",
            PostId = posts[1].Id,
            ParentCommentId = comment3.Id,
            IsApproved = true
        };

        _context.Comments.Add(reply3);

        var reply4 = new Comment
        {
            Content = "Дякую за пояснення! Як можна уникнути цієї проблеми?",
            AuthorName = "Максим Шевченко",
            AuthorEmail = "maksym@example.com",
            PostId = posts[1].Id,
            ParentCommentId = reply3.Id,
            IsApproved = true
        };

        _context.Comments.Add(reply4);

        var reply5 = new Comment
        {
            Content = "Для уникнення проблеми N+1 запитів можна використовувати Eager Loading (Include) або явне завантаження (Explicit Loading).",
            AuthorName = "Іван Петренко",
            AuthorEmail = "ivan.petrenko@example.com",
            PostId = posts[1].Id,
            ParentCommentId = reply4.Id,
            IsApproved = true
        };

        _context.Comments.Add(reply5);*/

        _context.SaveChanges();
    }

    /// <summary>
    /// Створює тестовий контент для демонстрації наслідування.
    /// </summary>
    private void CreateInheritanceContent()
    {
        Console.WriteLine("Створення контенту для демонстрації наслідування...");

        // Створення контенту для TPH (Table Per Hierarchy)
        CreateTPHContent();

        // Створення контенту для TPT (Table Per Type)
        CreateTPTContent();

        // Створення контенту для TPC (Table Per Concrete class)
        CreateTPCContent();

        _context.SaveChanges();
    }

    /// <summary>
    /// Створює тестовий контент для демонстрації TPH (Table Per Hierarchy).
    /// </summary>
    private void CreateTPHContent()
    {
        // Створення статей
        var article1 = new Article
        {
            Title = "Вступ до TPH в EF Core",
            Description = "Огляд підходу Table Per Hierarchy в Entity Framework Core.",
            Body = "Table Per Hierarchy (TPH) - це підхід до наслідування в Entity Framework Core, при якому всі класи ієрархії зберігаються в одній таблиці...",
            WordCount = 1200,
            ReadingTimeMinutes = 6,
            Keywords = "EF Core, TPH, наслідування, ORM",
            IsPublished = true,
            PublishedAt = DateTime.Now.AddDays(-2)
        };

        var article2 = new Article
        {
            Title = "Переваги та недоліки TPH",
            Description = "Аналіз переваг та недоліків підходу Table Per Hierarchy.",
            Body = "У цій статті ми розглянемо переваги та недоліки підходу Table Per Hierarchy (TPH) в Entity Framework Core...",
            WordCount = 1500,
            ReadingTimeMinutes = 8,
            Keywords = "EF Core, TPH, наслідування, аналіз",
            IsPublished = true,
            PublishedAt = DateTime.Now.AddDays(-1)
        };

        // Створення відео
        var video1 = new Video
        {
            Title = "Демонстрація TPH в EF Core",
            Description = "Відеодемонстрація підходу Table Per Hierarchy в Entity Framework Core.",
            Url = "https://example.com/videos/tph-demo.mp4",
            DurationInSeconds = 600,
            Format = "MP4",
            Resolution = "1080p",
            IsPublished = true,
            PublishedAt = DateTime.Now.AddDays(-3)
        };

        // Створення подкасту
        var podcast1 = new Podcast
        {
            Title = "Обговорення TPH в EF Core",
            Description = "Подкаст про підхід Table Per Hierarchy в Entity Framework Core.",
            AudioUrl = "https://example.com/podcasts/tph-discussion.mp3",
            DurationInSeconds = 1800,
            Host = "Іван Петренко",
            Guests = "Олександр Сидоренко, Марія Коваленко",
            Transcript = "Іван: Вітаю всіх у нашому подкасті...",
            IsPublished = true,
            PublishedAt = DateTime.Now.AddDays(-4)
        };

        _context.ArticlesTPH.Add(article1);
        _context.ArticlesTPH.Add(article2);
        _context.VideosTPH.Add(video1);
        _context.PodcastsTPH.Add(podcast1);
    }

    /// <summary>
    /// Створює тестовий контент для демонстрації TPT (Table Per Type).
    /// </summary>
    private void CreateTPTContent()
    {
        // Створення статей
        var article1 = new Article
        {
            Title = "Вступ до TPT в EF Core",
            Description = "Огляд підходу Table Per Type в Entity Framework Core.",
            Body = "Table Per Type (TPT) - це підхід до наслідування в Entity Framework Core, при якому для кожного типу створюється окрема таблиця...",
            WordCount = 1300,
            ReadingTimeMinutes = 7,
            Keywords = "EF Core, TPT, наслідування, ORM",
            IsPublished = true,
            PublishedAt = DateTime.Now.AddDays(-2)
        };

        var article2 = new Article
        {
            Title = "Переваги та недоліки TPT",
            Description = "Аналіз переваг та недоліків підходу Table Per Type.",
            Body = "У цій статті ми розглянемо переваги та недоліки підходу Table Per Type (TPT) в Entity Framework Core...",
            WordCount = 1600,
            ReadingTimeMinutes = 9,
            Keywords = "EF Core, TPT, наслідування, аналіз",
            IsPublished = true,
            PublishedAt = DateTime.Now.AddDays(-1)
        };

        // Створення відео
        var video1 = new Video
        {
            Title = "Демонстрація TPT в EF Core",
            Description = "Відеодемонстрація підходу Table Per Type в Entity Framework Core.",
            Url = "https://example.com/videos/tpt-demo.mp4",
            DurationInSeconds = 650,
            Format = "MP4",
            Resolution = "1080p",
            IsPublished = true,
            PublishedAt = DateTime.Now.AddDays(-3)
        };

        // Створення подкасту
        var podcast1 = new Podcast
        {
            Title = "Обговорення TPT в EF Core",
            Description = "Подкаст про підхід Table Per Type в Entity Framework Core.",
            AudioUrl = "https://example.com/podcasts/tpt-discussion.mp3",
            DurationInSeconds = 1850,
            Host = "Іван Петренко",
            Guests = "Олександр Сидоренко, Марія Коваленко",
            Transcript = "Іван: Вітаю всіх у нашому подкасті...",
            IsPublished = true,
            PublishedAt = DateTime.Now.AddDays(-4)
        };

        _context.ArticlesTPT.Add(article1);
        _context.ArticlesTPT.Add(article2);
        _context.VideosTPT.Add(video1);
        _context.PodcastsTPT.Add(podcast1);
    }

    /// <summary>
    /// Створює тестовий контент для демонстрації TPC (Table Per Concrete class).
    /// </summary>
    private void CreateTPCContent()
    {
        // Створення статей
        var article1 = new Article
        {
            Title = "Вступ до TPC в EF Core",
            Description = "Огляд підходу Table Per Concrete class в Entity Framework Core.",
            Body = "Table Per Concrete class (TPC) - це підхід до наслідування в Entity Framework Core, при якому для кожного конкретного класу створюється окрема таблиця...",
            WordCount = 1400,
            ReadingTimeMinutes = 8,
            Keywords = "EF Core, TPC, наслідування, ORM",
            IsPublished = true,
            PublishedAt = DateTime.Now.AddDays(-2)
        };

        var article2 = new Article
        {
            Title = "Переваги та недоліки TPC",
            Description = "Аналіз переваг та недоліків підходу Table Per Concrete class.",
            Body = "У цій статті ми розглянемо переваги та недоліки підходу Table Per Concrete class (TPC) в Entity Framework Core...",
            WordCount = 1700,
            ReadingTimeMinutes = 10,
            Keywords = "EF Core, TPC, наслідування, аналіз",
            IsPublished = true,
            PublishedAt = DateTime.Now.AddDays(-1)
        };

        // Створення відео
        var video1 = new Video
        {
            Title = "Демонстрація TPC в EF Core",
            Description = "Відеодемонстрація підходу Table Per Concrete class в Entity Framework Core.",
            Url = "https://example.com/videos/tpc-demo.mp4",
            DurationInSeconds = 700,
            Format = "MP4",
            Resolution = "1080p",
            IsPublished = true,
            PublishedAt = DateTime.Now.AddDays(-3)
        };

        // Створення подкасту
        var podcast1 = new Podcast
        {
            Title = "Обговорення TPC в EF Core",
            Description = "Подкаст про підхід Table Per Concrete class в Entity Framework Core.",
            AudioUrl = "https://example.com/podcasts/tpc-discussion.mp3",
            DurationInSeconds = 1900,
            Host = "Іван Петренко",
            Guests = "Олександр Сидоренко, Марія Коваленко",
            Transcript = "Іван: Вітаю всіх у нашому подкасті...",
            IsPublished = true,
            PublishedAt = DateTime.Now.AddDays(-4)
        };

        _context.ArticlesTPC.Add(article1);
        _context.ArticlesTPC.Add(article2);
        _context.VideosTPC.Add(video1);
        _context.PodcastsTPC.Add(podcast1);
    }
}
