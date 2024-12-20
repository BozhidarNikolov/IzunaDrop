using IzunaDrop.Data.Models;
using IzunaDrop.Services.Interface;
using IzunaDrop.ViewModels;
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
       
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(GameCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                
                return View(model);
            }
            


            var newGame = new Game
            {
                Name = model.Name,
                Description = model.Description,
                Genre = model.Genre,
                ReleaseDate = model.ReleaseDate,
                Developer = model.Developer,
                Publisher = model.Publisher,
                IsDeleted = false
            };

            
            await _gameService.CreateGameAsync(newGame);

            
            return RedirectToAction("Index", "Home");
        }
    }
}
