using ProjectGym.DataAccess.Entities.ValueObjects;
using ProjectGym.DataAccess.Enums;

namespace ProjectGym.DataAccess.Entities;

public abstract class User : BaseEntity
{
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public DateOnly DateOfBirth { get; set; }

    public string Phone { get; set; } = null!;
    public Gender Gender { get; set; }
    public Address Address { get; set; } = null!;
}
