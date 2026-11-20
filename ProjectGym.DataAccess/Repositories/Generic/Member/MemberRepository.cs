




using Microsoft.EntityFrameworkCore;
using ProjectGym.DataAccess.Data.Contexts;
using ProjectGym.DataAccess.Entities;
using ProjectGym.DataAccess.Repositories;

public class MemberRepository(GymDbContext gymDbContext) :
    Repository<Member>(gymDbContext), IMemberRepository
{
    private readonly GymDbContext _dbContext = gymDbContext;

    public Task<Member?> GetWithMemberShipAsyncById(int id, DateOnly today, CancellationToken cancellationToken = default)
    {
        return _dbContext.Set<Member>()
            .AsNoTracking()
            .Include(m => m.MemberShips
            .Where(ms => ms.StartDate <= today && ms.EndDate >= today))
            .ThenInclude(ms => ms.Plan)
            .FirstOrDefaultAsync(m => m.Id == id, cancellationToken);
    }
    public async Task<Member?> GetHealthRecordsyncById(int id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Set<Member>()
            .Include(m => m.HealthRecord)
            .FirstOrDefaultAsync(m => m.Id == id, cancellationToken);
    }

    public Task<bool> IsEmailTakenAsync(string email, int? excludeId = null, CancellationToken cancellationToken = default)
    => _dbContext.Set<Member>().AnyAsync(m => m.Email == email && (!excludeId.HasValue || m.Id != excludeId.Value), cancellationToken);

    public Task<bool> IsPhoneTakenAsync(string phoneNumber, int? excludeId = null, CancellationToken cancellationToken = default)
    => _dbContext.Set<Member>().AnyAsync(m => m.Phone == phoneNumber && (!excludeId.HasValue || m.Id != excludeId.Value), cancellationToken);

    public async Task<bool> HasUpCommingBookingAsync(int id,
        DateTime Today,
        CancellationToken cancellationToken = default)
    => await _dbContext.Set<Member>()
            .Where(m => m.Id == id)
            .SelectMany(m => m.Bookings)
            .AnyAsync(b => b.MemberId == id && b.Session.EndTime >= Today,
            cancellationToken);

}
