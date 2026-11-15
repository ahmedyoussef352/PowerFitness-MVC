using ProjectGym.BusinessLogic.ViewModel.HeathRecord;
using ProjectGym.DataAccess.Enums;
using System.ComponentModel.DataAnnotations;

namespace ProjectGym.BusinessLogic.ViewModel.Members;

public class CreateMemberViewModel
{
    [Required(ErrorMessage = "Name is required")]
    [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Name can only contain letters and spaces")]
    public string Name { get; set; } = null!;

    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email address")]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; } = null!;

    [Required(ErrorMessage = "Phone is required")]
    [Phone(ErrorMessage = "Invalid phone number")]
    [RegularExpression(@"^(010|011|012|015)\d{8}$", ErrorMessage = "Phone number must be 10 digits")]
    [DataType(DataType.PhoneNumber)]
    public string Phone { get; set; } = null!;

    [Required(ErrorMessage = "Date of Birth is required")]
    [DataType(DataType.Date)]
    public DateOnly DateOfBirth { get; set; }

    [Required(ErrorMessage = "Gender is required")]
    [EnumDataType(typeof(Gender), ErrorMessage = "Invalid Gender")]
    public Gender? Gender { get; set; } = null!;

    [Required(ErrorMessage = "Building Number is required")]
    [Range(1, 9000, ErrorMessage = "Building Number must be a positive integer")]
    public int BuildingNumber { get; set; }

    [Required(ErrorMessage = "City is required")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "City name is too long")]
    [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "City name can only contain letters and spaces")]
    public string City { get; set; } = null!;


    [Required(ErrorMessage = "Street is required")]
    [StringLength(150, MinimumLength = 2, ErrorMessage = "Street name is too long")]
    [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Street name can only contain letters and spaces")]
    public string Street { get; set; } = null!;

    public HealthRecordViewModel HealthRecord { get; set; } = null!;



}
