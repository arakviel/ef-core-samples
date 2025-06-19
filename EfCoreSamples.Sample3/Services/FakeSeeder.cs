using Bogus;
using EfCoreSamples.Sample3.Data;
using EfCoreSamples.Sample3.Models;
using Microsoft.EntityFrameworkCore;

namespace EfCoreSamples.Sample3.Services;

public class FakeSeeder
{
    private readonly BlogDbContext _context;
    private readonly Faker _faker;

    public FakeSeeder(BlogDbContext context)
    {
        _context = context;
        _faker = new Faker("ru");
    }

    public async Task SeedAsync(int seedCount = 10)
    {
        // Ensure database is created
        await _context.Database.EnsureCreatedAsync();

        // Check if data already exists
        if (await _context.Authors.AnyAsync())
        {
            return;
        }

        // Seed Categories
        var categories = GenerateCategories(seedCount / 2);
        await _context.Categories.AddRangeAsync(categories);
        await _context.SaveChangesAsync();

        // Seed Authors
        var authors = GenerateAuthors(seedCount);
        await _context.Authors.AddRangeAsync(authors);
        await _context.SaveChangesAsync();

        // Seed Blogs
        var blogs = GenerateBlogs(seedCount, authors, categories);
        await _context.Blogs.AddRangeAsync(blogs);
        await _context.SaveChangesAsync();

        // Seed Tags
        var tags = GenerateTags(seedCount);
        await _context.Tags.AddRangeAsync(tags);
        await _context.SaveChangesAsync();

        // Seed Posts
        var posts = GeneratePosts(seedCount * 2, authors, blogs, tags);
        await _context.Posts.AddRangeAsync(posts);
        await _context.SaveChangesAsync();

        // Seed Comments
        var comments = GenerateComments(seedCount * 4, posts);
        await _context.Comments.AddRangeAsync(comments);
        await _context.SaveChangesAsync();

        // Seed Comment Replies
        var replies = GenerateCommentReplies(seedCount * 2, comments, posts);
        await _context.Comments.AddRangeAsync(replies);
        await _context.SaveChangesAsync();
    }

    private List<Category> GenerateCategories(int count)
    {
        var categoryFaker = new Faker<Category>()
            .RuleFor(c => c.Name, f => f.Commerce.Categories(1)[0])
            .RuleFor(c => c.Slug, (f, c) => GenerateSlug(c.Name))
            .RuleFor(c => c.Description, f => f.Lorem.Paragraph());

        return categoryFaker.Generate(count);
    }

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

    private List<Blog> GenerateBlogs(int count, List<Author> authors, List<Category> categories)
    {
        var blogFaker = new Faker<Blog>()
            .RuleFor(b => b.Title, f => f.Lorem.Sentence(3, 5))
            .RuleFor(b => b.Slug, (f, b) => GenerateSlug(b.Title))
            .RuleFor(b => b.Description, f => f.Lorem.Paragraph())
            .RuleFor(b => b.CreatedAt, f => f.Date.Past(1))
            .RuleFor(b => b.UpdatedAt, f => f.Date.Recent(30))
            .RuleFor(b => b.Author, f => f.PickRandom(authors))
            .RuleFor(b => b.Category, f => f.PickRandom(categories));

        return blogFaker.Generate(count);
    }

    private List<Tag> GenerateTags(int count)
    {
        var tagFaker = new Faker<Tag>()
            .RuleFor(t => t.Name, f => f.Lorem.Word())
            .RuleFor(t => t.Slug, (f, t) => GenerateSlug(t.Name));

        return tagFaker.Generate(count);
    }

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
            .RuleFor(p => p.Blog, f => f.PickRandom(blogs))
            .RuleFor(p => p.Tags, f => f.PickRandom(tags, f.Random.Int(1, 3)).ToList());

        return postFaker.Generate(count);
    }

    private List<Comment> GenerateComments(int count, List<Post> posts)
    {
        var commentFaker = new Faker<Comment>()
            .RuleFor(c => c.Content, f => f.Lorem.Paragraph())
            .RuleFor(c => c.AuthorName, f => f.Name.FullName())
            .RuleFor(c => c.AuthorEmail, f => f.Internet.Email())
            .RuleFor(c => c.CreatedAt, f => f.Date.Past(1))
            .RuleFor(c => c.IsApproved, f => f.Random.Bool(0.9f))
            .RuleFor(c => c.Post, f => f.PickRandom(posts));

        return commentFaker.Generate(count);
    }

    private List<Comment> GenerateCommentReplies(int count, List<Comment> comments, List<Post> posts)
    {
        var replyFaker = new Faker<Comment>()
            .RuleFor(c => c.Content, f => f.Lorem.Paragraph())
            .RuleFor(c => c.AuthorName, f => f.Name.FullName())
            .RuleFor(c => c.AuthorEmail, f => f.Internet.Email())
            .RuleFor(c => c.CreatedAt, f => f.Date.Past(1))
            .RuleFor(c => c.IsApproved, f => f.Random.Bool(0.9f))
            .RuleFor(c => c.Post, f => f.PickRandom(posts))
            .RuleFor(c => c.ParentComment, f => f.PickRandom(comments));

        return replyFaker.Generate(count);
    }

    private string GenerateSlug(string input)
    {
        if (string.IsNullOrEmpty(input))
            return _faker.Lorem.Slug();

        var slug = input.ToLower()
            .Replace(" ", "-")
            .Replace(".", "")
            .Replace(",", "")
            .Replace("!", "")
            .Replace("?", "")
            .Replace("&", "and");

        // Remove any character that is not a letter, number, or hyphen
        slug = string.Concat(slug.Where(c => char.IsLetterOrDigit(c) || c == '-'));

        // Ensure slug is not empty
        return string.IsNullOrEmpty(slug) ? _faker.Lorem.Slug() : slug;
    }
}