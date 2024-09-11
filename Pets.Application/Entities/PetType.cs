using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pets.Application.Entities;
public class PetType
{
    public int Id { get; set; }

    public required string Name { get; set; }

    public ICollection<Pet> Pets { get; set; } = [];
}
