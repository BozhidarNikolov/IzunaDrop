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

        public async Task<Enemy> CreateEnemyAsync(Enemy enemy)
        {
            _context.Enemies.Add(enemy);
            await _context.SaveChangesAsync();
            return enemy;
        }

        public async Task<bool> DeleteEnemyAsync(int enemyId)
        {
            var enemy = await _context.Enemies.FindAsync(enemyId);
            if (enemy == null)
            {
                return false;
            }
            enemy.IsDeleted = true;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Enemy>> GetAllEnemiesAsync(int gameId)
        {
            return await _context.Enemies
                .Where(e => e.GameId == gameId)
                .ToListAsync();
        }

        public async Task<Enemy> GetEnemyByIdAsync(int enemyId, int gameId)
        {
            return await _context.Enemies
                .Where(e => e.GameId == gameId && e.Id == enemyId)
                .FirstOrDefaultAsync();
               
        }

        public async Task<bool> UpdateEnemyAsync(Enemy updatedEnemy)
        {
            var existingEnemy = await _context.Enemies.FindAsync(updatedEnemy.Id);
            if (existingEnemy == null)
            {
                return false;
            }

            existingEnemy.Name = updatedEnemy.Name;
            existingEnemy.Description = updatedEnemy.Description;
            existingEnemy.GameId = updatedEnemy.GameId;

            _context.Enemies.Update(existingEnemy);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
