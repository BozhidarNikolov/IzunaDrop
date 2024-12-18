using IzunaDrop.Data;
using IzunaDrop.Data.Models;
using IzunaDrop.Services;
using IzunaDrop.Services.Interface;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;

namespace IzunaDrop.Tests
{
    public class EnemyServiceTests
    {
        
        private IEnemyService _enemyService;
        private IzunaDropDbContext _context;

        private void InitializeDatabase()
        {
            var options = new DbContextOptionsBuilder<IzunaDropDbContext>()
                .UseInMemoryDatabase(databaseName: "EnemyTestDatabase")
                .Options;
            _context = new IzunaDropDbContext(options);
            _context.Database.EnsureDeleted();
            _context.Games.AddRange(new List<Game>
            {
                new Game {Id=3, Name="Game 1", Description="Test game 1", ReleaseDate=new DateTime(2012,12,12)},
                new Game {Id=4, Name="Game 2", Description="Test game 2",ReleaseDate=new DateTime(2010,10,10)}
            });
            _context.Enemies.AddRange(new List<Enemy>
            {
                new Enemy {Id=1,Name="Enemy 1",Description="Enemy test 1",GameId=3 },
                new Enemy {Id=2, Name="Enemy 2",Description="Enemy test 2",GameId=4},
                new Enemy {Id=3, Name="Enemy 3",Description="Enemy test 3",GameId=4}

            });
            _context.SaveChanges();

            _enemyService = new EnemyService(_context);

        }

       
        [Fact]
        public async Task GetAllEnemiesAsync_ShouldReturnAllEnemies()
        {
            InitializeDatabase();
            int gameId = 4;
            var result = await _enemyService.GetAllEnemiesAsync(gameId);
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
        }
        [Fact]
        public async Task GetEnemyByIdAsync_ShouldReturnEnemy()
        {
            InitializeDatabase();
            int testGameId = 3;
            int enemyId = 1;
            var result = await _enemyService.GetEnemyByIdAsync(enemyId, testGameId);

            Assert.NotNull(result);
            Assert.Equal(testGameId, result.GameId);
            Assert.Equal("Enemy 1", result.Name);
        }
        [Fact]
        public async Task GetEnemyByIdAsync_NonExistentId_ShouldReturnNull()
        {
            InitializeDatabase();
            int testGameId = 3;
            int enemyId = 99;
            var result = await _enemyService.GetEnemyByIdAsync(enemyId, testGameId);

            Assert.Null(result);
        }
        [Fact]
        public async Task CreateEnemyAsync_ShouldAddNewEnemy()
        {
            InitializeDatabase();
            var enemy = new Enemy
            {
                Name = "Test Enemy",
                Description = "A test description",
                GameId = 1,
                IsDeleted = false
            };

            var result = await _enemyService.CreateEnemyAsync(enemy);


            Assert.NotNull(result);
            Assert.NotEqual(0, result.Id);
            var createdEnemy = await _context.Enemies.FindAsync(result.Id);
            Assert.NotNull(createdEnemy);
            Assert.Equal("Test Enemy", createdEnemy.Name);
            Assert.Equal("A test description", createdEnemy.Description);
            Assert.False(createdEnemy.IsDeleted);
        }

        [Fact]
        public async Task UpdateEnemyAsync_ShouldReturnFalseIfNotFound()
        {
            InitializeDatabase();
            var updatedEnemy = new Enemy
            {
                Id = 999,
                Name = "DoesNotExist",
                Description = "No desc",
                GameId = 1
            };

            var result = await _enemyService.UpdateEnemyAsync(updatedEnemy);
            Assert.False(result);
        }

        [Fact]
        public async Task UpdateEnemyAsync_ShouldUpdateExistingEnemy()
        {
            InitializeDatabase();
            var enemy = new Enemy
            {
                Name = "Old Name",
                Description = "Old Description",
                GameId = 1
            };
            _context.Enemies.Add(enemy);
            await _context.SaveChangesAsync();

            var updatedEnemy = new Enemy
            {
                Id = enemy.Id,
                Name = "New Name",
                Description = "New Description",
                GameId = 2
            };

            var result = await _enemyService.UpdateEnemyAsync(updatedEnemy);
            Assert.True(result);

            var fromDb = await _context.Enemies.FindAsync(enemy.Id);
            Assert.Equal("New Name", fromDb.Name);
            Assert.Equal("New Description", fromDb.Description);
            Assert.Equal(2, fromDb.GameId);
        }

        [Fact]
        public async Task DeleteEnemyAsync_ShouldReturnFalseIfNotFound()
        {
            InitializeDatabase();

            var result = await _enemyService.DeleteEnemyAsync(999);
            Assert.False(result);
        }

        [Fact]
        public async Task DeleteEnemyAsync_ShouldSetIsDeletedToTrue()
        {
            InitializeDatabase();
            var enemy = new Enemy
            {
                Name = "Enemy to Delete",
                Description = "Delete me",
                GameId = 1,
                IsDeleted = false
            };

            _context.Enemies.Add(enemy);
            await _context.SaveChangesAsync();

            var result = await _enemyService.DeleteEnemyAsync(enemy.Id);

            Assert.True(result);
            var fromDb = await _context.Enemies.FindAsync(enemy.Id);
            Assert.NotNull(fromDb);
            Assert.True(fromDb.IsDeleted);
        }
    }
}