using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pets.Contracts.Responses;
public class PetsResponse
{
    public required IEnumerable<PetResponse> Items { get; init; } = Enumerable.Empty<PetResponse>();
}

