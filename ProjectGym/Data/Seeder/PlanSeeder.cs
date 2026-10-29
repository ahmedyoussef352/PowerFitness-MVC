using ProjectGym.Data.Contexts;
using ProjectGym.Models;

namespace ProjectGym.Data.Seeder;

public static class PlanSeeder
{
    public static async Task SeedAsync()
    {
        using var dbContext = new GymDbContext();

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
                CreatedAt = DateTime.UtcNow
            },
            new()
            {
                Name = "Silver",
                Description = "Gym access with cardio classes",
                DurationDays = 60,
                Price = 500,
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            },
            new()
            {
                Name = "Gold",
                Description = "Full gym access with trainer",
                DurationDays = 90,
                Price = 800,
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            }
        };

        await dbContext.Plans.AddRangeAsync(plans);
        await dbContext.SaveChangesAsync();

    }
}
