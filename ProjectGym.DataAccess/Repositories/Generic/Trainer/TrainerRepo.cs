using Microsoft.EntityFrameworkCore;
using ProjectGym.DataAccess.Data.Contexts;
using ProjectGym.DataAccess.Entities;

namespace ProjectGym.DataAccess.Repositories;

public class TrainerRepo(GymDbContext gymDbContext) :
Repository<Trainer>(gymDbContext), ITrainerRepo
{
    private readonly GymDbContext _dbContext = gymDbContext;
    public Task<bool> IsEmailTakenAsync(string email, int? excludeId = null, CancellationToken cancellationToken = default)
        => _dbContext.Set<Trainer>().AnyAsync(m => m.Email == email && (!excludeId.HasValue || m.Id != excludeId.Value), cancellationToken);

    public Task<bool> IsPhoneTakenAsync(string phoneNumber, int? excludeId = null, CancellationToken cancellationToken = default)
        => _dbContext.Set<Trainer>().AnyAsync(m => m.Phone == phoneNumber && (!excludeId.HasValue || m.Id != excludeId.Value), cancellationToken);

    public async Task<bool> HasUpCommingSessionAsync(int id, CancellationToken cancellationToken = default)
    {
        var now = DateTime.UtcNow;
        return await _dbContext.Set<Trainer>()
            .Where(t => t.Id == id)
            .SelectMany(t => t.Sessions)
            .AnyAsync(s => s.TrainerId == id && s.EndTime >= now,
            cancellationToken);
    }
}
