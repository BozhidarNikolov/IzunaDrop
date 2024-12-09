using IzunaDrop.Data.Models;

namespace IzunaDrop.Services.Interface
{
    public interface ICharacterService
    {
        Task<IEnumerable<Character>> GetAllCharactersAsync(int gameId);
        Task<Character> GetCharacterByIdAsync(int gameId, int characterId);
    }
    
}
