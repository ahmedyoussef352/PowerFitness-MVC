using Microsoft.EntityFrameworkCore;
using ProjectGym.BusinessLogic;
using ProjectGym.DataAccess;
using ProjectGym.DataAccess.Data.Contexts;
using ProjectGym.DataAccess.Data.Seeder;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Add DbContext

builder.Services.AddControllersWithViews();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddGymDataAccess(connectionString);

builder.Services.AddGymBusinessLogic();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseRouting();

//app.UseAuthorization();

app.MapStaticAssets();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

if (app.Environment.IsDevelopment())
{
    using var scope = app.Services.CreateAsyncScope();

    var dbContext = scope.ServiceProvider.GetRequiredService<GymDbContext>();

    await dbContext.Database.MigrateAsync();

    await DatabaseSeeder.SeedAllAsync(dbContext);
}

app.Run();
