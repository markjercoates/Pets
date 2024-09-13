using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Pets.Application.Interfaces;
using Pets.Application.Services;

namespace Pets.Application;

public static class ApplicationServiceRegistrations
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IPetService,PetService>();
        services.AddScoped<IPetTypeService, PetTypeService>();
        services.AddValidatorsFromAssemblyContaining<IApplicationMarker>(ServiceLifetime.Singleton);
        return services;
    }
}
