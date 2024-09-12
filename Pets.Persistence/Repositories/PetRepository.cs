using Pets.Application.Interfaces;
using Pets.Application.Entities;
using Pets.Persistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Pets.Application.Models;


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

    public async Task<IReadOnlyList<Pet>> ListAllWithOptionsAsync(GetAllPetsOptions options, CancellationToken token = default)
    {
        var pets = _dbContext.Pets.Include(p => p.PetType)
                            .OrderByDescending(o => o.CreatedDate)
                            .ThenByDescending(o => o.MissingSince).AsQueryable();

        if (options.PetTypeId != 0)
        {
            pets = pets.Where(p => p.PetTypeId == options.PetTypeId);
        }

        if (options.Name != null)
        {
            pets = pets.Where(p => p.Name.Contains(options.Name));
        }

        return await pets.ToListAsync(token);
       
    }
}