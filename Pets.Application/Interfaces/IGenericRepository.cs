using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pets.Application.Interfaces;
public interface IGenericRepository<T> where T : class
{
    Task<T?> GetByIdAsync(int id, CancellationToken token = default);
    Task<IReadOnlyList<T>> ListAllAsync(CancellationToken token = default);
    Task<T> AddAsync(T entity, CancellationToken token = default);
    Task UpdateAsync(T entity, CancellationToken token = default);
    Task DeleteAsync(T entity, CancellationToken token = default);
}
