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
                .UseInMemoryDatabase(databaseName: "ItemTestDatabase")
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
        [Fact]
        public async Task GetItemByIdAsync_ShouldReturnItem()
        {
            InitializeDatabase();
            int gameId = 7;
            int itemId = 2;
            var result = await _itemService.GetItemByIdAsync(gameId, itemId);

            Assert.NotNull(result);
            Assert.Equal(itemId, result.Id);
            Assert.Equal("Item 2", result.Name);
        }
        [Fact]
        public async Task CreateItemAsync_ShouldAddNewItem()
        {
            InitializeDatabase();
            var item = new Item
            {
                Name = "Test Item",
                Description = "A test description",
                GameId = 1,
                IsDeleted = false
            };

            var result = await _itemService.CreateItemAsync(item);


            Assert.NotNull(result);
            Assert.NotEqual(0, result.Id);
            var createdItem= await _context.Items.FindAsync(result.Id);
            Assert.NotNull(createdItem);
            Assert.Equal("Test Item", createdItem.Name);
            Assert.Equal("A test description", createdItem.Description);
            Assert.False(createdItem.IsDeleted);
        }

        [Fact]
        public async Task UpdateItemAsync_ShouldReturnFalseIfNotFound()
        {
            InitializeDatabase();
            var updatedItem = new Item
            {
                Id = 999,
                Name = "DoesNotExist",
                Description = "No desc",
                GameId = 1
            };

            var result = await _itemService.UpdateItemAsync(updatedItem);
            Assert.False(result);
        }

        [Fact]
        public async Task UpdateItemAsync_ShouldUpdateExistingItem()
        {
            InitializeDatabase();
            var item = new Item
            {
                Name = "Old Name",
                Description = "Old Description",
                GameId = 1
            };
            _context.Items.Add(item);
            await _context.SaveChangesAsync();

            var updatedItem = new Item
            {
                Id = item.Id,
                Name = "New Name",
                Description = "New Description",
                GameId = 2
            };

            var result = await _itemService.UpdateItemAsync(updatedItem);
            Assert.True(result);

            var fromDb = await _context.Items.FindAsync(item.Id);
            Assert.Equal("New Name", fromDb.Name);
            Assert.Equal("New Description", fromDb.Description);
            Assert.Equal(2, fromDb.GameId);
        }

        [Fact]
        public async Task DeleteItemAsync_ShouldReturnFalseIfNotFound()
        {
            InitializeDatabase();

            var result = await _itemService.DeleteItemAsync(999);
            Assert.False(result);
        }

        [Fact]
        public async Task DeleteItemAsync_ShouldSetIsDeletedToTrue()
        {
            InitializeDatabase();
            var item = new Item
            {
                Name = "Item to Delete",
                Description = "Delete me",
                GameId = 1,
                IsDeleted = false
            };

            _context.Items.Add(item);
            await _context.SaveChangesAsync();

            var result = await _itemService.DeleteItemAsync(item.Id);

            Assert.True(result);
            var fromDb = await _context.Items.FindAsync(item.Id);
            Assert.NotNull(fromDb);
            Assert.True(fromDb.IsDeleted);
        }

    }
}
