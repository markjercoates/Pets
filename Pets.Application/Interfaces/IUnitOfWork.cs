using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pets.Application.Interfaces;
public interface IUnitOfWork
{
    IPetRepository PetRepository { get; }

    IPetTypeRepository PetTypeRepository { get; }

    Task<bool> SaveChanges(CancellationToken cancellation = default);

    bool HasChanges();
}
