using IzunaDrop.Data;
using IzunaDrop.Data.Models;
using IzunaDrop.Services.Interface;

namespace IzunaDrop.Services
{
    public class CharacterService : ICharacterService
    {
        private readonly IzunaDropDbContext _context;

        public CharacterService(IzunaDropDbContext context)
        {
            _context = context;
        }

        public Task<IEnumerable<Character>> GetAllCharactersAsync(int gameId)
        {
            throw new NotImplementedException();
        }
    }
}
