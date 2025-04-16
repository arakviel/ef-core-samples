using Microsoft.EntityFrameworkCore;

namespace EfCoreSamples.Sample1.Services;

// Клас для демонстрації роботи зі зв'язками
public class RelationshipDemonstrator
{
    private readonly DbContext _context;

    public RelationshipDemonstrator(DbContext context)
    {
        _context = context;
    }

    // Демонстрація зв'язку One-to-One
    public void DemonstrateOneToOne()
    {
        Console.WriteLine("\n--- Демонстрація зв'язку One-to-One ---");

        // Визначаємо тип контексту та викликаємо відповідний метод
        if (_context is Data.ConventionsDbContext)
        {
            DemonstrateConventionsOneToOne();
        }
        else if (_context is Data.AnnotationsDbContext)
        {
            DemonstrateAnnotationsOneToOne();
        }
        else if (_context is Data.FluentAPIDbContext)
        {
            DemonstrateFluentAPIOneToOne();
        }
    }

    // Демонстрація зв'язку One-to-Many
    public void DemonstrateOneToMany()
    {
        Console.WriteLine("\n--- Демонстрація зв'язку One-to-Many ---");

        // Визначаємо тип контексту та викликаємо відповідний метод
        if (_context is Data.ConventionsDbContext)
        {
            DemonstrateConventionsOneToMany();
        }
        else if (_context is Data.AnnotationsDbContext)
        {
            DemonstrateAnnotationsOneToMany();
        }
        else if (_context is Data.FluentAPIDbContext)
        {
            DemonstrateFluentAPIOneToMany();
        }
    }

    // Демонстрація зв'язку Many-to-Many
    public void DemonstrateMany()
    {
        Console.WriteLine("\n--- Демонстрація зв'язку Many-to-Many ---");

        // Визначаємо тип контексту та викликаємо відповідний метод
        if (_context is Data.ConventionsDbContext)
        {
            DemonstrateConventionsManyToMany();
        }
        else if (_context is Data.AnnotationsDbContext)
        {
            DemonstrateAnnotationsManyToMany();
        }
        else if (_context is Data.FluentAPIDbContext)
        {
            DemonstrateFluentAPIManyToMany();
        }
    }

    #region Conventions Demonstrations

    private void DemonstrateConventionsOneToOne()
    {
        var context = _context as Data.ConventionsDbContext;

        // Отримання студента з профілем
        var student = context.Students
            .Include(s => s.Profile)
            .FirstOrDefault(s => s.Name == "Іван Петренко");

        if (student != null && student.Profile != null)
        {
            Console.WriteLine($"Студент: {student.Name}");
            Console.WriteLine($"Профіль: {student.Profile.Address}, {student.Profile.PhoneNumber}");

            // Демонстрація навігації від профілю до студента
            var profile = context.StudentProfiles
                .Include(p => p.Student)
                .FirstOrDefault(p => p.StudentId == student.Id);

            if (profile != null)
            {
                Console.WriteLine("\nНавігація від профілю до студента:");
                Console.WriteLine($"Профіль належить студенту: {profile.Student.Name}");
            }
        }
    }

    private void DemonstrateConventionsOneToMany()
    {
        var context = _context as Data.ConventionsDbContext;

        // Отримання студента з курсами
        var student = context.Students
            .Include(s => s.Courses)
            .FirstOrDefault(s => s.Name == "Марія Коваленко");

        if (student != null && student.Courses.Any())
        {
            Console.WriteLine($"Студент: {student.Name}");
            Console.WriteLine("Курси студента:");

            foreach (var _course in student.Courses)
            {
                Console.WriteLine($"- {_course.Name}");
            }

            // Демонстрація навігації від курсу до студента
            var course = context.Courses
                .Include(c => c.Student)
                .FirstOrDefault(c => c.Name == "Історія");

            if (course != null)
            {
                Console.WriteLine("\nНавігація від курсу до студента:");
                Console.WriteLine($"Курс '{course.Name}' належить студенту: {course.Student.Name}");
            }
        }
    }

