using Pets.Application.Entities;
using Pets.Application.Models;
using Pets.Contracts.Requests;
using Pets.Contracts.Responses;

namespace Pets.API.Mappings;

public static class ContractMapping
{
    public static Pet MapToPet(this CreatePetRequest request) => new Pet
    {
        Name = request.Name,
        Description = request.Description,
        MicroChipId = request.MicroChipId,
        PetTypeId = request.PetTypeId,
        MissingSince = request.MissingSince
    };

    public static Pet MapToPet(this UpdatePetRequest request, int id) => new Pet
    {
        Id = id,
        Name = request.Name,
        Description = request.Description,
        MicroChipId = request.MicroChipId,
        PetTypeId = request.PetTypeId,
        MissingSince = request.MissingSince,
    };

    public static PetResponse MapToResponse(this Pet pet) => new PetResponse
    {
        Id = pet.Id,
        Name = pet.Name,
        Description = pet.Description,
        MicroChipId = pet.MicroChipId,
        PetTypeId = pet.PetTypeId,
        PetTypeName = pet.PetType.Name,
        MissingSince = pet.MissingSince,
        CreatedDate = pet.CreatedDate   
    };

    public static PetsResponse MapToResponse(this IEnumerable<Pet> pets)
    {
        return new PetsResponse
        {
            Items = pets.Select(MapToResponse)
        };
    }

    public static GetAllPetsOptions MapToOptions(this GetAllPetsRequest request) => new GetAllPetsOptions
    {
        Name = request.Name,
        PetTypeId = request.PetTypeId ?? 0
    };
}
