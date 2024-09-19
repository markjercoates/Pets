using Pets.Application.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pets.Contracts.Requests;

namespace Pets.Application.Interfaces;
public interface IPetRepository : IGenericRepository<Pet>   
{
    Task<IReadOnlyList<Pet>> ListAllWithOptionsAsync(GetAllPetsRequestOptions options, CancellationToken token = default);

    Task<Pet?> GetByIdWithPetTypeAsync(int id, CancellationToken token = default);
}
