using Microsoft.AspNetCore.Identity;
using Pets.Application.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Pets.Persistence.Data;
public class Seed
{
    public static async Task SeedUsers( UserManager<AppUser> userManager,
         RoleManager<AppRole> roleManager)
    {
        if(!roleManager.Roles.Any())
        {
            var roles = new List<AppRole>
            {
                new AppRole {  Id = Guid.NewGuid().ToString(), Name = "Admin", NormalizedName = "ADMIN" },
                new AppRole {  Id = Guid.NewGuid().ToString(), Name = "User" , NormalizedName = "USER"},
            };

            foreach (var role in roles)
            {
                await roleManager.CreateAsync(role);
            }
        }         

        if (!userManager.Users.Any(x => x.UserName == "admin@test.com"))
        {
            var user = new AppUser
            {
                UserName = "admin@test.com",
                Email = "admin@test.com",
            };

            await userManager.CreateAsync(user, "Pa$$w0rd");
            await userManager.AddToRoleAsync(user, "Admin");
        }

        if (!userManager.Users.Any(x => x.UserName == "user@test.com"))
        {
            var user = new AppUser
            {
                UserName = "user@test.com",
                Email = "user@test.com",
            };

            await userManager.CreateAsync(user, "Pa$$w0rd");
            await userManager.AddToRoleAsync(user, "User");
        }
    }

    public static async Task SeedTestData(PetsDbContext context)
    {
        if (!context.PetTypes.Any())
        {
            var petTypesData = await File
                .ReadAllTextAsync("../Pets.Persistence/Data/SeedData/petTypes.json");

            var petTypes = JsonSerializer.Deserialize<List<PetType>>(petTypesData);

            if (petTypes == null) return;

            context.PetTypes.AddRange(petTypes);

            await context.SaveChangesAsync();
        }

        if (!context.Pets.Any())
        {
            var petsData = await File
                .ReadAllTextAsync("../Pets.Persistence/Data/SeedData/pets.json");

            var pets = JsonSerializer.Deserialize<List<Pet>>(petsData);

            if (pets == null) return;

            context.Pets.AddRange(pets);

            await context.SaveChangesAsync("eeb52ea8-5810-436e-91d3-7a086be918de");
        }
    }
}
