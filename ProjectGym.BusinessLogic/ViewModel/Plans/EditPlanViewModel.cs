using System.ComponentModel.DataAnnotations;

namespace ProjectGym.BusinessLogic.ViewModel.Plans;

public class EditPlanViewModel
{

    [Required(ErrorMessage = "Name is Required")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 100 characters")]
    public string PlanName { get; set; } = default!;

    [Required(ErrorMessage = "Description Is Required")]
    [StringLength(1000, MinimumLength = 5, ErrorMessage = "Description must be between 5 and 1000 characters")]

    public string Description { get; set; } = default!;

    [Required(ErrorMessage = "Price Is Required")]
    [Range(1, 100000, ErrorMessage = "Price must be greater than 0")]
    public decimal Price { get; set; }

    [Required(ErrorMessage = "Duration Days Is Required")]
    [Range(1, 9000, ErrorMessage = "Duration Days must be greater than 0")]
    public int DurationDays { get; set; }

}
