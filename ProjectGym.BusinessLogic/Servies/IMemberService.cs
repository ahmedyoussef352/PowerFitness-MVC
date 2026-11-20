using Gymy.BusinessLogic.Common;
using ProjectGym.BusinessLogic.ViewModel.HeathRecord;
using ProjectGym.BusinessLogic.ViewModel.Members;

namespace ProjectGym.BusinessLogic.Servies;

public interface IMemberService
{
    public Task<Result<IReadOnlyList<MemberIndexViewModel>>>
        GetAllAsync(CancellationToken cancellationToken = default);
    public Task<Result<MemberDetailsViewModel>> GetByIdAsync(int id, CancellationToken cancellationToken = default);

    Task<Result> CreateAsync(CreateMemberViewModel model, CancellationToken cancellationToken = default);
    public Task<Result<EditMemberViewModel>> GetForUpdateAsync(int id, CancellationToken cancellationToken = default);
    public Task<Result<HealthRecordViewModel>> GetHealthRecordAsync(int id, CancellationToken cancellationToken = default);
    public Task<Result> UpdateAsync(int id, EditMemberViewModel model, CancellationToken cancellationToken = default);
    public Task<Result> DeleteAsync(int id, CancellationToken cancellationToken);
    Task<Result<MemberDeleteViewModel>> GetForDeleteAsync(int id, CancellationToken cancellationToken = default);
}
