using System.ComponentModel.DataAnnotations;

namespace zajecia_dot_net.DTOs;

public class CreateReservationDto
{
    [Range(1, int.MaxValue, ErrorMessage = "Id obiektu musi być dodatnie.")]
    public int SportsFacilityId { get; set; }

    [Required(ErrorMessage = "Imię i nazwisko klienta są wymagane.")]
    public string CustomerName { get; set; } = string.Empty;

    public DateTimeOffset StartTime { get; set; }

    public DateTimeOffset EndTime { get; set; }
}
