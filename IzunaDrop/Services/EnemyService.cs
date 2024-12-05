using IzunaDrop.Data;
using IzunaDrop.Data.Models;
using IzunaDrop.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace IzunaDrop.Services
{
    public class EnemyService : IEnemyService
    {
        private readonly IzunaDropDbContext _context;
        public EnemyService(IzunaDropDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Enemy>> GetAllEnemiesAsync(int gameId)
        {
            return await _context.Enemies
                .Where(e => e.GameId == gameId)
                .ToListAsync();
        }

        public Task<Enemy> GetEnemyByIdAsync(int enemyId)
        {
            throw new NotImplementedException();
        }
    }
}
