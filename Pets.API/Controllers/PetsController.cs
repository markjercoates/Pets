using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pets.API.Extensions;
using Pets.API.Helpers;
using Pets.Application.Interfaces;
using Pets.Contracts.Requests;

[Route("api/[controller]")]
[ApiController]
public class PetsController : ControllerBase
{
    private readonly IPetService _petService;
    
    public PetsController(IPetService petService)
    {
        _petService = petService;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get([FromRoute] int id, CancellationToken token = default)
    {
        var response = await _petService.GetByIdAsync(id, token);
        if (response == null)
        {
            return NotFound();
        }

        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] GetAllPetsRequestOptions options,
            CancellationToken token = default)
    {
        var response = await _petService.GetAllAsync(options, token);

        Response.AddPaginationHeader(new PaginationHeader(response.CurrentPage, response.PageSize,
                response.TotalCount, response.TotalPages));

        return Ok(response);
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreatePetRequest request, CancellationToken token = default)
    {
        var response = await _petService.CreateAsync(request, token);
        if(response is null)
        {
            return BadRequest("Problem creating a new Pet");
        }

        return CreatedAtAction(nameof(Get), new { id = response.Id }, response);
    }

    [Authorize]
    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdatePetRequest request, CancellationToken token = default)
    {
        var result = await _petService.UpdateAsync(id, request, token);
        if (!result)
        {
            return BadRequest("Problem finding or updating the Pet");
        }

        return NoContent();
    }

    [Authorize]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute]int id, CancellationToken token = default)
    {
        var result = await _petService.DeleteAsync(id, token); 
        if(!result)
        {
            return BadRequest("Problem finding or deleting the Pet");
        }

        return NoContent();
    }
}
