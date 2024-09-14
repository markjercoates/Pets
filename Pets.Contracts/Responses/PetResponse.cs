using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pets.Contracts.Responses;
public class PetResponse
{
    public int Id { get; set; }

    public required string Name { get; set; }

    public string? Description { get; set; }

    public string? MicroChipId { get; set; }

    public int PetTypeId { get; set; }

    public string PetTypeName { get; set; } = string.Empty;

    public string? OwnerName { get; set; }

    public string? OwnerEmail { get; set; } 

    public DateTime MissingSince { get; set; }

    public DateTime CreatedDate { get; set; }
}
