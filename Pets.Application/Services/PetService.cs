using FluentValidation;
using Pets.Application.Entities;
using Pets.Application.Interfaces;
using Pets.Application.Validators;
using Pets.Contracts.Requests;
using Pets.Contracts.Responses;
using Pets.Contracts.Models;
using Pets.Application.Mappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pets.Application.Services;
public class PetService : IPetService
{
    private readonly IPetRepository _petRepository;
    private readonly IValidator<CreatePetRequest> _createPetValidator;
    private readonly IValidator<UpdatePetRequest> _updatePetValidator;
    public PetService(IPetRepository petRepository, IValidator<CreatePetRequest> createPetValidator, IValidator<UpdatePetRequest> updatePetValidator)
    {
        _petRepository = petRepository;
        _createPetValidator = createPetValidator;
        _updatePetValidator = updatePetValidator;
    }

    public async Task<PetResponse> CreateAsync(CreatePetRequest request, CancellationToken token = default)
    {
        await _createPetValidator.ValidateAndThrowAsync(request, token);

        var pet = request.MapToPet();

        var createdPet = await _petRepository.AddAsync(pet,token);

        return createdPet.MapToResponse();
    }

    public async Task<PetResponse> UpdateAsync(int id, UpdatePetRequest request, CancellationToken token = default)
    {
        await _updatePetValidator.ValidateAndThrowAsync(request, token);

        var pet = request.MapToPet(id);

        await _petRepository.UpdateAsync(pet, token);
        
        return pet.MapToResponse();
    }

    public async Task<PetResponse?> GetByIdAsync(int id, CancellationToken token = default)
    {
        var pet = await _petRepository.GetByIdAsync(id, token);

        if (pet == null)
        {
            return default(PetResponse);
        }

        return pet.MapToResponse();
    }

    public async Task<PagedList<PetResponse>> GetAllAsync(GetAllPetsRequestOptions options, CancellationToken token = default)
    {
        var pets = await _petRepository.ListAllWithOptionsAsync(options, token);
        if (!pets.Any())
        {
            return new PagedList<PetResponse>(new List<PetResponse>(),0, options.PageNumber, options.PageSize);
        }

        var response = pets.MapToResponse();

        return await PagedList<PetResponse>.CreateAsync(response.Items, options.PageNumber, options.PageSize);
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken token = default)
    {
        var petToDelete = await _petRepository.GetByIdAsync(id, token);
        if (petToDelete == null)
        {
            return false;
        }

        await _petRepository.DeleteAsync(petToDelete, token);
        return true;
    }
}
