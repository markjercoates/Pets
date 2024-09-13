using Pets.Application.Entities;
using Pets.Application.Interfaces;
using Pets.Persistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pets.Persistence.Repositories;
public class PetTypeRepository : GenericRepository<PetType>, IPetTypeRepository
{
    public PetTypeRepository(PetsDbContext dbContext) : base(dbContext)
    {
        
    }
}
