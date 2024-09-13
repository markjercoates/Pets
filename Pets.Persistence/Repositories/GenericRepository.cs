using Microsoft.EntityFrameworkCore;
using Pets.Application.Interfaces;
using Pets.Persistence.Data;

namespace Pets.Persistence.Repositories;

public abstract class GenericRepository<T> : IGenericRepository<T> where T : class
{
    protected readonly PetsDbContext _dbContext;

    protected GenericRepository(PetsDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public virtual async Task<T?> GetByIdAsync(int id, CancellationToken token = default)
    {
        return await _dbContext.Set<T>().FindAsync(id, token);
    }

    public async Task<IReadOnlyList<T>> ListAllAsync(CancellationToken token = default)
    {
        return await _dbContext.Set<T>().ToListAsync(token);
    }

    public async Task AddAsync(T entity, CancellationToken token = default)
    {
        await _dbContext.Set<T>().AddAsync(entity, token);
    }

    public void Update(T entity, CancellationToken token = default)
    {
        //_dbContext.Entry(entity).State = EntityState.Modified;
        _dbContext.Set<T>().Update(entity);        
    }

    public void Delete(T entity, CancellationToken token = default)
    {
         _dbContext.Set<T>().Remove(entity);
    }
}
