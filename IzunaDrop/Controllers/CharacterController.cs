using IzunaDrop.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace IzunaDrop.Controllers
{
    public class CharacterController : Controller
    {
        private readonly ILogger<CharacterController> _logger;
        private readonly ICharacterService _characterService;

        public CharacterController(ICharacterService characterService, ILogger<CharacterController> logger)
        {
            _characterService = characterService;
            _logger = logger;
        }
        public async Task<IActionResult> Index(int gameId)
        {
           var characters= await _characterService.GetAllCharactersAsync(gameId);
            return View(characters);
        }
        public async Task<IActionResult> Details(int gameId,int itemId)
        {
            var character = await _characterService.GetCharacterByIdAsync(gameId, itemId);
            return View(character);
        }
    }
}
