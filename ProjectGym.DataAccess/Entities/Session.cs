namespace ProjectGym.DataAccess.Entities;

public class Session : BaseEntity
{
    public string Description { get; set; } = null!;
    public int Capacity { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    // Trainer m-1
    public int TrainerId { get; set; }
    public Trainer Trainer { get; set; } = null!;
    // Category m-1
    public int CategoryId { get; set; }

    public Category Category { get; set; } = null!;

    public ICollection<Booking> Bookings { get; set; } = [];
}
