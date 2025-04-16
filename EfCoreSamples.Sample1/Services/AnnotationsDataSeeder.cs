using EfCoreSamples.Sample1.Data;
using EfCoreSamples.Sample1.Models.Annotations;

namespace EfCoreSamples.Sample1.Services;

// Клас для наповнення бази даних тестовими даними (підхід з анотаціями)
public class AnnotationsDataSeeder
{
    private readonly AnnotationsDbContext _context;

    public AnnotationsDataSeeder(AnnotationsDbContext context)
    {
        _context = context;
    }

    public void SeedData()
    {
        // Створення студентів
        var ivan = new Student { Name = "Іван Петренко" };
        var maria = new Student { Name = "Марія Коваленко" };
        var oleksandr = new Student { Name = "Олександр Сидоренко" };

        // Створення профілів студентів (One-to-One)
        var ivanProfile = new StudentProfile
        {
            Address = "вул. Шевченка, 10, м. Київ",
            PhoneNumber = "+380991234567",
            Student = ivan
        };

        var mariaProfile = new StudentProfile
        {
            Address = "вул. Франка, 15, м. Львів",
            PhoneNumber = "+380997654321",
            Student = maria
        };

        var oleksandrProfile = new StudentProfile
        {
            Address = "вул. Лесі Українки, 20, м. Одеса",
            PhoneNumber = "+380993456789",
            Student = oleksandr
        };

        // Встановлення зв'язків One-to-One
        ivan.Profile = ivanProfile;
        maria.Profile = mariaProfile;
        oleksandr.Profile = oleksandrProfile;

        // Створення курсів (One-to-Many)
        var mathCourse = new Course
        {
            Name = "Математика",
            Student = ivan
        };

        var physicsCourse = new Course
        {
            Name = "Фізика",
            Student = ivan
        };

        var historyCourse = new Course
        {
            Name = "Історія",
            Student = maria
        };

        var literatureCourse = new Course
        {
            Name = "Література",
            Student = maria
        };

        var programmingCourse = new Course
        {
            Name = "Програмування",
            Student = oleksandr
        };

        var databaseCourse = new Course
        {
            Name = "Бази даних",
            Student = oleksandr
        };

        // Додавання курсів до студентів
        ivan.Courses.Add(mathCourse);
        ivan.Courses.Add(physicsCourse);
        maria.Courses.Add(historyCourse);
        maria.Courses.Add(literatureCourse);
        oleksandr.Courses.Add(programmingCourse);
        oleksandr.Courses.Add(databaseCourse);

        // Створення гуртків (Many-to-Many)
        var chessClub = new Club { Name = "Шаховий гурток" };
        var footballClub = new Club { Name = "Футбольний гурток" };
        var literatureClub = new Club { Name = "Літературний гурток" };
        var theaterClub = new Club { Name = "Театральний гурток" };
        var computerClub = new Club { Name = "Комп'ютерний гурток" };
        var debateClub = new Club { Name = "Дебатний гурток" };

        // Встановлення зв'язків Many-to-Many
        ivan.Clubs.Add(chessClub);
        ivan.Clubs.Add(footballClub);
        ivan.Clubs.Add(literatureClub);

        maria.Clubs.Add(literatureClub);
        maria.Clubs.Add(theaterClub);

        oleksandr.Clubs.Add(computerClub);
        oleksandr.Clubs.Add(debateClub);
        oleksandr.Clubs.Add(chessClub);

        // Додавання студентів до гуртків
        chessClub.Students.Add(ivan);
        chessClub.Students.Add(oleksandr);

        footballClub.Students.Add(ivan);

        literatureClub.Students.Add(ivan);
        literatureClub.Students.Add(maria);

        theaterClub.Students.Add(maria);

        computerClub.Students.Add(oleksandr);

        debateClub.Students.Add(oleksandr);

        // Збереження даних
        _context.Students.AddRange(ivan, maria, oleksandr);
        _context.Clubs.AddRange(chessClub, footballClub, literatureClub, theaterClub, computerClub, debateClub);
        _context.SaveChanges();
    }
}
