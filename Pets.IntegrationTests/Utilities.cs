using Pets.Application.Entities;
using Pets.Persistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Pets.IntegrationTests;
public class Utilities
{
    public static void InitializeDbForTests(PetsDbContext context)
    {
        context.PetTypes.Add(new PetType
        { 
            Id = 1,
            Name = "PetType1"
        });
        context.PetTypes.Add(new PetType
        {
            Id = 2,
            Name = "PetType2"
        });

        context.SaveChanges();

        context.Pets.Add(new Pet
        {
            Name = "Pet1",
            Description = "Description1",
            MicroChipId = "MicroChipId1",
            OwnerName = "OwnerName1",
            OwnerEmail = "OwnerEmail1",
            CreatedDate = DateTime.Now.AddDays(-3),
            MissingSince = DateTime.Now.AddDays(-5),
            PetTypeId = 1
        });
        context.Pets.Add(new Pet
        {
            Name = "Pet2",
            Description = "Description2",
            MicroChipId = "MicroChipId2",
            OwnerName = "OwnerName2",
            OwnerEmail = "OwnerEmail2",
            CreatedDate = DateTime.Now.AddDays(-2),
            MissingSince = DateTime.Now.AddDays(-3),
            PetTypeId = 2
        });
        context.Pets.Add(new Pet
        {
            Name = "Pet3",
            Description = "Description3",
            MicroChipId = "MicroChipId3",
            OwnerName = "OwnerName3",
            OwnerEmail = "OwnerEmail3",
            CreatedDate = DateTime.Now.AddDays(-1),
            MissingSince = DateTime.Now.AddDays(-2),
            PetTypeId = 2
        });

        context.SaveChanges();

    }    
}
