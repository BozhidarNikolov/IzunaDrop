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
        
    }
}
