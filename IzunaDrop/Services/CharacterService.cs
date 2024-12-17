using IzunaDrop.Data;
using IzunaDrop.Data.Models;
using IzunaDrop.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace IzunaDrop.Services
{
    public class CharacterService : ICharacterService
    {
        private readonly IzunaDropDbContext _context;

        public CharacterService(IzunaDropDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Character>> GetAllCharactersAsync(int gameId)
        {
            return await _context.Characters
                .Where(c => c.GameId == gameId)
                .ToListAsync();
        }
        public async Task<Character> GetCharacterByIdAsync(int gameId,int characterId)
        {
            return await _context.Characters
                .Where(c => c.GameId == gameId && c.Id == characterId)
                .FirstOrDefaultAsync();
        }
        public async Task<Character> CreateCharacterAsync(Character character)
        {
            
            _context.Characters.Add(character);
            await _context.SaveChangesAsync();
            return character;
        }

        public async Task<bool> UpdateCharacterAsync(Character updatedCharacter)
        {
            var existingCharacter = await _context.Characters.FindAsync(updatedCharacter.Id);
            if (existingCharacter == null)
            {
                return false;
            }

            existingCharacter.Name = updatedCharacter.Name;
            existingCharacter.Description = updatedCharacter.Description;
            existingCharacter.GameId = updatedCharacter.GameId;

            _context.Characters.Update(existingCharacter);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DeleteCharacterAsync(int characterId)
        {
            var character = await _context.Characters.FindAsync(characterId);
            if (character == null)
            {
                return false;
            }
            character.IsDeleted = true;
            await _context.SaveChangesAsync();
            return true;
        }

    }
}
