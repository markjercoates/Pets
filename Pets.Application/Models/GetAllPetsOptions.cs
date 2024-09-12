using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pets.Application.Models;
public class GetAllPetsOptions
{
    private const int MaxPageSize = 50;
    
    public string? Name { get; set; }

    public int PetTypeId { get; set; }

    public int PageNumber { get; set; } = 1;

    private int _pageSize = 10;
    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
    }   
}
