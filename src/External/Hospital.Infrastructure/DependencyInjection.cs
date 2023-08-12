using Hospital.Application.Common.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Hospital.Infrastructure;
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddDbContext<ApplicationDbContext>();
        services.AddScoped<IApplicationDbContext, ApplicationDbContext>();
        return services;
    }
}
