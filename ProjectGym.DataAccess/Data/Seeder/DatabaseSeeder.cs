using ProjectGym.DataAccess.Data.Contexts;

namespace ProjectGym.DataAccess.Data.Seeder;

public static class DatabaseSeeder
{
    public static async Task SeedAllAsync(GymDbContext dbContext)
    {
        await PlanSeeder.SeedAsync(dbContext);
        await CategorySeeder.SeedAsync(dbContext);
    }
}
