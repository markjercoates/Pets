using Pets.Application.Entities;
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

    public static PetsResponse MapToResponse(this IEnumerable<Pet> movies)
    {
        return new PetsResponse
        {
            Items = movies.Select(MapToResponse)
        };
    }
}
