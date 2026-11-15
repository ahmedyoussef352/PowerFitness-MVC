using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ProjectGym.DataAccess.Data.Contexts;
using ProjectGym.DataAccess.Data.Interceptors;
using ProjectGym.DataAccess.Repositories;

namespace ProjectGym.DataAccess;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddGymDataAccess(this IServiceCollection services, string connectionString)
    {
        services.AddSingleton<AuditColumnsInterceptor>();

        services.AddDbContext<GymDbContext>((IServiceProvider sp, DbContextOptionsBuilder options) =>
        {
            options.UseSqlServer(connectionString);
            options.AddInterceptors(sp.GetRequiredService<AuditColumnsInterceptor>());
        });

        services.AddScoped<IPlanRepository, PlanRepository>();
        services.AddScoped<IMemberRepository, MemberRepository>();
        services.AddScoped<ITrainerRepo, TrainerRepo>();
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

        return services;
    }

}