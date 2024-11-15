using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using IzunaDrop.Data;
using IzunaDrop.Data.Models;
var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("IzunaDropDbContextConnection") ?? throw new InvalidOperationException("Connection string 'IzunaDropDbContextConnection' not found.");

builder.Services.AddDbContext<IzunaDropDbContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<IzunaDropUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<IzunaDropDbContext>();

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
