using ProjectGym.BusinessLogic.ViewModel.Session;

namespace ProjectGym.BusinessLogic.Servies
{
    public interface ISessionService
    {
        Task<IReadOnlyList<SessionIndexViewModel>> GetAllAsync(CancellationToken cancellationToken = default);

    }
}
