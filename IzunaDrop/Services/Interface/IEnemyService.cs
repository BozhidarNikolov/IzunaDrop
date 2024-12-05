using IzunaDrop.Data.Models;

namespace IzunaDrop.Services.Interface
{
    public interface IEnemyService
    {
        Task<IEnumerable<Enemy>> GetAllEnemiesAsync(int gameId);
        Task<Enemy> GetEnemyByIdAsync(int id);
    }
}
