using IzunaDrop.Data;
using IzunaDrop.Models;
using IzunaDrop.Services;
using IzunaDrop.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace IzunaDrop.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IGameService _gameService;

        public HomeController(IGameService gameService,ILogger<HomeController> logger)
        {
            _gameService = gameService;
            _logger = logger;
        }
       

        public async Task<IActionResult> Index(string searchTerm)
        {
            
            var games = await _gameService.GetAllGamesAsync();
            if (!string.IsNullOrEmpty(searchTerm))
            {
                games = games.Where(g => g.Name.Contains(searchTerm,StringComparison.OrdinalIgnoreCase));
            }
            return View(games);
        }

       

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
