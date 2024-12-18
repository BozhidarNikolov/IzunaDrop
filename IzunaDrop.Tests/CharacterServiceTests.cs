using IzunaDrop.Data;
using IzunaDrop.Data.Models;
using IzunaDrop.Services;
using IzunaDrop.Services.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IzunaDrop.Tests
{
    public class CharacterServiceTests
    {
        private ICharacterService _characterService;
        private IzunaDropDbContext _context;

        private void InitializeDatabase()
        {
            var options = new DbContextOptionsBuilder<IzunaDropDbContext>()
                .UseInMemoryDatabase(databaseName: "CharacterTestDatabase")
                .Options;
            _context = new IzunaDropDbContext(options);
            _context.Database.EnsureDeleted();
            _context.Games.AddRange(new List<Game>
            {
                new Game {Id=1, Name="Ninja Gaiden", Description="Test game 1", ReleaseDate=new DateTime(2012,12,12)},
                new Game {Id=2, Name="Dark Souls", Description="Test game 2",ReleaseDate=new DateTime(2010,10,10)}
            });
            _context.Characters.AddRange(new List<Character>
            {
                new Character {Id=1,Name="Character 1",Description="Character test 1",GameId=1},
                new Character {Id=2, Name="Character 2",Description="Character test 2",GameId=2},
                new Character {Id=3, Name="Character 3",Description="Character test 3",GameId=2}

            });
            _context.SaveChanges();

            _characterService = new CharacterService(_context);

        }
        [Fact]

        public async Task GetAllCharactersAsync_ShouldReturnAllCharacters()
        {
            InitializeDatabase();
            int gameId = 2;
            var result = await _characterService.GetAllCharactersAsync(gameId);

            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
        }
        [Fact]
        public async Task GetCharacterByIdAsync_ShouldReturnCharacter()
        {
            InitializeDatabase();
            int gameId = 2;
            int characterId = 2;
            var result = await _characterService.GetCharacterByIdAsync(gameId, characterId);

            Assert.NotNull(result);
            Assert.Equal("Character 2", result.Name);
            Assert.Equal(gameId, result.GameId);
        }
        [Fact]
        public async Task GetCharacterByIdAsync_ShouldReturnNull()
        {
            InitializeDatabase();
            int gameId = 2;
            int characterId = 99;
            var result = await _characterService.GetCharacterByIdAsync(gameId, characterId);

            Assert.Null(result);
        }
        [Fact]
        public async Task CreateCharacterAsync_ShouldAddNewCharacter()
        {
            InitializeDatabase();
            var character = new Character
            {
                Name = "Test Character",
                Description = "A test description",
                GameId = 1,
                IsDeleted = false
            };

            var result = await _characterService.CreateCharacterAsync(character);

           
            Assert.NotNull(result);
            Assert.NotEqual(0, result.Id); 
            var createdCharacter = await _context.Characters.FindAsync(result.Id);
            Assert.NotNull(createdCharacter);
            Assert.Equal("Test Character", createdCharacter.Name);
            Assert.Equal("A test description", createdCharacter.Description);
            Assert.False(createdCharacter.IsDeleted);
        }

        [Fact]
        public async Task UpdateCharacterAsync_ShouldReturnFalseIfNotFound()
        {
            InitializeDatabase();
            var updatedCharacter = new Character
            {
                Id = 999, 
                Name = "DoesNotExist",
                Description = "No desc",
                GameId = 1
            };

            var result = await _characterService.UpdateCharacterAsync(updatedCharacter);
            Assert.False(result);
        }

        [Fact]
        public async Task UpdateCharacterAsync_ShouldUpdateExistingCharacter()
        {
            InitializeDatabase();
            var character = new Character
            {
                Name = "Old Name",
                Description = "Old Description",
                GameId = 1
            };
            _context.Characters.Add(character);
            await _context.SaveChangesAsync();

            var updatedCharacter = new Character
            {
                Id = character.Id,
                Name = "New Name",
                Description = "New Description",
                GameId = 2
            };

            var result = await _characterService.UpdateCharacterAsync(updatedCharacter);
            Assert.True(result);

            var fromDb = await _context.Characters.FindAsync(character.Id);
            Assert.Equal("New Name", fromDb.Name);
            Assert.Equal("New Description", fromDb.Description);
            Assert.Equal(2, fromDb.GameId);
        }

        [Fact]
        public async Task DeleteCharacterAsync_ShouldReturnFalseIfNotFound()
        {
            InitializeDatabase();

            var result = await _characterService.DeleteCharacterAsync(999);
            Assert.False(result);
        }

        [Fact]
        public async Task DeleteCharacterAsync_ShouldSetIsDeletedToTrue()
        {
            InitializeDatabase();
            var character = new Character
            {
                Name = "Character to Delete",
                Description = "Delete me",
                GameId = 1,
                IsDeleted = false
            };

            _context.Characters.Add(character);
            await _context.SaveChangesAsync();

            var result = await _characterService.DeleteCharacterAsync(character.Id);

            Assert.True(result);
            var fromDb = await _context.Characters.FindAsync(character.Id);
            Assert.NotNull(fromDb);
            Assert.True(fromDb.IsDeleted);
        }
    }
}
