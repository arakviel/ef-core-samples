using Microsoft.EntityFrameworkCore;

namespace EfCoreSamples.Sample2.Models;

/// <summary>
/// Клас Address є ValueObject, який демонструє використання складних типів даних в EF Core.
/// ValueObject - це об'єкт, який не має власної ідентичності, а визначається своїми атрибутами.
/// </summary>
[Owned] // Атрибут, який вказує EF Core, що цей клас є ValueObject
public class Address
{
    /// <summary>
    /// Назва вулиці
    /// </summary>
    public string Street { get; set; } = null!;

    /// <summary>
    /// Номер будинку
    /// </summary>
    public string HouseNumber { get; set; } = null!;

    /// <summary>
    /// Місто
    /// </summary>
    public string City { get; set; } = null!;

    /// <summary>
    /// Поштовий індекс
    /// </summary>
    public string PostalCode { get; set; } = null!;

    /// <summary>
    /// Країна
    /// </summary>
    public string Country { get; set; } = null!;

    /// <summary>
    /// Повна адреса у вигляді рядка
    /// </summary>
    public string FullAddress => $"{Street}, {HouseNumber}, {City}, {PostalCode}, {Country}";

    /// <summary>
    /// Конструктор за замовчуванням (необхідний для EF Core)
    /// </summary>
    public Address() { }

    /// <summary>
    /// Конструктор з параметрами для зручного створення об'єкта
    /// </summary>
    public Address(string street, string houseNumber, string city, string postalCode, string country)
    {
        Street = street;
        HouseNumber = houseNumber;
        City = city;
        PostalCode = postalCode;
        Country = country;
    }
}
