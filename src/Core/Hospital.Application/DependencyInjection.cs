using System.Reflection;
using Hospital.Application.Common.Concrete;
using Hospital.Application.Common.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Hospital.Application;
public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        return services;
    }
}
