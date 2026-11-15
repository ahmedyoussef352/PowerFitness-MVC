using Microsoft.EntityFrameworkCore;
using ProjectGym.DataAccess.Data.Contexts;
using ProjectGym.DataAccess.Entities;
using System.Linq.Expressions;

namespace ProjectGym.DataAccess.Repositories;

public class Repository<TEntity>(GymDbContext dbContext) : IRepository<TEntity> where TEntity : BaseEntity
{
    private readonly GymDbContext _dbContext = dbContext;
    private readonly DbSet<TEntity> _dbSet = dbContext.Set<TEntity>();
    public async Task<IReadOnlyList<TEntity>> GetAllAsync(CancellationToken cancellationToken = default)
    => await _dbSet.AsNoTracking().ToListAsync();

    public async Task<TEntity?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    => await _dbSet.FirstOrDefaultAsync(e => e.Id == id, cancellationToken);

    public async Task<TEntity?> GetByIdIncludingDeletedAsync(int id, CancellationToken cancellationToken = default)
    => await _dbSet.IgnoreQueryFilters().FirstOrDefaultAsync(e => e.Id == id, cancellationToken);

    public async Task<bool> ExistAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
    => await _dbSet.AnyAsync(predicate, cancellationToken);

    public async Task<IReadOnlyList<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
    => await _dbSet.AsNoTracking().Where(predicate).ToListAsync();

    public Task SoftDeleteAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        entity.IsDeleted = true;
        _dbSet.Update(entity);
        return Task.CompletedTask;
    }

    public void Update(TEntity entity, CancellationToken cancellationToken = default)
    => _dbSet.Update(entity);
    public async Task AddAsync(TEntity entity, CancellationToken cancellationToken = default)
    => await _dbSet.AddAsync(entity, cancellationToken).AsTask();

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    => await _dbContext.SaveChangesAsync(cancellationToken);
    private static IQueryable<TEntity> ApplyIncludes(IQueryable<TEntity> query,
    params Expression<Func<TEntity, object>>[] includes)
    {
        foreach (var include in includes)
        {
            query = query.Include(include);
        }
        return query;
    }

    public async Task<TEntity?> GetDetailsByIdAsync
        (int id,
        bool trackChanges = true,
        CancellationToken cancellationToken = default,
        params Expression<Func<TEntity, object>>[] includes)
    {
        var query = ApplyIncludes(_dbSet.AsQueryable(), includes);
        if (!trackChanges)
        {
            query = query.AsNoTracking();
        }
        return await query
          .FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
    }
}
