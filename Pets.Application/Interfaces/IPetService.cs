using Pets.Contracts.Requests;
using Pets.Contracts.Responses;
using Pets.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pets.Application.Interfaces;
public interface IPetService
{
    public Task<PetResponse> CreateAsync(CreatePetRequest pet, CancellationToken token = default);

    public Task<PetResponse> UpdateAsync(int id, UpdatePetRequest pet, CancellationToken token = default);

    public Task<PagedList<PetResponse>> GetAllAsync(GetAllPetsRequestOptions options, CancellationToken token = default);

    public Task<PetResponse?> GetByIdAsync(int id, CancellationToken token = default);

    public Task<bool> DeleteAsync(int id, CancellationToken token = default);


}
