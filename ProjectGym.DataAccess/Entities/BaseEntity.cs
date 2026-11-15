namespace ProjectGym.DataAccess.Entities;

public class BaseEntity
{
    public int Id { get; set; }
    public DateOnly CreatedAt { get; set; }
    public DateOnly? UpdatedAt { get; set; }
    public DateOnly? Deleted { get; set; }
    public bool IsDeleted { get; set; }

}
