using Gymy.BusinessLogic.Common;
using ProjectGym.BusinessLogic.ViewModel.Members;
using ProjectGym.BusinessLogic.ViewModel.Trainers;

namespace ProjectGym.BusinessLogic.Servies;

public interface ITrainerService
{
    Task<Result<IReadOnlyList<TrainerIndexViewModel>>> GetAllAsync(CancellationToken cancellationToken = default);

    Task<Result<TrainerDetailsViewModel>> GetDetailsByIdAsync(int id, CancellationToken cancellationToken = default);

    Task<Result> CreateAsync(CreateTrainerViewModel model, CancellationToken cancellationToken = default);

    Task<Result<EditTrainerViewModel>> GetForUpdateAsync(int id, CancellationToken cancellationToken = default);

    Task<Result> UpdateAsync(int id, EditTrainerViewModel model, CancellationToken cancellationToken = default);

    Task<Result> DeleteAsync(int id, CancellationToken cancellationToken = default);
}