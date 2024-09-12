using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pets.Contracts.Models;

namespace Pets.Contracts.Requests;

public class GetAllPetsRequestOptions : PaginationParams
{
    public string? Name { get; init; }   

    public int PetTypeId { get; init; } 
}
