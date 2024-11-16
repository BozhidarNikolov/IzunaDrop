using IzunaDrop.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IzunaDrop.Data;

public class IzunaDropDbContext : IdentityDbContext<IzunaDropUser>
{
    public DbSet<Game> Games { get; set; }
    public DbSet<Enemy> Enemies { get; set; }
    public IzunaDropDbContext(DbContextOptions<IzunaDropDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
    }
}
