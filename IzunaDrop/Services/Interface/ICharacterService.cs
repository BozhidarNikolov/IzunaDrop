﻿using IzunaDrop.Data.Models;

namespace IzunaDrop.Services.Interface
{
    public interface ICharacterService
    {
        Task<IEnumerable<Character>> GetAllCharactersAsync(int gameId);
        Task<Character> GetCharacterByIdAsync(int gameId, int characterId);

        Task<Character> CreateCharacterAsync(Character character);

        Task<bool> UpdateCharacterAsync(Character updatedCharacter);

        Task<bool> DeleteCharacterAsync(int characterId);
    }
    
}
