using FluentValidation;
using Pets.Application.Entities;
using Pets.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pets.Application.Services;
public class PetService : IPetService
{
    private readonly IPetRepository _petRepository;
    private readonly IValidator<Pet> _petValidator; 
    public PetService(IPetRepository petRepository, IValidator<Pet> petValidator)
    {
        _petRepository = petRepository;
        _petValidator = petValidator;
    }

    public async Task<Pet> CreateAsync(Pet pet, CancellationToken token = default)
    {
        await _petValidator.ValidateAndThrowAsync(pet, token);

        return await _petRepository.AddAsync(pet,token);
    }  

    public async Task<IEnumerable<Pet>> GetAllAsync(CancellationToken token = default)
    {
        return await _petRepository.ListAllAsync(token);
    }

    public async Task<Pet?> GetByIdAsync(int id, CancellationToken token = default)
    {
        return await _petRepository.GetByIdAsync(id, token);    
    }

    public async Task<Pet> UpdateAsync(Pet pet, CancellationToken token = default)
    {
        await _petValidator.ValidateAndThrowAsync(pet, token);

        await _petRepository.UpdateAsync(pet, token);
        return pet;
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
