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
            int characterId = 99; //Id that doesnt exist
            var result = await _characterService.GetCharacterByIdAsync(gameId, characterId);

            Assert.Null(result);
        }
    }
}
