using ProjectGym.BusinessLogic.Comman;
using ProjectGym.BusinessLogic.ViewModel.Plans;

namespace ProjectGym.BusinessLogic.Servies;

public interface IPlanService
{
    public Task<IEnumerable<PlanViewModel>>
        GetAllAsync(CancellationToken cancellationToken = default);

    public Task<PlanViewModel> GetByIdAsync
        (int id, CancellationToken cancellationToken = default);

    public Task<EditPlanViewModel?> GetForUpdateAsync(int id, CancellationToken cancellationToken);
    public Task<Result> UpdateAsync(int id, EditPlanViewModel model, CancellationToken cancellationToken);
    public Task<Result> ChangeStatusStatusAsync(int id, CancellationToken cancellationToken);
}
