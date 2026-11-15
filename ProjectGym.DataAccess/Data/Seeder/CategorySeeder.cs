using ProjectGym.DataAccess.Data.Contexts;
using ProjectGym.DataAccess.Entities;

namespace ProjectGym.DataAccess.Data.Seeder
{
    public static class CategorySeeder
    {
        public static async Task SeedAsync(GymDbContext dbContext)
        {
            if (!dbContext.Categories.Any())
            {
                var categories = new List<Category>
                {
                    new Category { Name = "Yoga" },
                    new Category { Name = "Cardio" },
                    new Category { Name = "Flexibility" },
                    new Category { Name = "Balance" },
                    new Category { Name = "Endurance" }
                };
                dbContext.Categories.AddRange(categories);
                await dbContext.SaveChangesAsync();
            }
        }

    }
}
