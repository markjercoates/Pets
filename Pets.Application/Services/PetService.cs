using FluentValidation;
using Pets.Application.Interfaces;
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
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<CreatePetRequest> _createPetValidator;
    private readonly IValidator<UpdatePetRequest> _updatePetValidator;
    public PetService(IUnitOfWork unitOfWork, IValidator<CreatePetRequest> createPetValidator, 
        IValidator<UpdatePetRequest> updatePetValidator)
    {
        _unitOfWork = unitOfWork;
        _createPetValidator = createPetValidator;
        _updatePetValidator = updatePetValidator;
    }

    public async Task<PetResponse?> CreateAsync(CreatePetRequest request, CancellationToken token = default)
    {
        await _createPetValidator.ValidateAndThrowAsync(request, token);

        var pet = request.MapToPet();

        await _unitOfWork.PetRepository.AddAsync(pet, token);
        if(!await _unitOfWork.SaveChanges())
        {
            return default;
        }

        return pet.MapToResponse();
    }

    public async Task<bool> UpdateAsync(int id, UpdatePetRequest request, CancellationToken token = default)
    {
        await _updatePetValidator.ValidateAndThrowAsync(request, token);
        var pet = await _unitOfWork.PetRepository.GetByIdAsync(id, token);
        if(pet == null)
        {
            return false;
        }

        request.MapToPet(pet);

        _unitOfWork.PetRepository.Update(pet, token);
        return await _unitOfWork.SaveChanges();
    }

    public async Task<PetResponse?> GetByIdAsync(int id, CancellationToken token = default)
    {
        var pet = await _unitOfWork.PetRepository.GetByIdAsync(id, token);

        if (pet == null)
        {
            return default(PetResponse);
        }

        return pet.MapToResponse();
    }

    public async Task<PagedList<PetResponse>> GetAllAsync(GetAllPetsRequestOptions options, CancellationToken token = default)
    {
        var pets = await _unitOfWork.PetRepository.ListAllWithOptionsAsync(options, token);
        if (!pets.Any())
        {
            return new PagedList<PetResponse>(new List<PetResponse>(),0, options.PageNumber, options.PageSize);
        }

        var response = pets.MapToResponse();

        return await PagedList<PetResponse>.CreateAsync(response.Items, options.PageNumber, options.PageSize);
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken token = default)
    {
        var petToDelete = await _unitOfWork.PetRepository.GetByIdAsync(id, token);
        if (petToDelete == null)
        {
            return false;
        }

        _unitOfWork.PetRepository.Delete(petToDelete, token);
        return await _unitOfWork.SaveChanges();
    }
}
