using IzunaDrop.Data;
using IzunaDrop.Data.Models;
using IzunaDrop.Services.Interface;

namespace IzunaDrop.Services
{
    public class EnemyService:IEnemyService
    {
        private readonly IzunaDropDbContext _context;
        public EnemyService(IzunaDropDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Enemy>> GetAllEnemiesAsync(int gameId)
        {

        }
    }
}
