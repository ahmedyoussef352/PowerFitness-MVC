namespace ProjectGym.DataAccess.Entities;

public class Member : User
{
    public string? ImageUrl { get; set; }
    public DateOnly JoinDate { get; set; }

    public HealthRecord HealthRecord { get; set; } = null!;

    public ICollection<MemberShip> MemberShips { get; set; } = [];
    public ICollection<Booking> Bookings { get; set; } = [];
}
