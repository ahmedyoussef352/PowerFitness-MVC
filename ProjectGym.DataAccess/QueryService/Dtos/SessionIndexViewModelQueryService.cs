namespace ProjectGym.DataAccess.QueryService.Dtos
{
    public class SessionIndexViewModelQueryService
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
        public string Status { get; set; } = null!;
    }
}
