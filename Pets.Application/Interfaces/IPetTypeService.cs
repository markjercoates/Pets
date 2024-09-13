using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pets.Contracts.Responses;

namespace Pets.Application.Interfaces;
public interface IPetTypeService
{
    public Task<PetTypesResponse> GetAllAsync(CancellationToken token = default);

    public Task<PetTypeResponse?> GetByIdAsync(int id, CancellationToken token = default);
}
