using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pets.Application.Entities;
public class Pet : AuditableEntity
{
    public int Id { get; set; }

    public required string Name { get; set; }

    public string? Description { get; set; }

    public string? MicroChipId { get; set; }

    public string? OwnerName { get; set; } 

    public string? OwnerEmail { get; set; }    

    public int PetTypeId { get; set; }

    public PetType PetType { get; set; } = null!;

    public DateTime MissingSince { get; set; }

}
