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
                ReleaseDate = new DateTime(2006, 2, 23)
            }
            );
    }
}
