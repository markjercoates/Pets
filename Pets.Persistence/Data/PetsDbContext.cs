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
            .HasPrincipalKey(pt => pt.Id)
            .HasForeignKey(p => p.PetTypeId);

        builder.Entity<PetType>()
            .Property(p => p.Name)
            .HasMaxLength(50);

        builder.Entity<PetType>()
            .HasKey(pt => pt.Id);

        builder.Entity<PetType>()
           .Property(pt => pt.Id).ValueGeneratedNever();      
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedDate = DateTime.Now;
                    break;
                case EntityState.Modified:
                    entry.Entity.UpdatedDate = DateTime.Now;
                    break;
            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }
}
