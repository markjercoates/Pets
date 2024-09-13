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
    Task AddAsync(T entity, CancellationToken token = default);
    void Update(T entity, CancellationToken token = default);
    void Delete(T entity, CancellationToken token = default);
}
