namespace zajecia_dot_net.Models;

public class Reservation
{
    public int Id { get; set; }

    public int SportsFacilityId { get; set; }

    public string CustomerName { get; set; } = string.Empty;

    public DateTimeOffset StartTime { get; set; }

    public DateTimeOffset EndTime { get; set; }
}
