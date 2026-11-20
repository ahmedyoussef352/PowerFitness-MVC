using Microsoft.EntityFrameworkCore;
using ProjectGym.DataAccess.Data.Contexts;
using ProjectGym.DataAccess.QueryService.Dtos;

namespace ProjectGym.DataAccess.QueryService
{
    public class SessionQueryService(GymDbContext dbContext) : ISessionQueryService
    {
        public async Task<IReadOnlyList<SessionIndexViewModelQueryService>> GetIndexItemAsync(CancellationToken cancellationToken)
        {
            return await dbContext.Sessions
                .OrderBy(s => s.StartTime)
                .Select(s => new SessionIndexViewModelQueryService
                {
                    Id = s.Id,
                    Speciality = s.Category.Name,
                    CategoryName = s.Category.Name,
                    Description = s.Description,
                    TrainerName = s.Trainer.Name,
                    StartTime = s.StartTime,
                    EndTime = s.EndTime,
                    Capacity = s.Capacity,
                    BookedCount = s.Bookings.Count(b => !b.IsDeleted),
                }).ToListAsync();
        }
    }
}
