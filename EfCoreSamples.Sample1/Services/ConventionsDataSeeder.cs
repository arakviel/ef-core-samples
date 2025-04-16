using EfCoreSamples.Sample1.Data;
using EfCoreSamples.Sample1.Models.Conventions;

namespace EfCoreSamples.Sample1.Services;

// Клас для наповнення бази даних тестовими даними (підхід з угодами)
public class ConventionsDataSeeder
{
    private readonly ConventionsDbContext _context;

    public ConventionsDataSeeder(ConventionsDbContext context)
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
            PhoneNumber = "+380991234567"
        };

        var mariaProfile = new StudentProfile
        {
            Address = "вул. Франка, 15, м. Львів",
            PhoneNumber = "+380997654321"
        };

        var oleksandrProfile = new StudentProfile
        {
            Address = "вул. Лесі Українки, 20, м. Одеса",
            PhoneNumber = "+380993456789"
        };

        // Створення курсів (One-to-Many)
        var mathCourse = new Course { Name = "Математика" };
        var physicsCourse = new Course { Name = "Фізика" };
        var historyCourse = new Course { Name = "Історія" };
        var literatureCourse = new Course { Name = "Література" };
        var programmingCourse = new Course { Name = "Програмування" };
        var databaseCourse = new Course { Name = "Бази даних" };

        // Створення гуртків (Many-to-Many)
        var chessClub = new Club { Name = "Шаховий гурток" };
        var footballClub = new Club { Name = "Футбольний гурток" };
        var literatureClub = new Club { Name = "Літературний гурток" };
        var theaterClub = new Club { Name = "Театральний гурток" };
        var computerClub = new Club { Name = "Комп'ютерний гурток" };
        var debateClub = new Club { Name = "Дебатний гурток" };

        // Встановлення зв'язків One-to-One
        ivan.Profile = ivanProfile;
        maria.Profile = mariaProfile;
        oleksandr.Profile = oleksandrProfile;

        // Встановлення зв'язків One-to-Many
        mathCourse.Student = ivan;
        physicsCourse.Student = ivan;
        historyCourse.Student = maria;
        literatureCourse.Student = maria;
        programmingCourse.Student = oleksandr;
        databaseCourse.Student = oleksandr;

        // Додавання курсів до студентів
        ivan.Courses.Add(mathCourse);
        ivan.Courses.Add(physicsCourse);
        maria.Courses.Add(historyCourse);
        maria.Courses.Add(literatureCourse);
        oleksandr.Courses.Add(programmingCourse);
        oleksandr.Courses.Add(databaseCourse);

        // Встановлення зв'язків Many-to-Many
        ivan.Clubs.Add(chessClub);
        ivan.Clubs.Add(footballClub);
        maria.Clubs.Add(literatureClub);
        maria.Clubs.Add(theaterClub);
        oleksandr.Clubs.Add(computerClub);
        oleksandr.Clubs.Add(debateClub);

        // Додавання студентів до гуртків
        chessClub.Students.Add(ivan);
        footballClub.Students.Add(ivan);
        literatureClub.Students.Add(maria);
        theaterClub.Students.Add(maria);
        computerClub.Students.Add(oleksandr);
        debateClub.Students.Add(oleksandr);

        // Додаткові зв'язки для демонстрації багато-до-багатьох
        chessClub.Students.Add(oleksandr);
        oleksandr.Clubs.Add(chessClub);
        literatureClub.Students.Add(ivan);
        ivan.Clubs.Add(literatureClub);

        // Збереження даних
        _context.Students.AddRange(ivan, maria, oleksandr);
        _context.Clubs.AddRange(chessClub, footballClub, literatureClub, theaterClub, computerClub, debateClub);
        _context.SaveChanges();
    }
}
