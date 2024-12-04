using Microsoft.AspNetCore.Mvc;

namespace IzunaDrop.Controllers
{
    public class ItemController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
