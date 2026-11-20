using ProjectGym.DataAccess.Entities;
using ProjectGym.DataAccess.Repositories;


public interface IMemberRepository : IRepository<Member>
{
    public Task<bool> IsEmailTakenAsync(string email, int? excludeId = null, CancellationToken cancellationToken = default);
    public Task<bool> IsPhoneTakenAsync(string phone, int? excludeId = null, CancellationToken cancellationToken = default);


    Task<Member?> GetWithMemberShipAsyncById(int id, DateOnly today, CancellationToken cancellationToken = default);
    Task<Member?> GetHealthRecordsyncById(int id, CancellationToken cancellationToken = default);
    Task<bool> HasUpCommingBookingAsync(int id, DateTime utcNow, CancellationToken cancellationToken = default);

}
