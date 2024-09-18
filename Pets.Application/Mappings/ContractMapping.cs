using Pets.Application.Entities;
using Pets.Contracts.Requests;
using Pets.Contracts.Responses;

namespace Pets.Application.Mappings;

public static class ContractMapping
{
    public static Pet MapToPet(this CreatePetRequest request) => new Pet
    {
        Name = request.Name,
        Description = request.Description,
        MicroChipId = request.MicroChipId,
        PetTypeId = request.PetTypeId,
        MissingSince = request.MissingSince,
        OwnerName = request.OwnerName,
        OwnerEmail = request.OwnerEmail
    };

    public static Pet MapToPet(this UpdatePetRequest request, int id) => new Pet
    {
        Id = id,
        Name = request.Name,
        Description = request.Description,
        MicroChipId = request.MicroChipId,
        PetTypeId = request.PetTypeId,
        MissingSince = request.MissingSince,
        OwnerName = request.OwnerName,
        OwnerEmail = request.OwnerEmail
    };

    public static void MapToPet(this UpdatePetRequest request, Pet pet)
    {
        pet.Name = request.Name;
        pet.Description = request.Description;
        pet.MicroChipId = request.MicroChipId;
        pet.MissingSince = request.MissingSince;
        pet.PetTypeId = request.PetTypeId;
        pet.OwnerName = request.OwnerName;
        pet.OwnerEmail = request.OwnerEmail;
    }

    public static PetResponse MapToResponse(this Pet pet) => new PetResponse
    {
        Id = pet.Id,
        Name = pet.Name,
        Description = pet.Description,
        MicroChipId = pet.MicroChipId,
        PetTypeId = pet.PetTypeId,
        PetTypeName = pet.PetType?.Name ?? string.Empty,
        MissingSince = pet.MissingSince,
        OwnerName = pet.OwnerName,
        OwnerEmail = pet.OwnerEmail,
        CreatedDate = pet.CreatedDate   
    };

    public static PetsResponse MapToResponse(this IEnumerable<Pet> pets)
    {
        return new PetsResponse
        {
            Items = pets.Select(MapToResponse)
        };
    }

    public static PetTypeResponse MapToResponse(this PetType petType) => new PetTypeResponse
    {
        Id = petType.Id,
        Name = petType.Name
    };

    public static PetTypesResponse MapToResponse(this IEnumerable<PetType> petTypes)
    {
        return new PetTypesResponse
        {
            Items = petTypes.Select(MapToResponse)
        };
    }
}
