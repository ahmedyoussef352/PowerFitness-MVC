using Microsoft.Extensions.DependencyInjection;
using ProjectGym.BusinessLogic.Comman;
using ProjectGym.BusinessLogic.Mapping;
using ProjectGym.BusinessLogic.Servies;

namespace ProjectGym.BusinessLogic;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddGymBusinessLogic(this IServiceCollection services)
    {
        services.AddScoped<IMemberService, MemberService>();
        services.AddScoped<IPlanService, PlanService>();
        services.AddScoped<ITrainerService, TrainerService>();
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
        services.AddScoped<ISessionService, SessionService>();
        services.AddAutoMapper(conf => { },
            typeof(MemberProfile).Assembly);
        return services;
    }

}