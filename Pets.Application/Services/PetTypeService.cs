using Pets.Application.Interfaces;
using Pets.Application.Mappings;
using Pets.Contracts.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pets.Application.Services;
public class PetTypeService : IPetTypeService
{
    private readonly IUnitOfWork _unitOfWork;
    public PetTypeService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<PetTypesResponse> GetAllAsync(CancellationToken token = default)
    {
        var petTypes = await _unitOfWork.PetTypeRepository.ListAllAsync(token); 
        return petTypes.MapToResponse();
    }

    public async Task<PetTypeResponse?> GetByIdAsync(int id, CancellationToken token = default)
    {
        var petType = await _unitOfWork.PetTypeRepository.GetByIdAsync(id, token);

        if (petType == null)
        {
            return default(PetTypeResponse);
        }

        return petType.MapToResponse();
    }
}
