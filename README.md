# Entity Framework Core - Навчальні приклади

Цей проект містить навчальні приклади роботи з Entity Framework Core та SQLite. Проект створено для навчальних цілей.

## Приклади

### Sample1: Зв'язки між таблицями

Цей приклад демонструє різні типи зв'язків між таблицями в Entity Framework Core з використанням SQLite.

### Sample2: Блог з різними можливостями EF Core

Цей приклад демонструє розширені можливості Entity Framework Core:
- Робота з SQLite
- Eager loading та Lazy loading пов'язаних даних
- Використання ValueObjects (складних типів даних)
- Робота з ієрархічними даними (коментарі з відповідями)
- Наслідування від абстрактного класу
- Конфігурація сутностей через окремі класи

### Sample3: Розширена блог-система з LINQ запитами

Цей приклад демонструє розширену блог-систему з використанням Entity Framework Core та LINQ запитів:
- Робота з SQLite та Lazy loading
- Демонстрація різних типів LINQ запитів (базові, агрегатні, групування, з'єднання)
- Використання Include для завантаження пов'язаних даних
- Робота з ієрархічними даними (коментарі з відповідями)
- Наповнення бази даних тестовими та фейковими даними (з використанням Bogus)
- Використання Query Filters для фільтрації даних

## Зміст

- [Entity Framework Core - Навчальні приклади](#entity-framework-core---навчальні-приклади)
  - [Приклади](#приклади)
    - [Sample1: Зв'язки між таблицями](#sample1-звязки-між-таблицями)
    - [Sample2: Блог з різними можливостями EF Core](#sample2-блог-з-різними-можливостями-ef-core)
    - [Sample3: Розширена блог-система з LINQ запитами](#sample3-розширена-блог-система-з-linq-запитами)
  - [Зміст](#зміст)
  - [Огляд Entity Framework Core](#огляд-entity-framework-core)
  - [Типи зв'язків (Sample1)](#типи-звязків-sample1)
    - [One-to-One (Один до одного)](#one-to-one-один-до-одного)
    - [One-to-Many (Один до багатьох)](#one-to-many-один-до-багатьох)
    - [Many-to-Many (Багато до багатьох)](#many-to-many-багато-до-багатьох)
  - [Підходи до конфігурації зв'язків (Sample1)](#підходи-до-конфігурації-звязків-sample1)
    - [Угоди (Conventions)](#угоди-conventions)
    - [Анотації (Annotations)](#анотації-annotations)
    - [Fluent API](#fluent-api)
  - [Можливості EF Core (Sample2)](#можливості-ef-core-sample2)
    - [Eager Loading та Lazy Loading](#eager-loading-та-lazy-loading)
    - [ValueObjects (складні типи даних)](#valueobjects-складні-типи-даних)
    - [Ієрархічні дані](#ієрархічні-дані)
    - [Наслідування від абстрактного класу](#наслідування-від-абстрактного-класу)
  - [LINQ запити та робота з даними (Sample3)](#linq-запити-та-робота-з-даними-sample3)
    - [Структура бази даних](#структура-бази-даних)
    - [Базові LINQ запити](#базові-linq-запити)
    - [Агрегатні LINQ запити](#агрегатні-linq-запити)
    - [LINQ запити з групуванням](#linq-запити-з-групуванням)
    - [LINQ запити з з'єднанням таблиць](#linq-запити-з-зєднанням-таблиць)
    - [Завантаження пов'язаних даних (Include)](#завантаження-повязаних-даних-include)
    - [Наповнення бази даних тестовими даними](#наповнення-бази-даних-тестовими-даними)
    - [Використання Bogus для генерації фейкових даних](#використання-bogus-для-генерації-фейкових-даних)

## Огляд Entity Framework Core

Entity Framework Core (EF Core) - це легковісна, розширювана та крос-платформна версія популярної технології доступу до даних Entity Framework. EF Core є об'єктно-реляційним маппером (ORM), який дозволяє .NET розробникам працювати з базою даних, використовуючи .NET об'єкти, і усуває необхідність більшості коду доступу до даних, який зазвичай потрібно писати.

## Типи зв'язків (Sample1)

### One-to-One (Один до одного)

Зв'язок "один до одного" означає, що один запис у таблиці A пов'язаний з одним записом у таблиці B. Наприклад, один студент має один профіль.

```csharp
// Клас Student
public class Student
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;

    // Навігаційна властивість для зв'язку один-до-одного
    public StudentProfile? Profile { get; set; }
}

// Клас StudentProfile
public class StudentProfile
{
    public int Id { get; set; }
    public string Address { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;

    // Зовнішній ключ для зв'язку один-до-одного
    public int StudentId { get; set; }

    // Навігаційна властивість для зв'язку один-до-одного
    public Student Student { get; set; } = null!;
}
```

### One-to-Many (Один до багатьох)

Зв'язок "один до багатьох" означає, що один запис у таблиці A пов'язаний з багатьма записами в таблиці B. Наприклад, один студент може вивчати багато курсів.

```csharp
// Клас Student
public class Student
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;

    // Навігаційна властивість для зв'язку один-до-багатьох
    public List<Course> Courses { get; set; } = new();
}

// Клас Course
public class Course
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;

    // Зовнішній ключ для зв'язку один-до-багатьох
    public int StudentId { get; set; }

    // Навігаційна властивість для зв'язку один-до-багатьох
    public Student Student { get; set; } = null!;
}
```

### Many-to-Many (Багато до багатьох)

Зв'язок "багато до багатьох" означає, що багато записів у таблиці A пов'язані з багатьма записами в таблиці B. Наприклад, багато студентів можуть відвідувати багато гуртків.

```csharp
// Клас Student
public class Student
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;

    // Навігаційна властивість для зв'язку багато-до-багатьох
    public List<Club> Clubs { get; set; } = new();
}

// Клас Club
public class Club
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;

    // Навігаційна властивість для зв'язку багато-до-багатьох
    public List<Student> Students { get; set; } = new();
}
```

У EF Core 5.0 і вище, зв'язок "багато до багатьох" може бути налаштований без явного визначення проміжної таблиці. Однак, якщо вам потрібно додати додаткові властивості до зв'язку, ви можете явно визначити проміжну таблицю:

```csharp
// Проміжна таблиця для зв'язку багато-до-багатьох з додатковими властивостями
public class StudentClub
{
    public int StudentId { get; set; }
    public Student Student { get; set; } = null!;

    public int ClubId { get; set; }
    public Club Club { get; set; } = null!;

    // Додаткова властивість для зв'язку
    public DateTime JoinDate { get; set; }
}
```

## Підходи до конфігурації зв'язків (Sample1)

EF Core пропонує три основні підходи до конфігурації зв'язків між таблицями:

### Угоди (Conventions)

Угоди - це набір правил, які EF Core використовує за замовчуванням для визначення моделі з класів сутностей. Наприклад, властивість з назвою `Id` або `{EntityName}Id` буде автоматично налаштована як первинний ключ.

```csharp
// Зв'язок One-to-One за допомогою угод
public class StudentProfile
{
    // За угодою, якщо властивість називається {NavigationPropertyName}Id,
    // вона буде використана як зовнішній ключ
    public int StudentId { get; set; }

    // Навігаційна властивість
    public Student Student { get; set; } = null!;
}
```

### Анотації (Annotations)

Анотації - це атрибути, які можна застосувати до класів та властивостей для налаштування моделі.

```csharp
// Зв'язок One-to-One за допомогою анотацій
public class StudentProfile
{
    [Key]
    [ForeignKey("Student")]
    public int StudentId { get; set; }

    [Required]
    public Student Student { get; set; } = null!;
}
```

### Fluent API

Fluent API надає найбільш потужний спосіб налаштування моделі. Конфігурація виконується в методі `OnModelCreating` контексту бази даних.

```csharp
// Зв'язок One-to-One за допомогою Fluent API
protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    modelBuilder.Entity<StudentProfile>(entity =>
    {
        entity.HasOne(d => d.Student)
            .WithOne(p => p.Profile)
            .HasForeignKey<StudentProfile>(d => d.StudentId)
            .OnDelete(DeleteBehavior.Cascade);
    });
}
```

## Можливості EF Core (Sample2)

### Eager Loading та Lazy Loading

EF Core пропонує різні способи завантаження пов'язаних даних:

- **Eager Loading** - завантаження пов'язаних даних разом з основними даними в одному запиті за допомогою методів `Include` та `ThenInclude`.
- **Lazy Loading** - завантаження пов'язаних даних лише при зверненні до них через навігаційну властивість.
- **Explicit Loading** - явне завантаження пов'язаних даних за допомогою методів `Entry().Reference().Load()` та `Entry().Collection().Load()`.

```csharp
// Eager Loading
var blog = context.Blogs
    .Include(b => b.Posts) // Завантаження постів
    .Include(b => b.Author) // Завантаження автора
        .ThenInclude(a => a.Address) // Завантаження адреси автора
    .FirstOrDefault();

// Lazy Loading
// При зверненні до навігаційної властивості Author
// EF Core автоматично завантажить дані автора з бази даних
var authorName = blog.Author.FullName;
```

### ValueObjects (складні типи даних)

ValueObjects - це об'єкти, які не мають власної ідентичності, а визначаються своїми атрибутами. В EF Core вони реалізуються за допомогою атрибута `[Owned]` або методу `OwnsOne` в Fluent API.

```csharp
// Визначення ValueObject
[Owned]
public class Address
{
    public string Street { get; set; } = null!;
    public string City { get; set; } = null!;
    public string PostalCode { get; set; } = null!;
    public string Country { get; set; } = null!;
}

// Використання ValueObject в сутності
public class Author
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public Address Address { get; set; } = null!;
}
```

### Ієрархічні дані

EF Core дозволяє працювати з ієрархічними даними, наприклад, коментарями з відповідями, за допомогою самореферентних зв'язків.

```csharp
public class Comment
{
    public int Id { get; set; }
    public string Content { get; set; } = null!;

    // Зв'язок з батьківським коментарем
    public int? ParentCommentId { get; set; }
    public virtual Comment? ParentComment { get; set; }

    // Зв'язок з дочірніми коментарями (відповідями)
    public virtual ICollection<Comment> Replies { get; set; } = new List<Comment>();
}
```

### Наслідування від абстрактного класу

EF Core підтримує наслідування від абстрактного класу, що дозволяє створювати більш гнучкі моделі даних.

```csharp
// Абстрактний базовий клас

public abstract class Meta
{
    public int Id { get; set; }
    public string Slug { get; set; } = null!;
    public string? MetaTitle { get; set; }
    public string? MetaDescription { get; set; }
}

// Класи, які наслідують абстрактний клас
public class Blog : Meta
{
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
}

public class Post : Meta
{
    public string Title { get; set; } = null!;
    public string Content { get; set; } = null!;
}
```

## LINQ запити та робота з даними (Sample3)

Sample3 демонструє розширену блог-систему з використанням Entity Framework Core та LINQ запитів. Цей приклад фокусується на демонстрації різних типів LINQ запитів та роботі з даними.

### Структура бази даних

База даних Sample3 містить наступні моделі:

- **Author** - автор блогу
- **Category** - категорія блогу
- **Blog** - блог
- **Post** - пост у блозі
- **Comment** - коментар до посту (з можливістю відповідей на коментарі)
- **Tag** - тег для постів
- **PostTag** - проміжна таблиця для зв'язку багато-до-багатьох між Post і Tag

Основні зв'язки між таблицями:

```csharp
// Модель блогу
public class Blog
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public string Slug { get; set; } = null!;
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime? UpdatedAt { get; set; }
    public int AuthorId { get; set; }
    public int CategoryId { get; set; }

    // Навігаційні властивості
    public virtual Author Author { get; set; } = null!;
    public virtual Category Category { get; set; } = null!;
    public virtual ICollection<Post> Posts { get; set; } = new List<Post>();
}

// Модель поста
public class Post
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string? Summary { get; set; }
    public string Content { get; set; } = null!;
    public string Slug { get; set; } = null!;
    public string? ImageUrl { get; set; }
    public int ViewCount { get; set; }
    public bool IsPublished { get; set; }
    public DateTime? PublishedAt { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime? UpdatedAt { get; set; }
    public int BlogId { get; set; }
    public int AuthorId { get; set; }

    // Навігаційні властивості
    public virtual Blog Blog { get; set; } = null!;
    public virtual Author Author { get; set; } = null!;
    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();
    public virtual ICollection<Tag> Tags { get; set; } = new List<Tag>();
}
```

### Базові LINQ запити

Sample3 демонструє базові LINQ запити, такі як фільтрація, сортування та проекція:

```csharp
// Фільтрація з Where
var popularPosts = _context.Posts
    .Where(p => p.ViewCount > 1000)
    .ToList();

// Сортування з OrderBy
var orderedPosts = _context.Posts
    .OrderByDescending(p => p.ViewCount)
    .Take(3)
    .ToList();

// Проекція з Select
var postTitles = _context.Posts
    .Where(p => p.IsPublished)
    .Select(p => new { p.Title, p.PublishedAt })
    .ToList();
```

### Агрегатні LINQ запити

Sample3 демонструє агрегатні LINQ запити, такі як Count, Sum, Average, Min та Max:

```csharp
// Count - кількість постів у кожному блозі
var blogPostCounts = _context.Blogs
    .Select(b => new
    {
        b.Title,
        PostCount = b.Posts.Count
    })
    .ToList();

// Sum - загальна кількість переглядів постів для кожного автора
var authorViewCounts = _context.Authors
    .Select(a => new
    {
        FullName = a.FirstName + " " + a.LastName,
        TotalViews = a.Posts.Sum(p => p.ViewCount)
    })
    .ToList();

// Average - середня кількість переглядів постів
var averageViews = _context.Posts
    .Where(p => p.IsPublished)
    .Average(p => p.ViewCount);
```

### LINQ запити з групуванням

Sample3 демонструє LINQ запити з групуванням:

```csharp
// Групування постів за авторами
var postsByAuthor = _context.Posts
    .GroupBy(p => p.Author)
    .Select(g => new
    {
        Author = g.Key.FirstName + " " + g.Key.LastName,
        Posts = g.ToList(),
        PostCount = g.Count()
    })
    .OrderByDescending(x => x.PostCount)
    .ToList();

// Групування постів за місяцем публікації
var postsByMonth = _context.Posts
    .Where(p => p.PublishedAt != null)
    .GroupBy(p => new { Month = p.PublishedAt!.Value.Month, Year = p.PublishedAt!.Value.Year })
    .Select(g => new
    {
        Month = g.Key.Month,
        Year = g.Key.Year,
        Posts = g.ToList(),
        PostCount = g.Count()
    })
    .OrderByDescending(x => x.Year)
    .ThenByDescending(x => x.Month)
    .ToList();
```

### LINQ запити з з'єднанням таблиць

Sample3 демонструє LINQ запити з з'єднанням таблиць:

```csharp
// З'єднання таблиць з Join
var postsWithAuthors = _context.Posts
    .Join(
        _context.Authors,
        post => post.AuthorId,
        author => author.Id,
        (post, author) => new
        {
            PostTitle = post.Title,
            AuthorName = author.FirstName + " " + author.LastName
        })
    .ToList();

// Складний запит з підзапитами
var authorStats = _context.Authors
    .Select(a => new
    {
        AuthorName = a.FirstName + " " + a.LastName,
        PostCount = a.Posts.Count,
        CommentCount = a.Posts.SelectMany(p => p.Comments).Count(),
        MostPopularPost = a.Posts.OrderByDescending(p => p.ViewCount).FirstOrDefault()
    })
    .ToList();
```

### Завантаження пов'язаних даних (Include)

Sample3 демонструє завантаження пов'язаних даних за допомогою методу Include:

```csharp
// Простий Include
var postsWithAuthors = _context.Posts
    .Include(p => p.Author)
    .Take(3)
    .ToList();

// Багаторівневий Include
var blogsWithPostsAndComments = _context.Blogs
    .Include(b => b.Posts)
        .ThenInclude(p => p.Comments)
    .Take(2)
    .ToList();
```

### Наповнення бази даних тестовими даними

Sample3 демонструє наповнення бази даних тестовими даними за допомогою класу BlogDataSeeder:

```csharp
public void SeedData()
{
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
}
```

### Використання Bogus для генерації фейкових даних

Sample3 демонструє використання бібліотеки Bogus для генерації фейкових даних:

```csharp
// Генерація фейкових авторів
private List<Author> GenerateAuthors(int count)
{
    var authorFaker = new Faker<Author>()
        .RuleFor(a => a.FirstName, f => f.Name.FirstName())
        .RuleFor(a => a.LastName, f => f.Name.LastName())
        .RuleFor(a => a.Email, (f, a) => f.Internet.Email(a.FirstName, a.LastName))
        .RuleFor(a => a.Bio, f => f.Lorem.Paragraphs(2))
        .RuleFor(a => a.AvatarUrl, f => f.Internet.Avatar())
        .RuleFor(a => a.RegisteredAt, f => f.Date.Past(2));

    return authorFaker.Generate(count);
}

// Генерація фейкових постів
private List<Post> GeneratePosts(int count, List<Author> authors, List<Blog> blogs, List<Tag> tags)
{
    var postFaker = new Faker<Post>()
        .RuleFor(p => p.Title, f => f.Lorem.Sentence(5, 8))
        .RuleFor(p => p.Slug, (f, p) => GenerateSlug(p.Title))
        .RuleFor(p => p.Summary, f => f.Lorem.Paragraph())
        .RuleFor(p => p.Content, f => string.Join("\n\n", f.Lorem.Paragraphs(5)))
        .RuleFor(p => p.ImageUrl, f => f.Image.PicsumUrl())
        .RuleFor(p => p.ViewCount, f => f.Random.Int(0, 1000))
        .RuleFor(p => p.IsPublished, f => f.Random.Bool(0.8f))
        .RuleFor(p => p.PublishedAt, f => f.Date.Past(1))
        .RuleFor(p => p.CreatedAt, f => f.Date.Past(1))
        .RuleFor(p => p.UpdatedAt, f => f.Date.Recent(30))
        .RuleFor(p => p.Author, f => f.PickRandom(authors))
        .RuleFor(p => p.Blog, f => f.PickRandom(blogs));

    return postFaker.Generate(count);
}
```

## Результати роботи програми

### Демонстрація зв'язків за допомогою угод (Conventions)

![Демонстрація зв'язків за допомогою угод](screenshots/conventions.png)

### Демонстрація зв'язків за допомогою анотацій (Annotations)

![Демонстрація зв'язків за допомогою анотацій](screenshots/annotations.png)