    private void DemonstrateConventionsManyToMany()
    {
        var context = _context as Data.ConventionsDbContext;

        // Отримання студента з гуртками
        var student = context.Students
            .Include(s => s.Clubs)
            .FirstOrDefault(s => s.Name == "Олександр Сидоренко");

        if (student != null && student.Clubs.Any())
        {
            Console.WriteLine($"Студент: {student.Name}");
            Console.WriteLine("Гуртки студента:");

            foreach (var _club in student.Clubs)
            {
                Console.WriteLine($"- {_club.Name}");
            }

            // Демонстрація навігації від гуртка до студентів
            var club = context.Clubs
                .Include(c => c.Students)
                .FirstOrDefault(c => c.Name == "Шаховий гурток");

            if (club != null && club.Students.Any())
            {
                Console.WriteLine("\nНавігація від гуртка до студентів:");
                Console.WriteLine($"Гурток '{club.Name}' відвідують студенти:");

                foreach (var clubStudent in club.Students)
                {
                    Console.WriteLine($"- {clubStudent.Name}");
                }
            }
        }
    }

    #endregion

    #region Annotations Demonstrations

    private void DemonstrateAnnotationsOneToOne()
    {
        var context = _context as Data.AnnotationsDbContext;

        // Отримання студента з профілем
        var student = context.Students
            .Include(s => s.Profile)
            .FirstOrDefault(s => s.Name == "Іван Петренко");

        if (student != null && student.Profile != null)
        {
            Console.WriteLine($"Студент: {student.Name}");
            Console.WriteLine($"Профіль: {student.Profile.Address}, {student.Profile.PhoneNumber}");

            // Демонстрація навігації від профілю до студента
            var profile = context.StudentProfiles
                .Include(p => p.Student)
                .FirstOrDefault(p => p.StudentId == student.StudentId);

            if (profile != null)
            {
                Console.WriteLine("\nНавігація від профілю до студента:");
                Console.WriteLine($"Профіль належить студенту: {profile.Student.Name}");
            }
        }
    }

    private void DemonstrateAnnotationsOneToMany()
    {
        var context = _context as Data.AnnotationsDbContext;

        // Отримання студента з курсами
        var student = context.Students
            .Include(s => s.Courses)
            .FirstOrDefault(s => s.Name == "Марія Коваленко");

        if (student != null && student.Courses.Any())
        {
            Console.WriteLine($"Студент: {student.Name}");
            Console.WriteLine("Курси студента:");

            foreach (var _course in student.Courses)
            {
                Console.WriteLine($"- {_course.Name}");
            }

            // Демонстрація навігації від курсу до студента
            var course = context.Courses
                .Include(c => c.Student)
                .FirstOrDefault(c => c.Name == "Історія");

            if (course != null)
            {
                Console.WriteLine("\nНавігація від курсу до студента:");
                Console.WriteLine($"Курс '{course.Name}' належить студенту: {course.Student.Name}");
            }
        }
    }

    private void DemonstrateAnnotationsManyToMany()
    {
        var context = _context as Data.AnnotationsDbContext;

        // Отримання студента з гуртками
        var student = context.Students
            .Include(s => s.Clubs)
            .FirstOrDefault(s => s.Name == "Олександр Сидоренко");

        if (student != null && student.Clubs.Any())
        {
            Console.WriteLine($"Студент: {student.Name}");
            Console.WriteLine("Гуртки студента:");

            foreach (var _club in student.Clubs)
            {
                Console.WriteLine($"- {_club.Name}");
            }

            // Демонстрація навігації від гуртка до студентів
            var club = context.Clubs
                .Include(c => c.Students)
                .FirstOrDefault(c => c.Name == "Шаховий гурток");

            if (club != null && club.Students.Any())
            {
                Console.WriteLine("\nНавігація від гуртка до студентів:");
                Console.WriteLine($"Гурток '{club.Name}' відвідують студенти:");

                foreach (var clubStudent in club.Students)
                {
                    Console.WriteLine($"- {clubStudent.Name}");
                }
            }
        }
    }

