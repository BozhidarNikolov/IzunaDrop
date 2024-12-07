using IzunaDrop.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IzunaDrop.Data;

public class IzunaDropDbContext : IdentityDbContext<IzunaDropUser>
{
    public DbSet<Game> Games { get; set; }
    public DbSet<Enemy> Enemies { get; set; }
    public DbSet<Item> Items { get; set; }
    public DbSet<Character> Characters { get; set; }
    public IzunaDropDbContext(DbContextOptions<IzunaDropDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Game>()
            .HasData(
            new Game
            {
                Id = 1,
                Name = "TestGame:The testing",
                Description = "An action-packed adventure game.",
                ReleaseDate = new DateTime(2006, 2, 23),
                ImagePath= "/images/bigstock-test-icon-63758263-4108836978.jpg",
            }
            );
        builder.Entity<Game>()
            .HasData(
            new Game
            {
                Id = 2,
                Name = "NG3Test",
                Description = "Ninja Gaiden 3 balalalalaalalala",
                ReleaseDate = new DateTime(2012, 12, 12),
                ImagePath = "/images/2115068-box_ng3.jpg"
            }
            );
        builder.Entity<Enemy>()
            .HasData(
            new Enemy
            {
                Id = 1,
                Name="Ninja",
                Description="A ninja from a Ninja Game",
                GameId=2
            },
            new Enemy
            {
                Id = 2,
                Name = "Orc",
                Description = "A strong, brutish foe that loves combat.",
                GameId = 1 // Also belongs to the first game
            },
            new Enemy
            {
                Id = 3,
                Name = "Ninja #2",
                Description = "A ninja from a Ninja Game 2",
                GameId = 2
            }

            );
        builder.Entity<Character>()
            .HasData(
            new Character
            {
                Id = 1,
                Name = "TestCharacters 1",
                Description = "Test Description blalalalaal",
                GameId = 1
            },
            new Character
            {
                Id = 2,
                Name = "TestCharacters 2",
                Description = "Test Description blalalalaal",
                GameId = 2
            },
            new Character
            {
                Id = 3,
                Name = "TestCharacters 3",
                Description = "Test Description blalalalaal",
                GameId = 2
            },
            new Character
            {
                Id = 4,
                Name = "TestCharacters 4",
                Description = "Test Description blalalalaal",
                GameId = 2
            }
            );
    }
}
