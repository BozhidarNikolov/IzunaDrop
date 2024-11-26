using IzunaDrop.Data;
using IzunaDrop.Data.Models;
using IzunaDrop.Services;
using IzunaDrop.Services.Interface;
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
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            _context = new IzunaDropDbContext(options);
            _context.Games.AddRange(new List<Game>
            {
                new Game {Id=1, Name="Game 1", Description="Test game 1", ReleaseDate=new DateTime(2012,12,12)},
                new Game {Id=2, Name="Game 2", Description="Test game 2",ReleaseDate=new DateTime(2010,10,10)}
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
    }
}