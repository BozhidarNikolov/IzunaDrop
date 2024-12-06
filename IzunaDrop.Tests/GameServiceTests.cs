using IzunaDrop.Data;
using IzunaDrop.Data.Models;
using IzunaDrop.Services;
using IzunaDrop.Services.Interface;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;

namespace IzunaDrop.Tests
{
    public class GameServiceTests
    {
        private IGameService _gameService;
        private IEnemyService _enemyService;
        private IzunaDropDbContext _context;

        private void InitializeDatabase()
        {
            var options = new DbContextOptionsBuilder<IzunaDropDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            _context = new IzunaDropDbContext(options);
            _context.Database.EnsureDeleted();
            _context.Games.AddRange(new List<Game>
            {
                new Game {Id=1, Name="Game 1", Description="Test game 1", ReleaseDate=new DateTime(2012,12,12)},
                new Game {Id=2, Name="Game 2", Description="Test game 2",ReleaseDate=new DateTime(2010,10,10)}
            });
            _context.Enemies.AddRange(new List<Enemy>
            {
                new Enemy {Id=1,Name="Enemy 1",Description="Enemy test 1",GameId=1 },
                new Enemy {Id=2, Name="Enemy 2",Description="Enemy test 2",GameId=2},
                new Enemy {Id=3, Name="Enemy 3",Description="Enemy test 3",GameId=2}

            });
            _context.SaveChanges();

            _gameService = new GameService(_context);
            _enemyService = new EnemyService(_context);

        }

        [Fact]
        public async Task GetAllGamesAsync_ShouldReturnAllGames()
        {
            InitializeDatabase();
            var result = await _gameService.GetAllGamesAsync();

            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
        }
        [Fact]
        public async Task GetGameByIdAsync_ShouldReturnGame()
        {
            InitializeDatabase();
            int testGameId = 1;
            var result = await _gameService.GetGameByIdAsync(testGameId);

            Assert.NotNull(result);
            Assert.Equal(testGameId, result.Id);
            Assert.Equal("Game 1", result.Name);




        }
        [Fact]
        public async Task GetGameByIdAsync_NonExistentId_ShouldReturnNull()
        {
            
            InitializeDatabase();
            int nonExistentGameId = 99; 

            
            var result = await _gameService.GetGameByIdAsync(nonExistentGameId);

            
            Assert.Null(result); 
        }
        [Fact]
        public async Task GetAllEnemiesAsync_ShouldReturnAllEnemies()
        {
            InitializeDatabase();
            int gameId = 2;
            var result = await _enemyService.GetAllEnemiesAsync(gameId);
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
        }
    }
}