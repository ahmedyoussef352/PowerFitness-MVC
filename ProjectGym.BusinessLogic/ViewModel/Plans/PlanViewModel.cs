namespace ProjectGym.BusinessLogic.ViewModel.Plans;

public class PlanViewModel
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public decimal Price { get; set; }

    public bool IsActive { get; set; }
    public int DurationDays { get; set; }
}
