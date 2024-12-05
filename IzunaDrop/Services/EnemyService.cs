using IzunaDrop.Data;
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
    }
}
