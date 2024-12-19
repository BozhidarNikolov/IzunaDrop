using IzunaDrop.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace IzunaDrop.Controllers
{
    public class GameController : Controller
    {
        private readonly IGameService _gameService;
        private readonly ILogger<GameController> _logger;

        public GameController(IGameService gameService, ILogger<GameController> logger)
        {
            _gameService = gameService;
            _logger = logger;
        }

        public async Task<IActionResult> Details(int id)
        {
            var game = await _gameService.GetGameByIdAsync(id);
            if (game==null)
            {
                return View("NotFound"); 
            }
            return View(game);
        }
        public async Task<IActionResult> Add()
        {
            return View();
        }
    }
}
