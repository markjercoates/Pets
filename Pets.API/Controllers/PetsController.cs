using Microsoft.AspNetCore.Mvc;
using Pets.API.Mappings;
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

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken token = default)
    {
        var pets = await _petService.GetAllAsync(token);
        var response = pets.MapToResponse();
        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get([FromRoute] int id, CancellationToken token = default)
    {
        var pet = await _petService.GetByIdAsync(id, token);
        if (pet == null)
        {
            return NotFound();
        }
        
        var response = pet.MapToResponse(); 
        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreatePetRequest request, CancellationToken token = default)
    {
        var pet = request.MapToPet();

        var createdPet = await _petService.CreateAsync(pet, token);  
        var response = createdPet.MapToResponse();  

        return CreatedAtAction(nameof(Get), new { id = response.Id }, response);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdatePetRequest request, CancellationToken token = default)
    {
        var pet = request.MapToPet(id);

        var updatedPet = await _petService.UpdateAsync(pet, token);
        if (updatedPet == null)
        {
            return NotFound();
        }

        var response = updatedPet.MapToResponse();
        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute]int id, CancellationToken token = default)
    {
        var deleted = await _petService.DeleteAsync(id, token); 
        if(!deleted)
        {
            return NotFound();
        }

        return Ok();
    }
}
