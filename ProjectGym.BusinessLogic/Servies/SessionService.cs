using ProjectGym.BusinessLogic.Comman;
using ProjectGym.BusinessLogic.ViewModel.Session;
using ProjectGym.DataAccess.QueryService;

namespace ProjectGym.BusinessLogic.Servies
{
    public class SessionService(ISessionQueryService sessionQueries,
        IDateTimeProvider clock) : ISessionService
    {
        public async Task<IReadOnlyList<SessionIndexViewModel>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var sessions = await sessionQueries.GetIndexItemAsync
                (cancellationToken);

            var utcNow = clock.UtcNow;

            return [.. sessions.Select(
                s => {
                    SessionStatus status;

                    if(utcNow<s.StartTime)
                        status = SessionStatus.Upcoming;

                    else if(utcNow<=s.EndTime)
                        status = SessionStatus.Ongoing;

                    else
                        status = SessionStatus.Completed;

                    return new SessionIndexViewModel{
                    Id = s.Id,
                    Speciality = s.Speciality,
                    Description = s.Description,
                    TrainerName = s.TrainerName,
                    StartTime = s.StartTime,
                    EndTime = s.EndTime,
                    Capacity = s.Capacity,
                    BookedCount = s.BookedCount,
                    Status = status,
                    CategoryName = s.CategoryName


                    };
                })];
        }
    }
}

