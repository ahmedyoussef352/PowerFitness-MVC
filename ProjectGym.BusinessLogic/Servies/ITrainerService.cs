using ProjectGym.BusinessLogic.Comman;
using ProjectGym.BusinessLogic.ViewModel.Members;
using ProjectGym.BusinessLogic.ViewModel.Trainers;

namespace ProjectGym.BusinessLogic.Servies;

public interface ITrainerService
{
    public Task<IEnumerable<TrainerIndexViewModel>>
    GetAllAsync(CancellationToken cancellationToken = default);
    Task<Result> CreateAsync(CreateTrainerViewModel model, CancellationToken cancellationToken = default);
    Task<TrainerDetailsViewModel?> GetDetailsById(int id, CancellationToken cancellationToken = default);
    Task<EditTrainerViewModel?> GetForUpdateAsync(int id, CancellationToken cancellationToken = default);
    Task<Result> UpdateAsync(int id, EditTrainerViewModel model, CancellationToken cancellationToken = default);

    public Task<Result> DeleteAsync(int id, CancellationToken cancellationToken);
}
