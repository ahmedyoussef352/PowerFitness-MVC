using ProjectGym.DataAccess.Entities;

namespace ProjectGym.DataAccess.Repositories;

public interface IUnitOfWork : IAsyncDisposable
{
    IMemberRepository Members { get; }
    ITrainerRepo Trainers { get; }
    IPlanRepository Plans { get; }
    IRepository<HealthRecord> HealthRecords { get; }
    IRepository<Category> Categories { get; }
    Task<int> CommitAsync(CancellationToken cancellationToken);

    Task BeginTransactionAsync(CancellationToken cancellationToken);
    Task CommitTransactionAsync(CancellationToken cancellationToken);
    Task RollbackTransactionAsync(CancellationToken cancellationToken);
}
