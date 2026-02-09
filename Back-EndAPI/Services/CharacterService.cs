using ClassLibrary.DTOs;
using ClassLibrary.Entities;
using Microsoft.EntityFrameworkCore;

//
// SERVICE ROLE
// -------------
// Services contain BUSINESS LOGIC and DATA ACCESS.
// Controllers should never talk directly to the database.
//
// Services:
// - Decide WHAT data to fetch
// - Decide HOW data is shaped
// - Return DTOs (safe for UI)
//
// This keeps controllers simple and testable.
//

public class CharacterService
{
    // Database context injected via Dependency Injection
    private readonly AppDbContext _db;

    public CharacterService(AppDbContext db)
    {
        _db = db;
    }

    // Returns characters as DTOs (not entities)
    public async Task<List<CharacterDTO>> GetCharactersAsync()
    {
        // Query the database and PROJECT directly into DTOs
        // EF Core generates optimized SQL that selects only needed columns
        return await _db.Characters
            .Select(e => new CharacterDTO
            {
                Id = e.Id,
                Name = e.Name,
                health = e.health,
                Level = e.Level,
                strength = e.strength,
                charisma = e.charisma,
                intelegence = e.intelegence,
                wisdome = e.wisdome,
                constitution = e.constitution,
                dextarity = e.dextarity,
            })
            .ToListAsync();
    }

    // Example: SAME DATA, but using raw SQL instead of EF LINQ
    // This is for learning / reference purposes
    public async Task<List<CharacterDTO>> GetCharactersWithSqlAsync()
    {
        var results = new List<CharacterDTO>();

        // Get the raw database connection EF is using
        using var conn = _db.Database.GetDbConnection();
        await conn.OpenAsync();

        // Create a SQL command
        using var cmd = conn.CreateCommand();
        cmd.CommandText = """
            SELECT *
            FROM character
        """;

        // Execute query and read results
        using var reader = await cmd.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            results.Add(new CharacterDTO
            {
                Id = reader.GetInt32(0),
                Name = reader.GetString(1),
                health = reader.GetInt32(2),
                Level = reader.GetInt32(3),
                strength = reader.GetInt32(4),
                charisma = reader.GetInt32(5),
                intelegence = reader.GetInt32(6),
                wisdome = reader.GetInt32(7),
                constitution = reader.GetInt32(8),
                dextarity = reader.GetInt32(9),
            });
        }

        return results;
    }

    public async Task<CharacterDTO?> GetCharacterByIdAsync(int id)
    {
        return await _db.Characters
            .Where(e => e.Id == id)
            .Select(e => new CharacterDTO
            {
                Id = e.Id,
                Name = e.Name,
                health = e.health,
                Level = e.Level,
                strength = e.strength,
                charisma = e.charisma,
                intelegence = e.intelegence,
                wisdome = e.wisdome,
                constitution = e.constitution,
                dextarity = e.dextarity,
            })
            .FirstOrDefaultAsync();
    }
    public async Task<CharacterDTO> CreateCharacterAsync(CharacterDTO newCharacter)
    {
        var entity = new CharacterEntity
        {
            Name = newCharacter.Name,
            health = newCharacter.health,
            Level = newCharacter.Level,
            strength = newCharacter.strength,
            charisma = newCharacter.charisma,
            intelegence = newCharacter.intelegence,
            wisdome = newCharacter.wisdome,
            constitution = newCharacter.constitution,
            dextarity = newCharacter.dextarity,
        };

        _db.Characters.Add(entity);
        await _db.SaveChangesAsync();

        // Map back to DTO (including generated ID)
        return new CharacterDTO
        {
            Id = entity.Id,
            Name = entity.Name,
            health = entity.health,
            Level = entity.Level,
            strength = entity.strength,
            charisma = entity.charisma,
            intelegence = entity.intelegence,
            wisdome = entity.wisdome,
            constitution = entity.constitution,
            dextarity = entity.dextarity,
        };
    }
    public async Task<CharacterDTO?> UpdateCharacterAsync(int id, CharacterDTO updatedCharacter)
    {
        var entity = await _db.Characters.FindAsync(id);
        if (entity == null) return null;

        entity.Name = updatedCharacter.Name;
        entity.health = updatedCharacter.health;
        entity.Level = updatedCharacter.Level;
        entity.strength = updatedCharacter.strength;
        entity.charisma = updatedCharacter.charisma;
        entity.intelegence = updatedCharacter.intelegence;
        entity.wisdome = updatedCharacter.wisdome;
        entity.constitution = updatedCharacter.constitution;
        entity.dextarity = updatedCharacter.dextarity;

        await _db.SaveChangesAsync();

        return new CharacterDTO
        {
            Id = entity.Id,
            Name = entity.Name,
            health = entity.health,
            Level = entity.Level,
            strength = entity.strength,
            charisma = entity.charisma,
            intelegence = entity.intelegence,
            wisdome = entity.wisdome,
            constitution = entity.constitution,
            dextarity = entity.dextarity,
        };
    }
    public async Task<bool> DeleteCharacterAsync(int id)
    {
        var entity = await _db.Characters.FindAsync(id);
        if (entity == null) return false;

        _db.Characters.Remove(entity);
        await _db.SaveChangesAsync();
        return true;
    }
}
