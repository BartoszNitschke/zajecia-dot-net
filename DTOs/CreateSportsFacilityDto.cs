using System.ComponentModel.DataAnnotations;

namespace zajecia_dot_net.DTOs;

public class CreateSportsFacilityDto
{
    [Required(ErrorMessage = "Nazwa obiektu jest wymagana.")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Rodzaj sportu jest wymagany.")]
    public string SportType { get; set; } = string.Empty;

    [Required(ErrorMessage = "Adres obiektu jest wymagany.")]
    public string Address { get; set; } = string.Empty;

    [Range(typeof(decimal), "0.01", "1000000", ParseLimitsInInvariantCulture = true,
        ErrorMessage = "Stawka godzinowa musi być większa od zera.")]
    public decimal HourlyRate { get; set; }
}
