using Microsoft.EntityFrameworkCore;
using ProjectGym.DataAccess.Entities;

namespace ProjectGym.DataAccess.Data.Contexts;

public class GymDbContext : DbContext
{
    public GymDbContext(DbContextOptions options)
        : base(options) { }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(GymDbContext).Assembly);


    }
    public DbSet<Plan> Plans { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Session> Sessions { get; set; }
    public DbSet<MemberShip> MemberShip { get; set; }
    public DbSet<HealthRecord> HealthRecords { get; set; }
    public DbSet<Booking> Bookings { get; set; }
    public DbSet<Trainer> Trainers { get; set; }
    public DbSet<Member> Members { get; set; }

}
