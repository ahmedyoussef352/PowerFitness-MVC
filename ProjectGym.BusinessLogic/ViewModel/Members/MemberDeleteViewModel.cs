namespace ProjectGym.BusinessLogic.ViewModel.Members
{
    public class MemberDeleteViewModel
    {
        public int Id { get; set; }
        public string? PhotoUrl { get; set; }

        public string Name { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string PlanName { get; set; } = null!;
        public string MembershipStartDate { get; set; } = null!;
        public string MembershipEndDate { get; set; } = null!;
    }
}
