using Pets.Application.Interfaces;
using Pets.Application.Entities;
using Pets.Persistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pets.Persistence.Repositories;
public class PetRepository : GenericRepository<Pet>, IPetRepository
{
    public PetRepository(PetsDbContext dbContext) : base(dbContext)
    {
        
    }
}
