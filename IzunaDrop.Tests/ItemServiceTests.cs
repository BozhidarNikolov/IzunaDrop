using IzunaDrop.Data.Models;
using IzunaDrop.Data;
using IzunaDrop.Services.Interface;
using IzunaDrop.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IzunaDrop.Tests
{
    public class ItemServiceTests
    {
        private IItemService _itemService;
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
                new Game {Id=7, Name="Game 1", Description="Test game 1", ReleaseDate=new DateTime(2012,12,12)},
                new Game {Id=8, Name="Game 2", Description="Test game 2",ReleaseDate=new DateTime(2010,10,10)}
            });
            _context.Items.AddRange(new List<Item>
            {
                new Item{Id=1,Name="Item 1",Description="Test item 1",GameId=7},
                new Item{Id=2,Name="Item 2",Description="Test item 2",GameId=7}
            });
            _context.SaveChanges();

            _itemService = new ItemService(_context);
        }

        [Fact]
        public async Task GetAllItemsAsync_ShouldReturnAllItems()
        {
            InitializeDatabase();
            int gameId = 7;
            var result =await _itemService.GetAllItemsAsync(gameId);
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());

        }
    }
}
