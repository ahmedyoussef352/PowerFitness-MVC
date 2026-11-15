using ProjectGym.DataAccess.Data.Contexts;
using ProjectGym.DataAccess.Entities;

namespace ProjectGym.DataAccess.Data.Seeder;

public static class PlanSeeder
{
    public static async Task SeedAsync(GymDbContext dbContext)
    {
        if (dbContext.Plans.Any()) return;

        var plans = new List<Plan>
        {
            new()
            {
                Name = "Bronze",
                Description = "Basic gym access",
                DurationDays = 30,
                Price = 300,
                IsActive = true,
                CreatedAt = DateOnly.FromDateTime(DateTime.UtcNow)
            },
            new()
            {
                Name = "Silver",
                Description = "Gym access with cardio classes",
                DurationDays = 60,
                Price = 500,
                IsActive = true,
                CreatedAt = DateOnly.FromDateTime(DateTime.UtcNow)
            },
            new()
            {
                Name = "Gold",
                Description = "Full gym access with trainer",
                DurationDays = 90,
                Price = 800,
                IsActive = false,
                CreatedAt = DateOnly.FromDateTime(DateTime.UtcNow)
            }
        };

        await dbContext.Plans.AddRangeAsync(plans);
        await dbContext.SaveChangesAsync();

    }
}
