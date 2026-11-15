using ProjectGym.DataAccess.Entities;
using System.Linq.Expressions;

namespace ProjectGym.DataAccess.Repositories;

public interface IRepository<TEntity> where TEntity
    : BaseEntity
{
    Task<IReadOnlyList<TEntity>> GetAllAsync(CancellationToken cancellationToken = default);

    Task<TEntity?> GetByIdAsync(int id, CancellationToken cancellationToken = default);

    Task<TEntity?> GetDetailsByIdAsync(int id, bool trackChanges = true,
        CancellationToken cancellationToken = default,
        params Expression<Func<TEntity, object>>[] includes);
    Task<TEntity?> GetByIdIncludingDeletedAsync(int id, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);

    Task<bool> ExistAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);


    Task AddAsync(TEntity entity, CancellationToken cancellationToken = default);
    void Update(TEntity entity, CancellationToken cancellationToken = default);
    Task SoftDeleteAsync(TEntity entity, CancellationToken cancellationToken = default);

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
