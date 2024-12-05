using IzunaDrop.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace IzunaDrop.Controllers
{
    public class EnemyController : Controller
    {
        private readonly ILogger<EnemyController> _logger;
        private readonly IEnemyService _enemyService;

        public EnemyController(IEnemyService enemyService, ILogger<EnemyController> logger)
        {
            _enemyService = enemyService;
            _logger = logger;
        }
        public async Task<IActionResult> Index(int gameId)
        {
            var enemies = await _enemyService.GetAllEnemiesAsync(gameId);
            
            return View(enemies);
        }
    }
}
