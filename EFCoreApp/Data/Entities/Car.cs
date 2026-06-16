using Data.Entities.Enums;

namespace Data.Entities;

public class Car
{
    public Car()
    {
    }

    public int VanId { get; set; }

    public int ManufacturerId { get; set; }

    public string Model { get; set; } = string.Empty;

    public string PlateNumber { get; set; } = string.Empty;

    public CarType CarType { get; set; }

    public Manufacturer Manufacturer { get; set; } = null!;

    public ICollection<Service> Services { get; set; } = new List<Service>();
}
