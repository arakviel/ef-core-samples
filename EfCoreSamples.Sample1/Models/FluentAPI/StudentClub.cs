namespace EfCoreSamples.Sample1.Models.FluentAPI;

// Модель для явного визначення таблиці зв'язків для Many-to-Many
// Це дозволяє додати додаткові властивості до зв'язку
public class StudentClub
{
    public int StudentId { get; set; }
    public Student Student { get; set; } = null!;

    public int ClubId { get; set; }
    public Club Club { get; set; } = null!;

    // Додаткова властивість для зв'язку
    public DateTime JoinDate { get; set; }
}