    #endregion

    #region FluentAPI Demonstrations

    private void DemonstrateFluentAPIOneToOne()
    {
        var context = _context as Data.FluentAPIDbContext;

        // Отримання студента з профілем
        var student = context.Students
            .Include(s => s.Profile)
            .FirstOrDefault(s => s.Name == "Іван Петренко");

        if (student != null && student.Profile != null)
        {
            Console.WriteLine($"Студент: {student.Name}");
            Console.WriteLine($"Профіль: {student.Profile.Address}, {student.Profile.PhoneNumber}");

            // Демонстрація навігації від профілю до студента
            var profile = context.StudentProfiles
                .Include(p => p.Student)
                .FirstOrDefault(p => p.StudentId == student.Id);

            if (profile != null)
            {
                Console.WriteLine("\nНавігація від профілю до студента:");
                Console.WriteLine($"Профіль належить студенту: {profile.Student.Name}");
            }
        }
    }

    private void DemonstrateFluentAPIOneToMany()
    {
        var context = _context as Data.FluentAPIDbContext;

        // Отримання студента з курсами
        var student = context.Students
            .Include(s => s.Courses)
            .FirstOrDefault(s => s.Name == "Марія Коваленко");

        if (student != null && student.Courses.Any())
        {
            Console.WriteLine($"Студент: {student.Name}");
            Console.WriteLine("Курси студента:");

            foreach (var _course in student.Courses)
            {
                Console.WriteLine($"- {_course.Name}");
            }

            // Демонстрація навігації від курсу до студента
            var course = context.Courses
                .Include(c => c.Student)
                .FirstOrDefault(c => c.Name == "Історія");

            if (course != null)
            {
                Console.WriteLine("\nНавігація від курсу до студента:");
                Console.WriteLine($"Курс '{course.Name}' належить студенту: {course.Student.Name}");
            }
        }
    }

    private void DemonstrateFluentAPIManyToMany()
    {
        var context = _context as Data.FluentAPIDbContext;

        // Отримання студента з гуртками через проміжну таблицю
        var student = context.Students
            .FirstOrDefault(s => s.Name == "Олександр Сидоренко");

        if (student != null)
        {
            Console.WriteLine($"Студент: {student.Name}");

            // Отримання гуртків студента з датою приєднання
            var studentClubs = context.StudentClubs
                .Include(sc => sc.Club)
                .Where(sc => sc.StudentId == student.Id)
                .ToList();

            if (studentClubs.Any())
            {
                Console.WriteLine("Гуртки студента (з датою приєднання):");

                foreach (var sc in studentClubs)
                {
                    Console.WriteLine($"- {sc.Club.Name} (Дата приєднання: {sc.JoinDate.ToShortDateString()})");
                }
            }

            // Демонстрація навігації від гуртка до студентів
            var club = context.Clubs
                .FirstOrDefault(c => c.Name == "Шаховий гурток");

            if (club != null)
            {
                var clubStudents = context.StudentClubs
                    .Include(sc => sc.Student)
                    .Where(sc => sc.ClubId == club.Id)
                    .ToList();

                if (clubStudents.Any())
                {
                    Console.WriteLine("\nНавігація від гуртка до студентів:");
                    Console.WriteLine($"Гурток '{club.Name}' відвідують студенти (з датою приєднання):");

                    foreach (var sc in clubStudents)
                    {
                        Console.WriteLine($"- {sc.Student.Name} (Дата приєднання: {sc.JoinDate.ToShortDateString()})");
                    }
                }
            }
        }
    }

    #endregion
}
