using Microsoft.EntityFrameworkCore;
using ProjectGym.Models;

namespace ProjectGym.Data.Contexts;

public class GymDbContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS; Database=GymTestProject; Trusted_Connection=True; TrustServerCertificate=True");
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(GymDbContext).Assembly);
    }
    public DbSet<Plan> Plans { get; set; }
}
