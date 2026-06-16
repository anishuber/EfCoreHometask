namespace Data.Entities;

public class Manufacturer
{
    public Manufacturer() 
    { 
    }

    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Address { get; set; } = string.Empty;

    public bool IsAChildCompany { get; set; }

    public ICollection<Car> Cars { get; set; } = new List<Car>();
}
