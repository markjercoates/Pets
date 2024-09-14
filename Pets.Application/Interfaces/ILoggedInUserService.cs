using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pets.Application.Interfaces;
public interface ILoggedInUserService
{
    public string UserId { get; }
}
