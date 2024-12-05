using IzunaDrop.Data.Models;

namespace IzunaDrop.Services.Interface
{
    public interface IEnemyService
    {
        Task<IEnumerable<Enemy>> GetAllEnemies();
        Task<Enemy> GetEnemyByIdAsync(int id);
    }
}
