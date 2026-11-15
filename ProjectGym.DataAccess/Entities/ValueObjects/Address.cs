namespace ProjectGym.DataAccess.Entities.ValueObjects;

public class Address
{
    public string Street { get; set; } = null!;
    public string City { get; set; } = null!;
    public int BuildingNumber { get; set; }
}
