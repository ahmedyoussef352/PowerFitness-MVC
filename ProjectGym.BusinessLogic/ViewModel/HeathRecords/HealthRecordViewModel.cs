using ProjectGym.DataAccess.Enums;
using System.ComponentModel.DataAnnotations;

namespace ProjectGym.BusinessLogic.ViewModel.HeathRecord;

public class HealthRecordViewModel
{
    [Range(.1, 300, ErrorMessage = "Height must be between 1 and 300")]
    public decimal Height { get; set; }


    [Range(.1, 500, ErrorMessage = "Weight must be between 1 and 500")]
    public decimal Weight { get; set; }


    [Required(ErrorMessage = "Blood Type is required")]
    [EnumDataType(typeof(BloodType), ErrorMessage = "Invalid Blood Type")]
    public BloodType? BloodType { get; set; } = null!;

    public string? Notes { get; set; }
}
