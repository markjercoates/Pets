using Microsoft.EntityFrameworkCore;
using Pets.Application.Interfaces;
using Pets.Persistence.Data;

namespace Pets.Persistence.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    protected readonly PetsDbContext _dbContext;

    public GenericRepository(PetsDbContext dbContext)
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

    public async Task<T> AddAsync(T entity, CancellationToken token = default)
    {
        await _dbContext.Set<T>().AddAsync(entity, token);
        await _dbContext.SaveChangesAsync(token);
        return entity;
    }

    public async Task UpdateAsync(T entity, CancellationToken token = default)
    {
        _dbContext.Entry(entity).State = EntityState.Modified;
        await _dbContext.SaveChangesAsync(token);
    }

    public async Task DeleteAsync(T entity, CancellationToken token = default)
    {
        _dbContext.Set<T>().Remove(entity);
        await _dbContext.SaveChangesAsync(token);
    }
}
