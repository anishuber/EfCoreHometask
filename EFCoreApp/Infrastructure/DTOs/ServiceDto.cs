namespace Infrastructure.DTOs;

public class ServiceDto
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Address { get; set; } = string.Empty;

    public bool IsServiceWorking { get; set; }
}
