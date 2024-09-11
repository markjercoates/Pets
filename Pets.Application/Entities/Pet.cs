using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pets.Application.Entities;
public class Pet 
{
    public int Id { get; set; }

    public required string Name { get; set; }

    public string? Description { get; set; }

    public string? MicroChipId { get; set; }

    public int PetTypeId { get; set; }

    public PetType PetType { get; set; } = null!;

    // missing since
    public DateTime MissingSince { get; set; }

    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedDate { get; set; }

}
