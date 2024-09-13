using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pets.Contracts.Responses;
public class PetTypeResponse
{
    public int Id { get; set; }

    public required string Name { get; set; }
}
