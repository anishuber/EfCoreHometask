namespace Data.Entities;

public class Service
{
    public Service()
    {
    }

    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Address { get; set; } = string.Empty;

    public bool IsServiceWorking { get; set; }

    public ICollection<Car> Cars { get; set; } = new List<Car>();
}
