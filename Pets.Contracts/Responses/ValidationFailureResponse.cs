﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pets.Contracts.Responses;
public class ValidationFailureResponse
{
    //public required IEnumerable<ValidationResponse> Errors { get; init; }

    public required IEnumerable<string> Errors { get; init; }
}

public class ValidationResponse
{
    public required string PropertyName { get; init; }

    public required string Message { get; init; }
}

