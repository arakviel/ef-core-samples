using EfCoreSamples.Sample1.Data;
using EfCoreSamples.Sample1.Models.FluentAPI;

namespace EfCoreSamples.Sample1.Services;

// Клас для наповнення бази даних тестовими даними (підхід з Fluent API)
public class FluentAPIDataSeeder
{
    private readonly FluentAPIDbContext _context;

    public FluentAPIDataSeeder(FluentAPIDbContext context)
    {
        _context = context;
    }

    public void SeedData()
    {
        // Створення студентів
        var ivan = new Student { Name = "Іван Петренко" };
        var maria = new Student { Name = "Марія Коваленко" };
        var oleksandr = new Student { Name = "Олександр Сидоренко" };

        // Збереження студентів для отримання ID
        _context.Students.AddRange(ivan, maria, oleksandr);
        _context.SaveChanges();

        // Створення профілів студентів (One-to-One)
        var ivanProfile = new StudentProfile
        {
            Address = "вул. Шевченка, 10, м. Київ",
            PhoneNumber = "+380991234567",
            StudentId = ivan.Id,
            Student = ivan
        };

        var mariaProfile = new StudentProfile
        {
            Address = "вул. Франка, 15, м. Львів",
            PhoneNumber = "+380997654321",
            StudentId = maria.Id,
            Student = maria
        };

        var oleksandrProfile = new StudentProfile
        {
            Address = "вул. Лесі Українки, 20, м. Одеса",
            PhoneNumber = "+380993456789",
            StudentId = oleksandr.Id,
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
            StudentId = ivan.Id,
            Student = ivan
        };

        var physicsCourse = new Course
        {
            Name = "Фізика",
            StudentId = ivan.Id,
            Student = ivan
        };

        var historyCourse = new Course
        {
            Name = "Історія",
            StudentId = maria.Id,
            Student = maria
        };

        var literatureCourse = new Course
        {
            Name = "Література",
            StudentId = maria.Id,
            Student = maria
        };

        var programmingCourse = new Course
        {
            Name = "Програмування",
            StudentId = oleksandr.Id,
            Student = oleksandr
        };

        var databaseCourse = new Course
        {
            Name = "Бази даних",
            StudentId = oleksandr.Id,
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

        // Збереження гуртків для отримання ID
        _context.Clubs.AddRange(chessClub, footballClub, literatureClub, theaterClub, computerClub, debateClub);
        _context.SaveChanges();

        // Створення зв'язків Many-to-Many з додатковими властивостями
        var ivanChessClub = new StudentClub
        {
            StudentId = ivan.Id,
            ClubId = chessClub.Id,
            Student = ivan,
            Club = chessClub,
            JoinDate = DateTime.Now.AddMonths(-6)
        };

        var ivanFootballClub = new StudentClub
        {
            StudentId = ivan.Id,
            ClubId = footballClub.Id,
            Student = ivan,
            Club = footballClub,
            JoinDate = DateTime.Now.AddMonths(-5)
        };

        var ivanLiteratureClub = new StudentClub
        {
            StudentId = ivan.Id,
            ClubId = literatureClub.Id,
            Student = ivan,
            Club = literatureClub,
            JoinDate = DateTime.Now.AddMonths(-4)
        };

        var mariaLiteratureClub = new StudentClub
        {
            StudentId = maria.Id,
            ClubId = literatureClub.Id,
            Student = maria,
            Club = literatureClub,
            JoinDate = DateTime.Now.AddMonths(-3)
        };

        var mariaTheaterClub = new StudentClub
        {
            StudentId = maria.Id,
            ClubId = theaterClub.Id,
            Student = maria,
            Club = theaterClub,
            JoinDate = DateTime.Now.AddMonths(-2)
        };

        var oleksandrComputerClub = new StudentClub
        {
            StudentId = oleksandr.Id,
            ClubId = computerClub.Id,
            Student = oleksandr,
            Club = computerClub,
            JoinDate = DateTime.Now.AddMonths(-1)
        };

        var oleksandrDebateClub = new StudentClub
        {
            StudentId = oleksandr.Id,
            ClubId = debateClub.Id,
            Student = oleksandr,
            Club = debateClub,
            JoinDate = DateTime.Now.AddDays(-15)
        };

        var oleksandrChessClub = new StudentClub
        {
            StudentId = oleksandr.Id,
            ClubId = chessClub.Id,
            Student = oleksandr,
            Club = chessClub,
            JoinDate = DateTime.Now.AddDays(-7)
        };

        // Збереження зв'язків Many-to-Many
        _context.StudentClubs.AddRange(
            ivanChessClub, ivanFootballClub, ivanLiteratureClub,
            mariaLiteratureClub, mariaTheaterClub,
            oleksandrComputerClub, oleksandrDebateClub, oleksandrChessClub
        );

        // Збереження профілів та курсів
        _context.StudentProfiles.AddRange(ivanProfile, mariaProfile, oleksandrProfile);
        _context.Courses.AddRange(mathCourse, physicsCourse, historyCourse, literatureCourse, programmingCourse, databaseCourse);

        _context.SaveChanges();
    }
}
