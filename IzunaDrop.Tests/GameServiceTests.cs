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
        private IzunaDropDbContext _context;

        private void InitializeDatabase()
        {
            var options = new DbContextOptionsBuilder<IzunaDropDbContext>()
                .UseInMemoryDatabase(databaseName: "GameTestDatabase")
                .Options;
            _context = new IzunaDropDbContext(options);
            _context.Database.EnsureDeleted();
            _context.Games.AddRange(new List<Game>
            {
                new Game {Id=5, Name="Game 1", Description="Test game 1", ReleaseDate=new DateTime(2012,12,12)},
                new Game {Id=6, Name="Game 2", Description="Test game 2",ReleaseDate=new DateTime(2010,10,10)}
            });
            _context.SaveChanges();

            _gameService = new GameService(_context);
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
            int testGameId = 5;
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
        public async Task CreateGameAsync_ShouldAddNewGame()
        {
            InitializeDatabase();
            var game = new Game
            {
                Name = "Test Game",
                Description = "A test description",
                IsDeleted = false,
                ReleaseDate = new DateTime(2006, 2, 23)
            };

            var result = await _gameService.CreateGameAsync(game);


            Assert.NotNull(result);
            Assert.NotEqual(0, result.Id);
            var createdGame = await _context.Games.FindAsync(result.Id);
            Assert.NotNull(createdGame);
            Assert.Equal("Test Game", createdGame.Name);
            Assert.Equal("A test description", createdGame.Description);
            Assert.False(createdGame.IsDeleted);
        }

        [Fact]
        public async Task UpdateGameAsync_ShouldReturnFalseIfNotFound()
        {
            InitializeDatabase();
            var updatedGame = new Game
            {
                Id = 999,
                Name = "DoesNotExist",
                Description = "No desc",
                
            };

            var result = await _gameService.UpdateGameAsync(updatedGame);
            Assert.False(result);
        }

        [Fact]
        public async Task UpdateGameAsync_ShouldUpdateExistingGame()
        {
            InitializeDatabase();
            var game = new Game
            {
                Name = "Old Name",
                Description = "Old Description",
                ReleaseDate= new DateTime(2006, 2, 23)

            };
            _context.Games.Add(game);
            await _context.SaveChangesAsync();

            var updatedGame = new Game
            {
                Id = game.Id,
                Name = "New Name",
                Description = "New Description",
                ReleaseDate = new DateTime(2006, 2, 23)

            };

            var result = await _gameService.UpdateGameAsync(updatedGame);
            Assert.True(result);

            var fromDb = await _context.Games.FindAsync(game.Id);
            Assert.Equal("New Name", fromDb.Name);
            Assert.Equal("New Description", fromDb.Description);
            Assert.Equal(new DateTime(2006, 2, 23), fromDb.ReleaseDate);


        }

        [Fact]
        public async Task DeleteGameAsync_ShouldReturnFalseIfNotFound()
        {
            InitializeDatabase();

            var result = await _gameService.DeleteGameAsync(999);
            Assert.False(result);
        }

        [Fact]
        public async Task DeleteGameAsync_ShouldSetIsDeletedToTrue()
        {
            InitializeDatabase();
            var game = new Game
            {
                Name = "Game to Delete",
                Description = "Delete me",
                IsDeleted = false,
                ReleaseDate= new DateTime(2006, 2, 23)
            };

            _context.Games.Add(game);
            await _context.SaveChangesAsync();

            var result = await _gameService.DeleteGameAsync(game.Id);

            Assert.True(result);
            var fromDb = await _context.Games.FindAsync(game.Id);
            Assert.NotNull(fromDb);
            Assert.True(fromDb.IsDeleted);
        }
    }
}