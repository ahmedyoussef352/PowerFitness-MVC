using ProjectGym.DataAccess.QueryService.Dtos;

namespace ProjectGym.DataAccess.QueryService
{
    public interface ISessionQueryService
    {
        Task<IReadOnlyList<SessionIndexViewModelQueryService>> GetIndexItemAsync(CancellationToken cancellationToken);
    }
}
