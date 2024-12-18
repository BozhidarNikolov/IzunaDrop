using IzunaDrop.Data;
using IzunaDrop.Data.Models;
using IzunaDrop.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace IzunaDrop.Services
{
    public class GameService : IGameService
    {
        private readonly IzunaDropDbContext _context;

        public GameService(IzunaDropDbContext context)
        {
            _context = context;
        }

        public async Task<Game> CreateGameAsync(Game game)
        {
            _context.Games.Add(game);
            await _context.SaveChangesAsync();
            return game;
        }

        public async Task<bool> DeleteGameAsync(int gameId)
        {
            var game = await _context.Games.FindAsync(gameId);
            if (game == null)
            {
                return false;
            }
            game.IsDeleted = true;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Game>> GetAllGamesAsync()
        {
            return await _context.Games.ToListAsync();
        }

        public async Task<Game> GetGameByIdAsync(int id)
        {
            return await _context.Games
              .FirstOrDefaultAsync(g => g.Id == id);
            
        }

        public async Task<bool> UpdateGameAsync(Game updatedGame)
        {
            var existingGame = await _context.Games.FindAsync(updatedGame.Id);
            if (existingGame == null)
            {
                return false;
            }
            existingGame.Name = updatedGame.Name;
            existingGame.Description = updatedGame.Description;
            existingGame.ImagePath = updatedGame.ImagePath;
            existingGame.ReleaseDate = updatedGame.ReleaseDate;

            _context.Games.Update(existingGame);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
