using zajecia_dot_net.Models;

namespace zajecia_dot_net.Services;

public static class InMemoryDataStore
{
    public static object SyncRoot { get; } = new();

    public static List<SportsFacility> SportsFacilities { get; } =
    [
        new SportsFacility
        {
            Id = 1,
            Name = "Kort tenisowy 1",
            SportType = "Tenis",
            Address = "ul. Sportowa 1, Warszawa",
            HourlyRate = 80
        },
        new SportsFacility
        {
            Id = 2,
            Name = "Boisko wielofunkcyjne",
            SportType = "Piłka nożna",
            Address = "ul. Sportowa 5, Warszawa",
            HourlyRate = 120
        }
    ];

    public static List<Reservation> Reservations { get; } = [];
}
