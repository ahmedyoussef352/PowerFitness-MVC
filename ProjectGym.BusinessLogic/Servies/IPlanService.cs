using Gymy.BusinessLogic.Common;
using ProjectGym.BusinessLogic.ViewModel.Plans;
namespace ProjectGym.BusinessLogic.Servies;

public interface IPlanService
{
    Task<Result<IReadOnlyList<PlanViewModel>>> GetAllAsync(CancellationToken cancellationToken = default);

    Task<Result<PlanViewModel>> GetByIdAsync(int id, CancellationToken cancellationToken = default);

    Task<Result<EditPlanViewModel>> GetForUpdateAsync(int id, CancellationToken cancellationToken = default);

    Task<Result> UpdateAsync(int id, EditPlanViewModel model, CancellationToken cancellationToken = default);

    Task<Result> ChangeStatusStatusAsync(int id, CancellationToken cancellationToken = default);
}