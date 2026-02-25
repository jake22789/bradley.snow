using System.ComponentModel.DataAnnotations;
using ClassLibrary.DTOs;
using Microsoft.AspNetCore.Mvc;

//
// CONTROLLER ROLE
// ----------------
// Controllers are the "front door" of your backend.
// They receive HTTP requests, call services to do the work,
// and translate results into HTTP responses.
//
// Controllers should be THIN:
// - No database logic
// - No business rules
// - No data transformation logic
//

[ApiController] // Enables automatic model validation & API behavior
[Route("api/characters")] // Base route for this controller
public class CharacterController : ControllerBase
{
    // Service that contains the business/data logic
    private readonly CharacterService _characterService;

    // Constructor Injection:
    // ASP.NET gives us CharacterService automatically via Dependency Injection
    public CharacterController(CharacterService characterService)
    {
        _characterService = characterService;
    }

    // GET: api/characters
    // Returns a list of characters to the client
    [HttpGet]
    public async Task<ActionResult<List<CharacterDTO>>> GetCharacters()
    {
        // Ask the service for character data
        var characters = await _characterService.GetCharactersAsync();

        // Return HTTP 200 OK with JSON data
        return Ok(characters);
    }
    [HttpGet("{id}")]
    public async Task<ActionResult<CharacterDTO?>> GetCharacterById(int id)
    {
        var character = await _characterService.GetCharacterByIdAsync(id);
        if (character == null)
        {
            return NotFound(); 
        }
        return Ok(character); 
    } 

    [HttpPost]
    public async Task<ActionResult<CharacterDTO>> CreateCharacter(CharacterDTO newCharacter)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest("invalid data");
        }
        if (newCharacter.Id != 0)
        {
            return BadRequest("Id should not be provided");
        }
        try
        {
            await _characterService.CreateCharacterAsync(newCharacter);
        }
        catch (ValidationException ex)
        {
            return BadRequest(ex.Message);
        }

        var createdCharacter = await _characterService.CreateCharacterAsync(newCharacter);
        return Ok(createdCharacter);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<CharacterDTO?>> UpdateCharacter(int id, CharacterDTO updatedCharacter)
    {
        var updated = await _characterService.UpdateCharacterAsync(id, updatedCharacter);
        return Ok(updated);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<bool>> DeleteCharacter(int id)
    {
        var deleted = await _characterService.DeleteCharacterAsync(id);
        return Ok(deleted);
    }
}