namespace Infrastructure.DTOs;

public class CarDto
{
    public int VanId { get; set; }

    public int ManufacturerId { get; set; }

    public string Model { get; set; } = string.Empty;

    public string PlateNumber { get; set; } = string.Empty;

    public string CarType { get; set; } = string.Empty;
}
