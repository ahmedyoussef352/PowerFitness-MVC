namespace ProjectGym.BusinessLogic.ViewModel.Trainers;

public class TrainerDetailsViewModel
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Phone { get; set; } = null!;
    public string DateOfBirth { get; set; } = null!;
    public string Address { get; set; } = null!;
    public string Specialties { get; set; } = null!;
}
