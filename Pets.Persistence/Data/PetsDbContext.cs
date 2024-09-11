using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Pets.Application.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pets.Persistence.Data;
public class PetsDbContext : IdentityDbContext<AppUser>
{
    public PetsDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Pet> Pets { get; set; }

    public DbSet<PetType> PetTypes { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Pet>()
            .Property(p => p.Name)
            .HasMaxLength(50);

        builder.Entity<Pet>()
           .Property(p => p.Description)
           .HasMaxLength(100);

        builder.Entity<Pet>()
           .Property(p => p.MicroChipId)
           .HasMaxLength(20);

        builder.Entity<Pet>()
            .HasOne(p => p.PetType)
            .WithMany(pt => pt.Pets)
            .HasForeignKey(p => p.PetTypeId);

        builder.Entity<PetType>()
            .HasMany(pt => pt.Pets)
            .WithOne(p => p.PetType)
            .HasForeignKey(p => p.PetTypeId);

        builder.Entity<PetType>()
            .Property(p => p.Name)
            .HasMaxLength(50);

        builder.Entity<PetType>()
            .Property(pt => pt.Id).ValueGeneratedNever();

        //builder.Entity<IdentityRole>().HasData(
        //  new IdentityRole { Id = Guid.NewGuid().ToString(), Name = "Admin", NormalizedName = "ADMIN" },
        //  new IdentityRole { Id = Guid.NewGuid().ToString(), Name = "User", NormalizedName = "USER" }
        //  );

        //// 1 = Cat, 2 = Dog, 3 = Hamster, 4 = Bird, 5 = Rabbit, 6 = Fish, 7 = Lizard, 8 = Horse, 9 = Gerbil, 10 = Tortoise
        //builder.Entity<PetType>().HasData(
        //    new PetType { Id = 1, Name = "Cat" },
        //    new PetType { Id = 2, Name = "Dog" },
        //    new PetType { Id = 3, Name = "Hamster" },
        //    new PetType { Id = 4, Name = "Bird" },
        //    new PetType { Id = 5, Name = "Rabbit" },
        //    new PetType { Id = 6, Name = "Fish" },
        //    new PetType { Id = 7, Name = "Lizard" },
        //    new PetType { Id = 8, Name = "Horse" },
        //    new PetType { Id = 9, Name = "Gerbil" },
        //    new PetType { Id = 10, Name = "Tortoise" }
        //    );
    }



}
