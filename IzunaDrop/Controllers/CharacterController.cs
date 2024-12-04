using Microsoft.AspNetCore.Mvc;

namespace IzunaDrop.Controllers
{
    public class CharacterController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
