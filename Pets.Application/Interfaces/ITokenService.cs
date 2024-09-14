using Pets.Application.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pets.Application.Interfaces;
public interface ITokenService
{
    Task<string> CreateToken(AppUser user);
}
