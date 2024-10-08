﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Pets.API.Extensions;
using Pets.API.Helpers;
using Pets.Application.Interfaces;
using Pets.Application.Services;
using Pets.Contracts.Responses;


namespace Pets.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PetTypesController : ControllerBase
{
    private readonly IPetTypeService _petTypeService;

    public PetTypesController(IPetTypeService petTypeService)
    {
        _petTypeService = petTypeService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<PetTypeResponse>>> GetAll(CancellationToken token = default)
    {
        var response = await _petTypeService.GetAllAsync(token);

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PetTypeResponse>> Get([FromRoute] int id, CancellationToken token = default)
    {
        var response = await _petTypeService.GetByIdAsync(id, token);
        if (response == null)
        {
            return NotFound();
        }

        return Ok(response);
    }
}
