using Pets.Application.Entities;
using Pets.Application.Models;
using Pets.Applications.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pets.Application.Interfaces;
public interface IPetService
{
    public Task<Pet> CreateAsync(Pet pet, CancellationToken token = default);

    public Task<Pet> UpdateAsync(Pet pet, CancellationToken token = default);

    public Task<PagedList<Pet>> GetAllAsync(GetAllPetsOptions options, CancellationToken token = default);

    public Task<Pet?> GetByIdAsync(int id, CancellationToken token = default);

    public Task<bool> DeleteAsync(int id, CancellationToken token = default);


}
