using Pets.Application.Entities;
using Pets.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pets.Application.Interfaces;
public interface IPetRepository : IGenericRepository<Pet>   
{
    Task<IReadOnlyList<Pet>> ListAllWithOptionsAsync(GetAllPetsOptions options, CancellationToken token = default);
}
