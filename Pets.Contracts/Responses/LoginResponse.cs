using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pets.Contracts.Responses;
public class LoginResponse
{
    public required string Username { get; set; }

    public required string Token { get; set; }
}
