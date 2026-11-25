namespace zajecia_dot_net.Models;

public class SportsFacility
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string SportType { get; set; } = string.Empty;

    public string Address { get; set; } = string.Empty;

    public decimal HourlyRate { get; set; }
}
