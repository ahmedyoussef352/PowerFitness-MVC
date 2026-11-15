using ProjectGym.BusinessLogic.Comman;
using ProjectGym.BusinessLogic.ViewModel.HeathRecord;
using ProjectGym.BusinessLogic.ViewModel.Members;

namespace ProjectGym.BusinessLogic.Servies;

public interface IMemberService
{
    public Task<IEnumerable<MemberIndexViewModel>>
        GetAllAsync(CancellationToken cancellationToken = default);
    public Task<MemberDetailsViewModel?> GetByIdAsync(int id, CancellationToken cancellationToken = default);

    Task<Result> CreateAsync(CreateMemberViewModel model, CancellationToken cancellationToken = default);
    public Task<EditMemberViewModel?> GetForUpdateAsync(int id, CancellationToken cancellationToken = default);
    public Task<HealthRecordViewModel?> GetHealthRecordAsync(int id, CancellationToken cancellationToken = default);
    public Task<Result> UpdateAsync(int id, EditMemberViewModel model, CancellationToken cancellationToken = default);
    public Task<Result> DeleteAsync(int id, CancellationToken cancellationToken);
}
