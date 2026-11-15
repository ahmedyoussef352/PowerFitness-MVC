using ProjectGym.DataAccess.Entities;

namespace ProjectGym.DataAccess.Repositories;

public interface ITrainerRepo : IRepository<Trainer>
{
    public Task<bool> IsEmailTakenAsync(string email, int? excludeId = null, CancellationToken cancellationToken = default);
    public Task<bool> IsPhoneTakenAsync(string phone, int? excludeId = null, CancellationToken cancellationToken = default);
    Task<bool> HasUpCommingSessionAsync(int id, CancellationToken cancellationToken = default);
}
