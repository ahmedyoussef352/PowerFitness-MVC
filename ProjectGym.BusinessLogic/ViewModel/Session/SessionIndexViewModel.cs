namespace ProjectGym.BusinessLogic.ViewModel.Session;

public class SessionIndexViewModel
{
    public int Id { get; set; }
    public string Speciality { get; set; } = null!;
    public string CategoryName { get; set; } = null!;

    public string Description { get; set; } = null!;
    public string TrainerName { get; set; } = null!;
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public int Capacity { get; set; }
    public int BookedCount { get; set; }
    public SessionStatus? Status { get; set; }

    public TimeSpan Duration => EndTime - StartTime;
    public string HeaderClass => Status switch
    {
        SessionStatus.Upcoming => "bg-primary",
        SessionStatus.Ongoing => "bg-success",
        SessionStatus.Completed => "bg-secondary",
        _ => "bg-secondary"
    };
}
