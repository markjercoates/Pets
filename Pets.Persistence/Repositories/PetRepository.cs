using Pets.Application.Interfaces;
using Pets.Application.Entities;
using Pets.Persistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Pets.Persistence.Repositories;
public class PetRepository : GenericRepository<Pet>, IPetRepository
{
    public PetRepository(PetsDbContext dbContext) : base(dbContext)
    {

    }
    public override async Task<Pet?> GetByIdAsync(int id, CancellationToken token = default)
    {
        return await _dbContext.Pets.Include(p => p.PetType)
                .SingleOrDefaultAsync(p => p.Id == id, token);
    }
}
