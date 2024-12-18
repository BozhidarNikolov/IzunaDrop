using IzunaDrop.Data.Models;

namespace IzunaDrop.Services.Interface
{
    public interface IEnemyService
    {
        Task<IEnumerable<Enemy>> GetAllEnemiesAsync(int gameId);
        Task<Enemy> GetEnemyByIdAsync(int enemyId,int gameId);
        Task<Enemy> CreateEnemyAsync(Enemy enemy);

        Task<bool> UpdateEnemyAsync(Enemy enemy);

        Task<bool> DeleteEnemyAsync(int enemyId);
    }
}
