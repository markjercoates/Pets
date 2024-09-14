using Pets.Application.Interfaces;
using Pets.Persistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pets.Persistence.Repositories;
public class UnitOfWork : IUnitOfWork
{
    private readonly PetsDbContext _context;
    private IPetRepository _petRepository;
    private IPetTypeRepository _petTypeRepository;
    private ILoggedInUserService _loggedInUserService;
    public UnitOfWork(PetsDbContext context, IPetRepository petRepository, 
        IPetTypeRepository petTypeRepository, ILoggedInUserService loggedInUserService)
    {
        _context = context;
        _petRepository = petRepository;
        _petTypeRepository = petTypeRepository;
        _loggedInUserService = loggedInUserService;
    }

    public IPetRepository PetRepository => _petRepository ??= new PetRepository(_context);

    public IPetTypeRepository PetTypeRepository => _petTypeRepository ??= new PetTypeRepository(_context);

    public async Task<bool> SaveChanges(CancellationToken cancellationToken = default)
    {
        return await _context.SaveChangesAsync(_loggedInUserService.UserId, cancellationToken) > 0;
    }

    public bool HasChanges()
    {
        return _context.ChangeTracker.HasChanges();
    }
}
