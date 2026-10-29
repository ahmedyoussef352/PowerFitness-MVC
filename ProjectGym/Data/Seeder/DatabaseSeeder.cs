namespace ProjectGym.Data.Seeder;

public static class DatabaseSeeder
{
    public static async Task SeedAllAsync()
    {
        await PlanSeeder.SeedAsync();
    }
}
