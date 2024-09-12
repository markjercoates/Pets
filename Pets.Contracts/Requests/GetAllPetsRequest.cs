using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pets.Contracts.Requests;
public class GetAllPetsRequest
{
    public string? Name { get; init; }   

    public int? PetTypeId { get; init; } 
}
