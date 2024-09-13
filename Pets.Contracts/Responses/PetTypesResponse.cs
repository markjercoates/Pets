using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pets.Contracts.Responses;
public class PetTypesResponse
{
    public required IEnumerable<PetTypeResponse> Items { get; init; } = Enumerable.Empty<PetTypeResponse>();
}
