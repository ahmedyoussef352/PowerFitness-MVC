namespace ProjectGym.DataAccess.Entities;

public class MemberShip : BaseEntity
{
    // Member m-m
    public Member Member { get; set; } = null!;
    public int MemberId { get; set; }
    // Plan m-m
    public Plan Plan { get; set; } = null!;
    public int PlanId { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }

}
