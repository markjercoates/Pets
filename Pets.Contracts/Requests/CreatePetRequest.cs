using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pets.Contracts.Requests;
public class CreatePetRequest
{
    public required string Name { get; set; }

    public string? Description { get; set; }

    public string? MicroChipId { get; set; }

    public int PetTypeId { get; set; }   

    public DateTime MissingSince { get; set; }
  
}
