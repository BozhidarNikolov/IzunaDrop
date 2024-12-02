using IzunaDrop.Data;
using IzunaDrop.Data.Models;
using IzunaDrop.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace IzunaDrop.Services
{
    public class GameService:IGameService
    {
        private readonly IzunaDropDbContext _context;

        public GameService(IzunaDropDbContext context)
        {
            _context = context;
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
    }
}
