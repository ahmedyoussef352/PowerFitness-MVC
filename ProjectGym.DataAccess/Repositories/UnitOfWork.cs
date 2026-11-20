using Microsoft.EntityFrameworkCore.Storage;
using ProjectGym.DataAccess.Data.Contexts;
using ProjectGym.DataAccess.Entities;

namespace ProjectGym.DataAccess.Repositories
{

    public class UnitOfWork(GymDbContext dbContext) : IUnitOfWork
    {
        private readonly GymDbContext _dbContext = dbContext;
        private IDbContextTransaction? _dbTransaction;
        private IMemberRepository? _members;
        private ITrainerRepo? _trainers;
        private IPlanRepository? _plans;
        private IRepository<HealthRecord>? _healthRecords;
        private IRepository<Category>? _categories;


        public IMemberRepository Members => _members ??= new MemberRepository(_dbContext);
        public ITrainerRepo Trainers => _trainers ??= new TrainerRepo(_dbContext);
        public IPlanRepository Plans => _plans ??= new PlanRepository(_dbContext);
        public IRepository<HealthRecord> HealthRecords => _healthRecords ??= new Repository<HealthRecord>(_dbContext);
        public IRepository<Category> Categories => _categories ??= new Repository<Category>(_dbContext);



        public async Task<int> CommitAsync(CancellationToken cancellationToken = default)
        => await _dbContext.SaveChangesAsync(cancellationToken);

        public async Task BeginTransactionAsync(CancellationToken cancellationToken = default)
        {
            if (_dbTransaction != null)
                return;

            _dbTransaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken);
        }

        public async Task CommitTransactionAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                await _dbContext.SaveChangesAsync(cancellationToken);

                if (_dbTransaction != null)
                {
                    await _dbTransaction.CommitAsync(cancellationToken);
                }
            }
            catch
            {
                await RollbackTransactionAsync(cancellationToken);
                throw;
            }
            finally
            {
                if (_dbTransaction != null)
                {
                    await _dbTransaction.DisposeAsync();
                    _dbTransaction = null;
                }
            }
        }

        public async Task RollbackTransactionAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                if (_dbTransaction != null)
                {
                    await _dbTransaction.RollbackAsync(cancellationToken);
                }
            }
            finally
            {
                if (_dbTransaction != null)
                {
                    await _dbTransaction.DisposeAsync();
                    _dbTransaction = null;
                }
            }
        }

        public async ValueTask DisposeAsync()
        {
            if (_dbTransaction != null)
            {
                await _dbTransaction.DisposeAsync();
                _dbTransaction = null;
            }

            await _dbContext.DisposeAsync();
        }

    }
}
