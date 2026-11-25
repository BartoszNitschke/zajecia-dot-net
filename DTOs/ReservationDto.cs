namespace zajecia_dot_net.DTOs;

public class ReservationDto
{
    public int Id { get; set; }

    public int SportsFacilityId { get; set; }

    public string SportsFacilityName { get; set; } = string.Empty;

    public string CustomerName { get; set; } = string.Empty;

    public DateTimeOffset StartTime { get; set; }

    public DateTimeOffset EndTime { get; set; }
}
