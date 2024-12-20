using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using IzunaDrop.Data;
using IzunaDrop.Data.Models;
using IzunaDrop.Data.Seeders;
using IzunaDrop.Services.Interface;
using IzunaDrop.Services;
using Microsoft.AspNetCore.Identity.UI.Services;
var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("IzunaDropDbContextConnection") ?? throw new InvalidOperationException("Connection string 'IzunaDropDbContextConnection' not found.");

builder.Services.AddDbContext<IzunaDropDbContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddIdentity<IzunaDropUser,IdentityRole>(options => options.SignIn.RequireConfirmedAccount =false)
    .AddEntityFrameworkStores<IzunaDropDbContext>()
    .AddDefaultTokenProviders();
builder.Services.AddRazorPages();

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<IEmailSender, DummyEmailSender>();
builder.Services.AddScoped<IGameService, GameService>();
builder.Services.AddScoped<IEnemyService, EnemyService>();
builder.Services.AddScoped<ICharacterService, CharacterService>();
builder.Services.AddScoped<IItemService, ItemService>();



var app = builder.Build();



using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var configuration = services.GetRequiredService<IConfiguration>();
    try
    {
        await IdentitySeeder.SeedRolesAndAdminAsync(services, configuration);

    }
    catch (Exception ex)
    {
        
        throw;
    }
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.UseAuthentication();

app.MapRazorPages();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.Run();
