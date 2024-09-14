using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pets.Application.Interfaces;
using Pets.Persistence.Data;
using Pets.Persistence.Repositories;

namespace Pets.Persistence;

public static class PersistenceServiceRegistrations
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<PetsDbContext>(options =>
            options.UseSqlite(configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IPetRepository, PetRepository>();
        services.AddScoped<IPetTypeRepository, PetTypeRepository>();

        return services;
    }
}

//public static class DbContextOptionsBuilderExtensions
//{
//    public static DbContextOptionsBuilder UseLoggedInUserService(
//        this DbContextOptionsBuilder optionsBuilder,
//        ILoggedInUserService loggedInUserService)
//    {
//        // Assuming you have a way to set the loggedInUserService in the DbContext
//        optionsBuilder.ReplaceService<ILoggedInUserService, LoggedInUserService>(loggedInUserService);
//        return optionsBuilder;
//    }
//}
